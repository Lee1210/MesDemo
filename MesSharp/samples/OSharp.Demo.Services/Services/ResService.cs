using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using OSharp.Core;
using OSharp.Core.Data;
using OSharp.Demo.Contracts;
using OSharp.Demo.Dtos.Identity;
using OSharp.Demo.Models.Identity;
using OSharp.Utility.Data;
using OSharp.Utility.Extensions;


namespace OSharp.Demo.Services
{
    public class ResService : ServiceBase,IResContract
    {
        #region Implementation of IResContract

        public ResService(IUnitOfWork unitOfWork)
            :base(unitOfWork)
        {
            
        }

        public IRepository<Res, int> ResRepository { protected get; set; }
        /// <summary>
        /// 获取 资源信息查询数据集
        /// </summary>
        public IQueryable<Res> Res { get { return ResRepository.Entities; } }

        public bool CheckResExists(Expression<Func<Res, bool>> predicate, int id = 0)
        {
            return ResRepository.CheckExists(predicate, id);
        }



        public OperationResult AddRes(params ResDto[] dtos)
        {
            return ResRepository.Insert(dtos,
               dto =>
               {
                   if (ResRepository.CheckExists(m => m.Name == dto.Name ))
                   {
                       throw new Exception("同组织机构中名称为“{0}”的角色已存在，不能重复添加。".FormatWith(dto.Name));
                   }
               });
        }

        public OperationResult EditRes(params ResDto[] dtos)
        {
            return ResRepository.Update(dtos);
        }

        public OperationResult DeleteRes(params int[] ids)
        {
            return ResRepository.Delete(ids);
        }

        #endregion
    }
}
