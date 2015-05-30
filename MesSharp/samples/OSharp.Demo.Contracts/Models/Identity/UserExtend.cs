

using System.ComponentModel.DataAnnotations;

using Mes.Core.Data;


namespace Mes.Demo.Models.Identity
{
    /// <summary>
    /// 实体类——用户扩展信息
    /// </summary>
    public class UserExtend : EntityBase<int>
    {
        /// <summary>
        /// 注册IP地址
        /// </summary>
        [StringLength(15)]
        public string RegistedIp { get; set; }

        public virtual User User { get; set; }
    }
}