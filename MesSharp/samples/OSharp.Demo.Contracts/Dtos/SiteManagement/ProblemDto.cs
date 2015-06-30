using System;
using System.ComponentModel.DataAnnotations;

using Mes.Core.Data;
using Mes.Demo.Models.SiteManagement;


namespace Mes.Demo.Dtos.SiteManagement
{
    
    public class ProblemDto:  IAddDto, IEditDto<int>
    {
        [DataType(DataType.Date)]
        public DateTime ProblemTime { get; set; }

        [Display(Name = @"提问反馈部门"), StringLength(40)]
        public string Department { get; set; }

        [Display(Name = @"工厂"), StringLength(40)]
        public string Factory { get; set; }

        [Display(Name = @"问题来源"), StringLength(100)]
        public string QuestionFrom { get; set; }

        [Display(Name = @"异常内容")]
        [Required,StringLength(1000)]
        public string Detail { get; set; }

        [Display(Name = @"分析原因")]
        [Required, StringLength(1000)]
        public string Reason { get; set; }

        [Display(Name = @"解决方法")]
        [Required, StringLength(1000)]
        public string Solution { get; set; }

        [Display(Name = @"是否解决")]
        public bool IsComplete { get; set; }

        [Display(Name = @"是否人为")]
        public bool IsPeople { get; set; }

        [Display(Name = @"解决人")]
        public string Workers { get; set; }

        [Display(Name = @"建议")]
        [StringLength(1000)]
        public string Suggestion { get; set; }

        [Display(Name = @"备注")]
        [StringLength(1000)]
        public string Remark { get; set; }

        [Display(Name = @"异常类型"), StringLength(40)]
        public string Type { get; set; }

        #region Implementation of IEditDto<int>

        /// <summary>
        /// 获取或设置 主键，唯一标识
        /// </summary>
        public int Id { get; set; }

        #endregion
    }
}
