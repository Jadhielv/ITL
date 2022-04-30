using AutoMapper;
using ITL.Domain.DTOs;
using ITL.Domain.Entities;

namespace ITL.Infrastructure.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<PermissionDto, Permission>().ReverseMap();
        CreateMap<PermissionTypeDto, PermissionType>().ReverseMap();
    }
}
