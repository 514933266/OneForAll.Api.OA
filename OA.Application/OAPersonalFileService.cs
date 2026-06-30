using AutoMapper;
using OA.Application.Dtos;
using OA.Application.Interfaces;
using OA.Domain.AggregateRoots;
using OA.Domain.Interfaces;
using OA.Domain.Models;
using OA.Domain.Repositorys;
using OA.Domain.ValueObjects;
using OneForAll.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Application
{
    /// <summary>
    /// 用户档案
    /// </summary>
    public class OAPersonalFileService : IOAPersonalFileService
    {
        private readonly IMapper _mapper;
        private readonly IOAPersonalFileManager _manager;
        public OAPersonalFileService(IMapper mapper, IOAPersonalFileManager manager)
        {
            _mapper = mapper;
            _manager = manager;
        }

        /// <summary>
        /// 获取人员
        /// </summary>
        /// <returns>基础信息</returns>
        public async Task<OAPersonDto> GetAsync()
        {
            var data = await _manager.GetAsync();
            return _mapper.Map<OAPerson, OAPersonDto>(data);
        }

        /// <summary>
        /// 查找并绑定个人档案
        /// </summary>
        /// <param name="form">手机|身份证</param>
        /// <returns></returns>
        public async Task<BaseErrType> BindAsync(OABindPersonalFileForm form)
        {
            return await _manager.BindAsync(form.Key);
        }
    }
}
