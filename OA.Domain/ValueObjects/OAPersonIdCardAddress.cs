using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.ValueObjects
{
    /// <summary>
    /// 人员档案个人信息设置-身份证地址
    /// </summary>
    public class OAPersonIdCardAddress : OAPersonDefaultFieldVo
    {
        public OAPersonIdCardAddress()
        {
            Name = "IdCardAddress";
            Text = "身份证地址";
            Placeholder = "请填写身份证地址";
            IsRequired = false;
            IsEnableRequired = true;
            IsEmployeeEditable = false;
            IsEntryFileVisiable = true;
            IsEnableEmployeeEditable = true;
            IsEnableEmployeeVisiable = true;
            IsEnableEntryFileVisiable = true;
        }
    }
}
