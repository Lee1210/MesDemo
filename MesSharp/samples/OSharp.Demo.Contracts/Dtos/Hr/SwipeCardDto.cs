using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mes.Core.Data;


namespace Mes.Demo.Models.Hr
{
    
    public class SwipeCardDto : IAddDto, IEditDto<int>

    {
        public int Id { get; set; }
        public long Card { get; set; }
        public string EmpNo { get; set; }
        public string EmpName { get; set; }
        public string BarCode { get; set; }
        public string DoorIo { get; set; }
        public DateTime SwipeTime { get; set; }
        public int SwipdeDate { get; set; }
    }
}
