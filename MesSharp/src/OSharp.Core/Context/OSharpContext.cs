

using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;


namespace Mes.Core.Context
{
    /// <summary>
    /// Mes框架上下文，用于构造Mes框架运行环境
    /// </summary>
    [Serializable]
    public class MesContext : Dictionary<string, object>
    {
        private const string CallContextKey = "__Mes_CallContext";
        private const string OperatorKey = "__Mes_Context_Operator";
        private static readonly Lazy<MesContext> ContextLazy = new Lazy<MesContext>(() => new MesContext());

        /// <summary>
        /// 初始化一个<see cref="MesContext"/>类型的新实例
        /// </summary>
        public MesContext()
        { }

        /// <summary>
        /// 初始化一个<see cref="MesContext"/>类型的新实例
        /// </summary>
        protected MesContext(SerializationInfo info, StreamingContext context)
            :base(info, context)
        { }

        /// <summary>
        /// 获取或设置 当前上下文
        /// </summary>
        public static MesContext Current
        {
            get
            {
                MesContext context = CallContext.LogicalGetData(CallContextKey) as MesContext;
                if (context != null)
                {
                    return context;
                }
                context = ContextLazy.Value;
                CallContext.LogicalSetData(CallContextKey, context);
                return context;
            }
            set
            {
                if (value == null)
                {
                    CallContext.FreeNamedDataSlot(CallContextKey);
                    return;
                }
                CallContext.LogicalSetData(CallContextKey, value);
            }
        }

        /// <summary>
        /// 获取 当前操作者
        /// </summary>
        public Operator Operator
        {
            get
            {
                if (!ContainsKey(OperatorKey))
                {
                    this[OperatorKey] = new Operator();
                }
                return this[OperatorKey] as Operator;
            }
        }
    }
}