using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Mes.Core.Data;


namespace Mes.Demo.Models.Test
{
    [Description("站别")]
    public class Station : EntityBase<int>
    {
        /// <summary>
        /// 初始化一个<see cref="Station"/>类型的新实例
        /// </summary>
        public Station()
        {
            Lines = new List<Line>();
        }
        /// <summary>
        /// 获取或设置 角色名称
        /// </summary>
        [Required, StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 角色备注
        /// </summary>
        [StringLength(500)]
        public string Remark { get; set; }

        /// <summary>
        /// 获取或设置 是否是管理员
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// 获取或设置 是否锁定
        /// </summary>
        public bool IsLocked { get; set; }

        public ICollection<Line> Lines { get; set; } 
    }
}
