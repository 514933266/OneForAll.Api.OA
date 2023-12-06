using OA.Application.Dtos;
using OA.Domain.Enums;
using OA.Domain.Models;
using OneForAll.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OA.Application.Interfaces
{
    /// <summary>
    /// 企业组织架构
    /// </summary>
    public interface IOATeamService
    {
        #region 组织

        /// <summary>
        /// 获取列表树
        /// </summary>
        /// <param name="parentId">上级id</param>
        /// <param name="type">类型</param>
        /// <param name="deep">是否深度检索</param>
        /// <param name="scope">范围 -1全部 0有效 1被删除数据</param>
        /// <returns>列表</returns>
        Task<IEnumerable<OATeamTreeDto>> GetListAsync(
            Guid parentId,
            string type,
            bool deep,
            OATeamSearchScopeEnum scope);

        /// <summary>
        /// 获取指定组织
        /// </summary>
        /// <param name="id">组织id</param>
        /// <returns>列表</returns>
        Task<OATeamDto> GetAsync(Guid id);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> AddAsync(OATeamForm entity);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> UpdateAsync(OATeamForm entity);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">实体id</param>
        /// <param name="isTrue">彻底删除</param>
        /// <returns>结果</returns>
        Task<BaseErrType> DeleteAsync(Guid id, bool isTrue);

        /// <summary>
        /// 批量排序
        /// </summary>
        /// <param name="entities">排序表单</param>
        /// <returns>结果</returns>
        Task<BaseErrType> SortAsync(IEnumerable<OATeamSortForm> entities);

        #endregion

        #region 成员

        /// <summary>
        /// 获取成员列表
        /// </summary>
        /// <param name="id">组织id</param>
        /// <returns>列表</returns>
        Task<IEnumerable<OATeamMemberDto>> GetListMemberAsync(Guid id);

        /// <summary>
        /// 添加成员
        /// </summary>
        /// <param name="id">部门id</param>
        /// <param name="personIds">人员id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> AddMemberAsync(Guid id, IEnumerable<Guid> personIds);

        /// <summary>
        /// 删除成员（批量）
        /// </summary>
        /// <param name="id">组织id</param>
        /// <param name="contactIds">关联id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> DeleteMemberAsync(Guid id, IEnumerable<Guid> contactIds);

        /// <summary>
        /// 导入Excel
        /// </summary>
        /// <param name="id">部门id</param>
        /// <param name="data">表单</param>
        /// <returns>结果</returns>
        Task<BaseMessage> ImportMemberExcelAsync(Guid id, IEnumerable<OATeamMemberImportForm> data);

        #endregion
    }
}
