﻿using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using OneForAll.Core;
using OA.Application.Dtos;

namespace OA.Application.Interfaces
{
    /// <summary>
    /// 团队类型
    /// </summary>
    public interface IOATeamTypeService
    {
        /// <summary>
        /// 获取类型列表
        /// </summary>
        /// <param name="name">类型</param>
        /// <returns>组织架构树</returns>
        Task<IEnumerable<OATeamTypeDto>> GetListAsync(string name);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">实体id</param>
        /// <returns>列表</returns>
        Task<BaseErrType> DeleteAsync(Guid id);
    }
}
