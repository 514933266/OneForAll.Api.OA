using Quartz;
using OA.Host.Models;
using OA.Public.Models;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using OA.HttpService.Interfaces;
using OA.Application.Interfaces;

namespace OA.Host.QuartzJobs
{
    /// <summary>
    /// 监控个人日程
    /// </summary>
    public class MonitorPersonlScheduleJob : BaseLockJob
    {
        private readonly IConfiguration _configuration;
        private readonly IOAPersonalScheduleService _scheduleService;

        public MonitorPersonlScheduleJob(
            IConfiguration configuration,
            IOAPersonalScheduleService scheduleService,
            QuartzScheduleJobConfig jobConfig,
            AuthConfig authConfig,
            JobLockConfig lockConfig,
            IScheduleJobHttpService jobHttpService,
            ISysGlobalExceptionLogHttpService logHttpService)
            : base(jobConfig, authConfig, lockConfig, jobHttpService, logHttpService)
        {
            _configuration = configuration;
            _scheduleService = scheduleService;
        }

        /// <summary>
        /// 执行任务
        /// </summary>
        protected override async Task ExecuteInternalAsync(IJobExecutionContext context)
        {
            var templateId = _configuration["Wxgzh:ScheduleMsgTemplateId"];
            var num = await _scheduleService.MonitorNotifyAsync(templateId);
            await AddLogAsync($"巡检个人日程任务执行完成，共发送{num}条提醒");
        }
    }
}
