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

public class PermissionTypeService : IPermissionTypeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PermissionTypeService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PermissionTypeDto> AddPermissionType(PermissionTypeDto permissionTypeDto)
    {
        var exists = await _unitOfWork.PermissionTypeRepository.ExistAsync(x => x.Id == permissionTypeDto.Id);
        if (exists)
            throw new ConflictException("The permission type already exist.");

        var permissionType = _mapper.Map<PermissionType>(permissionTypeDto);

        await _unitOfWork.PermissionTypeRepository.AddAsync(permissionType);
        await _unitOfWork.SaveAsync();

        return _mapper.Map<PermissionTypeDto>(permissionType);
    }

    public async Task UpdatePermissionType(PermissionTypeDto permissionTypeDto)
    {
        var exists = await _unitOfWork.PermissionTypeRepository.ExistAsync(x => x.Id == permissionTypeDto.Id);
        if (!exists)
            throw new ConflictException("The permission type doesn't exist.");

        var permissionType = _mapper.Map<PermissionType>(permissionTypeDto);
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
        var permissionTypeDto = _mapper.Map<PermissionTypeDto>(permissionType);

        return permissionTypeDto;
    }

    public async Task<IEnumerable<PermissionTypeDto>> GetPermissionTypes()
    {
        var permissionsType = await _unitOfWork.PermissionTypeRepository.GetAllAsync();
        var permissionsTypeDto = _mapper.Map<IEnumerable<PermissionTypeDto>>(permissionsType);

        return permissionsTypeDto;
    }

    public async Task<IEnumerable<PermissionTypeDto>> GetPermissionTypes(Pagination pagination)
    {
        var permissionsType = await _unitOfWork.PermissionTypeRepository.GetAllAsync(pagination.Skip, pagination.Limit);
        var permissionsTypeDto = _mapper.Map<IEnumerable<PermissionTypeDto>>(permissionsType);

        return permissionsTypeDto;
    }
}
