using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.Enums
{
    /// <summary>
    /// 人员在职状况
    /// </summary>
    public enum OAPersonOnJobStatusEnum
    {
        None = -1,

        /// <summary>
        /// 在职
        /// </summary>
        OnJob = 0,

        /// <summary>
        /// 离职
        /// </summary>
        Leave = 1
    }
}
