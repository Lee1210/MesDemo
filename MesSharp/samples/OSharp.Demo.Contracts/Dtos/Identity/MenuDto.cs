

using System.ComponentModel.DataAnnotations;

using Mes.Core.Data;


namespace Mes.Demo.Dtos.Identity
{
    public class MenuDto : IAddDto, IEditDto<int>
    {
        /// <summary>
        /// 获取或设置 主键，唯一标识
        /// </summary>
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Remark { get; set; }

        [Range(0, 999)]
        public int SortCode { get; set; }

        public int? ParentId { get; set; }

        [StringLength(50)]
        public string ActionName { get; set; }
      
        public string TreePath { get; set; }
    }
}