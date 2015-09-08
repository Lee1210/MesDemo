using Mes.Core;
using Mes.Core.Data;
using Mes.Demo.Contracts.TestLog;


namespace Mes.Demo.Services.Hr
{
    public partial class HrService : ServiceBase,IHrContract
    {
        public HrService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        { }
    }
}
