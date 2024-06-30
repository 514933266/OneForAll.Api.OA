using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OA.Domain.AggregateRoots;
using OA.Domain.Aggregates;
using OA.Domain.Interfaces;
using OA.Domain.Models;
using OA.Domain.Repositorys;
using OneForAll.Core;
using OneForAll.Core.Extension;
using OneForAll.Core.Security;
using OneForAll.Core.Upload;
using OneForAll.File;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain
{
    /// <summary>
    /// 新员工入职档案填写相关
    /// </summary>
    public class OAPersonEntryFileManager : OABaseManager, IOAPersonEntryFileManager
    {
        private readonly string UPLOAD_PATH = "upload/tenants/{0}/persons";// 文件存储路径
        private readonly string VIRTUAL_PATH = "/resources/tenants/{0}/persons";// 虚拟路径：根据Startup配置设置,返回给前端访问资源

        private readonly IUploader _uploader;
        private readonly IOAPersonEntryRepository _repository;
        private readonly IOAPersonSettingRepository _settingRepository;
        private readonly IOAPersonSettingFieldRepository _fieldRepository;

        public OAPersonEntryFileManager(
            IMapper mapper,
            IUploader uploader,
            IHttpContextAccessor httpContextAccessor,
            IOAPersonEntryRepository repository,
            IOAPersonSettingRepository settingRepository,
            IOAPersonSettingFieldRepository fieldRepository) : base(mapper, httpContextAccessor)
        {
            _uploader = uploader;
            _repository = repository;
            _settingRepository = settingRepository;
            _fieldRepository = fieldRepository;
        }

        /// <summary>
        /// 获取档案设置列表
        /// </summary>
        /// <param name="id">入职档案id</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<OAPersonSettingAggr>> GetListSettingAsync(Guid id)
        {
            var data = await _repository.GetIQFAsync(id);
            if (data == null) return new List<OAPersonSettingAggr>();

            var settings = await _settingRepository.GetListByTenantIQFAsync(data.SysTenantId);
            // 只显示启用并允许编辑的设置
            settings = settings.Where(w => w.IsEnabled).ToList();
            var ids = settings.Select(s => s.Id).ToList();
            var fields = await _fieldRepository.GetListBySettingAsync(ids);


            var result = _mapper.Map<IEnumerable<OAPersonSettingAggr>>(settings);
            result.ForEach(e =>
            {
                e.OAPersonSettingFields = fields.Where(w => w.OAPersonSettingId == e.Id && w.IsEnabled && w.IsEntryFileVisiable).OrderBy(o => o.SortNumber).ToList();
            });
            return result.OrderBy(o => o.SortNumber).ToList();
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
