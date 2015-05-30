using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using OSharp.Core;
using OSharp.Demo.Dtos.Identity;
using OSharp.Demo.Models.Identity;
using OSharp.Utility.Data;


namespace OSharp.Demo.Contracts
{
    public interface IDelayTimeContract : IDependency
    {
        /// <summary>
        /// 获取 资源信息查询数据集
        /// </summary>
        IQueryable<DelayTime> DelayTimes { get; }

        bool CheckDelayTimeExists(Expression<Func<DelayTime, bool>> predicate, int id = 0);

        OperationResult AddDelayTime(params DelayTimeDto[] dtos);

        OperationResult EditDelayTime(params DelayTimeDto[] dtos);

        OperationResult DeleteDelayTime(params int[] ids);
    }
}
