using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using OneForAll.Core;
using OneForAll.EFCore;
using OneForAll.Core.Extension;
using OA.Domain.Models;
using OA.Domain.Interfaces;
using OA.Domain.Repositorys;
using OA.Domain.AggregateRoots;
using System.ComponentModel;
using OA.Domain.Aggregates;
using NPOI.SS.Formula.Functions;

namespace OA.Domain.Managers
{
    /// <summary>
    /// 部门成员
    /// </summary>
    public class OATeamMemberManager : OADomainManager, IOATeamMemberManager
    {
        private readonly IMapper _mapper;
        private readonly IOATeamRepository _teamRepository;
        private readonly IOAPersonRepository _personReposiotry;
        private readonly IOATeamPersonContactRepository _repository;

        public OATeamMemberManager(
            IMapper mapper,
            ICurrentUser currentUser,
            IOATeamRepository teamRepository,
            IOAPersonRepository personReposiotry,
            IOATeamPersonContactRepository repository) : base(currentUser)
        {
            _mapper = mapper;
            _repository = repository;
            _teamRepository = teamRepository;
            _personReposiotry = personReposiotry;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(OATeamMemberForm form)
        {
            var team = await _teamRepository.GetWithContactAsync(form.TeamId);
            if (team == null)
                return BaseErrType.DataError;

            var person = await _personReposiotry.GetAsync(form.Name, form.IdCard ?? "");
            if (person == null)
                return BaseErrType.DataError;

            // 团队设置了负责人，默认加入团队时就是管理
            if (team.LeaderId == person.Id)
                form.IsLeader = true;
            var contact = new OAMidTeamPerson()
            {
                SysTenantId = LoginUser.TenantId.TryGuid(),
                OATeamId = team.Id,
                OAPersonId = person.Id,
                IsLeader = form.IsLeader
            };

            return await ResultAsync(() => _repository.AddAsync(contact));
        }

        /// <summary>
        /// 添加（批量）
        /// </summary>
        /// <param name="teamId">组织id</param>
        /// <param name="personIds">人员id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(Guid teamId, IEnumerable<Guid> personIds)
        {
            if (!personIds.Any())
                return BaseErrType.DataEmpty;
            if (!personIds.Any())
                return BaseErrType.DataEmpty;
            var team = await _teamRepository.FindAsync(teamId);
            var exists = await _repository.GetListByTeamIdAsync(teamId);
            var contacts = new List<OAMidTeamPerson>();
            personIds.ForEach(pid =>
            {
                if (exists.Any(w => w.OAPersonId == pid))
                    return;
                contacts.Add(new OAMidTeamPerson()
                {
                    SysTenantId = LoginUser.TenantId.TryGuid(),
                    OAPersonId = pid,
                    OATeamId = teamId,
                    IsLeader = (pid == team.LeaderId)
                });
            });
            return await ResultAsync(() => _repository.AddRangeAsync(contacts));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="member">成员表单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(OATeamMemberForm member)
        {
            var data = await _repository.GetByTeamAndPersonAsync(member.TeamId, member.Id);
            if (data == null)
                return BaseErrType.DataNotFound;
            var team = await _teamRepository.FindAsync(member.TeamId);
            if (team == null)
                return BaseErrType.DataNotFound;

            data.IsLeader = member.IsLeader;
            data.UpdateTime = DateTime.Now;

            using (var tran = new UnitOfWork().BeginTransaction())
            {
                if (team.LeaderId == data.OAPersonId)
                {
                    // 取消团队直属负责人
                    team.LeaderId = Guid.Empty;
                    await _teamRepository.UpdateAsync(team, tran);
                }
                else if (team.LeaderId == Guid.Empty && data.IsLeader)
                {
                    // 设为主管
                    team.LeaderId = data.OAPersonId;
                    await _teamRepository.UpdateAsync(team, tran);
                }
                await _repository.UpdateAsync(data, tran);
                return await ResultAsync(tran.CommitAsync);
            }
        }

        /// <summary>
        /// 移除（批量）
        /// </summary>
        /// <param name="teamId">人员id</param>
        /// <param name="personIds">关联表id集合</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeleteAsync(Guid teamId, IEnumerable<Guid> personIds)
        {
            if (!personIds.Any()) return BaseErrType.DataEmpty;

            var data = await _repository.GetListByTeamAsync(teamId, personIds);
            if (!data.Any())
                return BaseErrType.DataEmpty;

            return await ResultAsync(() => _repository.DeleteRangeAsync(data.Select(s => s.Contact)));
        }

        /// <summary>
        /// 移除（批量）
        /// </summary>
        /// <param name="ids">关联表id集合</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeleteAsync(IEnumerable<Guid> ids)
        {
            if (!ids.Any())
                return BaseErrType.DataEmpty;

            var data = await _repository.GetListByIdsAsync(ids);
            return await ResultAsync(() => _repository.DeleteRangeAsync(data));
        }

        /// <summary>
        /// 解散移除
        /// </summary>
        /// <param name="teamId">团队id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DissolveAsync(Guid teamId)
        {
            var data = await _repository.GetListByTeamIdAsync(teamId);
            return await ResultAsync(() => _repository.DeleteRangeAsync(data));
        }

        /// <summary>
        /// 设置部门管理（批量）
        /// </summary>
        /// <param name="teamId">组织id</param>
        /// <param name="personIds">关联id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> SetLeaderAsync(Guid teamId, IEnumerable<Guid> personIds)
        {
            var data = await _repository.GetListByTeamAsync(teamId, personIds);
            if (!data.Any())
                return BaseErrType.DataEmpty;

            var contacts = data.Select(s => s.Contact).ToList();
            contacts.ForEach(e => e.IsLeader = true);
            return await ResultAsync(_repository.SaveChangesAsync);
        }

        /// <summary>
        /// 人员档案删除（批量）
        /// </summary>
        /// <param name="personIds">关联id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> RemoveFileAsync(IEnumerable<Guid> personIds)
        {
            if (!personIds.Any())
                return BaseErrType.DataEmpty;

            var data = await _repository.GetListByPersonIdsAsync(personIds);
            if (!data.Any())
                return BaseErrType.DataEmpty;

            return await ResultAsync(() => _repository.DeleteRangeAsync(data));
        }

        /// <summary>
        /// 人员离职（批量）
        /// </summary>
        /// <param name="teamId">团队id</param>
        /// <param name="personIds">关联id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> LeaveAsync(Guid teamId, IEnumerable<Guid> personIds)
        {
            if (!personIds.Any())
                return BaseErrType.DataEmpty;

            var data = await _repository.GetListByTeamAsync(teamId, personIds);
            if (!data.Any())
                return BaseErrType.DataEmpty;

            var contacts = data.Select(s => s.Contact).ToList();
            return await ResultAsync(() => _repository.DeleteRangeAsync(contacts));
        }

        /// <summary>
        /// 人员调动（批量）
        /// </summary>
        /// <param name="teamId">团队id</param>
        /// <param name="targetTeamId">目标团队id</param>
        /// <param name="personIds">关联id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> TransferAsync(Guid teamId, Guid targetTeamId, IEnumerable<Guid> personIds)
        {
            if (!personIds.Any())
                return BaseErrType.DataEmpty;

            var ids = new List<Guid>() { teamId, targetTeamId };
            var teams = await _teamRepository.GetListANTAsync(ids);
            var team = teams.FirstOrDefault(w => w.Id == teamId);
            var target = teams.FirstOrDefault(w => w.Id == targetTeamId);
            if (target == null || team == null)
                return BaseErrType.DataNotFound;


            var teamIds = teams.Select(s => s.Id);
            var members = await _repository.GetListByTeamAsync(teamId, personIds);
            var addList = new List<OAMidTeamPerson>();
            var delList = members.Select(s => s.Contact).ToList();
            members.ForEach(member =>
            {
                addList.Add(new OAMidTeamPerson()
                {
                    OAPersonId = member.Id,
                    OATeamId = targetTeamId,
                    SysTenantId = LoginUser.TenantId.TryGuid()
                });
            });

            using (var tran = new UnitOfWork().BeginTransaction())
            {
                await _repository.DeleteRangeAsync(delList, tran);
                await _repository.AddRangeAsync(addList, tran);
                return await ResultAsync(() => tran.CommitAsync());
            }
        }

        /// <summary>
        /// 导入Excel
        /// </summary>
        /// <param name="teamId">部门id</param>
        /// <param name="data">Excel人员列表</param>
        /// <returns>结果</returns>
        public async Task<BaseMessage> ImportExcelAsync(Guid teamId, IEnumerable<OATeamMemberImportForm> data)
        {
            var index = 0;
            var msg = new BaseMessage() { ErrType = BaseErrType.DataError };
            var errors = new List<ValidateTableResult>();

            if (data.Count() > 500)
            {
                msg.ErrType = BaseErrType.NotAllow;
                msg.Message = "每次最多导入500条数据";
                return msg;
            }

            var idcards = data.Select(s => s.IdCard).ToList();
            var teams = await _teamRepository.GetListValidANTAsync();
            var existsPersons = await _personReposiotry.GetListByIdCardAsync(idcards);
            var teamTree = _mapper.Map<IEnumerable<OATeam>, IEnumerable<OATeamTreeAggr>>(teams);

            // 如果指定了团队，则只能导入对应团队以及子团队的成员数据
            if (teamId != Guid.Empty)
            {
                var teamIds = teamTree.FindChildren(teamId).Select(s => s.Id).ToList();
                teamIds.Add(teamId);
                teams = teams.Where(w => teamIds.Contains(w.Id)).ToList();
            }

            data.ForEach(e =>
            {
                var error = new ValidateTableResult()
                {
                    RowIndex = index
                };

                if (!e.IdCard.Trim().IsNullOrEmpty())
                {
                    var existsPerson = existsPersons.FirstOrDefault(w => w.IdCard == e.IdCard);
                    if (existsPerson != null)
                    {
                        error.Columns.Add(new ValidateTableColumnResult()
                        {
                            ColumnIndex = 4,
                            Error = "已存在相同身份证员工信息"
                        });
                    }
                }

                if (!e.TeamName.IsNullOrEmpty())
                {
                    var team = teams.FirstOrDefault(w => w.Name == e.TeamName);
                    if (team == null)
                    {
                        error.Columns.Add(new ValidateTableColumnResult()
                        {
                            ColumnIndex = 5,
                            Error = "找不到对应的部门信息"
                        });
                    }
                }

                if (error.Columns.Count > 0)
                {
                    errors.Add(error);
                }
                index++;
            });

            if (errors.Count < 1)
            {
                if (data.Count() > 0)
                {
                    var persons = new List<OAPerson>();
                    var contacts = new List<OAMidTeamPerson>();

                    data.ForEach(e =>
                    {
                        var person = _mapper.Map<OAPerson>(e);
                        person.Id = Guid.NewGuid();
                        person.SysTenantId = LoginUser.TenantId.TryGuid();
                        persons.Add(person);

                        var team = teams.FirstOrDefault(w => w.Name == e.TeamName);
                        if (team != null)
                        {
                            contacts.Add(new OAMidTeamPerson()
                            {
                                Id = Guid.NewGuid(),
                                OATeamId = team.Id,
                                OAPersonId = person.Id,
                                IsLeader = e.IsLeader == "是" ? true : false
                            });
                        }
                    });

                    msg.ErrType = await ResultAsync(() => _personReposiotry.AddRangeAsync(persons));
                    if (contacts.Any())
                    {
                        await _repository.AddRangeAsync(contacts);
                    }
                }
                else
                {
                    msg.ErrType = BaseErrType.DataNotFound;
                }
            }
            else
            {
                msg.Data = errors;
            }
            return msg;
        }

        /// <summary>
        /// 统计当前登录用户管辖的部门与群组人数（主管身份计入部门，成员身份计入群组）
        /// </summary>
        /// <returns>管辖人数</returns>
        public async Task<OALedTeamCount> GetLedCountAsync()
        {
            var result = new OALedTeamCount();
            var userId = LoginUser.Id.TryGuid();
            var contacts = await _repository.GetListByUserAsync(userId);
            if (contacts == null || !contacts.Any())
                return result;

            var teams = await _teamRepository.GetListValidANTAsync();
            var teamIds = new List<Guid>();
            var groupIds = new List<Guid>();
            var tids = contacts.Where(w => w.IsLeader).Select(s => s.OATeamId).ToList();
            var gids = contacts.Where(w => !w.IsLeader).Select(s => s.OATeamId).ToList();
            tids.ForEach(e =>
            {
                var children = teams.FindChildren(e);
                teamIds.AddRange(children.Select(s => s.Id));
            });
            teamIds.AddRange(tids);
            gids.ForEach(e =>
            {
                var children = teams.FindChildren(e);
                groupIds.AddRange(children.Select(s => s.Id));
            });
            groupIds.AddRange(gids);

            if (teamIds.Any())
                result.TeamCount = await _repository.GetCountByTeamAsync(teamIds);
            if (groupIds.Any())
                result.GroupCount = await _repository.GetCountByTeamAsync(groupIds);
            return result;
        }
    }
}
