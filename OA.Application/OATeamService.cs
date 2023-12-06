using AutoMapper;
using OA.Application.Dtos;
using OA.Application.Interfaces;
using OA.Domain.AggregateRoots;
using OA.Domain.Aggregates;
using OA.Domain.Enums;
using OA.Domain.Interfaces;
using OA.Domain.Models;
using OneForAll.Core;
using OneForAll.Core.Extension;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Application
{
    /// <summary>
    /// 团队
    /// </summary>
    public class OATeamService : IOATeamService
    {
        private readonly IMapper _mapper;
        private readonly IOATeamManager _manager;
        private readonly IOATeamTypeManager _teamTypeManager;
        private readonly IOATeamMemberManager _teamMemberManager;

        public OATeamService(
            IMapper mapper,
            IOATeamManager manager,
            IOATeamTypeManager teamTypeManager,
            IOATeamMemberManager teamMemberManager
            )
        {
            _mapper = mapper;
            _manager = manager;
            _teamTypeManager = teamTypeManager;
            _teamMemberManager = teamMemberManager;
        }

        #region 团队

        /// <summary>
        /// 获取列表树
        /// </summary>
        /// <param name="id">上级id</param>
        /// <param name="type">类型</param>
        /// <param name="deep">是否深度检索</param>
        /// <param name="scope">范围 -1全部 0有效 1被删除数据</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<OATeamTreeDto>> GetListAsync(
            Guid id,
            string type,
            bool deep,
            OATeamSearchScopeEnum scope)
        {
            var data = await _manager.GetListAsync(id, type, deep, scope);
            return _mapper.Map<IEnumerable<OATeamTreeAggr>, IEnumerable<OATeamTreeDto>>(data);
        }

        /// <summary>
        /// 获取指定组织
        /// </summary>
        /// <param name="id">组织id</param>
        /// <returns>列表</returns>
        public async Task<OATeamDto> GetAsync(Guid id)
        {
            var data = await _manager.GetAsync(id);
            return _mapper.Map<OATeam, OATeamDto>(data);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(OATeamForm entity)
        {
            var result = await _manager.AddAsync(entity);
            if (result == BaseErrType.Success)
            {
                await _teamTypeManager.AddAsync(new OATeamTypeForm()
                {
                    Name = entity.Type
                });
            }
            return result;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(OATeamForm entity)
        {
            var result = await _manager.UpdateAsync(entity);
            if (result == BaseErrType.Success)
            {
                await _teamMemberManager.SetLeaderAsync(entity.Id, new List<Guid>() { entity.LeaderId });
                await _teamTypeManager.AddAsync(new OATeamTypeForm()
                {
                    Name = entity.Type
                });
            }
            return result;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">实体id</param>
        /// <param name="isTrue">是否彻底删除</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeleteAsync(Guid id, bool isTrue)
        {
            if (isTrue)
            {
                return await _manager.TrueDeleteAsync(id);
            }
            else
            {
                var errType = await _manager.DeleteAsync(id);
                if (errType == BaseErrType.Success)
                {
                    await _teamMemberManager.DissolveAsync(id);
                }
                return errType;
            }
        }

        /// <summary>
        /// 批量排序
        /// </summary>
        /// <param name="entities">排序表单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> SortAsync(IEnumerable<OATeamSortForm> entities)
        {
            return await _manager.SortAsync(entities);
        }

        #endregion

        #region 成员

        /// <summary>
        /// 获取成员列表
        /// </summary>
        /// <param name="id">团队id</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<OATeamMemberDto>> GetListMemberAsync(Guid id)
        {
            var data = await _teamMemberManager.GetListAsync(id, false);
            return _mapper.Map<IEnumerable<OATeamMemberAggr>, IEnumerable<OATeamMemberDto>>(data);
        }

        /// <summary>
        /// 添加成员（批量）
        /// </summary>
        /// <param name="id">部门id</param>
        /// <param name="personIds">人员</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddMemberAsync(Guid id, IEnumerable<Guid> personIds)
        {
            return await _teamMemberManager.AddAsync(id, personIds);
        }

        /// <summary>
        /// 删除成员（批量）
        /// </summary>
        /// <param name="id">部门id</param>
        /// <param name="personIds">关联id集合</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeleteMemberAsync(Guid id, IEnumerable<Guid> personIds)
        {
            return await _teamMemberManager.DeleteAsync(id, personIds);
        }

        /// <summary>
        /// 导入Excel
        /// </summary>
        /// <param name="id">部门id</param>
        /// <param name="data">表单</param>
        /// <returns>结果</returns>
        public async Task<BaseMessage> ImportMemberExcelAsync(Guid id, IEnumerable<OATeamMemberImportForm> data)
        {
            return await _teamMemberManager.ImportExcelAsync(id, data);
        }

        #endregion
    }
}
