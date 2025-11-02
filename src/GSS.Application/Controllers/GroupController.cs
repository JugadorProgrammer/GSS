using GSS.Domain.DomainModelServices;
using GSS.Domain.ResultObjects.OperationResults.Group;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GSS.Application.Controllers
{
    [Authorize]
    [ApiController]
    [Route("group")]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [Route("creatgroup")]
        public async Task<IActionResult> CreateGroup(string groupName)
        {
            var result = await _groupService.CreateGroup(groupName);
            if (result.OperationResult == GroupCreateOperationResult.InvalidName)
            {
                return BadRequest("Invalid group name");
            }

            var group = result.Entity;
            if (group is null)
            {
                return StatusCode(500, "Internal Server Error");
            }
            return Ok(group.Id);
        }
    }
}
