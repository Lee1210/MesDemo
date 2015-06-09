

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Mes.Core.Data;
using Mes.Utility.DataAnnotations;


namespace Mes.Demo.Dtos.Identity
{
    public class UserDto : IAddDto, IEditDto<int>
    {
        /// <summary>
        /// 获取或设置 主键，唯一标识
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 获取或设置 唯一用户名
        /// </summary>
        [Required, StringLength(100),Description("用户名")]
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 密码
        /// </summary>
        [StringLength(100), Password(RequiredDigit = false,RequiredLowercase=false, RequiredLength = 3),Description("密码")]
        public string Password { get; set; }

        /// <summary>
        /// 获取或设置 电子邮箱
        /// </summary>
        [Required, StringLength(100)]
        public string Email { get; set; }

        /// <summary>
        /// 获取或设置 用户昵称
        /// </summary>
        [StringLength(50)]
        public string NickName { get; set; }

        /// <summary>
        /// 获取或设置 是否锁定
        /// </summary>
        public bool IsLocked { get; set; }

        /// <summary>
        /// 注册IP地址
        /// </summary>
        [StringLength(15)]
        public string RegistedIp { get; set; }

    }
}