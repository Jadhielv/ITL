using ITL.Domain.Common;
using ITL.Domain.DTOs;
using ITL.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITL.API.Controllers;

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
        var result = await _permissionService.GetPermissions();
        return Ok(result);
    }

    [HttpGet("pagination")]
    public async Task<ActionResult<IEnumerable<PermissionDto>>> Get([FromQuery] Pagination pagination)
    {
        var result = await _permissionService.GetPermissions(pagination);
        return Ok(result);
    }

    [HttpGet("{permissionId}")]
    public async Task<ActionResult<PermissionDto>> Get(int permissionId)
    {
        var result = await _permissionService.GetPermission(permissionId);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<PermissionDto>> Post([FromBody] PermissionDto permissionDto)
    {
        var result = await _permissionService.AddPermission(permissionDto);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] PermissionDto permissionDto)
    {
        await _permissionService.UpdatePermission(permissionDto);
        return Ok();
    }

    [HttpDelete("{permissionId}")]
    public async Task<IActionResult> Delete(int permissionId)
    {
        await _permissionService.DeletePermission(permissionId);
        return Ok();
    }
}
