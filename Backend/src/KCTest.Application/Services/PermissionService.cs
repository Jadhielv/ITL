using AutoMapper;
using KCTest.Domain;
using KCTest.Domain.Common;
using KCTest.Domain.DTOs;
using KCTest.Domain.Entities;
using KCTest.Domain.Services;
using KCTest.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KCTest.Application.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PermissionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<HttpResponse>> AddPermission(PermissionDto permissionDto)
        {
            try
            {
                var permission = _mapper.Map<Permission>(permissionDto);
                var permissionType = _unitOfWork.PermissionTypeRepository.GetByIdAsync(permission.PermissionType.Id).Result;

                if (permissionType != null)
                    permission.PermissionType = permissionType;

                await _unitOfWork.PermissionRepository.AddAsync(permission);
                await _unitOfWork.SaveAsync();

                return HttpResponseHelper.NewResult(HttpStatusCode.Created, HttpResponseHelper.NewHttpResponse("New permission added", success: true));
            }
            catch (Exception e)
            {
                return HttpResponseHelper.NewResult(HttpStatusCode.InternalServerError, HttpResponseHelper.NewHttpResponse(error: e.Message));
            }
        }

        public async Task<Result<HttpResponse>> UpdatePermission(PermissionDto permissionDto)
        {
            try
            {
                var exists = await _unitOfWork.PermissionRepository.ExistAsync(x => x.Id == permissionDto.Id);
                if (!exists)
                    return HttpResponseHelper.NewResult(HttpStatusCode.Conflict, HttpResponseHelper.NewHttpResponse(error: "The permission doesn't exist."));

                var permissionTypeExist = await _unitOfWork.PermissionTypeRepository.ExistAsync(x => x.Id == permissionDto.PermissionType.Id);
                if (!permissionTypeExist)
                    return HttpResponseHelper.NewResult(HttpStatusCode.Conflict, HttpResponseHelper.NewHttpResponse(error: "The permission type doesn't exist."));

                var permission = _mapper.Map<Permission>(permissionDto);
                await _unitOfWork.PermissionRepository.UpdateAsync(permission);
                await _unitOfWork.SaveAsync();

                return HttpResponseHelper.NewResult(HttpStatusCode.OK, HttpResponseHelper.NewHttpResponse("The permission was updated it", success: true));
            }
            catch (Exception e)
            {
                return HttpResponseHelper.NewResult(HttpStatusCode.InternalServerError, HttpResponseHelper.NewHttpResponse(error: e.Message));
            }
        }

        public async Task<Result<HttpResponse>> DeletePermission(int permissionId)
        {
            try
            {
                var exists = await _unitOfWork.PermissionRepository.ExistAsync(x => x.Id == permissionId);
                if (!exists)
                    return HttpResponseHelper.NewResult(HttpStatusCode.BadRequest, HttpResponseHelper.NewHttpResponse(error: "The permission doesn't exist."));

                var permission = await _unitOfWork.PermissionRepository.GetByIdAsync(permissionId);
                await _unitOfWork.PermissionRepository.DeleteAsync(permission);
                await _unitOfWork.SaveAsync();

                return HttpResponseHelper.NewResult(HttpStatusCode.OK, HttpResponseHelper.NewHttpResponse(message: "The permission was deleted it.", success: true));
            }
            catch (Exception e)
            {
                return HttpResponseHelper.NewResult(HttpStatusCode.InternalServerError, HttpResponseHelper.NewHttpResponse(error: e.Message));
            }
        }

        public async Task<Result<HttpResponseWithElement<PermissionDto>>> GetPermission(int permissionId)
        {
            try
            {
                var exists = await _unitOfWork.PermissionRepository.ExistAsync(x => x.Id == permissionId);
                if (!exists)
                    return HttpResponseHelper.NewResult(HttpStatusCode.BadRequest, HttpResponseHelper.NewHttpResponseWithElement<PermissionDto>(error: "The permission doesn't exist."));

                var permission = await _unitOfWork.PermissionRepository.GetByIdAsync(permissionId, new List<string> { "PermissionType" });
                var permissionDto = _mapper.Map<PermissionDto>(permission);

                return HttpResponseHelper.NewResult(HttpStatusCode.OK, HttpResponseHelper.NewHttpResponseWithElement(element: permissionDto, success: true));
            }
            catch (Exception e)
            {
                return HttpResponseHelper.NewResult(HttpStatusCode.InternalServerError, HttpResponseHelper.NewHttpResponseWithElement<PermissionDto>(error: e.Message));
            }
        }

        public async Task<Result<HttpResponseWithList<PermissionDto>>> GetPermissions(Pagination pagination = null)
        {
            try
            {
                var permissions = await _unitOfWork.PermissionRepository.GetAllAsync(entitiesToInclude: new List<string> { "PermissionType" });
                var permissionsDto = _mapper.Map<IEnumerable<PermissionDto>>(permissions);

                return HttpResponseHelper.NewResult(HttpStatusCode.OK, HttpResponseHelper.NewHttpResponseList(elements: permissionsDto, success: true));
            }
            catch (Exception e)
            {
                return HttpResponseHelper.NewResult(HttpStatusCode.InternalServerError, HttpResponseHelper.NewHttpResponseList<PermissionDto>(error: e.Message));
            }
        }
    }
}