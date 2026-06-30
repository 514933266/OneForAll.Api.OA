using OA.Domain.AggregateRoots;
using OneForAll.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.Repositorys
{
    /// <summary>
    /// 人员档案字段设置
    /// </summary>
    public interface IOAPersonSettingFieldRepository : IEFCoreRepository<OAPersonSettingField>
    {
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="ids">实体id</param>
        /// <returns>人员列表</returns>
        Task<IEnumerable<OAPersonSettingField>> GetListAsync(IEnumerable<Guid> ids);

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="settingId">设置id</param>
        /// <returns>人员列表</returns>
        Task<IEnumerable<OAPersonSettingField>> GetListBySettingAsync(Guid settingId);

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="settingIds">设置id</param>
        /// <returns>人员列表</returns>
        Task<IEnumerable<OAPersonSettingField>> GetListBySettingAsync(IEnumerable<Guid> settingIds);
    }
}
