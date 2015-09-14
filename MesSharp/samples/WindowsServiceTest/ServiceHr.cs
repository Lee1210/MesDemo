using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using Quartz;
using Quartz.Impl;

namespace WindowsServiceTest
{
    public partial class ServiceHr : ServiceBase
    {
        public ServiceHr()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {

            CpkDemo.Program.EntranceTest();
            ISchedulerFactory schedFact = new StdSchedulerFactory();

            // get a scheduler
            IScheduler sched = schedFact.GetScheduler();
            sched.Start();

            // define the job and tie it to our HelloJob class
            IJobDetail job = JobBuilder.Create<HelloWorld>()
                .WithIdentity("myJob", "group1")
                .Build();

           // Trigger the job to run now, and then every 40 seconds
           ITrigger trigger = TriggerBuilder.Create()
             .WithIdentity("myTrigger", "group1")
             .StartNow()
             .WithSimpleSchedule(x => x
                 .WithIntervalInHours(1)
                 .RepeatForever())
             .Build();
            //ITrigger trigger = TriggerBuilder.Create()
            //.WithIdentity("myTrigger", "group1")
            //.WithCronSchedule("0/5 27 * * * ?")
            //.ForJob("myJob", "group1")
            //.Build();
            //sched.ScheduleJob(job, trigger);

        }

        protected override void OnStop()
        {
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter("C:\\log.txt", true))
            {
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "Stop.");
            }
        }
    }
}
