using ITL.Domain.Common;
using ITL.Domain.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITL.Domain.Services;

public interface IPermissionService
{
    Task<PermissionDto> AddPermission(PermissionDto permissionDto);
    Task UpdatePermission(PermissionDto permissionDto);
    Task DeletePermission(int permissionId);
    Task<PermissionDto> GetPermission(int permissionId);
    Task<IEnumerable<PermissionDto>> GetPermissions();
    Task<IEnumerable<PermissionDto>> GetPermissions(Pagination pagination);
}
