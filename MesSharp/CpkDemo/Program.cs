// -----------------------------------------------------------------------
//  <copyright file="Program.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>最后修改人</last-editor>
//  <last-date>2015-08-26 10:15</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;

using Autofac;

using CpkDemo.Menjin;

using FileClizz;

using menjin;

using Mes.Core;
using Mes.Core.Caching;
using Mes.Core.Data;
using Mes.Demo.Models.Hr;
using Mes.Demo.Models.TestLog;
using Mes.Utility.Develop;
using Mes.Utility.Extensions;

using Quartz;
using Quartz.Impl;


namespace CpkDemo
{

    internal class Program : IDependency
    {
        public static Program _program;
        // ReSharper disable once NotAccessedField.Local
        private static ICache _cache;
           public LogResolver LogResolver { get; set; }
        public  EntranceSolution EnSolution { get; set; }

        // ReSharper disable once UnusedParameter.Local
        private static void Main(string[] args)
        {
          
                  Startup.Start();
                  _program = Startup.Container.Resolve<Program>();
                  _cache = CacheManager.GetCacher(typeof(Program));
                  LogExcute();
               // Jobinitialization();


            //    EntranceSolution();
            //Console.ReadLine();
        }

        private static void Jobinitialization()
        {
            Console.WriteLine(@"此为CPK的JOB，请勿动");
            ISchedulerFactory schedFact = new StdSchedulerFactory();

            // get a scheduler
            IScheduler sched = schedFact.GetScheduler();
            sched.Start();

            //// define the job and tie it to our HelloJob class
            //IJobDetail hRjob = JobBuilder.Create<HrJob>()
            //    .WithIdentity("myJob", "group1")
            //    .Build();
            //ITrigger hRTrigger = TriggerBuilder.Create()
            //   .WithIdentity("myTrigger", "group1")
            //   .WithCronSchedule("0 0 3 * * ?")
            //   .ForJob("myJob", "group1")
            //   .Build();
            //sched.ScheduleJob(hRjob, hRTrigger);
            // Trigger the job to run now, and then every 40 seconds
            //ITrigger trigger = TriggerBuilder.Create()
            //  .WithIdentity("myTrigger", "group1")
            //  .StartNow()
            //  .WithSimpleSchedule(x => x
            //      .WithIntervalInHours(1)
            //      .RepeatForever())
            //  .Build();

            IJobDetail cpkjob = JobBuilder.Create<CpkJob>()
              .WithIdentity("myJob", "group1")
              .Build();
            ITrigger cpkTrigger = TriggerBuilder.Create()
               .WithIdentity("myTrigger", "group1")
               .WithCronSchedule("0 0 * * * ?")
               .ForJob("myJob", "group1")
               .Build();
            sched.ScheduleJob(cpkjob, cpkTrigger);



        }

        public static void LogExcute()
        {
            _program.LogResolver.CpkExcute(_program.LogResolver.CpkLogFromPath,
                _program.LogResolver.CpkLogToPath,
                _program.LogResolver.CpkTransfaseDataTable);
            _program.LogResolver.TestLogExcute(_program.LogResolver.TestLogFromPath,
                _program.LogResolver.TestLogToPath,
                _program.LogResolver.TestLogTransfaseDataTable);
        }
    }
}