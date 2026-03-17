using ITL.Domain;
using ITL.Domain.DTOs;
using ITL.Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ITL.Infrastructure.Mapper;

public class CustomMapper : IMapper
{
    public TDestination Map<TDestination>(object source)
    {
        if (source == null)
            return default;

        var sourceType = source.GetType();
        var destinationType = typeof(TDestination);

        // Simple mappings
        if (sourceType == typeof(Permission) && destinationType == typeof(PermissionDto))
            return (TDestination)(object)MapToPermissionDto((Permission)source);

        if (sourceType == typeof(PermissionDto) && destinationType == typeof(Permission))
            return (TDestination)(object)MapToPermission((PermissionDto)source);

        if (sourceType == typeof(PermissionType) && destinationType == typeof(PermissionTypeDto))
            return (TDestination)(object)MapToPermissionTypeDto((PermissionType)source);

        if (sourceType == typeof(PermissionTypeDto) && destinationType == typeof(PermissionType))
            return (TDestination)(object)MapToPermissionType((PermissionTypeDto)source);

        // Collection mappings
        if (typeof(IEnumerable).IsAssignableFrom(sourceType) && typeof(IEnumerable).IsAssignableFrom(destinationType))
        {
            var sourceItems = (IEnumerable)source;
            var destItemType = destinationType.IsGenericType
                ? destinationType.GetGenericArguments()[0]
                : destinationType.GetElementType();

            if (destItemType == null)
                throw new NotSupportedException($"Cannot determine element type for {destinationType.Name}");

            var mappedItems = sourceItems.Cast<object>()
                .Select(item => MapInternal(item, destItemType))
                .ToList();

            if (destinationType.IsArray)
            {
                var array = Array.CreateInstance(destItemType, mappedItems.Count);
                for (int i = 0; i < mappedItems.Count; i++)
                    array.SetValue(mappedItems[i], i);
                return (TDestination)(object)array;
            }

            if (destinationType.IsGenericType &&
                (destinationType.GetGenericTypeDefinition() == typeof(IEnumerable<>) ||
                 destinationType.GetGenericTypeDefinition() == typeof(List<>) ||
                 destinationType.GetGenericTypeDefinition() == typeof(IReadOnlyList<>) ||
                 destinationType.GetGenericTypeDefinition() == typeof(IList<>)))
            {
                var listType = typeof(List<>).MakeGenericType(destItemType);
                var list = Activator.CreateInstance(listType);
                var addMethod = listType.GetMethod("Add");
                foreach (var item in mappedItems)
                    addMethod.Invoke(list, new[] { item });

                return (TDestination)list;
            }
        }

        throw new NotSupportedException($"Mapping from {sourceType.Name} to {destinationType.Name} is not supported.");
    }

    private object MapInternal(object source, Type destinationType)
    {
        if (source == null) return null;
        var method = typeof(CustomMapper).GetMethod(nameof(Map)).MakeGenericMethod(destinationType);
        return method.Invoke(this, new[] { source });
    }

    private PermissionDto MapToPermissionDto(Permission source)
    {
        if (source == null) return null;
        return new PermissionDto
        {
            Id = source.Id,
            Name = source.Name,
            LastName = source.LastName,
            Date = source.Date,
            PermissionType = MapToPermissionTypeDto(source.PermissionType)
        };
    }

    private Permission MapToPermission(PermissionDto source)
    {
        if (source == null) return null;
        return new Permission
        {
            Id = source.Id,
            Name = source.Name,
            LastName = source.LastName,
            Date = source.Date ?? default,
            PermissionType = MapToPermissionType(source.PermissionType)
        };
    }

    private PermissionTypeDto MapToPermissionTypeDto(PermissionType source)
    {
        if (source == null) return null;
        return new PermissionTypeDto
        {
            Id = source.Id,
            Description = source.Description
        };
    }

    private PermissionType MapToPermissionType(PermissionTypeDto source)
    {
        if (source == null) return null;
        return new PermissionType
        {
            Id = source.Id,
            Description = source.Description
        };
    }
}
