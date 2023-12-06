using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.ValueObjects
{
    /// <summary>
    /// 人员档案紧急联系人设置-紧急联系人姓名
    /// </summary>
    public class OAPersonContactNameVo : OAPersonDefaultFieldVo
    {
        public OAPersonContactNameVo()
        {
            Name = "ContactName";
            Text = "紧急联系人姓名";
            Placeholder = "请填写紧急联系人姓名";
            IsRequired = true;
            IsEnableRequired = true;
            IsEmployeeEditable = false;
            IsEnableEmployeeEditable = true;
            IsEnableEmployeeVisiable = true;
            IsEntryFileVisiable = true;
            IsEnableEntryFileVisiable = true;
        }
    }
}
