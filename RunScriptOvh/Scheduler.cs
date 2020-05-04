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
            DateTime now = DateTime.Now;
            DateTime firstRun = new DateTime(now.Year, now.Month, now.Day, hourStart, minStart, 0, 0);
            DateTime lastRun = new DateTime(now.Year, now.Month, now.Day, hourEnd, minEnd, 0, 0);
            StdSchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = await factory.GetScheduler();
            await scheduler.Start();
            //Console.WriteLine(obj.GetType().Name);
            switch (obj.GetType().Name)
            {
                case "JobArticles":
                    job = JobBuilder.Create<JobArticles>().Build();
                    break;
                case "JobAttributsCaracteristiques":
                    job = JobBuilder.Create<JobAttributsCaracteristiques>().Build();
                    break;
                case "JobClients":
                    job = JobBuilder.Create<JobClients>().Build();
                    break;
                case "JobCommand":
                    job = JobBuilder.Create<JobCommand>().Build();
                    break;
                case "JobCopiePhotos":
                    job = JobBuilder.Create<JobCopiePhotos>().Build();
                    break;
                case "JobGamme":
                    job = JobBuilder.Create<JobGamme>().Build();
                    break;
                case "JobNettoyagePhotos":
                    job = JobBuilder.Create<JobNettoyagePhotos>().Build();
                    break;
                case "JobPhotos":
                    job = JobBuilder.Create<JobPhotos>().Build();
                    break;
                case "JobPrix":
                    job = JobBuilder.Create<JobPrix>().Build();
                    break;
                case "JobStock":
                    job = JobBuilder.Create<JobStock>().Build();
                    break;
                case "JobArticlesKnco":
                    job = JobBuilder.Create<JobArticlesKnco>().Build();
                    break;
                default:
                    Console.WriteLine(obj.GetType().Name);
                    break;
            }
            

                if (jours != null)
                {
                    foreach (jours jour in jours)
                    {
                        if (type == "Minutes")
                        {
                            ITrigger trigger = TriggerBuilder.Create()
                             .WithIdentity("IDGJob", "IDG")
                               .StartAt(firstRun).EndAt(lastRun)
                               .WithCronSchedule("0 0/" + intervalInHour + " * ? * " + jour.variable)
                               .Build();
                         scheduler.ScheduleJob(job, trigger);
                        }
                        else
                        {
                            ITrigger trigger = TriggerBuilder.Create()
                             .WithIdentity("IDGJob", "IDG")
                               .StartAt(firstRun).EndAt(lastRun)
                               .WithCronSchedule("0 0 " + intervalInHour + " ? * " + jour.variable)
                               .Build();
                         scheduler.ScheduleJob(job, trigger);
                        }
                    }
                }
                else
                {
                    if (type == "Minutes")
                    {
                        ITrigger trigger = TriggerBuilder.Create()
                         .WithIdentity("IDGJob", "IDG")
                           .StartAt(firstRun).EndAt(lastRun)
                           .WithCronSchedule("0 0/" + intervalInHour + " * ? * *")
                           .Build();
                    scheduler.ScheduleJob(job, trigger);
                    }
                    else
                    {
                        ITrigger trigger = TriggerBuilder.Create()
                         .WithIdentity("IDGJob", "IDG")
                           .StartAt(firstRun).EndAt(lastRun)
                           .WithCronSchedule("0 0 " + intervalInHour + " ? * *")
                           .Build();
                     scheduler.ScheduleJob(job, trigger);
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
