using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Application.Dtos
{
    public class OAPersonSelectionDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 头像地址
        /// </summary>
        public string IconUrl { get; set; }
    }
}