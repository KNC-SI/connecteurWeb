using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RunScriptOvh
{
    class SchedulerService
    {
        private static SchedulerService _instance;
        private List<Timer> timers = new List<Timer>();
        private SchedulerService() { }
        public static SchedulerService Instance => _instance ?? (_instance = new SchedulerService());


        public void ScheduleTask(int hourStart, int minStart, int hourEnd, int minEnd, double intervalInHour, Action task)
        {
           
            DateTime now = DateTime.Now;
            DateTime firstRun = new DateTime(now.Year, now.Month, now.Day, hourStart, minStart, 0, 0);
            DateTime lastRun = new DateTime(now.Year, now.Month, now.Day, hourEnd, minEnd, 0, 0);
            int i = 0;
            int j = 0;
            int dateFirst = 0;
            int dateLast = 0;
             dateFirst =DateTime.Compare(firstRun, now);
             dateLast = DateTime.Compare(now, lastRun);
            Console.WriteLine(dateLast);
            if (dateFirst<0 && dateLast < 0)
                {
                i++;
                Console.WriteLine(" i=" + i);
                TimeSpan timeToGo = lastRun - now;
                Console.WriteLine("timeToGo " + timeToGo);
               
                    var timer = new Timer(x =>
                    {
                        task.Invoke();
                    }, null, timeToGo, TimeSpan.FromHours(intervalInHour));
                    timers.Add(timer);
               
            }
            else
            {
                j++;
                Console.WriteLine(" j=" + j);
                var timer = new Timer(x =>
                {
                    task.Invoke();
                }, null, -1, (System.Int32)intervalInHour);
                timers.Add(timer);
            }
            


            /* if (timeToGo <= TimeSpan.Zero)
             {
                 timeToGo = TimeSpan.Zero;
                 Console.WriteLine("pour la condition timespan.zero i=" + i);


             }*/
            
         
           
           
            


        }



    }


    }

