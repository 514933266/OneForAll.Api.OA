using OA.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Application.Dtos
{
    /// <summary>
    /// 员工入职资料填写
    /// </summary>
    public class OAPersonalEntryFileDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 扩展信息
        /// </summary>
        public List<OAPersonExtenInformationFieldVo> ExtendInformations { get; set; } = new List<OAPersonExtenInformationFieldVo>();
    }
}
