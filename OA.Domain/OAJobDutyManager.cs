using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using OneForAll.Core;
using OneForAll.Core.Extension;
using OA.Domain.Enums;
using OA.Domain.Models;
using OA.Domain.Interfaces;
using OA.Domain.Aggregates;
using OA.Domain.Repositorys;
using OA.Domain.AggregateRoots;

namespace OA.Domain
{
    /// <summary>
    /// 职务管理
    /// </summary>
    public class OAJobDutyManager : OABaseManager, IOAJobDutyManager
    {
        private readonly IOAJobDutyRepository _repository;

        public OAJobDutyManager(
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IOAJobDutyRepository repository) : base(mapper, httpContextAccessor)
        {
            _repository = repository;
        }

        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>分页列表</returns>
        public async Task<PageList<OAJobDuty>> GetPageAsync(int pageIndex, int pageSize, string key)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 10;
            if (pageSize > 100) pageSize = 100;

            return await _repository.GetPageAsync(pageIndex, pageSize, key);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<OAJobDuty>> GetListAsync(string key)
        {
            if (key.IsNullOrEmpty())
            {
                return await _repository.GetListAsync();
            }
            else
            {
                return await _repository.GetListAsync(w => w.Name.Contains(key));
            }
        }

        /// <summary>
        /// 创建系统默认职位分类
        /// </summary>
        /// <returns></returns>
        public async Task<BaseErrType> CreateDefaultAsync()
        {
            var data = new List<OAJobDuty>() {
                new OAJobDuty() { SysTenantId = LoginUser.SysTenantId, Name = "高级管理层", Remark = "包括董事长、首席执行官（CEO）、首席财务官（CFO）、首席技术官（CTO）等" },
                new OAJobDuty() { SysTenantId = LoginUser.SysTenantId, Name = "部门经理", Remark = "包括销售经理、市场经理、人力资源经理、财务经理、技术经理等" },
                new OAJobDuty() { SysTenantId = LoginUser.SysTenantId, Name = "项目经理", Remark = "负责规划、协调和管理特定项目的实施和完成" },
                new OAJobDuty() { SysTenantId = LoginUser.SysTenantId, Name = "行政支持", Remark = "包括行政助理、行政秘书、办公室管理员等，负责提供行政支持和协助高层管理人员" },
                new OAJobDuty() { SysTenantId = LoginUser.SysTenantId, Name = "销售与客户服务", Remark = "包括销售代表、客户关系经理、客户支持专员等，负责销售和客户关系管理" },
                new OAJobDuty() { SysTenantId = LoginUser.SysTenantId, Name = "财务和会计", Remark = "包括财务分析师、会计师、财务主管等，负责财务管理和会计工作" },
                new OAJobDuty() { SysTenantId = LoginUser.SysTenantId, Name = "技术与研发", Remark = "包括软件工程师、系统分析师、研发工程师等，负责技术开发和研究" },
                new OAJobDuty() { SysTenantId = LoginUser.SysTenantId, Name = "人力资源", Remark = "包括人力资源专员、招聘经理、培训发展经理等，负责招聘、培训和员工发展" },
                new OAJobDuty() { SysTenantId = LoginUser.SysTenantId, Name = "市场营销", Remark = "包括市场营销经理、市场策划专员、市场调研分析师等，负责市场推广和品牌管理" },
                new OAJobDuty() { SysTenantId = LoginUser.SysTenantId, Name = "生产与运营", Remark = "包括生产经理、物流经理、供应链分析师等，负责生产和运营管理" }
            };
            return await ResultAsync(() => _repository.AddRangeAsync(data));
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(OAJobDutyForm form)
        {
            var exists = await _repository.GetAsync(w => w.Name == form.Name);
            if (exists != null) return BaseErrType.DataExist;

            var data = _mapper.Map<OAJobDuty>(form);
            data.SysTenantId = LoginUser.SysTenantId;
            return await ResultAsync(() => _repository.AddAsync(data));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(OAJobDutyForm form)
        {
            var exists = await _repository.GetAsync(w => w.Name == form.Name);
            if (exists != null && exists.Id != form.Id) return BaseErrType.DataExist;

            var data = await _repository.FindAsync(form.Id);
            if (data == null) return BaseErrType.DataNotFound;

            _mapper.Map(form, data);
            return await ResultAsync(() => _repository.UpdateAsync(data));
        }

        /// <summary>
        /// 删除（批量）
        /// </summary>
        /// <param name="ids">项目id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeleteAsync(IEnumerable<Guid> ids)
        {
            var data = await _repository.GetListAsync(w => ids.Contains(w.Id));
            if (!data.Any()) return BaseErrType.DataEmpty;
            return await ResultAsync(() => _repository.DeleteRangeAsync(data));
        }
    }
}
