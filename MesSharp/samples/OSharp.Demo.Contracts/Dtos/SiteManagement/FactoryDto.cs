using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mes.Core.Data;


namespace Mes.Demo.Models.SiteManagement
{
    public class FactoryDto : IAddDto, IEditDto<int>
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
