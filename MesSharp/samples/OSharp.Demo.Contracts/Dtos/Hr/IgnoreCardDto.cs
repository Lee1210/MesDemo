using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mes.Core.Data;


namespace Mes.Demo.Models.Hr
{
    public class IgnoreCardDto : IAddDto, IEditDto<int>
    {
        public int Id { get; set; }
        public string EmpNo { get; set; }
     
    }
}
