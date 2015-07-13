using System;
using System.Collections.Generic;
using System.Linq;

using Mes.Core.Data;
using Mes.Demo.Models.Identity;
using Mes.Demo.Services;
using Mes.Utility.Data;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using NSubstitute;


namespace UnitTestProject1
{
    [TestClass]
    public class UnitTestIdentityService
    {
        private IUnitOfWork _unitOfWork;
        private IdentityService _identityService;
        private IRepository<User, int> _userRepository;
        private IRepository<Role, int> _roleRepository;
        private List<User> _users;
        private List<Role> _roles;
        
        [TestInitialize]
        public void Initialize()
        {
            //Initialize_data
            User user1 = new User() { Id = 1, Email = "123", CreatedTime = DateTime.Now, Name = "user1", NickName = "梁贵", Password = "123", };
            User user2 = new User() { Id = 2, Email = "123", CreatedTime = DateTime.Now, Name = "user2", NickName = "梁贵2", Password = "123", };
            _users = new List<User> { user1, user2 };

            Role role1 = new Role() { Id = 1, Name = "role1", Remark = "role1" };
            Role role2 = new Role() { Id = 2, Name = "role2", Remark = "role2" };
            _roles = new List<Role> { role1, role2 };
            user1.Roles = _roles;

            //Initialize_interface
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _identityService = Substitute.For<IdentityService>(_unitOfWork);
            _userRepository = Substitute.For<IRepository<User, int>>();
            _roleRepository = Substitute.For<IRepository<Role, int>>();
            _userRepository.GetByKey(Arg.Any<int>()).ReturnsForAnyArgs(x => _users.Find(r => r.Id == (int)x[0]));
            _userRepository.Entities.Returns(_users.AsQueryable());
            _roleRepository.Entities.Returns(_roles.AsQueryable());
            _roleRepository.GetByKey(Arg.Any<int>()).Returns(x => _roles.Find(r => r.Id == (int)x[0]));
            _identityService.UserRepository.Returns(_userRepository);
            _identityService.RoleRepository.Returns(_roleRepository);
        }

        //{1,2}-{1}
        [TestMethod]
        public void Test_IdentityService_SetUserRoles_设置成功()
        {
            OperationResult operationResult = _identityService.SetUserRoles(1, new[] { 1 });
            _userRepository.Received().GetByKey(1);
            _roleRepository.Received().GetByKey(2);
            Assert.AreEqual(operationResult.ResultType, OperationResultType.Success);
            Assert.AreEqual(((User)operationResult.Data).Roles.First(), _roles[0]);
            
        }

        //{1,2}-{1,2}
        [TestMethod]
        public void Test_IdentityService_SetUserRoles_去掉全部()
        {
            OperationResult operationResult = _identityService.SetUserRoles(1, null);
            _userRepository.Received().GetByKey(1);
            _roleRepository.Received().GetByKey(1);
            _roleRepository.Received().GetByKey(2);
            Assert.AreEqual(operationResult.ResultType, OperationResultType.Success);
            Assert.AreEqual(((User)operationResult.Data).Roles.Count, 0);
        }

        //null+{1,2}
        [TestMethod]
        public void Test_IdentityService_SetUserRoles_添加全部()
        {
            OperationResult operationResult = _identityService.SetUserRoles(2, new[] { 1,2 });
            _userRepository.Received().GetByKey(2);
            _roleRepository.Received().GetByKey(1);
            _roleRepository.Received().GetByKey(2);
            Assert.AreEqual(operationResult.ResultType, OperationResultType.Success);
            Assert.AreEqual(((User)operationResult.Data).Roles.Count, 2);
        }

        //null+{2}
        [TestMethod]
        public void Test_IdentityService_SetUserRoles_添加一部分()
        {
            OperationResult operationResult = _identityService.SetUserRoles(2, new[] { 2 });
            _userRepository.Received().GetByKey(2);
            _roleRepository.Received().GetByKey(2);
            Assert.AreEqual(operationResult.ResultType, OperationResultType.Success);
            Assert.AreEqual(((User)operationResult.Data).Roles.Count, 1);
        }

        [TestMethod]
        public void Test_IdentityService_Login_登录成功()
        {
            OperationResult operationResult = _identityService.Login("user1", "123");
            Assert.AreEqual(operationResult.ResultType, OperationResultType.Success);
        }

        [TestMethod]
        public void Test_IdentityService_Login_2个账号相同()
        {
            _users.First().Name = "user2";
            OperationResult operationResult = _identityService.Login("user2", "123");
            Assert.AreEqual(operationResult.ResultType, OperationResultType.Success);
        }

        [TestMethod]
        public void Test_IdentityService_Login_用户不存在()
        {
            OperationResult operationResult = _identityService.Login("user3", "123");
            Assert.AreEqual(operationResult.ResultType, OperationResultType.Error);
            Assert.AreEqual(operationResult.Message, "用户不存在");
            
        }

        [TestMethod]
        public void Test_IdentityService_Login_密码错误()
        {
            OperationResult operationResult = _identityService.Login("user1", "1234");
            Assert.AreEqual(operationResult.ResultType, OperationResultType.Error);
            Assert.AreEqual(operationResult.Message, "密码错误");
            
        }

        [TestMethod]
        public void Test_IdentityService_Login_账号密码为空()
        {
            OperationResult operationResult = _identityService.Login(null, null);
            Assert.AreEqual(operationResult.ResultType, OperationResultType.Error);
            Assert.AreEqual(operationResult.Message, "账号密码不能为空");

        }

       
    }
}
