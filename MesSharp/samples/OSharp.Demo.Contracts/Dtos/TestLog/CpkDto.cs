using System;
using System.ComponentModel.DataAnnotations;

using Mes.Core.Data;
using Mes.Demo.Models.TestLog;


namespace Mes.Demo.Dtos.TestLog
{
    public class CpkDto:IAddDto, IEditDto<int>
    {
        public int Id { get; set; }

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


        public float TestVal { get; set; }

        public float MinVal { get; set; }

        public float MaxVal { get; set; }

        public DateTime ReadLogDate { get; set; }

        public int TestDate { get; set; }


        public string LogFileName { get; set; }

        public string ZipFileName { get; set; }
    }
}
