using KCTest.Domain.Common;
using KCTest.Domain.DTOs;
using System.Threading.Tasks;

namespace KCTest.Domain.Services
{
    public interface IPermissionService
    {
        Task<Result<ResponseWithElement<PermissionDto>>> AddPermission(PermissionDto permissionDto);
        Task<Result<Response>> UpdatePermission(PermissionDto permissionDto);
        Task<Result<Response>> DeletePermission(int permissionId);
        Task<Result<ResponseWithElement<PermissionDto>>> GetPermission(int permissionId);
        Task<Result<ResponseWithList<PermissionDto>>> GetPermissions(Pagination pagination = null);
    }
}