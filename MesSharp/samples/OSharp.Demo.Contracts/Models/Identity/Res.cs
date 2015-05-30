using System.ComponentModel.DataAnnotations;

using OSharp.Core.Data;


namespace OSharp.Demo.Models.Identity
{
    public class Res:EntityBase<int>
    {
        [Required,StringLength(50)]
        public string ResCode { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 角色备注
        /// </summary>
        [StringLength(500)]
        public string Remark { get; set; }
    }
}