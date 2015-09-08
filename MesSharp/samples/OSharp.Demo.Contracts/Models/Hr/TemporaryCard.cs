using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mes.Core.Data;


namespace Mes.Demo.Models.Hr
{
    public class TemporaryCard : EntityBase<int>, IAddDto
    {
        public string Card { get; set; }
        public string EmpNo { get; set; }
        public string EmpName { get; set; }
    }
}
