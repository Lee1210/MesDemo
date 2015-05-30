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
    public interface IResContract : IDependency
    {
        /// <summary>
        /// 获取 资源信息查询数据集
        /// </summary>
        IQueryable<Res> Res { get; }

        bool CheckResExists(Expression<Func<Res, bool>> predicate, int id = 0);

        OperationResult AddRes(params ResDto[] dtos);

        OperationResult EditRes(params ResDto[] dtos);

        OperationResult DeleteRes(params int[] ids);
    }
}
