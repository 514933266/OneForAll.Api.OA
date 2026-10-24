using OA.Domain.Aggregates;
using OA.Domain.Models;
using OneForAll.Core;
using OneForAll.Core.UploadFile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.Interfaces
{
    /// <summary>
    /// 新员工入职档案填写相关
    /// </summary>
    public interface IOAPersonEntryFileManager
    {
        /// <summary>
        /// 填写入职档案
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        Task<BaseErrType> SubmitAsync(OAPersonalEntryFileForm form);

        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="id">人员id</param>
        /// <param name="filename">文件名</param>
        /// <param name="file">文件流</param>
        /// <returns>结果</returns>
        Task<IUploadResult> UploadFileAsync(Guid id, string filename, Stream file);
    }
}
