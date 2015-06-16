using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Util;
using Util.Logs;
using Enum = Util.Enum;


namespace UtilTest
{
    [TestClass]
    public class EnumTest
    {
        #region 常量

        /// <summary>
        /// LogLevel.Debug实例
        /// </summary>
        public const LogLevel DebugInstance = LogLevel.Debug;

        /// <summary>
        /// LogLevel.Debug的名称：Debug
        /// </summary>
        public const string DebugName = "Debug";

        /// <summary>
        /// LogLevel.Debug的值：5
        /// </summary>
        public const int DebugValue = 5;

        /// <summary>
        /// LogLevel.Debug的描述："调试"
        /// </summary>
        public const string DebugDescription = "调试";

        #endregion

        #region GetInstance(获取实例)
        /// <summary>
        /// 1. 功能：获取实例,
        /// 2. 场景：参数为null
        /// 3. 预期：抛出异常
        ///</summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestGetInstance_ArgumentIsNull_Throw()
        {
            try
            {
                Enum.GetInstance<LogLevel>(null);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.Message.Contains("member"));
                throw;
            }
        }

         /// <summary>
        /// 1. 功能：获取实例,
        /// 2. 场景：参数为空字符串
        /// 3. 预期：抛出异常
        ///</summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentNullException ) )]
        public void TestGetInstance_ArgumentIsEmpty_Throw() {
            try {
                Enum.GetInstance<LogLevel>( string.Empty );
            }
            catch ( ArgumentNullException ex ) {
                Assert.IsTrue( ex.Message.Contains( "member" ) );
                throw;
            }
        }

        /// <summary>
        /// 通过成员名获取实例
        ///</summary>
        [TestMethod]
        public void GetInstance_Name() {
            Assert.AreEqual( DebugInstance, Enum.GetInstance<LogLevel>( DebugName ) );
        }

        /// <summary>
        /// 通过成员值获取实例
        /// </summary>
        [TestMethod]
        public void GetInstance_Value() {
            Assert.AreEqual( DebugInstance, Enum.GetInstance<LogLevel>( DebugValue ) );
        }

        /// <summary>
        ///通过成员名获取可空枚举实例
        ///</summary>
        [TestMethod]
        public void GetInstance_Name_Nullable() {
            Assert.AreEqual( DebugInstance, Enum.GetInstance<LogLevel?>( DebugName ) );
        }

        /// <summary>
        /// 通过成员值获取可空枚举实例
        /// </summary>
        [TestMethod]
        public void GetInstance_Value_Nullable() {
            Assert.AreEqual( DebugInstance, Enum.GetInstance<LogLevel?>( DebugValue ) );
        }

        #endregion

        #region GetName(获取成员名)

        /// <summary>
        /// 1. 功能：获取成员名,
        /// 2. 场景：参数为空，
        /// 3. 预期：返回空字符串
        ///</summary>
        [TestMethod]
        public void GetName_ArgumentIsNull_ReturnEmpty() {
            Assert.AreEqual( string.Empty, Enum.GetName<LogLevel>( null ) );
        }

        /// <summary>
        ///通过成员名获取成员名
        ///</summary>
        [TestMethod]
        public void GetName_Name() {
            Assert.AreEqual( DebugName, Enum.GetName<LogLevel>( DebugName ) );
        }

        /// <summary>
        ///通过成员值获取成员名
        ///</summary>
        [TestMethod]
        public void GetName_Value() {
            Assert.AreEqual( DebugName, Enum.GetName<LogLevel>( DebugValue ) );
        }

        /// <summary>
        ///通过实例获取成员名
        ///</summary>
        [TestMethod]
        public void GetName_Instance() {
            Assert.AreEqual( DebugName, Enum.GetName<LogLevel>( DebugInstance ) );
        }

        /// <summary>
        ///通过成员值获取可空枚举成员名
        ///</summary>
        [TestMethod]
        public void GetName_Value_Nullable() {
            Assert.AreEqual( DebugName, Enum.GetName<LogLevel?>( DebugValue ) );
        }

        #endregion

        #region GetValue(获取成员值)

        /// <summary>
        /// 1. 功能：获取成员值,
        /// 2. 场景：参数为null
        /// 3. 预期：抛出异常
        ///</summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentNullException ) )]
        public void GetValue_ArgumentIsNull_Throw() {
            try {
                Enum.GetValue<LogLevel>( null );
            }
            catch ( ArgumentNullException ex ) {
                Assert.IsTrue( ex.Message.Contains( "member" ) );
                throw;
            }
        }

        /// <summary>
        /// 1. 功能：获取成员值,
        /// 2. 场景：参数为空字符串，
        /// 3. 预期：抛出异常
        ///</summary>
        [TestMethod]
        [ExpectedException( typeof( ArgumentNullException ) )]
        public void GetValue_ArgumentIsEmpty_Throw() {
            try {
                Enum.GetValue<LogLevel>( string.Empty );
            }
            catch ( ArgumentNullException ex ) {
                Assert.IsTrue( ex.Message.Contains( "member" ) );
                throw;
            }
        }

        /// <summary>
        ///通过成员名获取成员值
        ///</summary>
        [TestMethod]
        public void GetValue_Name() {
            int actual = Enum.GetValue<LogLevel>( DebugName );
            Assert.AreEqual( DebugValue, actual );
        }

        /// <summary>
        ///通过成员值获取成员值
        ///</summary>
        [TestMethod]
        public void GetValue_Value() {
            int actual = Enum.GetValue<LogLevel>( DebugValue );
            Assert.AreEqual( DebugValue, actual );
        }

        /// <summary>
        ///通过实例获取成员值
        ///</summary>
        [TestMethod]
        public void GetValue_Instance() {
            int actual = Enum.GetValue<LogLevel>( DebugInstance );
            Assert.AreEqual( DebugValue, actual );
        }

        /// <summary>
        ///通过成员名获取可空枚举成员值
        ///</summary>
        [TestMethod]
        public void GetValue_Name_Nullable() {
            int actual = Enum.GetValue<LogLevel?>( DebugName );
            Assert.AreEqual( DebugValue, actual );
        }

        /// <summary>
        ///通过成员值获取可空枚举成员值
        ///</summary>
        [TestMethod]
        public void GetValue_Value_Nullable() {
            int actual = Enum.GetValue<LogLevel?>( DebugValue );
            Assert.AreEqual( DebugValue, actual );
        }

        /// <summary>
        ///通过实例获取可空枚举成员值
        ///</summary>
        [TestMethod]
        public void GetValue_Instance_Nullable() {
            int actual = Enum.GetValue<LogLevel?>( DebugInstance );
            Assert.AreEqual( DebugValue, actual );
        }

        #endregion

        #region GetDescription(获取描述)

        /// <summary>
        /// 1. 功能：获取描述,
        /// 2. 场景：参数为空，
        /// 3. 预期：返回空字符串
        ///</summary>
        [TestMethod]
        public void GetDescription_ArgumentIsNull_ReturnEmpty() {
            Assert.AreEqual( string.Empty, Enum.GetDescription<LogLevel>( null ) );
        }

        /// <summary>
        /// 1. 功能：获取描述,
        /// 2. 场景：参数为空字符串，
        /// 3. 预期：返回空字符串
        ///</summary>
        [TestMethod]
        public void GetDescription_ArgumentIsEmpty_ReturnEmpty() {
            Assert.AreEqual( string.Empty, Enum.GetDescription<LogLevel>( string.Empty ) );
        }

        /// <summary>
        ///通过成员名获取描述
        ///</summary>
        [TestMethod]
        public void GetDescription_Name() {
            Assert.AreEqual( DebugDescription, Enum.GetDescription<LogLevel>( DebugName ) );
        }

        /// <summary>
        ///通过成员值获取描述
        ///</summary>
        [TestMethod]
        public void GetDescription_Value() {
            Assert.AreEqual( DebugDescription, Enum.GetDescription<LogLevel>( DebugValue ) );
        }

        /// <summary>
        ///通过实例获取描述
        ///</summary>
        [TestMethod]
        public void GetDescription_Instance() {
            Assert.AreEqual( DebugDescription, Enum.GetDescription<LogLevel>( DebugInstance ) );
        }

        /// <summary>
        ///通过成员名获取可空枚举描述
        ///</summary>
        [TestMethod]
        public void GetDescription_Name_Nullable() {
            Assert.AreEqual( DebugDescription, Enum.GetDescription<LogLevel?>( DebugName ) );
        }

        /// <summary>
        /// 1. 功能：获取描述,
        /// 2. 场景：无效成员名，
        /// 3. 预期：返回空字符串
        ///</summary>
        [TestMethod]
        public void GetDescription_InvalidName_ReturnEmpty() {
            Assert.AreEqual( string.Empty, Enum.GetDescription<LogLevel>( "Debug1" ) );
        }

        /// <summary>
        /// 1. 功能：获取描述,
        /// 2. 场景：无效成员值，
        /// 3. 预期：返回空字符串
        ///</summary>
        [TestMethod]
        public void GetDescription_InvalidValue_ReturnEmpty() {
            Assert.AreEqual( string.Empty, Enum.GetDescription<LogLevel>( 100 ) );
        }

        /// <summary>
        /// 1. 功能：获取描述,
        /// 2. 场景：无效枚举，
        /// 3. 预期：返回空字符串
        ///</summary>
        [TestMethod]
        public void GetDescription_InvalidEnum_ReturnEmpty() {
            Assert.AreEqual( string.Empty, Enum.GetDescription<string>( DebugValue ) );
        }

        #endregion

        #region GetItems(获取描述项集合)

        /// <summary>
        /// 获取描述项集合
        /// </summary>
        [TestMethod]
        public void GetItems_Success() {
            var items = Enum.GetItems<LogLevel>();
            Assert.AreEqual( 5, items.Count );
            Assert.AreEqual("致命错误", items[0].Text);
            Assert.AreEqual( "1", items[0].Value );
            Assert.AreEqual("信息", items[3].Text);
            Assert.AreEqual( "4", items[3].Value );
            Assert.AreEqual("调试", items[4].Text);
            Assert.AreEqual( "5", items[4].Value );
        }

      
        #endregion GetItems
    }
}
