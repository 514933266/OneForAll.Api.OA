using OA.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Application.Interfaces
{
    /// <summary>
    /// 仪表盘
    /// </summary>
    public interface IOADashboardService
    {
        /// <summary>
        /// 获取新入职员工列表（近2个月）
        /// </summary>
        /// <returns>分页列表</returns>
        Task<IEnumerable<OAPersonBasicInfoDto>> GetListNewPersonAsync();

        /// <summary>
        /// 获取人员统计数据
        /// </summary>
        /// <returns></returns>
        Task<OAPersonStatisticV2Dto> GetStatisticsAsync();
    }
}
