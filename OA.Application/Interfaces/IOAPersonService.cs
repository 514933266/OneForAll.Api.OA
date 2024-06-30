using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using OneForAll.Core;
using OneForAll.Core.Upload;
using OA.Domain.Enums;
using OA.Domain.Models;
using OA.Application.Dtos;

namespace OA.Application.Interfaces
{
    /// <summary>
    /// 人员资料
    /// </summary>
    public interface IOAPersonService
    {
        /// <summary>
        /// 获取人员分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <param name="onJobStatus">类型 -1全部 0在职 1离职</param>
        /// <returns>分页列表</returns>
        Task<PageList<OAPersonDto>> GetPageAsync(int pageIndex, int pageSize, string key, OAPersonOnJobStatusEnum onJobStatus);

        /// <summary>
        /// 获取人员列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="onJobStatus">类型 -1全部 0在职 1离职</param>
        /// <returns>分页列表</returns>
        Task<IEnumerable<OAPersonDto>> GetListAsync(string key, OAPersonOnJobStatusEnum onJobStatus);

        /// <summary>
        /// 获取人员（基础信息）
        /// </summary>
        /// <param name="id">人员id</param>
        /// <returns>基础信息</returns>
        Task<OAPersonDto> GetAsync(Guid id);

        /// <summary>
        /// 获取人员统计数据
        /// </summary>
        /// <returns></returns>
        Task<OAPersonStatisticDto> GetStatisticsAsync();

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> AddAsync(OAPersonForm entity);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> UpdateAsync(OAPersonForm entity);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">人员id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> DeleteAsync(IEnumerable<Guid> ids);

        /// <summary>
        /// 上传头像
        /// </summary>
        /// <param name="id">人员id</param>
        /// <param name="url">头像地址</param>
        /// <returns>结果</returns>
        Task<BaseErrType> UpdateHeaderAsync(Guid id, string url);

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="id">实体id</param>
        /// <param name="filename">文件名</param>
        /// <param name="file">文件流</param>
        /// <returns>上传结果</returns>
        Task<IUploadResult> UploadFileAsync(Guid id, string filename, Stream file);

        /// <summary>
        /// 人员离职（批量）
        /// </summary>
        /// <param name="ids">人员id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> LeaveAsync(IEnumerable<Guid> ids);

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="onJobStatus">类型 -1全部 0在职 1离职</param>
        /// <param name="employeeStatus">员工状态</param>
        /// <param name="employeeType">员工类型</param>
        /// <param name="fields">选择导出字段</param>
        /// <param name="jobs">岗位职级</param>
        /// <param name="startEntryDate">开始入职时间</param>
        /// <param name="endEntryDate">结束入职时间</param>
        /// <returns>文件流</returns>
        Task<byte[]> ExportExcelAsync(
            OAPersonOnJobStatusEnum onJobStatus,
            string employeeType,
            string employeeStatus,
            IEnumerable<string> fields,
            IEnumerable<string> jobs,
            DateTime? startEntryDate,
            DateTime? endEntryDate);

        /// <summary>
        /// 导入Excel
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileStream"></param>
        /// <returns></returns>
        Task<BaseErrType> ImportExcelAsync(string fileName, Stream fileStream);
    }
}
