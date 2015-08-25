using Mes.Core.Data;


namespace Mes.Demo.Models.TestLog
{
    public class OperationLog : EntityBase<int>
    {
        public int Num { get; set; }
        public double MoveTimes { get; set; }
        public double ReadTimes { get; set; }
        public double ZipTimes { get; set; }
        public double DeleteTimes { get; set; }
        public double TotalTimes { get; set; }

        public string OperationType { get; set; }
    }
}
