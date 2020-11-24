using KCTest.Domain.DTOs;
using KCTest.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KCTest.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;

        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PermissionDto>>> Get()
        {
            var result = await _permissionService.GetPermissions(null);
            return StatusCode(result.StatusCode, result.HttpResponse);
        }

        [HttpGet("{permissionId}")]
        public async Task<ActionResult<PermissionDto>> Get(int permissionId)
        {
            var result = await _permissionService.GetPermission(permissionId);
            return StatusCode(result.StatusCode, result.HttpResponse);
        }

        [HttpPost]
        public async Task<ActionResult<PermissionDto>> Post([FromBody] PermissionDto permissionDto)
        {
            var result = await _permissionService.AddPermission(permissionDto);
            return StatusCode(result.StatusCode, result.HttpResponse);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] PermissionDto permissionDto)
        {
            var result = await _permissionService.UpdatePermission(permissionDto);
            return StatusCode(result.StatusCode, result.HttpResponse);
        }

        [HttpDelete("{permissionId}")]
        public async Task<ActionResult<PermissionDto>> Delete(int permissionId)
        {
            var result = await _permissionService.DeletePermission(permissionId);
            return StatusCode(result.StatusCode, result.HttpResponse);
        }
    }
}