using AutoMapper;
using ITL.Domain;
using ITL.Domain.Common;
using ITL.Domain.DTOs;
using ITL.Domain.Entities;
using ITL.Domain.Exceptions;
using ITL.Domain.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITL.Application.Services;

public class PermissionService : IPermissionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PermissionService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<PermissionDto> AddPermission(PermissionDto permissionDto)
    {
        var permission = _mapper.Map<Permission>(permissionDto);
        permission.PermissionType = await _unitOfWork.PermissionTypeRepository.GetByIdAsync(permission.PermissionType.Id);

        await _unitOfWork.PermissionRepository.AddAsync(permission);
        await _unitOfWork.SaveAsync();

        return _mapper.Map<PermissionDto>(permission);
    }

    public async Task UpdatePermission(PermissionDto permissionDto)
    {
        var exists = await _unitOfWork.PermissionRepository.ExistAsync(x => x.Id == permissionDto.Id);
        if (!exists)
            throw new ConflictException("The permission doesn't exist");

        var permissionTypeExist = await _unitOfWork.PermissionTypeRepository.ExistAsync(x => x.Id == permissionDto.PermissionType.Id);

        if (!permissionTypeExist)
            throw new ConflictException("The permission type doesn't exist.");

        var permission = _mapper.Map<Permission>(permissionDto);
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
        var permissionDto = _mapper.Map<PermissionDto>(permission);

        return permissionDto;
    }

    public async Task<IEnumerable<PermissionDto>> GetPermissions()
    {
        var permissions = await _unitOfWork.PermissionRepository.GetAllAsync(entitiesToInclude: new List<string> { "PermissionType" });
        var permissionsDto = _mapper.Map<IEnumerable<PermissionDto>>(permissions);

        return permissionsDto;
    }

    public async Task<IEnumerable<PermissionDto>> GetPermissions(Pagination pagination)
    {
        var permissions = await _unitOfWork.PermissionRepository.GetAllAsync(pagination.Skip, pagination.Limit, entitiesToInclude: new List<string> { "PermissionType" });
        var permissionsDto = _mapper.Map<IEnumerable<PermissionDto>>(permissions);

        return permissionsDto;
    }
}
