using System;
using System.Linq;
using System.Linq.Expressions;

using Mes.Core.Data;
using Mes.Demo.Dtos.WareHouse;
using Mes.Demo.Models.WareHouse;
using Mes.Utility.Data;
using Mes.Utility.Extensions;


namespace Mes.Demo.Services.WareHouse
{
    public partial class WareHouseService
    {
        public IRepository<PurchaseAndDelivery, int> PurchaseAndDeliveryRepository { protected get; set; }

        /// <summary>
        /// 获取部门 信息查询数据集
        /// </summary>
        public IQueryable<PurchaseAndDelivery> PurchaseAndDeliverys { get { return PurchaseAndDeliveryRepository.Entities; } }

        /// <summary>
        /// 检查部门信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的部门信息编号</param>
        /// <returns>部门信息是否存在</returns>
        public bool CheckPurchaseAndDeliveryExists(Expression<Func<PurchaseAndDelivery, bool>> predicate, int id = 0)
        {
            return PurchaseAndDeliveryRepository.CheckExists(predicate, id);
        }
        /// <summary>
        /// 添加部门信息
        /// </summary>
        /// <param name="dtos">要添加的部门信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public OperationResult AddPurchaseAndDeliverys(params PurchaseAndDeliveryDto[] dtos)
        {
            return PurchaseAndDeliveryRepository.Insert(dtos);
        }

        /// <summary>
        /// 更新部门信息
        /// </summary>
        /// <param name="dtos">包含更新信息的部门DTO信息</param>
        /// <returns>业务操作结果</returns>
        public OperationResult EditPurchaseAndDeliverys(params PurchaseAndDeliveryDto[] dtos)
        {
            return PurchaseAndDeliveryRepository.Update(dtos);
        }

        /// <summary>
        /// 删除部门信息
        /// </summary>
        /// <param name="ids">要删除的部门信息编号</param>
        /// <returns>业务操作结果</returns>
        public OperationResult DeletePurchaseAndDeliverys(params int[] ids)
        {
            return PurchaseAndDeliveryRepository.Delete(ids);
        }

        public OperationResult OutWareHouse(string sn)
        {
            if (sn.IsNullOrEmpty())
                return new OperationResult(OperationResultType.Error) { Message = "SN不能为空" };
            var delivery = PurchaseAndDeliveryRepository.Entities.FirstOrDefault(m => m.Sn == sn);
            if (delivery == null)
            {
                return new OperationResult(OperationResultType.Error) { Message = $"SN号:{sn}不存在！" };
            }
            if (delivery.AdType == PurchaseAndDeliveryType.已退货)
            {
                delivery.AdType = PurchaseAndDeliveryType.已进货;
                delivery.D2=DateTime.Now;
                delivery.Day2 = DateTime.Now.ToString("yyyyMMdd").CastTo<int>();
                PurchaseAndDeliveryRepository.Update(delivery);
                return new OperationResult(OperationResultType.Success) { Message = $"SN号:{sn}进货成功" };
            }
            if(delivery.AdType == PurchaseAndDeliveryType.二次退货)
            {
                delivery.AdType = PurchaseAndDeliveryType.二次进货;
                delivery.D4 = DateTime.Now;
                delivery.Day4 = DateTime.Now.ToString("yyyyMMdd").CastTo<int>();
                PurchaseAndDeliveryRepository.Update(delivery);
                return new OperationResult(OperationResultType.Success) { Message = $"SN号:{sn}二次进货成功" };
            }
            return new OperationResult(OperationResultType.Error) { Message = $"SN:{sn} 当前状态为{delivery.AdType.ToDescription()},不能进货" };
        }

        public OperationResult InWareHouse(string model, string sn)
        {
            if (sn.IsNullOrEmpty())
              return  new OperationResult(OperationResultType.Error){ Message = "SN不能为空" };
            var delivery = PurchaseAndDeliveryRepository.Entities.FirstOrDefault(m => m.Sn == sn);
            if (delivery == null)
            {
                PurchaseAndDeliveryRepository.Insert(new PurchaseAndDelivery()
                {
                    AdType = PurchaseAndDeliveryType.已退货,
                    Model = model,
                    Sn = sn,
                    Day1 = DateTime.Now.ToString("yyyyMMdd").CastTo<int>()
                });
                return new OperationResult(OperationResultType.Success) { Message = $"SN号:{sn}退货成功！" };
            }
            if (delivery.AdType == PurchaseAndDeliveryType.已进货)
            {
                delivery.AdType = PurchaseAndDeliveryType.二次退货;
                delivery.D3 = DateTime.Now;
                delivery.Day3 = DateTime.Now.ToString("yyyyMMdd").CastTo<int>();
                PurchaseAndDeliveryRepository.Update(delivery);
                return new OperationResult(OperationResultType.Success) { Message = $"SN号:{sn}二次退货成功" };
            }
            return new OperationResult(OperationResultType.Error) { Message = $"SN:{sn} 当前状态为{delivery.AdType.ToDescription()},不能退货" };
        }
    }
   
}
