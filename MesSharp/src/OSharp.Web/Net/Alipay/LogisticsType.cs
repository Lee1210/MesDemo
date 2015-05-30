

// ReSharper disable InconsistentNaming
namespace Mes.Web.Net.Alipay
{
    /// <summary>
    /// 表示物流类型的枚举
    /// </summary>
    public enum LogisticsType
    {
        /// <summary>
        /// 平邮
        /// </summary>
        POST = 0,

        /// <summary>
        /// 快递
        /// </summary>
        EXPRESS = 1,

        /// <summary>
        /// EMS
        /// </summary>
        EMS = 2,

        /// <summary>
        /// 无需物流，在发货时使用
        /// </summary>
        DIRECT = 3
    }
}