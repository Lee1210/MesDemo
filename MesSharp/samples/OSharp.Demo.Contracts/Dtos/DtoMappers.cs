using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using AutoMapper;

using Mes.Core.Data;
using Mes.Demo.Dtos.Identity;
using Mes.Demo.Dtos.SiteManagement;
using Mes.Demo.Dtos.Test;
using Mes.Demo.Models.Identity;
using Mes.Demo.Models.SiteManagement;
using Mes.Demo.Models.Test;
using Mes.Utility.Extensions;


namespace Mes.Demo.Dtos
{
    public class DtoMappers
    {
        public static void MapperRegister()
        {
            Assembly assembly = Assembly.Load("Mes.Demo.Contracts");
            Type dtoBaseType = typeof(IAddDto);
            Type modelBaseType = typeof(EntityBase<>);
            IEnumerable<Type> dtoTypes = assembly.GetTypes().Where(m => dtoBaseType.IsAssignableFrom(m) && !m.IsAbstract);
            IEnumerable<Type> modelTypes = assembly.GetTypes().Where(m => modelBaseType.IsGenericAssignableFrom(m) && !m.IsAbstract);
            foreach (var dtoType in dtoTypes)
            {
                Type testType = modelTypes.FirstOrDefault(m => m.Name == dtoType.Name.Split("Dto")[0]);
                if (testType != null)
                    Mapper.CreateMap(dtoType, testType);
                    //不同属性
                   // Mapper.CreateMap(dtoType, testType,MerberList);
            }


            //Identity
            //Mapper.CreateMap<OrganizationDto, Organization>();
            //Mapper.CreateMap<UserDto, User>();
            //Mapper.CreateMap<RoleDto, Role>();
            //Mapper.CreateMap<MenuDto, Menu>();

            ////Test
            //Mapper.CreateMap<LineDto, Line>();
            //Mapper.CreateMap<StationDto, Station>();

            ////SiteManagement
            //Mapper.CreateMap<ProblemDto, Problem>();
            //Mapper.CreateMap<FactoryDto, Factory>();
            //Mapper.CreateMap<DepartmentDto, Department>();
            //Mapper.CreateMap<ProblemSourceDto, ProblemSource>();
            //Mapper.CreateMap<ProblemTypeDto, ProblemType>();
        }
    }
}