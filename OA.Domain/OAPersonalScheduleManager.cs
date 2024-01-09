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
using System.Data;
using OneForAll.Core.DDD;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Org.BouncyCastle.Crypto;
using OneForAll.Core.ORM;

namespace OA.Domain
{
    /// <summary>
    /// 个人日程
    /// </summary>
    public class OAPersonalScheduleManager : OABaseManager, IOAPersonalScheduleManager
    {
        private readonly IOAPersonalScheduleRepository _repository;

        public OAPersonalScheduleManager(
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IOAPersonalScheduleRepository repository) : base(mapper, httpContextAccessor)
        {
            _repository = repository;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="date">月份</param>
        /// <param name="isClosed">是否已关闭</param>
        /// <returns>分页列表</returns>
        public async Task<IEnumerable<OAPersonalSchedule>> GetListAsync(DateTime date, bool? isClosed)
        {
            var firstDate = TimeHelper.ConvertToFirstDate(date);
            var lastDate = TimeHelper.ConvertToLastDate(date);
            var predicate = PredicateBuilder.Create<OAPersonalSchedule>(w => w.SettingDate >= firstDate && w.SettingDate <= lastDate && w.SysUserId == LoginUser.Id);
            if (isClosed.HasValue)
                predicate = predicate.And(w => w.IsClosed == isClosed);
            return await _repository.GetListAsync(predicate);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(OAPersonalScheduleForm form)
        {
            if (form.SettingDate < DateTime.Now)
                return BaseErrType.DataNotMatch;

            var data = await _repository.FindAsync(form.Id);
            if (data == null)
            {
                var exists = await _repository.CountAsync(w => w.SysUserId == LoginUser.Id && w.SettingDate == form.SettingDate);
                if (exists > 0)
                    return BaseErrType.DataExist;

                data = _mapper.Map<OAPersonalScheduleForm, OAPersonalSchedule>(form);
                data.InitPersonalNotification(LoginUser);
                data.CalculateNotifyTime();
                return await ResultAsync(() => _repository.AddAsync(data));
            }
            else
            {
                if (data.IsClosed)
                    return BaseErrType.NotAllow;

                _mapper.Map(form, data);
                data.CalculateNotifyTime();
                return await ResultAsync(() => _repository.UpdateAsync(data));
            }
        }

        /// <summary>
        /// 删除（批量）
        /// </summary>
        /// <param name="ids">项目id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeleteAsync(IEnumerable<Guid> ids)
        {
            var data = await _repository.GetListAsync(ids);
            if (!data.Any())
                return BaseErrType.DataEmpty;
            return await ResultAsync(() => _repository.DeleteRangeAsync(data));
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="id">日程id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> CloseAsync(Guid id)
        {
            var data = await _repository.FindAsync(id);
            if (data == null)
                return BaseErrType.DataNotFound;

            data.IsClosed = true;
            return await ResultAsync(_repository.SaveChangesAsync);
        }
    }
}
