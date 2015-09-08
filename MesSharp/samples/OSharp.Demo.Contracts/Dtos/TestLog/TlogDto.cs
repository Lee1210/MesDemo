using System;
using System.ComponentModel.DataAnnotations;

using Mes.Core.Data;
using Mes.Demo.Models.TestLog;


namespace Mes.Demo.Dtos.TestLog
{
    public class TlogDto:IAddDto, IEditDto<int>
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Sn { get; set; }
        public int TestDate { get; set; }

        public int TestTime { get; set; }

        public string LogFileName { get; set; }

        public string ZipFileName { get; set; }

        public string Path { get; set; }

        public LogFileType LogFileType { get; set; }

        public TestReslut TestReslut { get; set; }
    }
}
