using ITL.Domain.DTOs;
using ITL.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ITL.Domain.Extensions;

public static class MappingExtensions
{
    public static PermissionDto ToDto(this Permission source)
    {
        if (source == null) return null;
        return new PermissionDto
        {
            Id = source.Id,
            Name = source.Name,
            LastName = source.LastName,
            Date = source.Date,
            PermissionType = source.PermissionType.ToDto()
        };
    }

    public static Permission ToEntity(this PermissionDto source)
    {
        if (source == null) return null;
        return new Permission
        {
            Id = source.Id,
            Name = source.Name,
            LastName = source.LastName,
            Date = source.Date ?? default,
            PermissionType = source.PermissionType.ToEntity()
        };
    }

    public static PermissionTypeDto ToDto(this PermissionType source)
    {
        if (source == null) return null;
        return new PermissionTypeDto
        {
            Id = source.Id,
            Description = source.Description
        };
    }

    public static PermissionType ToEntity(this PermissionTypeDto source)
    {
        if (source == null) return null;
        return new PermissionType
        {
            Id = source.Id,
            Description = source.Description
        };
    }

    public static IEnumerable<PermissionDto> ToDto(this IEnumerable<Permission> source)
    {
        return source?.Select(x => x.ToDto());
    }

    public static IEnumerable<PermissionTypeDto> ToDto(this IEnumerable<PermissionType> source)
    {
        return source?.Select(x => x.ToDto());
    }
}
