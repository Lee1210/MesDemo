using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace WindowsServiceTest
{
	static class Program
	{
        public static CpkDemo.Program CpkProgram { get; set; }
		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		static void Main()
		{
            CpkDemo.Program.Infinal();
            CpkProgram = CpkDemo.Program._program;
            ServiceBase[] ServicesToRun;
			ServicesToRun = new ServiceBase[] 
			{ 
				new ServiceHr() 
			};
			ServiceBase.Run(ServicesToRun);
		}
	}
}
