using System;
using System.Collections.Generic;
using System.Linq;

using Mes.Core;
using Mes.Core.Data;
using Mes.Demo.Contracts.SiteManagement;
using Mes.Demo.Models.Identity;
using Mes.Demo.Models.SiteManagement;
using Mes.Demo.Services;
using Mes.Demo.Services.Test;
using Mes.Utility.Data;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using NSubstitute;


namespace UnitTestService
{
    [TestClass]
    public class UnitTestSiteManagementService
    {
        [TestMethod]
        public void TestMethod1()
        {
            Factory factory1 = new Factory() { Text = "龙旗一厂", Value = "龙旗一厂" };
            Factory factory2 = new Factory() { Text = "龙旗二厂", Value = "龙旗二厂" };
            Factory factory3 = new Factory() { Text = "深圳振华", Value = "深圳振华" };
            List<Factory> factorys = new List<Factory> { factory1, factory2, factory3 };
            IUnitOfWork unitOfWork = Substitute.For<IUnitOfWork>();
            var siteManagementService = Substitute.For<ISiteManagementContract, ServiceBase>(unitOfWork);
            siteManagementService.Factorys.Returns(factorys.AsQueryable());
            Assert.AreEqual(siteManagementService.Factorys.Count(), 3);
        }

        [TestMethod]
        public void Test_SiteManagementService_AddFactorys()
        {
            IUnitOfWork unitOfWork = Substitute.For<IUnitOfWork>();
            var siteManagementService = Substitute.For<SiteManagementService>(unitOfWork);
            var factoryRepository = Substitute.For<IRepository<Factory, int>>();
            FactoryDto[] dtos = new FactoryDto[3];
            OperationResult operationResult = new OperationResult(OperationResultType.Success);
            factoryRepository.Insert(dtos).ReturnsForAnyArgs(operationResult);
            siteManagementService.FactoryRepository.Returns(factoryRepository);
            Assert.AreEqual(siteManagementService.AddFactorys(dtos), operationResult);
        }

        [TestMethod]
        public void Test_IdentityService_SetUserRoles()
        {
            User user1 = new User() {Id=1, Email = "123", CreatedTime = DateTime.Now, Name = "user1", NickName = "梁贵", Password = "123", };
            User user2 = new User() { Id = 2, Email = "123", CreatedTime = DateTime.Now, Name = "user2", NickName = "梁贵2", Password = "123", };
            List<User> users = new List<User> { user1, user2 };
            
            Role role1 = new Role() { Id = 1, Name = "role1", Remark = "role1" };
            Role role2 = new Role() { Id = 2, Name = "role2", Remark = "role2" };
            List<Role> roles = new List<Role> { role1, role2 };

            user1.Roles = roles;
            role1.Users = users;
            IUnitOfWork unitOfWork = Substitute.For<IUnitOfWork>();
            var identityService = Substitute.For<IdentityService>(unitOfWork);
            var userRepository = Substitute.For<IRepository<User, int>>();
            var roleRepository = Substitute.For<IRepository<Role, int>>();
            OperationResult operationResult = new OperationResult(OperationResultType.Success);
            userRepository.GetByKey(Arg.Any<int>()).ReturnsForAnyArgs(x => users.Find(r => r.Id == (int)x[0]));
            roleRepository.GetByKey(Arg.Any<int>()).Returns(x => roles.Find(r => r.Id == (int)x[0]));
            identityService.UserRepository.Returns(userRepository);
            identityService.RoleRepository.Returns(roleRepository);
            Assert.AreEqual(identityService.SetUserRoles(2, new[] { 1 }).ResultType, OperationResultType.Success);
        }
    }
}
