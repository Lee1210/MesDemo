

using AutoMapper;

using Mes.Demo.Dtos.Identity;
using Mes.Demo.Dtos.Test;
using Mes.Demo.Models.Identity;
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

            //Test
            Mapper.CreateMap<LineDto, Line>();
            Mapper.CreateMap<StationDto, Station>();
        }
    }
}