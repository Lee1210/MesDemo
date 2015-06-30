using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mes.Core.Data;


namespace Mes.Demo.Models.SiteManagement
{
    public class ProblemSource : EntityBase<int>
    {
        [Required,StringLength(200)]
        public string Text { get; set; }

        [Required, StringLength(200)]
        public string Value { get; set; }
    }
}
