using AutoMapper;
using OA.Domain.Aggregates;
using OA.Domain.Interfaces;
using OA.Domain.Models;
using OA.Domain.Repositorys;
using OneForAll.Core;
using OneForAll.Core.Extension;
using OneForAll.Core.UploadFile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.Managers
{
    /// <summary>
    /// 新员工入职档案填写相关
    /// </summary>
    public class OAPersonEntryFileManager : OADomainManager, IOAPersonEntryFileManager
    {
        private readonly string UPLOAD_PATH = "upload/tenants/{0}/persons";// 文件存储路径
        private readonly string VIRTUAL_PATH = "/resources/tenants/{0}/persons";// 虚拟路径：根据Startup配置设置,返回给前端访问资源

        private readonly IUploader _uploader;
        private readonly IOAPersonEntryRepository _repository;

        public OAPersonEntryFileManager(
            IUploader uploader,
            ICurrentUser currentUser,
            IOAPersonEntryRepository repository) : base(currentUser)
        {
            _uploader = uploader;
            _repository = repository;
        }

        /// <summary>
        /// 填写入职档案
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> SubmitAsync(OAPersonalEntryFileForm form)
        {
            var data = await _repository.GetIQFAsync(form.Id);
            if (data == null) return BaseErrType.DataNotFound;

            data.ExtendInformationJson = form.ExtendInformations.ToJson();
            data.IsSubmitEntryFile = true;

            var effected = await _repository.SaveChangesAsync();
            if (effected > 0)
            {
                return BaseErrType.Success;
            }
            return BaseErrType.Fail;
        }

        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="id">人员id</param>
        /// <param name="filename">文件名</param>
        /// <param name="file">文件流</param>
        /// <returns>结果</returns>
        public async Task<IUploadResult> UploadFileAsync(Guid id, string filename, Stream file)
        {
            IUploadResult result = new UploadResult();
            var entry = await _repository.GetIQFAsync(id);
            if (entry == null)
                return result;

            var maxSize = 5 * 1024 * 1024;
            var floder = id.ToString("N");

            var uploadPath = AppContext.BaseDirectory + Path.Combine(UPLOAD_PATH.Fmt(entry.SysTenantId), floder);
            var virtualPath = Path.Combine(VIRTUAL_PATH.Fmt(entry.SysTenantId), floder, filename);
            result = await _uploader.WriteAsync(file, uploadPath, filename, maxSize);
            // 设置返回虚拟路径
            if (result.State.Equals(UploadEnum.Success))
            {
                result.Url = virtualPath;
            }
            return result;
        }
    }
}
