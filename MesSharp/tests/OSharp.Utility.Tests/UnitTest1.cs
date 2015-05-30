using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Mes.Utility.IO;


namespace Mes.Utility.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Type type = typeof(List<int>);
            if (type.IsGenericType)
            {
                Type[] ts = type.GetGenericArguments();
                Assert.IsTrue(ts.Length == 1);
            }
        }
    }
}
