﻿using OA.Domain.AggregateRoots;
using OA.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.Interfaces
{
    /// <summary>
    /// 员工生日关怀
    /// </summary>
    public interface IOAPersonCareManager
    {
        /// <summary>
        /// 获取员工生日列表
        /// </summary>
        /// <param name="teamId">团队id</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>列表</returns>
        Task<IEnumerable<OATeamMemberAggr>> GetListBirthdayAsync(Guid teamId, DateTime startDate, DateTime endDate);

        /// <summary>
        /// 获取入职周年列表
        /// </summary>
        /// <param name="teamId">团队id</param>
        /// <param name="date">日期</param>
        /// <returns>列表</returns>
        Task<IEnumerable<OATeamMemberAggr>> GetListCompanyAsync(Guid teamId, DateTime date);
    }
}
