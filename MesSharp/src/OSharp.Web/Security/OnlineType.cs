

using System;


namespace Mes.Web.Security
{
    /// <summary>
    /// 表示在线类型的枚举
    /// </summary>
    [Serializable]
    public enum OnlineType
    {
        /// <summary>
        /// 网站在线类型
        /// </summary>
        Site,

        /// <summary>
        /// 客户端在线类型
        /// </summary>
        Client,

        ///// <summary>
        ///// 移动在线类型
        ///// </summary>
        //App
    }
}