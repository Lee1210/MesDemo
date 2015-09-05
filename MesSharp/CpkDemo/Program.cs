// -----------------------------------------------------------------------
//  <copyright file="Program.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>最后修改人</last-editor>
//  <last-date>2015-08-26 10:15</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

using FileClizz;

using Mes.Utility.Develop;
using Mes.Utility.Extensions;




namespace CpkDemo
{
   
    internal class Program
    {
      



        // ReSharper disable once UnusedParameter.Local
        private static void Main(string[] args)
        {
            LogResolver resolver=new LogResolver();
            FileInfo fileInfo=new FileInfo(@"C:\Users\lianggui\Desktop\临时\log格式\Main_MicrophoneAMPL_20150611.xls");
            resolver.TestLogTransfaseDataTable(fileInfo,null,null);
            resolver.CpkExcute(resolver.CpkLogFromPath, resolver.CpkLogToPath, resolver.CpkTransfaseDataTable,"Cpks");
            //  Console.WriteLine("Receiverampl".ToUpper().CastTo<LogFileType>().CastTo<int>());
           
        }
       
    }
}