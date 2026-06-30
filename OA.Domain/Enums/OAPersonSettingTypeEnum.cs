using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.Enums
{
    /// <summary>
    /// OA人员档案字段设置类型
    /// </summary>
    public enum OAPersonSettingTypeEnum
    {
        /// <summary>
        /// 自定义
        /// </summary>
        None = -1,

        /// <summary>
        /// 基本信息
        /// </summary>
        BasicInformation = 0,

        /// <summary>
        /// 工作信息
        /// </summary>
        WorkInformation = 1,

        /// <summary>
        /// 个人信息
        /// </summary>
        PersonalInformation = 2,

        /// <summary>
        /// 学历信息
        /// </summary>
        EducationInformation = 3,

        /// <summary>
        /// 银行卡信息
        /// </summary>
        BankCardInformation = 4,

        /// <summary>
        /// 用工合同信息
        /// </summary>
        ContractInformation = 5,

        /// <summary>
        /// 紧急联系人
        /// </summary>
        EmergencyContact = 6,

        /// <summary>
        /// 家庭成员
        /// </summary>
        FamilyMember = 7,

        /// <summary>
        /// 个人资料
        /// </summary>
        PersonalData = 8
    }
}
