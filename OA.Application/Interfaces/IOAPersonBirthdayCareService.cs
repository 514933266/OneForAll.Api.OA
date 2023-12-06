using OA.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Application.Interfaces
{
    /// <summary>
    /// 员工生日
    /// </summary>
    public interface IOAPersonBirthdayCareService
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="teamId">团队id</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>分页列表</returns>
        Task<IEnumerable<OAPersonBirthdayCareDto>> GetListBirthdayAsync(Guid teamId, DateTime startDate, DateTime endDate);

        /// <summary>
        /// 获取入职周年列表
        /// </summary>
        /// <param name="teamId">团队id</param>
        /// <param name="date">日期</param>
        /// <returns>列表</returns>
        Task<IEnumerable<OAPersonCompanyCareDto>> GetListCompanyAsync(Guid teamId, DateTime date);
    }
}
