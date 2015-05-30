using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OSharp.Demo.Web.Dtos
{
    public class ResDto
    {

        /// <summary>
        /// 获取或设置 主键，唯一标识
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 获取或设置 角色名称
        /// </summary>
        [Required, StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 角色名称
        /// </summary>
        [Required, StringLength(500)]
        public string Remark { get; set; }
    }
}