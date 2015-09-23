using Mes.Core;
using Mes.Core.Data;
using Mes.Demo.Contracts.WareHouse;


namespace Mes.Demo.Services.WareHouse
{
    public partial class WareHouseService : ServiceBase, IWareHouseContract
    {
        public WareHouseService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        { }
    }
}
