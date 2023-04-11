using System;
using Jh.Web.CronJob.Interfaces;

namespace Jh.Web.CronJob
{
    public class ScheduleConfig<T> : IScheduleConfig<T>
    {
        public string CronExpression { get; set; } = string.Empty;
        public TimeZoneInfo TimeZoneInfo { get; set; } = TimeZoneInfo.Local;
    }
}
