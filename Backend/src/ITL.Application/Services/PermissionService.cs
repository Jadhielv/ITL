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

public class PermissionService : IPermissionService
{
    private readonly IUnitOfWork _unitOfWork;

    public PermissionService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PermissionDto> AddPermission(PermissionDto permissionDto)
    {
        var permission = permissionDto.ToEntity();
        permission.PermissionType = await _unitOfWork.PermissionTypeRepository.GetByIdAsync(permission.PermissionType.Id);

        await _unitOfWork.PermissionRepository.AddAsync(permission);
        await _unitOfWork.SaveAsync();

        return permission.ToDto();
    }

    public async Task UpdatePermission(PermissionDto permissionDto)
    {
        var exists = await _unitOfWork.PermissionRepository.ExistAsync(x => x.Id == permissionDto.Id);
        if (!exists)
            throw new ConflictException("The permission doesn't exist");

        var permissionTypeExist = await _unitOfWork.PermissionTypeRepository.ExistAsync(x => x.Id == permissionDto.PermissionType.Id);

        if (!permissionTypeExist)
            throw new ConflictException("The permission type doesn't exist.");

        var permission = permissionDto.ToEntity();
        await _unitOfWork.PermissionRepository.UpdateAsync(permission);
        await _unitOfWork.SaveAsync();
    }

    public async Task DeletePermission(int permissionId)
    {
        var exists = await _unitOfWork.PermissionRepository.ExistAsync(x => x.Id == permissionId);
        if (!exists)
            throw new NotFoundException("The permission doesn't exist.");

        var permission = await _unitOfWork.PermissionRepository.GetByIdAsync(permissionId);
        await _unitOfWork.PermissionRepository.DeleteAsync(permission);
        await _unitOfWork.SaveAsync();
    }

    public async Task<PermissionDto> GetPermission(int permissionId)
    {
        var exists = await _unitOfWork.PermissionRepository.ExistAsync(x => x.Id == permissionId);
        if (!exists)
            throw new NotFoundException("The permission doesn't exist.");

        var permission = await _unitOfWork.PermissionRepository.GetByIdAsync(permissionId, new List<string> { "PermissionType" });
        var permissionDto = permission.ToDto();

        return permissionDto;
    }

    public async Task<IEnumerable<PermissionDto>> GetPermissions()
    {
        var permissions = await _unitOfWork.PermissionRepository.GetAllAsync(entitiesToInclude: new List<string> { "PermissionType" });
        var permissionsDto = permissions.ToDto();

        return permissionsDto;
    }

    public async Task<IEnumerable<PermissionDto>> GetPermissions(Pagination pagination)
    {
        var permissions = await _unitOfWork.PermissionRepository.GetAllAsync(pagination.Skip, pagination.Limit, entitiesToInclude: new List<string> { "PermissionType" });
        var permissionsDto = permissions.ToDto();

        return permissionsDto;
    }
}
