using System;
using System.Collections.Generic;
using Quartz;
using Quartz.Impl;

namespace RunScriptOvh
{
    class Scheduler
    {
       
        public IJobDetail job;
        public async void Start(int hourStart, int minStart, int hourEnd, int minEnd, int intervalInHour,object obj, List<jours> jours, string type)

        {
            foreach (jours jour in jours)
            {
                DateTime now = DateTime.Now;
                DateTime firstRun = new DateTime(now.Year, now.Month, now.Day, hourStart, minStart, 0, 0);
                DateTime lastRun = new DateTime(now.Year, now.Month, now.Day, hourEnd, minEnd, 0, 0);
                StdSchedulerFactory factory = new StdSchedulerFactory();
                IScheduler scheduler = await factory.GetScheduler();
                await scheduler.Start();
                switch (obj.GetType().Name)
                {
                    case "Job":
                        job = JobBuilder.Create<Job>().Build();
                        break;
                    case "Job1":
                        Console.WriteLine("Case 2");
                        break;
                    default:
                        Console.WriteLine("Default case");
                        break;
                }
                if (jours!=null)
                {
                    
                    if (type== "Secondes")
                    {
                        ITrigger trigger = TriggerBuilder.Create()
                         .WithIdentity("IDGJob", "IDG")
                           .WithCronSchedule(""+intervalInHour+ " 0 "+hourStart+"-"+hourEnd+ " ? * "+ jour)
                           .ForJob(job)
                           .Build();
                        //await scheduler.ScheduleJob(job, trigger);
                    }
                    else if (type == "Minutes")
                    {
                        ITrigger trigger = TriggerBuilder.Create()
                         .WithIdentity("IDGJob", "IDG")
                           .StartAt(firstRun).EndAt(lastRun)
                           .WithCronSchedule("0 0/2 * ? * *")
                           //.WithCronSchedule(" 0 "+(0/intervalInHour)+" "+ hourStart + "-" + hourEnd + " ? * " + jour)
                           //.ForJob(job)
                           .Build();
                       // await 
                        scheduler.ScheduleJob(job, trigger);
                    }
                    else
                    {

                    }
                }
               
                /*ITrigger trigger = TriggerBuilder.Create()
                 .WithIdentity("IDGJob", "IDG")
                   .StartAt(firstRun).EndAt(lastRun).WithCronSchedule("")
                   .WithPriority(1)
                   .Build();*/
                //await scheduler.ScheduleJob(job, trigger);
            }
            

        }
    }
}
