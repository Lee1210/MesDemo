using System;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

using Mes.Core.Data;
using Mes.Demo.Dtos.TestLog;
using Mes.Demo.Models.Hr;
using Mes.Demo.Models.TestLog;
using Mes.Utility.Data;


namespace Mes.Demo.Services.Hr
{
    public partial class HrService 
    {
        public IRepository<TemporaryCard, int> TemporaryCardRepository { protected get; set; }

        /// <summary>
        /// 获取部门 信息查询数据集
        /// </summary>
        public IQueryable<TemporaryCard> TemporaryCards { get { return TemporaryCardRepository.Entities; } }

        /// <summary>
        /// 检查部门信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的部门信息编号</param>
        /// <returns>部门信息是否存在</returns>
        public bool CheckTemporaryCardExists(Expression<Func<TemporaryCard, bool>> predicate, int id = 0)
        {
            return TemporaryCardRepository.CheckExists(predicate, id);
        }
        /// <summary>
        /// 添加部门信息
        /// </summary>
        /// <param name="dtos">要添加的部门信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public OperationResult AddTemporaryCards(params TemporaryCardDto[] dtos)
        {
            return TemporaryCardRepository.Insert(dtos);
        }

        /// <summary>
        /// 更新部门信息
        /// </summary>
        /// <param name="dtos">包含更新信息的部门DTO信息</param>
        /// <returns>业务操作结果</returns>
        public OperationResult EditTemporaryCards(params TemporaryCardDto[] dtos)
        {
            return TemporaryCardRepository.Update(dtos);
        }

        /// <summary>
        /// 删除部门信息
        /// </summary>
        /// <param name="ids">要删除的部门信息编号</param>
        /// <returns>业务操作结果</returns>
        public OperationResult DeleteTemporaryCards(params int[] ids)
        {
            return TemporaryCardRepository.Delete(ids);
        }
      
    }
   
}
