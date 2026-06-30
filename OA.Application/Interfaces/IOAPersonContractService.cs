using OA.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Application.Interfaces
{
    /// <summary>
    /// 合同管理
    /// </summary>
    public interface IOAPersonContractService
    {
        /// <summary>
        /// 获取近二个月合同即将过期/已过期列表
        /// </summary>
        /// <param name="teamId">所属部门id</param>
        /// <returns>列表</returns>
        Task<IEnumerable<OAPersonContractDto>> GetListAsync(Guid teamId);
    }
}
