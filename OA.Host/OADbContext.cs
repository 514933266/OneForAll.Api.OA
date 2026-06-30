using System;
using Microsoft.EntityFrameworkCore;
using OA.Domain.AggregateRoots;

namespace OA.Host
{
    public partial class OADbContext : DbContext
    {
        private Guid _tenantId;

        public OADbContext(DbContextOptions<OADbContext> options)
            : base(options)
        {

        }

        public OADbContext(
            DbContextOptions<OADbContext> options,
            ITenantProvider tenantProvider)
            : base(options)
        {
            _tenantId = tenantProvider.GetTenantId();
        }

        #region 基础管理
        public virtual OAPerson OAPerson { get; set; }

        public virtual OATeam OATeam { get; set; }
        public virtual OATeamType OATeamType { get; set; }
        public virtual OAMidTeamPerson OAMidTeamPerson { get; set; }
        public virtual OAPersonSetting OAPersonSetting { get; set; }
        public virtual OAPersonSettingField OAPersonSettingField { get; set; }
        public virtual OATableColumnSetting OATableColumnSetting { get; set; }
        public virtual OAMidPersonUser OAMidPersonUser { get; set; }


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
                entity.ToTable("oa_person");
                entity.HasIndex(e => e.SysTenantId);
                entity.HasQueryFilter(e => e.SysTenantId == _tenantId);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<OATableColumnSetting>(entity =>
            {
                entity.ToTable("oa_table_column_setting");
                entity.HasIndex(e => e.SysTenantId);
                entity.HasQueryFilter(e => e.SysTenantId == _tenantId);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<OATeam>(entity =>
            {
                entity.ToTable("oa_team");
                entity.HasIndex(e => e.SysTenantId);
                entity.HasQueryFilter(e => e.SysTenantId == _tenantId);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<OATeamType>(entity =>
            {
                entity.ToTable("oa_team_type");
                entity.HasIndex(e => e.SysTenantId);
                entity.HasQueryFilter(e => e.SysTenantId == _tenantId);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<OAMidTeamPerson>(entity =>
            {
                entity.ToTable("oa_mid_team_person");
                entity.HasQueryFilter(e => e.SysTenantId == _tenantId);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<OAMidPersonUser>(entity =>
            {
                entity.ToTable("oa_mid_person_user");
                entity.HasKey(e => new { e.SysUserId, e.OAPersonId });
            });

            #endregion

            #region 人员异动

            modelBuilder.Entity<OATeamMemberHistory>(entity =>
            {
                entity.ToTable("oa_team_member_history");
                entity.HasIndex(e => e.SysTenantId);
                entity.HasQueryFilter(e => e.SysTenantId == _tenantId);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<OAPersonEntry>(entity =>
            {
                entity.ToTable("oa_person_entry");
                entity.HasIndex(e => e.SysTenantId);
                entity.HasQueryFilter(e => e.SysTenantId == _tenantId);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<OAPersonLeave>(entity =>
            {
                entity.ToTable("oa_person_leave");
                entity.HasIndex(e => e.SysTenantId);
                entity.HasQueryFilter(e => e.SysTenantId == _tenantId);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            #endregion

            #region 设置

            modelBuilder.Entity<OADingDingSetting>(entity =>
            {
                entity.ToTable("oa_dingding_setting");
                entity.HasIndex(e => e.SysTenantId);
                entity.HasQueryFilter(e => e.SysTenantId == _tenantId);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<OAPersonSetting>(entity =>
            {
                entity.ToTable("oa_person_setting");
                entity.HasIndex(e => e.SysTenantId);
                entity.HasQueryFilter(e => e.SysTenantId == _tenantId);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<OAPersonSettingField>(entity =>
            {
                entity.ToTable("oa_person_setting_field");
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<OAJob>(entity =>
            {
                entity.ToTable("oa_job");
                entity.HasIndex(e => e.SysTenantId);
                entity.HasQueryFilter(e => e.SysTenantId == _tenantId);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<OAJobLevel>(entity =>
            {
                entity.ToTable("oa_job_level");
                entity.HasIndex(e => e.SysTenantId);
                entity.HasQueryFilter(e => e.SysTenantId == _tenantId);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<OAJobType>(entity =>
            {
                entity.ToTable("oa_job_type");
                entity.HasIndex(e => e.SysTenantId);
                entity.HasQueryFilter(e => e.SysTenantId == _tenantId);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<OAJobDuty>(entity =>
            {
                entity.ToTable("oa_job_duty");
                entity.HasIndex(e => e.SysTenantId);
                entity.HasQueryFilter(e => e.SysTenantId == _tenantId);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            #endregion

            #region 个人日程

            modelBuilder.Entity<OAPersonalSchedule>(entity =>
            {
                entity.ToTable("oa_personal_schedule");
                entity.HasIndex(e => e.SysTenantId);
                entity.HasQueryFilter(e => e.SysTenantId == _tenantId);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            #endregion
        }
    }
}
