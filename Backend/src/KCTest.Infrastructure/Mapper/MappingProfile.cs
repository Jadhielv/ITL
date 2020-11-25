using AutoMapper;
using KCTest.Domain.DTOs;
using KCTest.Domain.Entities;

namespace KCTest.Infrastructure.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PermissionDto, Permission>().ReverseMap();
            CreateMap<PermissionTypeDto, PermissionType>().ReverseMap();
        }
    }
}