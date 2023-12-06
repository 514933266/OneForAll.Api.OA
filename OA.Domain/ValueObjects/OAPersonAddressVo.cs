using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.ValueObjects
{
    /// <summary>
    /// 人员档案个人信息设置-居住地址
    /// </summary>
    public class OAPersonAddressVo : OAPersonDefaultFieldVo
    {
        public OAPersonAddressVo()
        {
            Name = "Address";
            Text = "居住地址";
            Placeholder = "请填写居住地址";
            IsRequired = false;
            IsEnableRequired = true;
            IsEmployeeEditable = false;
            IsEnableEmployeeEditable = true;
            IsEnableEmployeeVisiable = true;
            IsEntryFileVisiable = true;
            IsEnableEntryFileVisiable = true;
        }
    }
}
