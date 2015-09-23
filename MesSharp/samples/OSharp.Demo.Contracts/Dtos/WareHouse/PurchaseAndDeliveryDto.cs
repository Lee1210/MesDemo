using System;
using System.ComponentModel.DataAnnotations;

using Mes.Core.Data;
using Mes.Demo.Models.TestLog;
using Mes.Demo.Models.WareHouse;


namespace Mes.Demo.Dtos.WareHouse
{
    public class PurchaseAndDeliveryDto : IAddDto, IEditDto<int>
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Sn { get; set; }
        public PurchaseAndDeliveryType AdType { get; set; }

        public DateTime? D2 { get; set; }
        public DateTime? D3 { get; set; }
        public DateTime? D4 { get; set; }

        public int Day1 { get; set; }
        public int Day2 { get; set; }
        public int Day3 { get; set; }
        public int Day4 { get; set; }
    }
}
