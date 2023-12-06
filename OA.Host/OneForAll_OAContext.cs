using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using OA.Domain.AggregateRoots;

namespace OA.Host
{
    public partial class OneForAll_OAContext : DbContext
    {
        private Guid _tenantId;

        public OneForAll_OAContext(DbContextOptions<OneForAll_OAContext> options)
            : base(options)
        {

        }

        public OneForAll_OAContext(
            DbContextOptions<OneForAll_OAContext> options,
            ITenantProvider tenantProvider)
            : base(options)
        {
            _tenantId = tenantProvider.GetTenantId();
        }

        #region 基础管理
        public virtual OAPerson OAPerson { get; set; }

        public virtual OATeam OATeam { get; set; }
        public virtual OATeamType OATeamType { get; set; }
        public virtual OATeamPersonContact OATeamPersonContact { get; set; }
        public virtual OAPersonSetting OAPersonSetting { get; set; }
        public virtual OAPersonSettingField OAPersonSettingField { get; set; }
        public virtual OATableColumnSetting OATableColumnSetting { get; set; }
        public virtual OAPersonUserContact OAPersonUserContact { get; set; }


        #endregion

        #region 人员异动
        public virtual OATeamMemberHistory OATeamMemberHistory { get; set; }
        public virtual OAPersonEntry OAPersonEntry { get; set; }
        public virtual OAPersonLeave OAPersonLeave { get; set; }
        #endregion

        #region 设置
        public virtual OADingDingSetting OADingDingSetting { get; set; }
        public virtual OAJob OAJob { get; set; }
        public virtual OAJobLevel OAJobLevel { get; set; }
        public virtual OAJobType OAJobType { get; set; }
        public virtual OAJobDuty OAJobDuty { get; set; }
        #endregion

        #region 个人日程

        public virtual OAPersonalSchedule OAPersonalSchedule { get; set; }

        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region 基础管理

            modelBuilder.Entity<OAPerson>(entity =>
            {
                entity.ToTable("OA_Person");
                entity.HasIndex(e => e.SysTenantId);
                entity.HasQueryFilter(e => e.SysTenantId == _tenantId);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<OATableColumnSetting>(entity =>
            {
                entity.ToTable("OA_TableColumnSetting");
                entity.HasIndex(e => e.SysTenantId);
                entity.HasQueryFilter(e => e.SysTenantId == _tenantId);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<OATeam>(entity =>
            {
                entity.ToTable("OA_Team");
                entity.HasIndex(e => e.SysTenantId);
                entity.HasQueryFilter(e => e.SysTenantId == _tenantId);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<OATeamType>(entity =>
            {
                entity.ToTable("OA_TeamType");
                entity.HasIndex(e => e.SysTenantId);
                entity.HasQueryFilter(e => e.SysTenantId == _tenantId);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<OATeamPersonContact>(entity =>
            {
                entity.ToTable("OA_TeamPersonContact");
                entity.HasQueryFilter(e => e.SysTenantId == _tenantId);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<OAPersonUserContact>(entity =>
            {
                entity.ToTable("OA_PersonUserContact");
                entity.HasKey(e => new { e.SysUserId, e.OAPersonId });
            });

            #endregion

            #region 人员异动

            modelBuilder.Entity<OATeamMemberHistory>(entity =>
            {
                entity.ToTable("OA_TeamMemberHistory");
                entity.HasIndex(e => e.SysTenantId);
                entity.HasQueryFilter(e => e.SysTenantId == _tenantId);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<OAPersonEntry>(entity =>
            {
                entity.ToTable("OA_PersonEntry");
                entity.HasIndex(e => e.SysTenantId);
                entity.HasQueryFilter(e => e.SysTenantId == _tenantId);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<OAPersonLeave>(entity =>
            {
                entity.ToTable("OA_PersonLeave");
                entity.HasIndex(e => e.SysTenantId);
                entity.HasQueryFilter(e => e.SysTenantId == _tenantId);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            #endregion

            #region 设置

            modelBuilder.Entity<OADingDingSetting>(entity =>
            {
                entity.ToTable("OA_DingDingSetting");
                entity.HasIndex(e => e.SysTenantId);
                entity.HasQueryFilter(e => e.SysTenantId == _tenantId);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<OAPersonSetting>(entity =>
            {
                entity.ToTable("OA_PersonSetting");
                entity.HasIndex(e => e.SysTenantId);
                entity.HasQueryFilter(e => e.SysTenantId == _tenantId);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<OAPersonSettingField>(entity =>
            {
                entity.ToTable("OA_PersonSettingField");
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<OAJob>(entity =>
            {
                entity.ToTable("OA_Job");
                entity.HasIndex(e => e.SysTenantId);
                entity.HasQueryFilter(e => e.SysTenantId == _tenantId);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<OAJobLevel>(entity =>
            {
                entity.ToTable("OA_JobLevel");
                entity.HasIndex(e => e.SysTenantId);
                entity.HasQueryFilter(e => e.SysTenantId == _tenantId);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<OAJobType>(entity =>
            {
                entity.ToTable("OA_JobType");
                entity.HasIndex(e => e.SysTenantId);
                entity.HasQueryFilter(e => e.SysTenantId == _tenantId);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<OAJobDuty>(entity =>
            {
                entity.ToTable("OA_JobDuty");
                entity.HasIndex(e => e.SysTenantId);
                entity.HasQueryFilter(e => e.SysTenantId == _tenantId);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            #endregion

            #region 个人日程

            modelBuilder.Entity<OAPersonalSchedule>(entity =>
            {
                entity.ToTable("OA_PersonalSchedule");
                entity.HasIndex(e => e.SysTenantId);
                entity.HasQueryFilter(e => e.SysTenantId == _tenantId);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            #endregion
        }
    }
}
