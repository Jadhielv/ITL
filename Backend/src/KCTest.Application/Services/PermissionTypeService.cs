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
        public async Task<Result<HttpResponse>> AddPermissionType(PermissionTypeDto permissionTypeDto)
        {
            try
            {
                var exists = await _unitOfWork.PermissionTypeRepository.ExistAsync(x => x.Id == permissionTypeDto.Id);
                if (exists)
                    return HttpResponseHelper.NewResult(HttpStatusCode.Conflict, HttpResponseHelper.NewHttpResponse(error: "The permission type already exist."));

                var permissionType = _mapper.Map<PermissionType>(permissionTypeDto);

                await _unitOfWork.PermissionTypeRepository.AddAsync(permissionType);
                await _unitOfWork.SaveAsync();

                return HttpResponseHelper.NewResult(HttpStatusCode.Created, HttpResponseHelper.NewHttpResponse("New permission type added", success: true));
            }
            catch (Exception e)
            {
                return HttpResponseHelper.NewResult(HttpStatusCode.InternalServerError, HttpResponseHelper.NewHttpResponse(error: e.Message));
            }
        }

        public async Task<Result<HttpResponse>> UpdatePermissionType(PermissionTypeDto permissionTypeDto)
        {
            try
            {
                var exists = await _unitOfWork.PermissionTypeRepository.ExistAsync(x => x.Id == permissionTypeDto.Id);
                if (!exists)
                    return HttpResponseHelper.NewResult(HttpStatusCode.Conflict, HttpResponseHelper.NewHttpResponse(error: "The permission type doesn't exist."));

                var permissionType = _mapper.Map<PermissionType>(permissionTypeDto);
                await _unitOfWork.PermissionTypeRepository.UpdateAsync(permissionType);
                await _unitOfWork.SaveAsync();

                return HttpResponseHelper.NewResult(HttpStatusCode.OK, HttpResponseHelper.NewHttpResponse("The permission type was updated it", success: true));
            }
            catch (Exception e)
            {
                return HttpResponseHelper.NewResult(HttpStatusCode.InternalServerError, HttpResponseHelper.NewHttpResponse(error: e.Message));
            }
        }

        public async Task<Result<HttpResponse>> DeletePermissionType(int permissionTypeId)
        {
            try
            {
                var exists = await _unitOfWork.PermissionTypeRepository.ExistAsync(x => x.Id == permissionTypeId);
                if (!exists)
                    return HttpResponseHelper.NewResult(HttpStatusCode.BadRequest, HttpResponseHelper.NewHttpResponse(error: "The permission type doesn't exist."));

                var permissionType = await _unitOfWork.PermissionTypeRepository.GetByIdAsync(permissionTypeId);
                await _unitOfWork.PermissionTypeRepository.DeleteAsync(permissionType);
                await _unitOfWork.SaveAsync();

                return HttpResponseHelper.NewResult(HttpStatusCode.OK, HttpResponseHelper.NewHttpResponse(message: "The permission type was deleted it.", success: true));
            }
            catch (Exception e)
            {
                return HttpResponseHelper.NewResult(HttpStatusCode.InternalServerError, HttpResponseHelper.NewHttpResponse(error: e.Message));
            }
        }

        public async Task<Result<HttpResponseWithElement<PermissionTypeDto>>> GetPermissionType(int permissionTypeId)
        {
            try
            {
                var exists = await _unitOfWork.PermissionTypeRepository.ExistAsync(x => x.Id == permissionTypeId);
                if (!exists)
                    return HttpResponseHelper.NewResult(HttpStatusCode.BadRequest, HttpResponseHelper.NewHttpResponseWithElement<PermissionTypeDto>(error: "The permission type doesn't exist."));

                var permissionType = await _unitOfWork.PermissionTypeRepository.GetByIdAsync(permissionTypeId);
                var permissionTypeDto = _mapper.Map<PermissionTypeDto>(permissionType);

                return HttpResponseHelper.NewResult(HttpStatusCode.OK, HttpResponseHelper.NewHttpResponseWithElement(element: permissionTypeDto, success: true));
            }
            catch (Exception e)
            {
                return HttpResponseHelper.NewResult(HttpStatusCode.InternalServerError, HttpResponseHelper.NewHttpResponseWithElement<PermissionTypeDto>(error: e.Message));
            }
        }

        public async Task<Result<HttpResponseWithList<PermissionTypeDto>>> GetPermissionTypes(Pagination pagination = null)
        {
            try
            {
                var permissionsType = await _unitOfWork.PermissionTypeRepository.GetAllAsync();
                var permissionsTypeDto = _mapper.Map<IEnumerable<PermissionTypeDto>>(permissionsType);

                return HttpResponseHelper.NewResult(HttpStatusCode.OK, HttpResponseHelper.NewHttpResponseList(elements: permissionsTypeDto, success: true));
            }
            catch (Exception e)
            {
                return HttpResponseHelper.NewResult(HttpStatusCode.InternalServerError, HttpResponseHelper.NewHttpResponseList<PermissionTypeDto>(error: e.Message));
            }
        }
    }
}
