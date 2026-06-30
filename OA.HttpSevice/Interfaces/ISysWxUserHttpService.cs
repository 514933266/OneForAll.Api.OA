using OA.HttpService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.HttpService.Interfaces
{
    /// <summary>
    /// 微信用户信息
    /// </summary>
    public interface ISysWxUserHttpService
    {  /// <summary>
       /// 获取患者微信用户信息
       /// </summary>
       /// <param name="userId">用户id</param>
       /// <returns></returns>
        Task<SysWxgzhSubscribeUserResponse> GetSysWxgzhUsersAsync(Guid userId);
    }
}

