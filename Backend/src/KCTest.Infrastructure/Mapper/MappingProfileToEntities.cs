using AutoMapper;
using KCTest.Domain.DTOs;
using KCTest.Domain.Entities;

namespace KCTest.Infrastructure.Mapper
{
    public class MappingProfileToEntities : Profile
    {
        public MappingProfileToEntities()
        {
            CreateMap<PermissionDto, Permission>();
            CreateMap<PermissionTypeDto, PermissionType>();
        }
    }
}