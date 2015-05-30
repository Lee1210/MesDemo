using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

using Autofac;

using Mes.Core;
using Mes.Core.Caching;
using Mes.Core.Data;
using Mes.Demo.Contracts;
using Mes.Demo.Models.Identity;
using Mes.Utility.Develop.T4;
using Mes.Utility.Extensions;


namespace Mes.Demo.Consoles
{
    internal class Program : IDependency
    {
        private static ICache Cache;
        private static Program _program;

        public IIdentityContract IdentityContract { get; set; }
        public ITestContract ITestContract { get; set; }

        private static void Main(string[] args)
        {
            try
            {
                Startup.Start();
                _program = Startup.Container.Resolve<Program>();
                Cache = CacheManager.GetCacher(typeof(Program));
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
            
            Console.WriteLine(_program.ITestContract.Lines.Count());
        }

        private static void Method03()
        {
          
        }

        private static void Method04()
        {
            
        }

        private static void Method05()
        {
          
        }

        private static void Method06()
        {
           
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
                                    string ConstructorArgumentsString = "";
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
            throw new NotImplementedException();
        }

        private static void Method11()
        {
            throw new NotImplementedException();
        }

        private static void Method12()
        {
            throw new NotImplementedException();
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
