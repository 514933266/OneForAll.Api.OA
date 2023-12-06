using AutoMapper;
using Microsoft.AspNetCore.Http;
using NPOI.POIFS.Properties;
using OA.Domain.AggregateRoots;
using OA.Domain.Aggregates;
using OA.Domain.Interfaces;
using OA.Domain.Repositorys;
using OneForAll.Core.DDD;
using OneForAll.Core.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain
{
    /// <summary>
    /// 人员团队信息
    /// </summary>
    public class OAPersonTeamInfoManager : OABaseManager, IOAPersonTeamInfoManager
    {
        private readonly IOATeamPersonContactRepository _contactRepository;

        public OAPersonTeamInfoManager(
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IOATeamPersonContactRepository contactRepository) : base(mapper, httpContextAccessor)
        {
            _contactRepository = contactRepository;
        }

        /// <summary>
        /// 获取人员列表
        /// </summary>
        /// <param name="teams">团队</param>
        /// <param name="key">人员姓名</param>
        /// <returns>人员列表</returns>
        public async Task<IEnumerable<OAPersonTeamInfoAggr>> GetListAsync(IEnumerable<OATeamTreeAggr> teams, string key)
        {
            var result = new List<OAPersonTeamInfoAggr>();
            var contacts = await _contactRepository.GetListByTeamAsync(Guid.Empty, key);
            foreach (var contact in contacts)
            {
                var data = _mapper.Map<OAPersonTeamInfoAggr>(contact);
                data.Teams = GetListTeamTree(teams, contact.OATeam.Id);
                data.Teams.Reverse();
                result.Add(data);
            }
            return result;
        }

        /// <summary>
        /// 获取人员
        /// </summary>
        /// <param name="teams">团队</param>
        /// <param name="personId">人员id</param>
        /// <returns>人员</returns>
        public async Task<OAPersonTeamInfoAggr> GetAsync(IEnumerable<OATeamTreeAggr> teams, Guid personId)
        {
            var result = new List<OAPersonTeamInfoAggr>();
            var contacts = await _contactRepository.GetListByPersonAsync(personId);
            foreach (var contact in contacts)
            {
                var data = _mapper.Map<OAPersonTeamInfoAggr>(contact);
                data.Teams = GetListTeamTree(teams, contact.OATeam.Id);
                result.Add(data);
            }
            return result.FirstOrDefault();
        }

        // 获取人员的所有团队
        private List<OATeamTreeAggr> GetListTeamTree(IEnumerable<OATeamTreeAggr> teams, Guid teamId)
        {
            var list = new List<OATeamTreeAggr>();
            var item = teams.FindNode(teamId);
            if (item != null)
            {
                list.Add(item);
                var parentList = GetListTeamTree(teams, item.ParentId);
                if (parentList.Any())
                    list.AddRange(parentList);
            }
            return list;
        }
    }
}
