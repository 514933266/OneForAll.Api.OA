using AutoMapper;
using Microsoft.AspNetCore.Http;
using OA.Domain.Interfaces;
using OA.Domain.Repositorys;
using OA.Domain.ValueObjects;
using OneForAll.Core;
using OneForAll.Core.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain
{
    /// <summary>
    /// 业务数据修复
    /// </summary>
    public class OARepairManager : OABaseManager, IOARepairManager
    {
        private readonly IOAPersonRepository _repository;

        public OARepairManager(
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IOAPersonRepository repository) : base(mapper, httpContextAccessor)
        {
            _repository = repository;
        }

        #region 人员信息

        /// <summary>
        /// 修复人员基础信息
        /// </summary>
        /// <returns>列表</returns>
        public async Task<BaseErrType> RepairPersonInfomation(Guid tenantId)
        {
            var data = await _repository.GetListByTenantId(tenantId);
            data.ForEach(e =>
            {
                if (!e.ExtendInformationJson.IsNullOrEmpty())
                {
                    var info = e.ExtendInformationJson.FromJson<List<OAPersonExtenInformationFieldVo>>();
                    e.InitExtendInfomation(info);
                }

                //if (e.OATeamPersonContacts.Any())
                //{
                //    // 团队和直属主管
                //    var teams = e.OATeamPersonContacts.Select(s => s.OATeam).ToList();
                //    var teamLeaderIds = teams.Select(s => s.LeaderId).ToList();
                //    if (teamLeaderIds.Any())
                //    {
                //        var leaders = _repository.GetListAsync(teamLeaderIds).Result;
                //        e.InitExtendInfomation(teams, leaders);
                //    }
                //}
            });
            return await ResultAsync(() => _repository.UpdateRangeAsync(data));
        }

        #endregion
    }
}
