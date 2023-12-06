using AutoMapper;
using OA.Application.Dtos;
using OA.Application.Interfaces;
using OA.Domain;
using OA.Domain.AggregateRoots;
using OA.Domain.Aggregates;
using OA.Domain.Interfaces;
using OA.Domain.Models;
using OA.Domain.Repositorys;
using OneForAll.Core;
using OneForAll.Core.Extension;
using OneForAll.Core.Upload;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Application
{
    /// <summary>
    /// 员工入职资料填写
    /// </summary>
    public class OAPersonalEntryFileService : IOAPersonalEntryFileService
    {
        private readonly IMapper _mapper;
        private readonly IOAPersonEntryFileManager _manager;

        private readonly IOAPersonEntryRepository _repository;

        public OAPersonalEntryFileService(
            IMapper mapper,
            IOAPersonEntryFileManager manager,
            IOAPersonEntryRepository repository)
        {
            _mapper = mapper;
            _manager = manager;
            _repository = repository;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id">实体id</param>
        /// <returns>列表</returns>
        public async Task<OAPersonalEntryFileDto> GetAsync(Guid id)
        {
            var data = await _repository.GetIQFAsync(id);
            return _mapper.Map<OAPersonEntry, OAPersonalEntryFileDto>(data);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="id">入职申请id</param>
        /// <returns>组织架构树</returns>
        public async Task<IEnumerable<OAPersonSettingDto>> GetListSettingAsync(Guid id)
        {
            var data = await _manager.GetListSettingAsync(id);
            return _mapper.Map<IEnumerable<OAPersonSettingAggr>, IEnumerable<OAPersonSettingDto>>(data);
        }

        /// <summary>
        /// 填写入职档案
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(OAPersonalEntryFileForm form)
        {
            return await _manager.SubmitAsync(form);
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="id">实体id</param>
        /// <param name="filename">文件名</param>
        /// <param name="file">文件流</param>
        /// <returns>上传结果</returns>
        public async Task<IUploadResult> UploadFileAsync(Guid id, string filename, Stream file)
        {
            return await _manager.UploadFileAsync(id, filename, file);
        }
    }
}
