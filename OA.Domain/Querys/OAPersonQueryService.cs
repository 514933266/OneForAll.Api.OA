using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using OneForAll.Core;
using OneForAll.Core.Extension;
using OA.Domain.Enums;
using OA.Domain.Interfaces;
using OA.Domain.Repositorys;
using OA.Domain.AggregateRoots;

namespace OA.Domain.Querys
{
    /// <summary>
    /// 人员资料查询服务
    /// </summary>
    public class OAPersonQueryService : IOAPersonQueryService
    {
        private readonly IOAPersonRepository _repository;

        public OAPersonQueryService(
            IOAPersonRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 获取人员分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <param name="onJobStatus">类型 -1全部 0在职 1离职</param>
        /// <returns>分页列表</returns>
        public async Task<PageList<OAPerson>> GetPageAsync(
            int pageIndex,
            int pageSize,
            string key,
            OAPersonOnJobStatusEnum onJobStatus)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 10;
            if (pageSize > 100) pageSize = 100;

            var data = await _repository.GetPageAsync(pageIndex, pageSize, key, onJobStatus);

            data.Items.ForEach(e =>
            {
                // 没有保存过档案的，根据默认信息赋值到档案扩展信息
                if (e.ExtendInformationJson.IsNullOrWhiteSpace())
                {
                    e.InitExtendInfomation();
                }
            });
            return data;
        }

        /// <summary>
        /// 获取人员列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="onJobStatus">类型 -1全部 0在职 1离职</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<OAPerson>> GetListAsync(string key, OAPersonOnJobStatusEnum onJobStatus)
        {
            return await _repository.GetListAsync(key, onJobStatus);
        }

        /// <summary>
        /// 获取人员
        /// </summary>
        /// <param name="id">人员id</param>
        /// <returns>基础信息</returns>
        public async Task<OAPerson> GetAsync(Guid id)
        {
            var data = await _repository.FindAsync(id);
            if (data.ExtendInformationJson.IsNullOrEmpty())
            {
                data.InitExtendInfomation();
            }
            return data;
        }

        /// <summary>
        /// 按姓名与身份证精确查询人员
        /// </summary>
        /// <param name="name">人员名称</param>
        /// <param name="idCard">身份证号</param>
        /// <returns>人员</returns>
        public async Task<OAPerson> GetAsync(string name, string idCard)
        {
            return await _repository.GetAsync(name, idCard);
        }

        /// <summary>
        /// 获取新入职员工列表（近2个月）
        /// </summary>
        /// <returns>列表</returns>
        public async Task<IEnumerable<OAPerson>> GetListNewEntryAsync()
        {
            return await _repository.GetListNewEntryAsync();
        }
    }
}
