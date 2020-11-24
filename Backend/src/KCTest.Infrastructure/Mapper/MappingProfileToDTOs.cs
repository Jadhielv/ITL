using AutoMapper;
using KCTest.Domain.DTOs;
using KCTest.Domain.Entities;

namespace KCTest.Infrastructure.Mapper
{
    public class MappingProfileToDTOs : Profile
    {
        public MappingProfileToDTOs()
        {
            CreateMap<Permission, PermissionDto>();
            CreateMap<PermissionType, PermissionTypeDto>();
        }
    }
}