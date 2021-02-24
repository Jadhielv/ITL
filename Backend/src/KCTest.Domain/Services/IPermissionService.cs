using KCTest.Domain.Common;
using KCTest.Domain.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KCTest.Domain.Services
{
    public interface IPermissionService
    {
        Task<PermissionDto> AddPermission(PermissionDto permissionDto);
        Task UpdatePermission(PermissionDto permissionDto);
        Task DeletePermission(int permissionId);
        Task<PermissionDto> GetPermission(int permissionId);
        Task<IEnumerable<PermissionDto>> GetPermissions();
        Task<IEnumerable<PermissionDto>> GetPermissions(Pagination pagination);
    }
}