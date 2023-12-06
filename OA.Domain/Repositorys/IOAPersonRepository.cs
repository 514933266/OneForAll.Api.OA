using System;
using System.Text;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using OneForAll.Core;
using OneForAll.EFCore;
using OA.Domain.Enums;
using OA.Domain.AggregateRoots;

namespace OA.Domain.Repositorys
{
    /// <summary>
    /// 仓储：人员资料
    /// </summary>
    public interface IOAPersonRepository : IEFCoreRepository<OAPerson>
    {
        #region 列表

        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <param name="onJobStatus">类型 -1全部 0在职 1离职</param>
        /// <returns>分页列表</returns>
        Task<PageList<OAPerson>> GetPageAsync(
            int pageIndex,
            int pageSize,
            string key,
            OAPersonOnJobStatusEnum onJobStatus);

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="onJobStatus">类型 -1全部 0在职 1离职</param>
        /// <returns>列表</returns>
        Task<IEnumerable<OAPerson>> GetListAsync(string key, OAPersonOnJobStatusEnum onJobStatus);

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="onJobStatus">类型 -1全部 0在职 1离职</param>
        /// <param name="employeeStatus">员工状态</param>
        /// <param name="employeeType">员工类型</param>
        /// <param name="startEntryDate">开始入职时间</param>
        /// <param name="endEntryDate">结束入职时间</param>
        /// <returns>列表</returns>
        Task<IEnumerable<OAPerson>> GetListAsync(
            OAPersonOnJobStatusEnum onJobStatus,
            string employeeType,
            string employeeStatus,
            DateTime? startEntryDate,
            DateTime? endEntryDate);

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="ids">id</param>
        /// <returns>列表</returns>
        Task<IEnumerable<OAPerson>> GetListAsync(IEnumerable<Guid> ids);

        /// <summary>
        /// 查询列表（在职）
        /// </summary>
        /// <returns>结果</returns>
        Task<IEnumerable<OAPerson>> GetListNoTeamAsync();

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="idCards">身份证</param>
        /// <returns>列表</returns>
        Task<IEnumerable<OAPerson>> GetListByIdCardAsync(IEnumerable<string> idCards);

        /// <summary>
        /// 查询生日列表
        /// </summary>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>列表</returns>
        Task<IEnumerable<OAPerson>> GetListBirthdayAsync(DateTime startDate, DateTime endDate);

        /// <summary>
        /// 查询入职周年列表
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>列表</returns>
        Task<IEnumerable<OAPerson>> GetListCompanyAsync(DateTime date);

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="tenantId">租户id</param>
        /// <returns>列表</returns>
         Task<IEnumerable<OAPerson>> GetListByTenantId(Guid tenantId);

        /// <summary>
        /// 查询新入职员工列表
        /// </summary>
        /// <returns>列表</returns>
        Task<IEnumerable<OAPerson>> GetListNewEntryAsync();

        #endregion

        #region 实体

        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="id">实体id</param>
        /// <returns></returns>
        Task<OAPerson> GetIQFAsync(Guid id);

        /// <summary>
        /// 查询人员
        /// </summary>
        /// <param name="name">人员名称</param>
        /// <param name="idCard">身份证号</param>
        /// <returns>人员</returns>
        Task<OAPerson> GetAsync(string name, string idCard);

        #endregion
    }
}
