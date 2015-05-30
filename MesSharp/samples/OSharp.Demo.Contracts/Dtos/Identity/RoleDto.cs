

using System.ComponentModel.DataAnnotations;

using Mes.Core.Data;


namespace Mes.Demo.Dtos.Identity
{
    public class RoleDto : IAddDto, IEditDto<int>
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
        /// 获取或设置 角色描述
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

        /// <summary>
        /// 获取或设置 组织机构编号
        /// </summary>
        public int OrganizationId { get; set; }
    }
}