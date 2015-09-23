using System;

using Mes.Core.Data;


namespace Mes.Demo.Models.WareHouse
{
    public class PurchaseAndDelivery : EntityBase<int>, IAddDto
    {
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
