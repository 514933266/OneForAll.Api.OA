using OA.Domain.Enums;
using OneForAll.Core.DDD;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Domain.AggregateRoots
{
    /// <summary>
    /// 人员档案设置
    /// </summary>
    public class OAPersonSetting : AggregateRoot<Guid>
    {
        /// <summary>
        /// 所属机构Id
        /// </summary>
        [Required]
        public Guid SysTenantId { get; set; }

        /// <summary>
        /// 显示文本
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Text { get; set; }

        /// <summary>
        /// 类型（用来识别系统默认模板）
        /// </summary>
        [Required]
        public OAPersonSettingTypeEnum Type { get; set; }

        /// <summary>
        /// 排序编号
        /// </summary>
        [Required]
        public int SortNumber { get; set; }

        /// <summary>
        /// 是否可排序
        /// </summary>
        [Required]
        public bool IsSortable { get; set; }

        /// <summary>
        /// 是否默认(默认不可删除)
        /// </summary>
        [Required]
        public bool IsDefault { get; set; }

        /// <summary>
        /// 是否可编辑
        /// </summary>
        [Required]
        public bool IsEditable { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Required]
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 是否分组（分组即可填写多栏信息）
        /// </summary>
        [Required]
        public bool IsGrouped { get; set; }

        /// <summary>
        /// 是否显示可分组（分组即可填写多栏信息）
        /// </summary>
        [Required]
        public bool IsShowGrouped { get; set; }

        /// <summary>
        /// 初始化默认模板
        /// </summary>
        /// <param name="type">模板类型</param>
        public void Init(OAPersonSettingTypeEnum type)
        {
            switch (type)
            {
                case OAPersonSettingTypeEnum.BasicInformation:
                    InitBasicInformation();
                    break;
                case OAPersonSettingTypeEnum.WorkInformation:
                    InitWorkInformation();
                    break;
                case OAPersonSettingTypeEnum.PersonalInformation:
                    InitPersonalInformation();
                    break;
                case OAPersonSettingTypeEnum.EducationInformation:
                    InitEducationInformation();
                    break;
                case OAPersonSettingTypeEnum.BankCardInformation:
                    InitBankCardInformation();
                    break;
                case OAPersonSettingTypeEnum.ContractInformation:
                    InitContractInformation();
                    break;
                case OAPersonSettingTypeEnum.EmergencyContact:
                    InitEmergencyContact();
                    break;
                case OAPersonSettingTypeEnum.FamilyMember:
                    InitFamilyMember();
                    break;
                case OAPersonSettingTypeEnum.PersonalData:
                    InitPersonalData();
                    break;
            }
        }

        /// <summary>
        /// 初始化基础信息
        /// </summary>
        private void InitBasicInformation()
        {
            Id = Guid.NewGuid();
            Text = "基础信息";
            SortNumber = 0;
            IsDefault = true;
            IsEnabled = true;
            IsSortable = false;
            IsEditable = false;
            IsGrouped = false;
            IsShowGrouped = false;
            Type = OAPersonSettingTypeEnum.BasicInformation;
        }

        /// <summary>
        /// 初始化工作信息
        /// </summary>
        private void InitWorkInformation()
        {
            Id = Guid.NewGuid();
            Text = "工作信息";
            SortNumber = 1;
            IsDefault = true;
            IsEnabled = true;
            IsSortable = false;
            IsEditable = false;
            IsGrouped = false;
            IsShowGrouped = false;
            Type = OAPersonSettingTypeEnum.WorkInformation;
        }

        /// <summary>
        /// 初始化个人信息
        /// </summary>
        private void InitPersonalInformation()
        {
            Id = Guid.NewGuid();
            Text = "个人信息";
            SortNumber = 2;
            IsDefault = true;
            IsEnabled = true;
            IsEditable = false;
            IsGrouped = false;
            IsShowGrouped = false;
            Type = OAPersonSettingTypeEnum.PersonalInformation;
        }

        /// <summary>
        /// 初始化学历信息
        /// </summary>
        private void InitEducationInformation()
        {
            Id = Guid.NewGuid();
            Text = "学历信息";
            SortNumber = 3;
            IsDefault = true;
            IsEnabled = true;
            IsEditable = true;
            IsGrouped = true;
            IsShowGrouped = true;
            Type = OAPersonSettingTypeEnum.EducationInformation;
        }

        /// <summary>
        /// 初始化银行卡信息
        /// </summary>
        private void InitBankCardInformation()
        {
            Id = Guid.NewGuid();
            Text = "银行卡信息";
            SortNumber = 4;
            IsDefault = true;
            IsEnabled = true;
            IsEditable = true;
            IsGrouped = true;
            IsShowGrouped = true;
            Type = OAPersonSettingTypeEnum.BankCardInformation;
        }

        /// <summary>
        /// 初始化合同信息
        /// </summary>
        private void InitContractInformation()
        {
            Id = Guid.NewGuid();
            Text = "合同信息";
            SortNumber = 5;
            IsDefault = true;
            IsEnabled = true;
            IsEditable = true;
            IsGrouped = false;
            IsShowGrouped = false;
            Type = OAPersonSettingTypeEnum.ContractInformation;
        }

        /// <summary>
        /// 初始化紧急联系人
        /// </summary>
        private void InitEmergencyContact()
        {
            Id = Guid.NewGuid();
            Text = "紧急联系人";
            SortNumber = 6;
            IsDefault = true;
            IsEnabled = true;
            IsEditable = true;
            IsGrouped = true;
            IsShowGrouped = true;
            Type = OAPersonSettingTypeEnum.EmergencyContact;
        }

        /// <summary>
        /// 初始化家庭信息
        /// </summary>
        private void InitFamilyMember()
        {
            Id = Guid.NewGuid();
            Text = "家庭信息";
            SortNumber = 7;
            IsDefault = true;
            IsEnabled = true;
            IsEditable = true;
            IsGrouped = true;
            IsShowGrouped = true;
            Type = OAPersonSettingTypeEnum.FamilyMember;
        }

        /// <summary>
        /// 初始化个人材料
        /// </summary>
        private void InitPersonalData()
        {
            Id = Guid.NewGuid();
            Text = "个人材料";
            SortNumber = 8;
            IsDefault = true;
            IsEnabled = true;
            IsEditable = true;
            IsGrouped = false;
            IsShowGrouped = false;
            Type = OAPersonSettingTypeEnum.PersonalData;
        }
    }
}
