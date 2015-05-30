using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.Core.Data;


namespace OSharp.Demo.Dtos.Identity
{
    public class DelayTimeDto : IAddDto, IEditDto<int>
    {
        #region Implementation of IEditDto<int>

        /// <summary>
        /// 获取或设置 主键，唯一标识
        /// </summary>
        public int Id { get; set; }

        #endregion

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
