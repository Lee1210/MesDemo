using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;

namespace WindowsServiceTest
{
    class HelloWorld:IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter("E:\\log.txt", true))
            {
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss "));
            }
        }
    }
}
