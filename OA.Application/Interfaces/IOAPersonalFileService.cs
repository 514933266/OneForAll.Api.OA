using OA.Application.Dtos;
using OA.Domain.AggregateRoots;
using OA.Domain.Models;
using OneForAll.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Application.Interfaces
{
    /// <summary>
    /// 用户档案
    /// </summary>
    public interface IOAPersonalFileService
    {
        /// <summary>
        /// 获取人员
        /// </summary>
        /// <returns>基础信息</returns>
        Task<OAPersonDto> GetAsync();

        /// <summary>
        /// 查找并绑定个人档案
        /// </summary>
        /// <param name="form">手机|身份证</param>
        /// <returns></returns>
        Task<BaseErrType> BindAsync(OABindPersonalFileForm form);
    }
}
