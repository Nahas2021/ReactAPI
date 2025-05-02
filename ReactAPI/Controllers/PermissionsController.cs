using Microsoft.AspNetCore.Mvc;
using ReactAPI.Services.Interfaces;
using ReactAPI.Core.Models;

namespace ReactAPI.Controllers
{
    [ApiController]
    [Route("api/permissions")]
    public class PermissionsController : ControllerBase
    {
        private readonly IPermissionService  _permissionService;

        public PermissionsController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpGet("tree")]
        public async Task<IActionResult> GetPermissionTree([FromQuery] int groupId)
        {
            var tree = await _permissionService.GetPermissionTreeAsync(groupId);
            return Ok(tree);
        }
        [HttpGet("usergroups")]
        public async Task<ActionResult<IEnumerable<UserGroup>>> GetUserGroups()
        {
            var groups = await _permissionService.GetUserGroups();
            return Ok(groups);
        }
        [HttpPost("save")]
        public async Task<IActionResult> SavePermissions(
            [FromQuery] int groupId,
            [FromBody] List<PermissionAction> permissions)
        {
            await _permissionService.SavePermissionsAsync(groupId, permissions);
            return Ok(new { message = "Permissions saved successfully." });
        }
    }

}
