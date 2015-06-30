using System.ComponentModel;

namespace Util.Logs {
    /// <summary>
    /// 日志级别
    /// </summary>
    public enum LogLevel {
        /// <summary>
        /// 致命错误
        /// </summary>
        [Description( "致命错误" )]
        Fatal=1,
        /// <summary>
        /// 错误
        /// </summary>
        [Description( "错误" )]
        Error=2,
        /// <summary>
        /// 警告
        /// </summary>
        [Description( "警告" )]
        Warning=3,
        /// <summary>
        /// 信息
        /// </summary>
        [Description( "信息" )]
        Information=4,
        /// <summary>
        /// 调试
        /// </summary>
        [Description( "调试" )]
        Debug=5
    }
}
