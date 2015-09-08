using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Mes.Core.Data;


namespace Mes.Demo.Models.TestLog
{
    public class Tlog : EntityBase<int>, IAddDto
    {
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