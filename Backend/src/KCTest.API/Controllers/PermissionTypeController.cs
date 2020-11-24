using KCTest.Domain.DTOs;
using KCTest.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KCTest.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionTypeController : ControllerBase
    {
        private readonly IPermissionTypeService _permissionTypeService;

        public PermissionTypeController(IPermissionTypeService permissionTypeService)
        {
            _permissionTypeService = permissionTypeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PermissionTypeDto>>> Get()
        {
            var result = await _permissionTypeService.GetPermissionTypes(null);
            return StatusCode(result.StatusCode, result.HttpResponse);
        }

        [HttpGet("{permissionTypeId}")]
        public async Task<ActionResult<PermissionTypeDto>> Get(int permissionTypeId)
        {
            var result = await _permissionTypeService.GetPermissionType(permissionTypeId);
            return StatusCode(result.StatusCode, result.HttpResponse);
        }

        [HttpPost]
        public async Task<ActionResult<PermissionTypeDto>> Post([FromBody] PermissionTypeDto permissionTypeDto)
        {
            var result = await _permissionTypeService.AddPermissionType(permissionTypeDto);
            return StatusCode(result.StatusCode, result.HttpResponse);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] PermissionTypeDto permissionTypeDto)
        {
            var result = await _permissionTypeService.UpdatePermissionType(permissionTypeDto);
            return StatusCode(result.StatusCode, result.HttpResponse);
        }

        [HttpDelete("{permissionTypeId}")]
        public async Task<ActionResult<PermissionTypeDto>> Delete(int permissionTypeId)
        {
            var result = await _permissionTypeService.DeletePermissionType(permissionTypeId);
            return StatusCode(result.StatusCode, result.HttpResponse);
        }
    }
}