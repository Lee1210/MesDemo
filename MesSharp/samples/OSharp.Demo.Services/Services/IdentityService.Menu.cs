

using System;
using System.Linq;
using System.Linq.Expressions;

using Mes.Demo.Dtos.Identity;
using Mes.Demo.Models.Identity;
using Mes.Utility.Data;


namespace Mes.Demo.Services
{
    public partial class IdentityService
    {
        #region Implementation of IIdentityContract

        /// <summary>
        /// 获取 菜单信息查询数据集
        /// </summary>
        public IQueryable<Menu> Menus
        {
            get { return MenuRepository.Entities; }
        }

        /// <summary>
        /// 检查菜单信息信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的菜单信息编号</param>
        /// <returns>菜单信息是否存在</returns>
        public bool CheckMenuExists(Expression<Func<Menu, bool>> predicate, int id = 0)
        {
            return MenuRepository.CheckExists(predicate, id);
        }
        

        /// <summary>
        /// 添加菜单信息信息
        /// </summary>
        /// <param name="dtos">要添加的菜单信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public OperationResult AddMenus(params MenuDto[] dtos)
        {
            return MenuRepository.Insert(dtos);
        }

        /// <summary>
        /// 更新菜单信息信息
        /// </summary>
        /// <param name="dtos">包含更新信息的菜单信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public OperationResult EditMenus(params MenuDto[] dtos)
        {
            return MenuRepository.Update(dtos);
        }

        /// <summary>
        /// 删除菜单信息信息
        /// </summary>
        /// <param name="ids">要删除的菜单信息编号</param>
        /// <returns>业务操作结果</returns>
        public OperationResult DeleteMenus(params int[] ids)
        {
            return MenuRepository.Delete(ids);
        }

       

        #endregion
    }
}