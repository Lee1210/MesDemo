using AutoMapper;

using Mes.Demo.Dtos.Identity;
using Mes.Demo.Dtos.SiteManagement;
using Mes.Demo.Dtos.Test;
using Mes.Demo.Models.Identity;
using Mes.Demo.Models.SiteManagement;
using Mes.Demo.Models.Test;


namespace Mes.Demo.Dtos
{
    public class DtoMappers
    {
        public static void MapperRegister()
        {
            //Identity
            Mapper.CreateMap<OrganizationDto, Organization>();
            Mapper.CreateMap<UserDto, User>();
            Mapper.CreateMap<RoleDto, Role>();
            Mapper.CreateMap<MenuDto, Menu>();

            //Test
            Mapper.CreateMap<LineDto, Line>();
            Mapper.CreateMap<StationDto, Station>();

            //SiteManagement
            Mapper.CreateMap<ProblemDto, Problem>();
            Mapper.CreateMap<FactoryDto, Factory>();
            Mapper.CreateMap<DepartmentDto, Department>();
            Mapper.CreateMap<ProblemSourceDto, ProblemSource>();
            Mapper.CreateMap<ProblemTypeDto, ProblemType>();
        }
    }
}