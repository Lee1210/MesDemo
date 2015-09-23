using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Reflection;

using Autofac;

using Mafly.Mail;

using Mes.Core;
using Mes.Core.Caching;
using Mes.Core.Data;
using Mes.Demo.Contracts;
using Mes.Demo.Contracts.Test;
using Mes.Demo.Contracts.TestLog;
using Mes.Demo.Contracts.WareHouse;
using Mes.Demo.Models.Hr;
using Mes.Demo.Models.Identity;
using Mes.Demo.Models.SiteManagement;
using Mes.Demo.Models.TestLog;
using Mes.Utility.Develop;
using Mes.Utility.Develop.T4;
using Mes.Utility.Extensions;
using Mes.Utility.Logging;


namespace Mes.Demo.Consoles
{
    internal class Program : IDependency
    {
        private static ICache _cache;
        private static Program _program;
        private static ILogger _logger;


        public IIdentityContract IdentityContract { get; set; }
        public ITestContract TestContract { get; set; }

        public ITestLogContract TestLogContract { get; set; }

        public IRepository<Menu, int> MenuRepository { get; set; }
        public IRepository<User, int> UserRepository { get; set; }
        public IRepository<Role, int> RoleRepository { get; set; }
        public IRepository<Problem, int> ProblemRepository { get; set; }
        public IRepository<ProblemType, int> ProblemTypeRepository { get; set; }
        public IRepository<Factory, int> FactoryRepository { get; set; }

        public IRepository<ProblemSource, int> ProblemSourceRepository { get; set; }

        public IRepository<Department, int> DepartmentRepository { get; set; }

        public IRepository<Cpk, int> CpkRepository { get; set; }
        public IRepository<Tlog, int> TlogRepository { get; set; }

        public IWareHouseContract WareHouseContract { get; set; }
        private static void Main(string[] args)
        {
            try
            {
                Startup.Start();
                _program = Startup.Container.Resolve<Program>();
                _cache = CacheManager.GetCacher(typeof(Program));
                _logger = LogManager.GetLogger(typeof(Program));
                // ReSharper disable once LocalizableElement
                Console.WriteLine("程序初始化完毕并启动成功。");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.FormatMessage());
                Console.ReadLine();
                return;
            }
            bool exit = false;
            while (true)
            {
                try
                {
                    Console.WriteLine(@"请输入命令：0; 退出程序，功能命令：1 - n");
                    string input = Console.ReadLine();
                    if (input == null)
                    {
                        continue;
                    }
                    switch (input.ToLower())
                    {
                        case "0":
                            exit = true;
                            break;
                        case "1":
                            Method01();
                            break;
                        case "2":
                            Method02();
                            break;
                        case "3":
                            Method03();
                            break;
                        case "4":
                            Method04();
                            break;
                        case "5":
                            Method05();
                            break;
                        case "6":
                            Method06();
                            break;
                        case "7":
                            Method07();
                            break;
                        case "8":
                            Method08();
                            break;
                        case "9":
                            Method09();
                            break;
                        case "10":
                            Method10();
                            break;
                        case "11":
                            Method11();
                            break;
                        case "12":
                            Method12();
                            break;
                        case "13":
                            Method13();
                            break;
                        case "14":
                            Method14();
                            break;
                        case "15":
                            Method15();
                            break;
                        case "16":
                            Method16();
                            break;
                        case "17":
                            Method17();
                            break;
                        case "18":
                            Method18();
                            break;
                    }
                    if (exit)
                    {
                        break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.FormatMessage());
                }
            }
        }

        private static void Method01()
        {
            string[] names = _program.IdentityContract.Organizations.Select(m => m.Name).ToArray();
            Console.WriteLine(names.ExpandAndToString());
        }

        private static void Method02()
        {
            Console.WriteLine(_program.WareHouseContract.InWareHouse("model1", "123").Message);
        }

        private static void Method03()
        {
            List<Cpk> cpks = new List<Cpk>()
            {
                new Cpk { Result = TestReslut.Pass, TestVal = 1, MaxVal = 1, MinVal = 1, ReadLogDate = DateTime.Now,TestDate = 1},
                new Cpk { Result = TestReslut.Pass, TestVal = 1, MaxVal = 1, MinVal = 1, ReadLogDate = DateTime.Now,TestDate = 1},
                new Cpk { Result = TestReslut.Pass, TestVal = 1, MaxVal = 1, MinVal = 1, ReadLogDate = DateTime.Now,TestDate = 1},
            };
            _program.CpkRepository.BulkInsertAll(cpks);
        }

        private static void Method04()
        {
            Console.WriteLine(_program.WareHouseContract.OutWareHouse("123").Message);
            _logger.Info("TEST");
        }

        /// <summary>
        /// 数据库初始化
        /// </summary>
        private static void Method05()
        {
            Menu root = new Menu() { Name = "root", Remark = "根", Parent = null, TreePath = "0", ActionName = "Index", SortCode = 0 };

            Menu privage = new Menu() { Name = "privage", Remark = "权限", Parent = root, TreePath = "1", ActionName = "Index", SortCode = 2 };
            Menu privage21 = new Menu() { Name = "Users", Remark = "用户管理", Parent = privage, TreePath = "2", ActionName = "Index", SortCode = 1 };
            Menu privage22 = new Menu() { Name = "Roles", Remark = "角色管理", Parent = privage, TreePath = "2", ActionName = "Index", SortCode = 2 };

            Menu siteManagement = new Menu() { Name = "SiteManagement", Remark = "现场管理", Parent = root, TreePath = "1", ActionName = "Index", SortCode = 3 };
            Menu siteManagement31 = new Menu() { Name = "Problem", Remark = "异常管理", Parent = siteManagement, TreePath = "2", ActionName = "Index", SortCode = 1 };
            Menu siteManagement32 = new Menu() { Name = "Problem", Remark = "异常报表", Parent = siteManagement, TreePath = "2", ActionName = "report", SortCode = 2 };
            Menu siteManagement33 = new Menu() { Name = "Department", Remark = "部门", Parent = siteManagement, TreePath = "2", ActionName = "Index", SortCode = 3 };
            Menu siteManagement34 = new Menu() { Name = "Factory", Remark = "工厂", Parent = siteManagement, TreePath = "2", ActionName = "Index", SortCode = 4 };
            Menu siteManagement35 = new Menu() { Name = "ProblemSource", Remark = "问题来源", Parent = siteManagement, TreePath = "2", ActionName = "Index", SortCode = 5 };
            Menu siteManagement36 = new Menu() { Name = "ProblemType", Remark = "问题类型", Parent = siteManagement, TreePath = "2", ActionName = "Index", SortCode = 6 };

            Menu testLog = new Menu() { Name = "TestLog", Remark = "测试数据", Parent = root, TreePath = "1", ActionName = "Index", SortCode = 4 };
            Menu testLog1 = new Menu() { Name = "Cpk", Remark = "Cpk测试数据", Parent = testLog, TreePath = "2", ActionName = "Index", SortCode = 1 };
            Menu testLog2 = new Menu() { Name = "OperationLog", Remark = "测试数据操作Log", Parent = testLog, TreePath = "2", ActionName = "Index", SortCode = 2 };

            Menu hr = new Menu() { Name = "Hr", Remark = "Hr数据", Parent = root, TreePath = "1", ActionName = "Index", SortCode = 5 };
            Menu hr1 = new Menu() { Name = "SwipeCard", Remark = "刷卡管理", Parent = hr, TreePath = "2", ActionName = "Index", SortCode = 1 };
            Menu hr2 = new Menu() { Name = "TemporaryCard", Remark = "临时卡管理", Parent = hr, TreePath = "2", ActionName = "Index", SortCode = 2 };
            Menu hr3 = new Menu() { Name = "IgnoreCard", Remark = "过滤卡管理", Parent = hr, TreePath = "2", ActionName = "Index", SortCode = 3 };
            Menu warehouse=new Menu() { Name = "WareHouse", Remark = "WareHouse数据", Parent = root, TreePath = "1", ActionName = "Index", SortCode = 6 }; 
            Menu warehouse1 = new Menu() { Name = "PurchaseAndDelivery", Remark = "进出货查询", Parent = warehouse, TreePath = "2", ActionName = "Index", SortCode = 1 };
            Menu warehouse2 = new Menu() { Name = "PurchaseAndDelivery", Remark = "进出货管理", Parent = warehouse, TreePath = "2", ActionName = "InAndOut", SortCode = 2 };

            List<Menu> menus = new List<Menu> { root, privage, privage21, privage22,
                siteManagement, siteManagement31, siteManagement32, siteManagement33, siteManagement34, siteManagement35, siteManagement36,
                testLog, testLog1, testLog2,
                hr,hr1,hr2,hr3,
                warehouse,warehouse1,warehouse2
            };

            User user1 = new User() { Email = "123", CreatedTime = DateTime.Now, Name = "user1", NickName = "梁贵", Password = "123", };
            User user2 = new User() { Email = "123", CreatedTime = DateTime.Now, Name = "user2", NickName = "梁贵2", Password = "123", };
            List<User> users = new List<User> { user1, user2 };

            Role role1 = new Role() { Name = "role1", Remark = "role1" };
            Role role2 = new Role() { Name = "role2", Remark = "role2" };
            role1.Menus = menus;
            role1.Users = users;
            List<Role> roles = new List<Role> { role1, role2 };

            Factory factory1 = new Factory() { Text = "龙旗一厂", Value = "龙旗一厂" };
            Factory factory2 = new Factory() { Text = "龙旗二厂", Value = "龙旗二厂" };
            Factory factory3 = new Factory() { Text = "深圳振华", Value = "深圳振华" };
            List<Factory> factorys = new List<Factory> { factory1, factory2, factory3 };

            Department department1 = new Department() { Text = "生产", Value = "生产" };
            Department department2 = new Department() { Text = "计划", Value = "计划" };
            Department department3 = new Department() { Text = "工程", Value = "工程" };
            Department department4 = new Department() { Text = "质量", Value = "质量" };
            Department department5 = new Department() { Text = "仓库", Value = "仓库" };
            List<Department> departments = new List<Department> { department1, department2, department3, department4, department5 };

            ProblemSource problemSource1 = new ProblemSource() { Text = "邮件预警", Value = "邮件预警" };
            ProblemSource problemSource2 = new ProblemSource() { Text = "用户反馈", Value = "用户反馈" };
            ProblemSource problemSource3 = new ProblemSource() { Text = "客户反馈", Value = "客户反馈" };
            List<ProblemSource> problemSources = new List<ProblemSource> { problemSource1, problemSource2, problemSource3 };

            ProblemType problemType1 = new ProblemType() { Text = "MES系统", Value = "MES系统" };
            ProblemType problemType2 = new ProblemType() { Text = "订单导入", Value = "订单导入" };
            ProblemType problemType3 = new ProblemType() { Text = "入库比对", Value = "入库比对" };
            ProblemType problemType4 = new ProblemType() { Text = "出库扫描", Value = "出库扫描" };
            ProblemType problemType5 = new ProblemType() { Text = "数据回传", Value = "数据回传" };
            List<ProblemType> problemTypes = new List<ProblemType> { problemType1, problemType2, problemType3, problemType4, problemType5 };

            Problem problem1 = new Problem() { Department = "生产", Factory = "龙旗一厂", QuestionFrom = "邮件预警", Type = "MES系统", Workers = "梁贵", ProblemTime = DateTime.Now, Detail = "detail1", Reason = "reason1", IsComplete = true, IsPeople = true, IsDeleted = false, Remark = "remark1", Solution = "solution1", Suggestion = "suggestion1" };
            Problem problem2 = new Problem() { Department = "生产", Factory = "龙旗一厂", QuestionFrom = "邮件预警", Type = "MES系统", Workers = "梁贵", ProblemTime = DateTime.Now.AddDays(1), Detail = "detail1", Reason = "reason1", IsComplete = true, IsPeople = true, IsDeleted = false, Remark = "remark1", Solution = "solution1", Suggestion = "suggestion1" };
            List<Problem> problems = new List<Problem> { problem1, problem2 };
         
            _program.MenuRepository.UnitOfWork.TransactionEnabled = true;
            Console.WriteLine(_program.MenuRepository.Insert(menus.AsEnumerable()));
            Console.WriteLine(_program.UserRepository.Insert(users.AsEnumerable()));
            Console.WriteLine(_program.RoleRepository.Insert(roles.AsEnumerable()));
            Console.WriteLine(_program.FactoryRepository.Insert(factorys.AsEnumerable()));
            Console.WriteLine(_program.DepartmentRepository.Insert(departments.AsEnumerable()));
            Console.WriteLine(_program.ProblemSourceRepository.Insert(problemSources.AsEnumerable()));
            Console.WriteLine(_program.ProblemTypeRepository.Insert(problemTypes.AsEnumerable()));
            Console.WriteLine(_program.ProblemRepository.Insert(problems.AsEnumerable()));
            _program.MenuRepository.UnitOfWork.SaveChanges();
        }

        private static void Method06()
        {
            var mailService = new Mail();

            //参数：接收者邮箱、内容
            mailService.Send("229042735@qq.com", "测试邮件发送！");

            //参数：接收者邮箱、接收者名字、内容
            mailService.Send("229042735@qq.com", "mafly", "测试邮件发送！");

            //参数：接收者邮箱、接收者名字、邮件主题、内容
            mailService.Send("229042735@qq.com", "mafly", "邮件发送", "测试邮件发送！");

            //使用MailInfo对象模式  参数：接收者邮箱、接收者名字、邮件主题、内容
            mailService.Send(new MailInfo
            {
                Receiver = "229042735@qq.com",
                ReceiverName = "mafly",
                Subject = "邮件发送",
                Body = "测试邮件发送！"
            });

            //使用MailInfo对象模式  参数：接收者邮箱、接收者名字、邮件主题、内容、附件路径
            mailService.Send(
                new MailInfo
                {
                    Receiver = "229042735@qq.com",
                    ReceiverName = "mafly",
                    Subject = "带附件邮件发送",
                    Body = "测试带附件邮件发送！"
                }, "../../Program.cs");

            //使用MailInfo对象模式  参数：接收者邮箱、接收者名字、邮件主题、内容、多附件路径
            mailService.Send(
                new MailInfo
                {
                    Receiver = "229042735@qq.com",
                    ReceiverName = "mafly",
                    Subject = "带附件邮件发送",
                    Body = "测试带附件邮件发送！"
                }, new Attachment("../../Program.cs"), new Attachment("../../App.config"));
        }

        public class ActionPermission
        {
            /// <summary>
            /// 请求地址
            /// </summary>
            public virtual string ActionName { get; set; }

            /// <summary>
            /// 请求地址
            /// </summary>
            public virtual string ControllerName { get; set; }

            /// <summary>
            /// 描述
            /// </summary>
            public virtual string Description { get; set; }

            /// <summary>
            /// 角色列表
            /// </summary>
            public virtual ISet<Role> Roles { get; set; }
        }
        private static void Method07()
        {
            var types = Assembly.Load("Mes.Demo.Web").GetTypes();
            var result = new List<ActionPermission>();
            foreach (var type in types)
            {
                // ReSharper disable once PossibleNullReferenceException
                if (type.BaseType.Name == "AdminBaseController")//如果是Controller
                {
                    var members = type.GetMethods();
                    foreach (var member in members)
                    {
                        if (member.ReturnType.Name == "ActionResult")//如果是Action
                        {

                            var ap = new ActionPermission();

                            ap.ActionName = member.Name;
                            Debug.Assert(member.DeclaringType != null, "member.DeclaringType != null");
                            ap.ControllerName = member.DeclaringType.Name.Substring(0, member.DeclaringType.Name.Length - 10); // 去掉“Controller”后缀

                            object[] attrs = member.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), true);
                            if (attrs.Length > 0)
                                ap.Description = (attrs[0] as System.ComponentModel.DescriptionAttribute).Description;
                            result.Add(ap);

                        }

                    }
                }
            }
            Console.ReadKey();
        }

        private static void Method08()
        {
            Assembly assembly = Assembly.Load("Mes.Demo.Contracts");
            Type baseType = typeof(IAddDto);
            IEnumerable<Type> modelTypes = assembly.GetTypes().Where(m => baseType.IsAssignableFrom(m) && !m.IsAbstract);
            foreach (var modelType in modelTypes)
            {
                Console.WriteLine(modelType.Name + " || " + modelType.Name.Split("Dto")[0]);
            }
        }

        private static void Method09()
        {
            Assembly assembly = Assembly.Load("Mes.Demo.Contracts");
            Type baseType = typeof(EntityBase<>);
            IEnumerable<Type> modelTypes = assembly.GetTypes().Where(m => baseType.IsGenericAssignableFrom(m) && !m.IsAbstract);
            foreach (Type modelType in modelTypes)
            {
                T4ModelInfo model = new T4ModelInfo(modelType, true);

                if (model.ModuleName == "Test")
                {
                    foreach (var pro in model.Properties)
                    {
                        List<string> igoreList = new List<string> { "CreatedTime", "Timestamp", "IsDeleted" };
                        if (!igoreList.Contains(pro.Name))
                        {
                            List<string> igoreString = new List<string> { "Timestamp" };
                            igoreString.Contains(pro.Name);
                            if (pro.PropertyType.Name == "ICollection`1")
                                Console.WriteLine(pro.PropertyType.GenericTypeArguments.FirstOrDefault().Name);
                            else if (pro.CustomAttributes != null)
                            {
                                foreach (var customAttribute in pro.CustomAttributes)
                                {
                                    switch (customAttribute.AttributeType.Name)
                                    {
                                        case "RequiredAttribute":
                                            Console.WriteLine("[Required]");
                                            break;
                                        case "StringLengthAttribute":
                                            Console.WriteLine("[StringLength({0})]", customAttribute.ConstructorArguments[0]);
                                            break;
                                    }
                                    string constructorArgumentsString = "";
                                    if (customAttribute.ConstructorArguments != null)
                                    {
                                        Console.Write(String.Join(",", customAttribute.ConstructorArguments.Select(m => m.Value).ToArray()));
                                    }
                                    Console.Write("[" + customAttribute.AttributeType.Name.Replace("Attribute", "") + "]");
                                }
                                Console.WriteLine();
                                Console.WriteLine(pro.PropertyType.Name);
                            }


                        }
                        // pro.CustomAttributes.FirstOrDefault().ToString();


                    }
                }
                //实体映射类
            }
        }

        private static void Method10()
        {
            CodeTimer.Time("insert",
                1,
                () =>
                {
                    List<Cpk> cpks = new List<Cpk>();
                    for (int i = 0; i < 10000; i++)
                    {
                        cpks.Add(new Cpk() { Ip = "123.123.123.123", CreatedTime = DateTime.Now, ReadLogDate = DateTime.Today, LogFileName = "12341234123412341234", IsDeleted = false, MaxVal = 21.01, MinVal = 20.12, Opcode = "cit", Pcl = "pcl" });
                    }
                    _program.CpkRepository.Insert(cpks);
                });

        }

        private static void Method11()
        {
            CodeTimer.Time("insert",
               1,
               () =>
               {
                   List<Cpk> cpks = new List<Cpk>();
                   for (int i = 0; i < 10000; i++)
                   {
                       cpks.Add(new Cpk() { Ip = "123.123.123.123", CreatedTime = DateTime.Now, ReadLogDate = DateTime.Today, LogFileName = "12341234123412341234", IsDeleted = false, MaxVal = 21.01, MinVal = 20.12, Opcode = "cit", Pcl = "pcl" });
                   }
                   _program.CpkRepository.BulkInsertAll(cpks);
               });
        }

        private static void Method12()
        {
            CodeTimer.Time("insert",
                1,
                () =>
                {
                    var list = _program.CpkRepository.Entities.ToList();
                    double sum = 0;
                    foreach (var a in list)
                    {
                        sum += a.TestVal;
                    }
                    Console.WriteLine(sum / list.Count);
                });
        }

        private static void Method13()
        {
            throw new NotImplementedException();
        }

        private static void Method14()
        {
            throw new NotImplementedException();
        }

        private static void Method15()
        {
            throw new NotImplementedException();
        }

        private static void Method16()
        {
            throw new NotImplementedException();
        }

        private static void Method17()
        {
            throw new NotImplementedException();
        }

        private static void Method18()
        {
            throw new NotImplementedException();
        }
    }
}
