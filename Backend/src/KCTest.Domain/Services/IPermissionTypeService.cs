using KCTest.Domain.Common;
using KCTest.Domain.DTOs;
using System.Threading.Tasks;

namespace KCTest.Domain.Services
{
    public interface IPermissionTypeService
    {
        Task<Result<ResponseWithElement<PermissionDto>>> AddPermissionType(PermissionTypeDto permissionDto);
        Task<Result<Response>> UpdatePermissionType(PermissionTypeDto permissionDto);
        Task<Result<Response>> DeletePermissionType(int permissionId);
        Task<Result<ResponseWithElement<PermissionTypeDto>>> GetPermissionType(int permissionId);
        Task<Result<ResponseWithList<PermissionTypeDto>>> GetPermissionTypes(Pagination pagination = null);
    }
}