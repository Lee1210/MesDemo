using System;
using System.ComponentModel.DataAnnotations;

using OSharp.Core.Data;


namespace OSharp.Demo.Dtos.Identity
{
    public class ResDto:IAddDto, IEditDto<int>
    {

        /// <summary>
        /// 获取或设置 主键，唯一标识
        /// </summary>
        public int Id { get; set; }

        [Required,StringLength(50)]
        public string ResCode { get; set; }
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

      
        public bool IsDeleted { get; set; }
    }
}