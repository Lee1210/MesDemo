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
        [Description("已退货")]
        已退货=1,
        [Description("已进货")]
        已进货 = 2,
        [Description("二次退货")]
        二次退货 =3,
        [Description("二次进货")]
        二次进货 =4
    }
}
