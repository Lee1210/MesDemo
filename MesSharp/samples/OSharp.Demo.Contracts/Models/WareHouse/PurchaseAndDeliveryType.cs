using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mes.Demo.Models.WareHouse
{
    public enum PurchaseAndDeliveryType
    {
        [Description("已退板")]
        已退板 = 1,
        [Description("已收板")]
        已收板 = 2,
        [Description("二次退板")]
        二次退板 = 3,
        [Description("二次收板")]
        二次收板 = 4
    }
}
