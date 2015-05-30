using System;
using System.Linq;
using System.Linq.Expressions;

using OSharp.Core;
using OSharp.Core.Data;
using OSharp.Demo.Contracts;
using OSharp.Demo.Dtos.Identity;
using OSharp.Demo.Models.Identity;
using OSharp.Utility.Data;


namespace OSharp.Demo.Services
{
    public class DelayTimeService : ServiceBase,IDelayTimeContract
    {
        #region Implementation of IDelayTimeContract

        public DelayTimeService(IUnitOfWork unitOfWork)
            :base(unitOfWork)
        {
            
        }

        public IRepository<DelayTime, int> DelayTimeRepository { protected get; set; }
        /// <summary>
        /// 获取 资源信息查询数据集
        /// </summary>
        public IQueryable<DelayTime> DelayTimes { get { return DelayTimeRepository.Entities; } }

        public bool CheckDelayTimeExists(Expression<Func<DelayTime, bool>> predicate, int id = 0)
        {
            return DelayTimeRepository.CheckExists(predicate, id);
        }



        public OperationResult AddDelayTime(params DelayTimeDto[] dtos)
        {
            return DelayTimeRepository.Insert(dtos,
               dto =>
               {
                   if (DelayTimeRepository.CheckExists(m => m.Duty == dto.Duty&&m.WorkDate==dto.WorkDate&&m.Line==dto.Line ))
                   {
                       throw new Exception("该责任部门该线当天已有数据，不能重复添加。");
                   }
               });
        }

        public OperationResult EditDelayTime(params DelayTimeDto[] dtos)
        {
            return DelayTimeRepository.Update(dtos);
        }

        public OperationResult DeleteDelayTime(params int[] ids)
        {
            return DelayTimeRepository.Delete(ids);
        }

        #endregion
    }
}
