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

        public async Task<Result<ResponseWithElement<PermissionDto>>> AddPermission(PermissionDto permissionDto)
        {
            try
            {
                var permission = _mapper.Map<Permission>(permissionDto);
                var permissionType = _unitOfWork.PermissionTypeRepository.GetByIdAsync(permission.PermissionType.Id).Result;

                if (permissionType != null)
                    permission.PermissionType = permissionType;

                await _unitOfWork.PermissionRepository.AddAsync(permission);
                await _unitOfWork.SaveAsync();

                return ResponseHelper.NewResult(HttpStatusCode.Created, ResponseHelper.NewResponseWithElement(message: "New permission added", element: _mapper.Map<PermissionDto>(permissionDto), success: true));
            }
            catch (Exception e)
            {
                return ResponseHelper.NewResult(HttpStatusCode.InternalServerError, ResponseHelper.NewResponseWithElement<PermissionDto>(error: e.Message));
            }
        }

        public async Task<Result<Response>> UpdatePermission(PermissionDto permissionDto)
        {
            try
            {
                var exists = await _unitOfWork.PermissionRepository.ExistAsync(x => x.Id == permissionDto.Id);
                if (!exists)
                    return ResponseHelper.NewResult(HttpStatusCode.Conflict, ResponseHelper.NewResponse(error: "The permission doesn't exist."));

                var permissionTypeExist = await _unitOfWork.PermissionTypeRepository.ExistAsync(x => x.Id == permissionDto.PermissionType.Id);
                if (!permissionTypeExist)
                    return ResponseHelper.NewResult(HttpStatusCode.Conflict, ResponseHelper.NewResponse(error: "The permission type doesn't exist."));

                var permission = _mapper.Map<Permission>(permissionDto);
                await _unitOfWork.PermissionRepository.UpdateAsync(permission);
                await _unitOfWork.SaveAsync();

                return ResponseHelper.NewResult(HttpStatusCode.OK, ResponseHelper.NewResponse("The permission was updated it", success: true));
            }
            catch (Exception e)
            {
                return ResponseHelper.NewResult(HttpStatusCode.InternalServerError, ResponseHelper.NewResponse(error: e.Message));
            }
        }

        public async Task<Result<Response>> DeletePermission(int permissionId)
        {
            try
            {
                var exists = await _unitOfWork.PermissionRepository.ExistAsync(x => x.Id == permissionId);
                if (!exists)
                    return ResponseHelper.NewResult(HttpStatusCode.BadRequest, ResponseHelper.NewResponse(error: "The permission doesn't exist."));

                var permission = await _unitOfWork.PermissionRepository.GetByIdAsync(permissionId);
                await _unitOfWork.PermissionRepository.DeleteAsync(permission);
                await _unitOfWork.SaveAsync();

                return ResponseHelper.NewResult(HttpStatusCode.OK, ResponseHelper.NewResponse(message: "The permission was deleted it.", success: true));
            }
            catch (Exception e)
            {
                return ResponseHelper.NewResult(HttpStatusCode.InternalServerError, ResponseHelper.NewResponse(error: e.Message));
            }
        }

        public async Task<Result<ResponseWithElement<PermissionDto>>> GetPermission(int permissionId)
        {
            try
            {
                var exists = await _unitOfWork.PermissionRepository.ExistAsync(x => x.Id == permissionId);
                if (!exists)
                    return ResponseHelper.NewResult(HttpStatusCode.BadRequest, ResponseHelper.NewResponseWithElement<PermissionDto>(error: "The permission doesn't exist."));

                var permission = await _unitOfWork.PermissionRepository.GetByIdAsync(permissionId, new List<string> { "PermissionType" });
                var permissionDto = _mapper.Map<PermissionDto>(permission);

                return ResponseHelper.NewResult(HttpStatusCode.OK, ResponseHelper.NewResponseWithElement(element: permissionDto, success: true));
            }
            catch (Exception e)
            {
                return ResponseHelper.NewResult(HttpStatusCode.InternalServerError, ResponseHelper.NewResponseWithElement<PermissionDto>(error: e.Message));
            }
        }

        public async Task<Result<ResponseWithList<PermissionDto>>> GetPermissions(Pagination pagination = null)
        {
            try
            {
                var permissions = await _unitOfWork.PermissionRepository.GetAllAsync(entitiesToInclude: new List<string> { "PermissionType" });
                var permissionsDto = _mapper.Map<IEnumerable<PermissionDto>>(permissions);

                return ResponseHelper.NewResult(HttpStatusCode.OK, ResponseHelper.NewResponseList(elements: permissionsDto, success: true));
            }
            catch (Exception e)
            {
                return ResponseHelper.NewResult(HttpStatusCode.InternalServerError, ResponseHelper.NewResponseList<PermissionDto>(error: e.Message));
            }
        }
    }
}