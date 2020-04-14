using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunScriptOvh
{
    class MyScheduler
    {
        public static void IntervalInSeconds(int hourStart, int secStart, int hourEnd, int secEnd, double interval, Action task)
        {
            interval = interval / 3600;
            
            SchedulerService.Instance.ScheduleTask(hourStart, secStart, hourEnd, secEnd, interval, task);
        }
        public static void IntervalInMinutes(int hourStart, int minStart, int hourEnd, int minEnd, double interval, Action task)
        {
            interval = interval / 60;
            SchedulerService.Instance.ScheduleTask(hourStart, minStart, hourEnd, minEnd, interval, task);
        }
        public static void IntervalInHours(int hourStart, int minStart, int hourEnd, int minEnd, double interval, Action task)
        {
            SchedulerService.Instance.ScheduleTask(hourStart, minStart,  hourEnd,  minEnd, interval, task);
        }
        /*public static void IntervalInDays(int hour, int min, double interval, Action task)
        {
            interval = interval * 24;
            SchedulerService.Instance.ScheduleTask(hour, min, interval, task);
        }*/
    }
}
