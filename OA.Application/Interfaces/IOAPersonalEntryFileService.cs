using OA.Application.Dtos;
using OA.Domain.Models;
using OneForAll.Core;
using OneForAll.Core.Upload;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Application.Interfaces
{
    /// <summary>
    /// 员工入职资料填写
    /// </summary>
    public interface IOAPersonalEntryFileService
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id">实体id</param>
        /// <returns>列表</returns>
        Task<OAPersonalEntryFileDto> GetAsync(Guid id);

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="id">入职申请的id</param>
        /// <returns>组织架构树</returns>
        Task<IEnumerable<OAPersonSettingDto>> GetListSettingAsync(Guid id);

        /// <summary>
        /// 填写入职档案
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        Task<BaseErrType> UpdateAsync(OAPersonalEntryFileForm form);

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="id">实体id</param>
        /// <param name="filename">文件名</param>
        /// <param name="file">文件流</param>
        /// <returns>上传结果</returns>
        Task<IUploadResult> UploadFileAsync(Guid id, string filename, Stream file);
    }
}
