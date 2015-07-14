

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using Mes.Demo.Dtos.Identity;
using Mes.Demo.Models.Identity;
using Mes.Utility;
using Mes.Utility.Data;


namespace Mes.Demo.Services
{
    public partial class IdentityService
    {
        #region Implementation of IIdentityContract

        /// <summary>
        /// 获取 用户信息查询数据集
        /// </summary>
        public IQueryable<User> Users
        {
            get { return UserRepository.Entities; }
        }

        /// <summary>
        /// 检查用户信息信息是否存在
        /// </summary>
        /// <param name="predicate">检查谓语表达式</param>
        /// <param name="id">更新的用户信息编号</param>
        /// <returns>用户信息是否存在</returns>
        public bool CheckUserExists(Expression<Func<User, bool>> predicate, int id = 0)
        {
            return UserRepository.CheckExists(predicate, id);
        }

        /// <summary>
        /// 添加用户信息信息
        /// </summary>
        /// <param name="dtos">要添加的用户信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public OperationResult AddUsers(params UserDto[] dtos)
        {
            return UserRepository.Insert(dtos,
                dto =>
                {
                    if (CheckUserExists(u=>u.Name==dto.Name))
                        throw new Exception(string.Format("{0}已存在",dto.Name));
                },
                (dto,user) =>
                {
                    user.Password = "123";
                    return user;
                });
        }

        /// <summary>
        /// 更新用户信息信息
        /// </summary>
        /// <param name="dtos">包含更新信息的用户信息DTO信息</param>
        /// <returns>业务操作结果</returns>
        public OperationResult EditUsers(params UserDto[] dtos)
        {
            return UserRepository.Update(dtos,
                dto =>
                {
                    if (CheckUserExists(u => u.Name == dto.Name))
                        throw new Exception(string.Format("{0}已存在", dto.Name));
                });
        }

        /// <summary>
        /// 删除用户信息信息
        /// </summary>
        /// <param name="ids">要删除的用户信息编号</param>
        /// <returns>业务操作结果</returns>
        public OperationResult DeleteUsers(params int[] ids)
        {
            return UserRepository.Delete(ids);
        }

        /// <summary>
        /// 设置用户的角色
        /// </summary>
        /// <param name="id">用户编号</param>
        /// <param name="select">角色编号集合</param>
        /// <returns>业务操作结果</returns>
        public OperationResult SetUserRoles(int id, int[] select)
        {
            var user = UserRepository.GetByKey(id);
            OperationResult operationResult = new OperationResult(OperationResultType.Success);
            int[] beforeSelect = user.Roles.Select(r => r.Id).ToArray();
            try
            {
                select.CheckNotNullOrEmpty("select");
            }
            catch (Exception)
            {
                select = new int[0];
            }

            beforeSelect.Except(select).ToList().ForEach(n => user.Roles.Remove(RoleRepository.GetByKey(n)));
            select.Except(beforeSelect).ToList().ForEach(n => user.Roles.Add(RoleRepository.GetByKey(n)));
            operationResult.Message = "修改了：" + UserRepository.UnitOfWork.SaveChanges() + "条数据";
            operationResult.Data = user;
            return operationResult;
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public OperationResult Login(string userName, string password)
        {
            OperationResult operationResult = new OperationResult(OperationResultType.Error);
            try
            {
                userName.CheckNotNullOrEmpty("userName");
                userName.CheckNotNullOrEmpty("password");
            }
            catch (Exception)
            {
                operationResult.Message = "账号密码不能为空";
                return operationResult;
            }
            var user = Users.FirstOrDefault(u => u.Name == userName);
            if (user == null)
            {
                operationResult.Message = "用户不存在";
                return operationResult;
            }
            if (user.Password!=password)
            {
                operationResult.Message = "密码错误";
                return operationResult;
            }
            operationResult.ResultType=OperationResultType.Success;
            operationResult.Data = user;
            return operationResult;
        }
        #endregion
    }
}