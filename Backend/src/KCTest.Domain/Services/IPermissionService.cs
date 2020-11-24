using KCTest.Domain.Common;
using KCTest.Domain.DTOs;
using System.Threading.Tasks;

namespace KCTest.Domain.Services
{
    public interface IPermissionService
    {
        Task<Result<HttpResponse>> AddPermission(PermissionDto permissionDto);
        Task<Result<HttpResponse>> UpdatePermission(PermissionDto permissionDto);
        Task<Result<HttpResponse>> DeletePermission(int permissionId);
        Task<Result<HttpResponseWithElement<PermissionDto>>> GetPermission(int permissionId);
        Task<Result<HttpResponseWithList<PermissionDto>>> GetPermissions(Pagination pagination = null);
    }
}