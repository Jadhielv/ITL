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
    public class PermissionTypeService : IPermissionTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PermissionTypeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<ResponseWithElement<PermissionTypeDto>>> AddPermissionType(PermissionTypeDto permissionTypeDto)
        {
            try
            {
                var exists = await _unitOfWork.PermissionTypeRepository.ExistAsync(x => x.Id == permissionTypeDto.Id);
                if (exists)
                    return ResponseHelper.NewResult(HttpStatusCode.Conflict, ResponseHelper.NewResponseWithElement<PermissionTypeDto>(error: "The permission type already exist."));

                var permissionType = _mapper.Map<PermissionType>(permissionTypeDto);

                await _unitOfWork.PermissionTypeRepository.AddAsync(permissionType);
                await _unitOfWork.SaveAsync();

                return ResponseHelper.NewResult(HttpStatusCode.Created, ResponseHelper.NewResponseWithElement(message: "New permission type added", element: _mapper.Map<PermissionTypeDto>(permissionType), success: true));
            }
            catch (Exception e)
            {
                return ResponseHelper.NewResult(HttpStatusCode.InternalServerError, ResponseHelper.NewResponseWithElement<PermissionTypeDto>(error: e.Message));
            }
        }

        public async Task<Result<Response>> UpdatePermissionType(PermissionTypeDto permissionTypeDto)
        {
            try
            {
                var exists = await _unitOfWork.PermissionTypeRepository.ExistAsync(x => x.Id == permissionTypeDto.Id);
                if (!exists)
                    return ResponseHelper.NewResult(HttpStatusCode.Conflict, ResponseHelper.NewResponse(error: "The permission type doesn't exist."));

                var permissionType = _mapper.Map<PermissionType>(permissionTypeDto);
                await _unitOfWork.PermissionTypeRepository.UpdateAsync(permissionType);
                await _unitOfWork.SaveAsync();

                return ResponseHelper.NewResult(HttpStatusCode.OK, ResponseHelper.NewResponse("The permission type was updated it", success: true));
            }
            catch (Exception e)
            {
                return ResponseHelper.NewResult(HttpStatusCode.InternalServerError, ResponseHelper.NewResponse(error: e.Message));
            }
        }

        public async Task<Result<Response>> DeletePermissionType(int permissionTypeId)
        {
            try
            {
                var exists = await _unitOfWork.PermissionTypeRepository.ExistAsync(x => x.Id == permissionTypeId);
                if (!exists)
                    return ResponseHelper.NewResult(HttpStatusCode.BadRequest, ResponseHelper.NewResponse(error: "The permission type doesn't exist."));

                var permissionType = await _unitOfWork.PermissionTypeRepository.GetByIdAsync(permissionTypeId);
                await _unitOfWork.PermissionTypeRepository.DeleteAsync(permissionType);
                await _unitOfWork.SaveAsync();

                return ResponseHelper.NewResult(HttpStatusCode.OK, ResponseHelper.NewResponse(message: "The permission type was deleted it.", success: true));
            }
            catch (Exception e)
            {
                return ResponseHelper.NewResult(HttpStatusCode.InternalServerError, ResponseHelper.NewResponse(error: e.Message));
            }
        }

        public async Task<Result<ResponseWithElement<PermissionTypeDto>>> GetPermissionType(int permissionTypeId)
        {
            try
            {
                var exists = await _unitOfWork.PermissionTypeRepository.ExistAsync(x => x.Id == permissionTypeId);
                if (!exists)
                    return ResponseHelper.NewResult(HttpStatusCode.BadRequest, ResponseHelper.NewResponseWithElement<PermissionTypeDto>(error: "The permission type doesn't exist."));

                var permissionType = await _unitOfWork.PermissionTypeRepository.GetByIdAsync(permissionTypeId);
                var permissionTypeDto = _mapper.Map<PermissionTypeDto>(permissionType);

                return ResponseHelper.NewResult(HttpStatusCode.OK, ResponseHelper.NewResponseWithElement(element: permissionTypeDto, success: true));
            }
            catch (Exception e)
            {
                return ResponseHelper.NewResult(HttpStatusCode.InternalServerError, ResponseHelper.NewResponseWithElement<PermissionTypeDto>(error: e.Message));
            }
        }

        public async Task<Result<ResponseWithList<PermissionTypeDto>>> GetPermissionTypes(Pagination pagination = null)
        {
            try
            {
                var permissionsType = await _unitOfWork.PermissionTypeRepository.GetAllAsync();
                var permissionsTypeDto = _mapper.Map<IEnumerable<PermissionTypeDto>>(permissionsType);

                return ResponseHelper.NewResult(HttpStatusCode.OK, ResponseHelper.NewResponseList(elements: permissionsTypeDto, success: true));
            }
            catch (Exception e)
            {
                return ResponseHelper.NewResult(HttpStatusCode.InternalServerError, ResponseHelper.NewResponseList<PermissionTypeDto>(error: e.Message));
            }
        }
    }
}
