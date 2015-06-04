

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

using Mes.Core.Data;


namespace Mes.Demo.Models.Identity
{
    /// <summary>
    /// 实体类——组织机构信息
    /// </summary>
    public class Menu : EntityBase<int>
    {
        /// <summary>
        /// 初始化一个<see cref="Menu"/>类型的新实例
        /// </summary>
        public Menu()
        {
            Children = new List<Menu>();
            Roles = new List<Role>();
        }

        /// <summary>
        /// 获取或设置 名称
        /// </summary>
        [Required, StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 描述
        /// </summary>
        [StringLength(500)]
        public string Remark { get; set; }

        /// <summary>
        /// 获取或设置 排序码
        /// </summary>
        [Range(0, 999)]
        public int SortCode { get; set; }

        /// <summary>
        /// 获取或设置 树形路径编号数组
        /// </summary>
        [NotMapped]
        public int[] TreePathIds { get { return TreePath.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray(); } }

        /// <summary>
        /// 获取或设置 树形路径，树链的Id以逗号分隔构成的字符串
        /// </summary>
        public string TreePath { get; set; }

        /// <summary>
        /// 获取或设置 父级组织机构信息
        /// </summary>
        public virtual Menu Parent { get; set; }

        /// <summary>
        /// 获取或设置 子级组织机构信息集合
        /// </summary>
        public virtual ICollection<Menu> Children { get; set; }

        /// <summary>
        /// 获取或设置 角色信息集合
        /// </summary>
        public virtual ICollection<Role> Roles { get; set; }
    }
}