using System;
using System.Collections.Generic;
using System.Linq;

using Mes.Demo.Contracts.SiteManagement;
using Mes.Demo.Models.SiteManagement;
using Mes.Demo.Services.Test;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using NSubstitute;


namespace UnitTestService
{
    [TestClass]
    public class UnitTestService
    {
        [TestMethod]
        public void TestMethod1()
        {
            var siteManagementService=Substitute.For<ISiteManagementContract>();
            
        }
    }
}
