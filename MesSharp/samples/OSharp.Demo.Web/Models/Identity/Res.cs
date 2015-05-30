using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

using OSharp.Core.Data;


namespace OSharp.Demo.Web.Models
{
    public class Res:EntityBase<int>
    {
        [Required, StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 角色备注
        /// </summary>
        [StringLength(500)]
        public string Remark { get; set; }
    }
}