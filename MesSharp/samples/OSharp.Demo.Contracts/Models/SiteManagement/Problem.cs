using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Mes.Core.Data;


namespace Mes.Demo.Models.SiteManagement
{
    public enum Department
    {
        [Description("部门1")]
        Description1 = 1,
        [Description("部门2")]
        Description2 = 2,
        [Description("部门3")]
        Description3 = 3,
        [Description("部门4")]
        Description4 = 4

    }


    public enum Factory
    {
        [Description("工厂1")]
        Factory1 = 1,
        [Description("工厂2")]
        Factory2 = 2,
        [Description("工厂3")]
        Factory3 = 3,
        [Description("工厂4")]
        Factory4 = 4
    }

    public enum QuestionFrom
    {
        [Description("来源1")]
        QuestionFrom1 = 1,
        [Description("来源2")]
        QuestionFrom2 = 2,
        [Description("来源3")]
        QuestionFrom3 = 3,
        [Description("来源4")]
        QuestionFrom4 = 4
    }

    public enum ProblemType
    {
        [Description("类型1")]
        ProblemType1 = 1,
        [Description("类型2")]
        ProblemType2 = 2,
        [Description("类型3")]
        ProblemType3 = 3,
        [Description("类型4")]
        ProblemType4 = 4
    }
    public class Problem: EntityBase<int>
    {
        [DataType(DataType.Date)]
        public DateTime ProblemTime { get; set; }

        [Display(Name = @"提问反馈部门"),StringLength(40)]
        public string Department { get; set; }

        [Display(Name = @"工厂"), StringLength(40)]
        public string Factory { get; set; }

        [Display(Name = @"问题来源"), StringLength(100)]
        public string QuestionFrom { get; set; }

        [Display(Name = @"异常内容")]
        [Required,StringLength(200)]
        public string Detail { get; set; }

        [Display(Name = @"分析原因")]
        [Required, StringLength(200)]
        public string Reason { get; set; }

        [Display(Name = @"解决方法")]
        [Required, StringLength(200)]
        public string Solution { get; set; }

        [Display(Name = @"是否解决")]
        public bool IsComplete { get; set; }

        [Display(Name = @"是否人为")]
        public bool IsPeople { get; set; }

        [Display(Name = @"解决人")]
        public string Workers { get; set; }

        [Display(Name = @"建议")]
        [StringLength(200)]
        public string Suggestion { get; set; }

        [Display(Name = @"备注")]
        [StringLength(200)]
        public string Remark { get; set; }

        [Display(Name = @"异常类型"), StringLength(40)]
        public string Type { get; set; }
    }
}
