
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using OneForAll.Core.DDD;
using System.ComponentModel.DataAnnotations.Schema;
using OneForAll.Core.Extension;
using OneForAll.Core.Utility;
using OA.Domain.Enums;
using OA.Domain.ValueObjects;
using System.Linq;

namespace OA.Domain.AggregateRoots
{
    /// <summary>
    /// 基础表：人员资料
    /// </summary>
    public class OAPerson : AggregateRoot<Guid>
    {
        /// <summary>
        /// 所属机构Id
        /// </summary>
        [Required]
        public Guid SysTenantId { get; set; }

        /// <summary>
        /// 所属用户Id
        /// </summary>
        [Required]
        public Guid SysUserId { get; set; } = Guid.Empty;

        /// <summary>
        /// 姓名
        /// </summary>
        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        [Required]
        [StringLength(20)]
        public string WorkNumber { get; set; } = "";

        /// <summary>
        /// 职级
        /// </summary>
        [Required]
        [StringLength(20)]
        public string Job { get; set; }

        /// <summary>
        /// 员工类型
        /// </summary>
        [Required]
        [StringLength(20)]
        public string EmployeeType { get; set; } = "全职";

        /// <summary>
        /// 员工状态
        /// </summary>
        [Required]
        [StringLength(20)]
        public string EmployeeStatus { get; set; } = "正式员工";

        /// <summary>
        /// 头像
        /// </summary>
        [Required]
        [StringLength(300)]
        public string IconUrl { get; set; } = "";

        /// <summary>
        /// 性别 0女 1男
        /// </summary>
        [Required]
        public bool Sex { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        [Range(0, 255)]
        public int Age { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        [Required]
        [Column(TypeName = "varchar(20)")]
        public string IdCard { get; set; } = "";

        /// <summary>
        /// 生日
        /// </summary>
        [Column(TypeName = "date")]
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 入职日期
        /// </summary>
        [Column(TypeName = "date")]
        public DateTime? EntryDate { get; set; }

        /// <summary>
        /// 离职日期
        /// </summary>
        [Column(TypeName = "date")]
        public DateTime? LeaveDate { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [Required]
        [StringLength(20)]
        public string MobilePhone { get; set; } = "";

        /// <summary>
        /// 邮箱
        /// </summary>
        [Required]
        [StringLength(200)]
        public string Email { get; set; } = "";

        /// <summary>
        /// 司龄
        /// </summary>
        [Required]
        [Column(TypeName = "decimal(6,2)")]
        public decimal JoinAge { get; set; }

        /// <summary>
        /// 创建人id
        /// </summary>
        [Required]
        public Guid CreatorId { get; set; }

        /// <summary>
        /// 创建人id
        /// </summary>
        [Required]
        [StringLength(20)]
        public string CreatorName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 备注
        /// </summary>
        [Required]
        [StringLength(500)]
        public string Remark { get; set; } = "";

        /// <summary>
        /// 扩展信息
        /// </summary>
        [Required]
        public string ExtendInformationJson { get; set; } = "";

        /// <summary>
        /// 初始化扩展信息
        /// </summary>
        public void InitExtendInfomation()
        {
            var info = new List<OAPersonExtenInformationFieldVo>();
            info.Add(new OAPersonExtenInformationFieldVo() { Name = new OAPersonJobVo().Name, Value = Job });
            info.Add(new OAPersonExtenInformationFieldVo() { Name = new OAPersonNameVo().Name, Value = Name });
            info.Add(new OAPersonExtenInformationFieldVo() { Name = new OAPersonEmailVo().Name, Value = Email });
            info.Add(new OAPersonExtenInformationFieldVo() { Name = new OAPersonRemarkVo().Name, Value = Remark });
            info.Add(new OAPersonExtenInformationFieldVo() { Name = new OAPersonIdCardVo().Name, Value = IdCard });
            info.Add(new OAPersonExtenInformationFieldVo() { Name = new OAPersonWorkNumberVo().Name, Value = WorkNumber });
            info.Add(new OAPersonExtenInformationFieldVo() { Name = new OAPersonMobilePhoneVo().Name, Value = MobilePhone });
            info.Add(new OAPersonExtenInformationFieldVo() { Name = new OAPersonEmployeeTypeVo().Name, Value = EmployeeType });
            info.Add(new OAPersonExtenInformationFieldVo() { Name = new OAPersonEmployeeStatusVo().Name, Value = EmployeeStatus });
            info.Add(new OAPersonExtenInformationFieldVo() { Name = new OAPersonSexVo().Name, Value = Sex ? "男" : "女" });
            info.Add(new OAPersonExtenInformationFieldVo() { Name = new OAPersonAgeVo().Name, Value = Age.ToString() });
            info.Add(new OAPersonExtenInformationFieldVo() { Name = new OAPersonBirthdayVo().Name, Value = Birthday.ToString() });
            info.Add(new OAPersonExtenInformationFieldVo() { Name = new OAPersonEntryDateVo().Name, Value = EntryDate?.ToString() });
            info.Add(new OAPersonExtenInformationFieldVo() { Name = new OAPersonLeaveDateVo().Name, Value = LeaveDate?.ToString() });
            ExtendInformationJson = info.ToJson();
        }

        /// <summary>
        /// 重新初始化信息
        /// </summary>
        public void ReInitExtendInfomation()
        {
            var infos = ExtendInformationJson.FromJson<List<OAPersonExtenInformationFieldVo>>();
            if (infos == null)
                infos = new List<OAPersonExtenInformationFieldVo>();
            InitExtendInfomation(infos);
        }

        /// <summary>
        /// 初始化扩展信息
        /// </summary>
        /// <param name="extendInformations">扩展信息</param>
        public void InitExtendInfomation(ICollection<OAPersonExtenInformationFieldVo> extendInformations)
        {
            if (extendInformations.Any())
            {
                #region 字段初始化
                var name = extendInformations.FirstOrDefault(w => w.Name == new OAPersonNameVo().Name);
                var job = extendInformations.FirstOrDefault(w => w.Name == new OAPersonJobVo().Name);
                var team = extendInformations.FirstOrDefault(w => w.Name == new OAPersonTeamVo().Name);
                var teamLeader = extendInformations.FirstOrDefault(w => w.Name == new OAPersonTeamLeaderVo().Name);
                var email = extendInformations.FirstOrDefault(w => w.Name == new OAPersonEmailVo().Name);
                var remark = extendInformations.FirstOrDefault(w => w.Name == new OAPersonRemarkVo().Name);
                var idCard = extendInformations.FirstOrDefault(w => w.Name == new OAPersonIdCardVo().Name);
                var sex = extendInformations.FirstOrDefault(w => w.Name == new OAPersonSexVo().Name);
                var age = extendInformations.FirstOrDefault(w => w.Name == new OAPersonAgeVo().Name);
                var workAge = extendInformations.FirstOrDefault(w => w.Name == new OAPersonWorkAgeVo().Name);
                var joinAge = extendInformations.FirstOrDefault(w => w.Name == new OAPersonJoinAgeVo().Name);
                var birthday = extendInformations.FirstOrDefault(w => w.Name == new OAPersonBirthdayVo().Name);
                var entryDate = extendInformations.FirstOrDefault(w => w.Name == new OAPersonEntryDateVo().Name);
                var leaveDate = extendInformations.FirstOrDefault(w => w.Name == new OAPersonLeaveDateVo().Name);
                var workNumber = extendInformations.FirstOrDefault(w => w.Name == new OAPersonWorkNumberVo().Name);
                var mobilePhone = extendInformations.FirstOrDefault(w => w.Name == new OAPersonMobilePhoneVo().Name);
                var firstWorkDate = extendInformations.FirstOrDefault(w => w.Name == new OAPersonFirstWorkDateVo().Name);
                var employeeType = extendInformations.FirstOrDefault(w => w.Name == new OAPersonEmployeeTypeVo().Name);
                var employeeStatus = extendInformations.FirstOrDefault(w => w.Name == new OAPersonEmployeeStatusVo().Name);
                var actualEntryDate = extendInformations.FirstOrDefault(w => w.Name == new OAPersonActualEntryDateVo().Name);

                if (name != null) Name = name.Value;
                if (job != null) Job = job.Value;
                if (email != null) Email = email.Value;
                if (remark != null) Remark = remark.Value;
                if (idCard != null) IdCard = idCard.Value;
                if (workNumber != null) WorkNumber = workNumber.Value;
                if (mobilePhone != null) MobilePhone = mobilePhone.Value;
                if (employeeType != null) EmployeeType = employeeType.Value;
                if (employeeStatus != null) EmployeeStatus = employeeStatus.Value;
                if (entryDate != null) EntryDate = entryDate.Value.IsNullOrEmpty() ? null : entryDate.Value.TryDateTime();
                if (leaveDate != null) LeaveDate = leaveDate.Value.IsNullOrEmpty() ? null : leaveDate.Value.TryDateTime();

                #endregion

                #region 计算赋值信息
                if (joinAge == null)
                {
                    joinAge = new OAPersonExtenInformationFieldVo() { Name = new OAPersonJoinAgeVo().Name };
                    extendInformations.Add(joinAge);
                }
                if (workAge == null)
                {
                    workAge = new OAPersonExtenInformationFieldVo() { Name = new OAPersonWorkAgeVo().Name };
                    extendInformations.Add(workAge);
                }
                if (birthday == null)
                {
                    birthday = new OAPersonExtenInformationFieldVo() { Name = new OAPersonBirthdayVo().Name };
                    extendInformations.Add(birthday);
                }
                if (sex == null)
                {
                    sex = new OAPersonExtenInformationFieldVo() { Name = new OAPersonSexVo().Name };
                    extendInformations.Add(sex);
                }
                if (age == null)
                {
                    age = new OAPersonExtenInformationFieldVo() { Name = new OAPersonAgeVo().Name };
                    extendInformations.Add(age);
                }

                // 通过入职日期得到司龄
                if (EntryDate != null)
                {
                    var timespan = Math.Round(((DateTime.Now - EntryDate.Value).TotalDays / 365), 2).ToString();
                    JoinAge = timespan.TryDecimal();
                    joinAge.Value = timespan;
                }

                // 通过首次参加工作日期得到工龄
                if (firstWorkDate != null && !firstWorkDate.Value.IsNullOrEmpty() && workAge != null)
                {
                    workAge.Value = Math.Round(((DateTime.Now - firstWorkDate.Value.TryDateTime()).TotalDays / 365), 2).ToString();
                }

                // 通过身份证得到性别、年龄、出生日期
                if (!IdCard.IsNullOrEmpty() && IdCard.Length >= 15)
                {
                    var strYear = DateTime.Now.Year;
                    var strMonth = DateTime.Now.Month;
                    var strDay = DateTime.Now.Day;
                    var idSex = 0;
                    if (IdCard.Length == 15)
                    {
                        strYear = IdCard.Substring(6, 4).TryInt();
                        strMonth = IdCard.Substring(8, 2).TryInt();
                        strDay = IdCard.Substring(10, 2).TryInt();
                        idSex = IdCard.Substring(12, 3).TryInt();
                    }
                    if (IdCard.Length == 18)
                    {
                        strYear = IdCard.Substring(6, 4).TryInt();
                        strMonth = IdCard.Substring(10, 2).TryInt();
                        strDay = IdCard.Substring(12, 2).TryInt(); ;
                        idSex = IdCard.Substring(14, 3).TryInt();

                    }

                    if (strYear > 0 && strMonth > 0 && strDay > 0)
                    {
                        Birthday = new DateTime(strYear, strMonth, strDay);
                        Sex = (idSex % 2) == 0 ? false : true;
                        Age = DateTime.Now.Year - strYear;
                        if (DateTime.Now.Month < Birthday.Value.Month)
                            Age -= 1;
                    }

                    birthday.Value = Birthday.ToString();
                    sex.Value = Sex ? "男" : "女";
                    age.Value = Age.ToString();
                }

                #endregion

                ExtendInformationJson = extendInformations.ToJson();
            }
            else
            {
                InitExtendInfomation();
            }
        }

        /// <summary>
        /// 初始化扩展信息
        /// </summary>
        /// <param name="teams">团队</param>
        /// <param name="leaders">主管</param>
        public void InitExtendInfomation(IEnumerable<OATeam> teams, IEnumerable<OAPerson> leaders)
        {
            var extendInformations = ExtendInformationJson.FromJson<IEnumerable<OAPersonExtenInformationFieldVo>>();
            if (teams.Any())
            {
                teams = teams.OrderBy(o => o.CreateTime);
                var team = extendInformations.FirstOrDefault(w => w.Name == new OAPersonTeamVo().Name);
                if (team != null)
                {
                    team.Value = string.Join(",", teams.Select(s => s.Name));
                }
            }

            if (leaders.Any())
            {
                var leader = extendInformations.FirstOrDefault(w => w.Name == new OAPersonTeamLeaderVo().Name);
                if (leader != null)
                {
                    leader.Value = string.Join(",", leaders.Select(s => s.Name));
                }
            }

            ExtendInformationJson = extendInformations.ToJson();
        }
    }
}
