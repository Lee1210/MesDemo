using Mes.Core.Data;
using Mes.Demo.Models.SiteManagement;
using Mes.Demo.Services.Test;
using Mes.Utility.Data;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using NSubstitute;


namespace UnitTestService
{
    [TestClass]
    public class UnitTestSiteManagementService
    {
        private IUnitOfWork _unitOfWork ;
        private SiteManagementService _siteManagementService;
        private IRepository<Factory, int> _factoryRepository;
       
        [TestInitialize]
        public void Initialize()
        {
             _unitOfWork = Substitute.For<IUnitOfWork>();
             _siteManagementService = Substitute.For<SiteManagementService>(_unitOfWork);
             _factoryRepository = Substitute.For<IRepository<Factory, int>>();
        }
 
        [TestMethod]
        public void Test_SiteManagementService_AddFactorys()
        {
            FactoryDto[] dtos = new FactoryDto[3];
            OperationResult operationResult = new OperationResult(OperationResultType.Success);
            _factoryRepository.Insert(dtos).ReturnsForAnyArgs(operationResult);
            _siteManagementService.FactoryRepository.Returns(_factoryRepository);
            Assert.AreEqual(_siteManagementService.AddFactorys(dtos), operationResult);
        }
    }
}
