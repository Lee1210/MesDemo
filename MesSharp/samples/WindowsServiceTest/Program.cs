using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

using Autofac;

using CpkDemo;
using CpkDemo.Menjin;

using Mes.Core.Caching;


namespace WindowsServiceTest
{
	static class Program
	{
        // ReSharper disable once NotAccessedField.Local
       
        //   public LogResolver LogResolver { get; set; }
       
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
          
            ServiceHr s = new ServiceHr();
            //if (Environment.UserInteractive)
            //{
            //    s.DebugStart();
            //    try
            //    {
            //        Startup.Start();
            //    }
            //    catch (Exception e)
            //    {

            //        using (var stream = new System.IO.StreamWriter("E:\\log.txt", true))
            //        {
            //            stream.WriteLine(DateTime.Now.ToString("hh:mm:ss"));
            //        }
            //    }
            //    Console.ReadKey();

            //    s.DebugStop();
            //}
            //Startup.Start();
            ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                    s
                };
                ServiceBase.Run(ServicesToRun);
            
        }
	}
}
