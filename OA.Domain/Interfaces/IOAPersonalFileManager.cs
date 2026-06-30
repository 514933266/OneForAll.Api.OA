using OA.Domain.AggregateRoots;
using OneForAll.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.Interfaces
{
    /// <summary>
    /// 用户档案
    /// </summary>
    public interface IOAPersonalFileManager
    {
        /// <summary>
        /// 获取个人档案
        /// </summary>
        Task<OAPerson> GetAsync();

        /// <summary>
        /// 查找并绑定个人档案
        /// </summary>
        /// <param name="key">手机|身份证</param>
        /// <returns>结果</returns>
        Task<BaseErrType> BindAsync(string key);
    }
}
