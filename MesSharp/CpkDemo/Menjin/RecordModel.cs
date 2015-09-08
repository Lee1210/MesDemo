using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace menjin
{
    public class RecordModel
    {
        public string Card { get; set; }
        public DateTime SwipeTime { get; set; }
        public int SwipeDate { get; set; }
        public Int64 Status { get; set; }
        public string Cmd { get; set; }

        public string EmpNo { get; set; }
        public string EmpName { get; set; }
    }
}
