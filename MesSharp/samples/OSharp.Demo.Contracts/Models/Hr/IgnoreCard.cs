using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mes.Core.Data;


namespace Mes.Demo.Models.Hr
{
    public class IgnoreCard : EntityBase<int>, IAddDto
    {
        public string EmpNo { get; set; }
   
    }
}
