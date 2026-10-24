using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using OneForAll.Core;
using OA.Domain.Enums;
using OA.Domain.AggregateRoots;

namespace OA.Domain.Interfaces
{
    /// <summary>
    /// 人员资料查询服务
    /// </summary>
    public interface IOAPersonQueryService
    {
        /// <summary>
        /// 获取人员分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <param name="onJobStatus">类型 -1全部 0在职 1离职</param>
        /// <returns>分页列表</returns>
        Task<PageList<OAPerson>> GetPageAsync(int pageIndex, int pageSize, string key, OAPersonOnJobStatusEnum onJobStatus);

        /// <summary>
        /// 获取人员列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="onJobStatus">类型 -1全部 0在职 1离职</param>
        /// <returns>列表</returns>
        Task<IEnumerable<OAPerson>> GetListAsync(string key, OAPersonOnJobStatusEnum onJobStatus);

        /// <summary>
        /// 获取人员
        /// </summary>
        /// <param name="id">人员id</param>
        /// <returns>基础信息</returns>
        Task<OAPerson> GetAsync(Guid id);

        /// <summary>
        /// 按姓名与身份证精确查询人员
        /// </summary>
        /// <param name="name">人员名称</param>
        /// <param name="idCard">身份证号</param>
        /// <returns>人员</returns>
        Task<OAPerson> GetAsync(string name, string idCard);

        /// <summary>
        /// 获取新入职员工列表（近2个月）
        /// </summary>
        /// <returns>列表</returns>
        Task<IEnumerable<OAPerson>> GetListNewEntryAsync();
    }
}
