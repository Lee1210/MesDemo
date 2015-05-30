using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using OSharp.Core.Data;


namespace OSharp.Demo.Models.Identity
{
    public class DelayTime:EntityBase<int>
    {
        [Required, StringLength(20)]
        public string Line { get; set; }

        [Required, StringLength(20)]
        public string Duty { get; set; }

        [Required]
        public float Hour { get; set; }

        [Required]
        public int WorkDate { get; set; }

        public bool IsPassed { get; set; }

      
    }
}
