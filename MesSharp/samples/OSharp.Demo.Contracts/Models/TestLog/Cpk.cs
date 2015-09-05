using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Mes.Core.Data;


namespace Mes.Demo.Models.TestLog
{
    public class Cpk : EntityBase<int>, IAddDto
    {
        [StringLength(50)]
        public string PlatForm { get; set; }
        [StringLength(50)]

        public string ProjectName { get; set; }
        [StringLength(50)]

        public string Opcode { get; set; }
        [StringLength(50)]

        public string Ip { get; set; }
        [StringLength(50)]

        public string Sn { get; set; }
        [StringLength(50)]

        public string TestTime { get; set; }

        public TestReslut Result { get; set; }
        [StringLength(50)]

        public string Wgsn { get; set; }
        [StringLength(50)]

        public string Tch { get; set; }
        [StringLength(50)]

        public string Pcl { get; set; }
        [StringLength(50)]

        public string TestItem { get; set; }
        

        public double TestVal { get; set; }

        public double MinVal { get; set; }

        public double MaxVal { get; set; }

        public DateTime ReadLogDate { get; set; }

        public int TestDate { get; set; }
        

        public string LogFileName { get; set; }

        public string ZipFileName { get; set; }

     
    }
}