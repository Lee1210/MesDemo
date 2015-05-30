

using AutoMapper;

using Mes.Demo.Dtos.Identity;
using Mes.Demo.Models.Identity;


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
        }
    }
}