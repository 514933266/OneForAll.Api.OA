using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using OneForAll.Core;
using OneForAll.File;
using OneForAll.EFCore;
using OneForAll.Core.Upload;
using OneForAll.Core.Utility;
using OneForAll.Core.Extension;
using OA.Domain.Enums;
using OA.Domain.Models;
using OA.Domain.Interfaces;
using OA.Domain.Repositorys;
using OA.Domain.AggregateRoots;
using OA.Domain.ValueObjects;
using System.Data;
using OA.Domain.ExcelModels;
using NPOI.SS.UserModel;
using OA.Domain.Aggregates;

namespace OA.Domain
{
    /// <summary>
    /// 员工关怀
    /// </summary>
    public class OAPersonCareManager : OABaseManager, IOAPersonCareManager
    {
        private readonly IOAPersonRepository _repository;
        private readonly IOATeamPersonContactRepository _contactRepository;
        public OAPersonCareManager(
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IOAPersonRepository repository,
            IOATeamPersonContactRepository contactRepository) : base(mapper, httpContextAccessor)
        {
            _repository = repository;
            _contactRepository = contactRepository;
        }

        /// <summary>
        /// 获取员工生日列表
        /// </summary>
        /// <param name="teamId">团队id</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<OATeamMemberAggr>> GetListBirthdayAsync(Guid teamId, DateTime startDate, DateTime endDate)
        {
            var data = await _repository.GetListBirthdayAsync(startDate, endDate);
            var ids = data.Select(s => s.Id).ToList();
            return await _contactRepository.GetListByTeamAsync(teamId, ids);
        }

        /// <summary>
        /// 获取入职周年列表
        /// </summary>
        /// <param name="teamId">团队id</param>
        /// <param name="date">日期</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<OATeamMemberAggr>> GetListCompanyAsync(Guid teamId, DateTime date)
        {
            var data = await _repository.GetListCompanyAsync(date);
            var ids = data.Select(s => s.Id).ToList();
            return await _contactRepository.GetListByTeamAsync(teamId, ids);
        }
    }
}
