using Mes.Core;
using Mes.Core.Data;
using Mes.Demo.Contracts.TestLog;


namespace Mes.Demo.Services.TestLog
{
    public partial class TestLogService : ServiceBase, ITestLogContract
    {
        public TestLogService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        { }
    }
}
