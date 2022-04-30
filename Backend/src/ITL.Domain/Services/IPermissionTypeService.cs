using ITL.Domain.Common;
using ITL.Domain.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITL.Domain.Services;

public interface IPermissionTypeService
{
    Task<PermissionTypeDto> AddPermissionType(PermissionTypeDto permissionTypeDto);
    Task UpdatePermissionType(PermissionTypeDto permissionTypeDto);
    Task DeletePermissionType(int permissionTypeId);
    Task<PermissionTypeDto> GetPermissionType(int permissionTypeId);
    Task<IEnumerable<PermissionTypeDto>> GetPermissionTypes();
    Task<IEnumerable<PermissionTypeDto>> GetPermissionTypes(Pagination pagination);
}
