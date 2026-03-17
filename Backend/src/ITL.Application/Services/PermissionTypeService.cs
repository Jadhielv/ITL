using ITL.Domain;
using ITL.Domain.Common;
using ITL.Domain.DTOs;
using ITL.Domain.Entities;
using ITL.Domain.Exceptions;
using ITL.Domain.Services;
using ITL.Domain.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITL.Application.Services;

public class PermissionTypeService : IPermissionTypeService
{
    private readonly IUnitOfWork _unitOfWork;

    public PermissionTypeService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PermissionTypeDto> AddPermissionType(PermissionTypeDto permissionTypeDto)
    {
        var exists = await _unitOfWork.PermissionTypeRepository.ExistAsync(x => x.Id == permissionTypeDto.Id);
        if (exists)
            throw new ConflictException("The permission type already exist.");

        var permissionType = permissionTypeDto.ToEntity();

        await _unitOfWork.PermissionTypeRepository.AddAsync(permissionType);
        await _unitOfWork.SaveAsync();

        return permissionType.ToDto();
    }

    public async Task UpdatePermissionType(PermissionTypeDto permissionTypeDto)
    {
        var exists = await _unitOfWork.PermissionTypeRepository.ExistAsync(x => x.Id == permissionTypeDto.Id);
        if (!exists)
            throw new ConflictException("The permission type doesn't exist.");

        var permissionType = permissionTypeDto.ToEntity();
        await _unitOfWork.PermissionTypeRepository.UpdateAsync(permissionType);
        await _unitOfWork.SaveAsync();
    }

    public async Task DeletePermissionType(int permissionTypeId)
    {
        var exists = await _unitOfWork.PermissionTypeRepository.ExistAsync(x => x.Id == permissionTypeId);
        if (!exists)
            throw new NotFoundException("The permission type doesn't exist.");

        var permissionType = await _unitOfWork.PermissionTypeRepository.GetByIdAsync(permissionTypeId);
        await _unitOfWork.PermissionTypeRepository.DeleteAsync(permissionType);
        await _unitOfWork.SaveAsync();
    }

    public async Task<PermissionTypeDto> GetPermissionType(int permissionTypeId)
    {
        var exists = await _unitOfWork.PermissionTypeRepository.ExistAsync(x => x.Id == permissionTypeId);
        if (!exists)
            throw new NotFoundException("The permission type doesn't exist.");

        var permissionType = await _unitOfWork.PermissionTypeRepository.GetByIdAsync(permissionTypeId);
        var permissionTypeDto = permissionType.ToDto();

        return permissionTypeDto;
    }

    public async Task<IEnumerable<PermissionTypeDto>> GetPermissionTypes()
    {
        var permissionsType = await _unitOfWork.PermissionTypeRepository.GetAllAsync();
        var permissionsTypeDto = permissionsType.ToDto();

        return permissionsTypeDto;
    }

    public async Task<IEnumerable<PermissionTypeDto>> GetPermissionTypes(Pagination pagination)
    {
        var permissionsType = await _unitOfWork.PermissionTypeRepository.GetAllAsync(pagination.Skip, pagination.Limit);
        var permissionsTypeDto = permissionsType.ToDto();

        return permissionsTypeDto;
    }
}
