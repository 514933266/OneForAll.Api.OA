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
    /// 职级管理
    /// </summary>
    public class OAJobLevelManager : OABaseManager, IOAJobLevelManager
    {
        private readonly IOAJobLevelRepository _repository;

        public OAJobLevelManager(
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IOAJobLevelRepository repository) : base(mapper, httpContextAccessor)
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
        public async Task<PageList<OAJobLevel>> GetPageAsync(int pageIndex, int pageSize, string key)
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
        public async Task<IEnumerable<OAJobLevel>> GetListAsync(string key)
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
        /// 创建系统默认职级
        /// </summary>
        /// <returns></returns>
        public async Task<BaseErrType> CreateDefaultAsync()
        {
            var data = new List<OAJobLevel>() {
                new OAJobLevel() { SysTenantId = LoginUser.SysTenantId, Name = "P1", Remark = "初级专员：入门级职位，负责基础的专业任务执行，学习并掌握工作流程和基本技能" },
                new OAJobLevel() { SysTenantId = LoginUser.SysTenantId, Name = "P2", Remark = "专员：能够独立完成分配的专业任务，对特定领域有较好的理解和操作能力" },
                new OAJobLevel() { SysTenantId = LoginUser.SysTenantId, Name = "P3", Remark = "高级专员/助理专家：在专业领域内具备较强的能力，能指导P1、P2级别员工，参与复杂项目" },
                new OAJobLevel() { SysTenantId = LoginUser.SysTenantId, Name = "P4", Remark = "专家：拥有深厚的专业知识和经验，负责解决高难度问题，参与决策过程" },
                new OAJobLevel() { SysTenantId = LoginUser.SysTenantId, Name = "P5", Remark = "首席专家/高级顾问：行业内的权威，负责制定专业标准、策略，对外代表公司专业形象" },
                new OAJobLevel() { SysTenantId = LoginUser.SysTenantId, Name = "P6", Remark = "总监级专家：跨部门或跨业务线的专业领导，负责战略规划与实施" },
                new OAJobLevel() { SysTenantId = LoginUser.SysTenantId, Name = "P7", Remark = "高级总监/首席专家顾问：顶层专业领导者，对公司战略有重大影响，行业领军人物" },
                new OAJobLevel() { SysTenantId = LoginUser.SysTenantId, Name = "T1", Remark = "初级工程师：技术入门，学习基础编程与工具使用" },
                new OAJobLevel() { SysTenantId = LoginUser.SysTenantId, Name = "T2", Remark = "工程师：独立开发小型项目，熟练掌握一种或多种编程语言" },
                new OAJobLevel() { SysTenantId = LoginUser.SysTenantId, Name = "T3", Remark = "高级工程师：负责模块设计与核心代码编写，技术指导T1、T2" },
                new OAJobLevel() { SysTenantId = LoginUser.SysTenantId, Name = "T4", Remark = "技术专家：解决复杂技术难题，参与架构设计，技术创新" },
                new OAJobLevel() { SysTenantId = LoginUser.SysTenantId, Name = "T5", Remark = "主任工程师/技术主管：带领技术团队，负责关键技术决策和项目管理" },
                new OAJobLevel() { SysTenantId = LoginUser.SysTenantId, Name = "T6", Remark = "技术总监：跨项目技术领导，制定技术战略，推动技术创新" },
                new OAJobLevel() { SysTenantId = LoginUser.SysTenantId, Name = "T7", Remark = "首席技术官(CTO)级别：公司最高技术决策者，引领公司技术发展方向" },
                new OAJobLevel() { SysTenantId = LoginUser.SysTenantId, Name = "M1", Remark = "组长/主管：管理小团队，协调日常事务，监督工作进度" },
                new OAJobLevel() { SysTenantId = LoginUser.SysTenantId, Name = "M2", Remark = "部门经理：负责部门运营，制定部门计划，管理中层人员" },
                new OAJobLevel() { SysTenantId = LoginUser.SysTenantId, Name = "M3", Remark = "部门总监：跨部门协作，制定部门战略，管理多个经理" },
                new OAJobLevel() { SysTenantId = LoginUser.SysTenantId, Name = "M4", Remark = "副总裁：公司高层管理，负责业务板块或职能领域战略决策" },
                new OAJobLevel() { SysTenantId = LoginUser.SysTenantId, Name = "M5", Remark = "执行副总裁：协助CEO，管理多个部门或业务线，重大决策参与者" },
                new OAJobLevel() { SysTenantId = LoginUser.SysTenantId, Name = "M6", Remark = "首席运营官(COO)/首席财务官(CFO)等：公司运营或财务最高负责人" },
                new OAJobLevel() { SysTenantId = LoginUser.SysTenantId, Name = "M7", Remark = "首席执行官(CEO)：公司最高领导者，负责全局战略和决策" },
                new OAJobLevel() { SysTenantId = LoginUser.SysTenantId, Name = "O1", Remark = "运营助理：协助日常运营工作，学习运营流程和工具" },
                new OAJobLevel() { SysTenantId = LoginUser.SysTenantId, Name = "O2", Remark = "运营专员：负责具体运营项目，提升用户参与度和产品表现" },
                new OAJobLevel() { SysTenantId = LoginUser.SysTenantId, Name = "O3", Remark = "高级运营专员：策划并执行大型运营活动，数据分析，优化策略" },
                new OAJobLevel() { SysTenantId = LoginUser.SysTenantId, Name = "O4", Remark = "运营经理：管理运营团队，制定运营计划，达成业绩目标" },
                new OAJobLevel() { SysTenantId = LoginUser.SysTenantId, Name = "O5", Remark = "运营总监：跨部门合作，制定运营战略，管理多条业务线运营" },
                new OAJobLevel() { SysTenantId = LoginUser.SysTenantId, Name = "O6", Remark = "高级运营总监/副总：公司运营战略制定者，领导大规模运营项目" },
                new OAJobLevel() { SysTenantId = LoginUser.SysTenantId, Name = "O7", Remark = "首席运营官(COO)级别：公司运营最高负责人，直接参与公司战略决策" }
            };
            return await ResultAsync(() => _repository.AddRangeAsync(data));
        }


        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(OAJobLevelForm form)
        {
            var exists = await _repository.GetAsync(w => w.Name == form.Name);
            if (exists != null) return BaseErrType.DataExist;

            var data = _mapper.Map<OAJobLevel>(form);
            data.SysTenantId = LoginUser.SysTenantId;
            return await ResultAsync(() => _repository.AddAsync(data));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(OAJobLevelForm form)
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
