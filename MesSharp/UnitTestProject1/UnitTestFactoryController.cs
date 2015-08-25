using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mes.Demo.Contracts.SiteManagement;
using Mes.Demo.Dtos.SiteManagement;
using Mes.Demo.Models.SiteManagement;
using Mes.Demo.Web.Areas.Admin.Controllers;
using Mes.Demo.Web.Areas.Admin.Controllers.SiteManagement;
using Mes.Utility.Data;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using NSubstitute;


namespace UnitTestProject1
{
    [TestClass]
    public class UnitTestFactoryController
    {
        [TestMethod]
        public void Test_FactoryController_AddFactory()
        {
            List<FactoryDto> factoryDtos = new List<FactoryDto>
            {
                new FactoryDto { Id = 1, Text = "fac1", Value = "fac1" },
                new FactoryDto { Id = 2, Text = "fac2", Value = "fac2" },
            };
            OperationResult operationResult=new OperationResult(OperationResultType.Success);
            var factoryController = Substitute.For<FactoryController>();
            var siteManagementContract = Substitute.For<ISiteManagementContract>();
            siteManagementContract.AddFactorys(Arg.Any<FactoryDto[]>()).Returns(operationResult);
            factoryController.SiteManagementContract = siteManagementContract;
           
            Assert.AreEqual(factoryController.Add(factoryDtos),null);
        }
    }
}
