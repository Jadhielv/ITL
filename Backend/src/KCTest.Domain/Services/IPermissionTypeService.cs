using KCTest.Domain.Common;
using KCTest.Domain.DTOs;
using System.Threading.Tasks;

namespace KCTest.Domain.Services
{
    public interface IPermissionTypeService
    {
        Task<Result<HttpResponse>> AddPermissionType(PermissionTypeDto permissionDto);
        Task<Result<HttpResponse>> UpdatePermissionType(PermissionTypeDto permissionDto);
        Task<Result<HttpResponse>> DeletePermissionType(int permissionId);
        Task<Result<HttpResponseWithElement<PermissionTypeDto>>> GetPermissionType(int permissionId);
        Task<Result<HttpResponseWithList<PermissionTypeDto>>> GetPermissionTypes(Pagination pagination = null);
    }
}