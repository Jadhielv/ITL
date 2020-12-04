using KCTest.Domain.Common;
using KCTest.Domain.DTOs;
using System.Threading.Tasks;

namespace KCTest.Domain.Services
{
    public interface IPermissionTypeService
    {
        Task<Result<ResponseWithElement<PermissionTypeDto>>> AddPermissionType(PermissionTypeDto permissionTypeDto);
        Task<Result<Response>> UpdatePermissionType(PermissionTypeDto permissionTypeDto);
        Task<Result<Response>> DeletePermissionType(int permissionTypeId);
        Task<Result<ResponseWithElement<PermissionTypeDto>>> GetPermissionType(int permissionTypeId);
        Task<Result<ResponseWithList<PermissionTypeDto>>> GetPermissionTypes(Pagination pagination = null);
    }
}