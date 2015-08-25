using System.ComponentModel.DataAnnotations;

using Mes.Core.Data;


namespace Mes.Demo.Dtos.SiteManagement
{
    public class ProblemSourceDto : IAddDto, IEditDto<int>
    {
        [Required,StringLength(200)]
        public string Text { get; set; }

        [Required, StringLength(200)]
        public string Value { get; set; }

        #region Implementation of IEditDto<int>

        /// <summary>
        /// 获取或设置 主键，唯一标识
        /// </summary>
        public int Id { get; set; }

        #endregion
    }
}
