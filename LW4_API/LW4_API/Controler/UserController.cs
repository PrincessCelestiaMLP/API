using LW4_API.Enums;
using LW4_API.Server.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LW4_API.Controler
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = nameof(Roles.Admin))]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [Authorize(Roles = $"{nameof(Roles.Admin)}")]
        [HttpGet]
        public async Task<ActionResult> GetAll() =>
           Ok(await _userService.GetAsync());

        [Authorize(Roles = $"{nameof(Roles.Admin)}")]
        [HttpPost("setRoles")]
        public async Task<IActionResult> SetRoles(string id, Roles roles)
        {
            var user = await _userService.GetAsync(id);
            if (user == null) return NotFound("User not found");

            user.Role = roles;
            await _userService.UpdateAsync(user);

            return Ok(new { username = user.Username, roles = user.Role, rolesValue = (int)user.Role });
        }
        [Authorize(Roles = $"{nameof(Roles.Admin)}")]
        [HttpPost("addRole")]
        public async Task<IActionResult> AddRole(string id, Roles role)
        {
            var user = await _userService.GetAsync(id);
            if (user == null) return NotFound();

            user.Role |= role; // додаємо прапорець
            await _userService.UpdateAsync(user);

            return Ok(new { username = user.Username, roles = user.Role, rolesValue = (int)user.Role });
        }
        [Authorize(Roles = $"{nameof(Roles.Admin)}")]
        [HttpPost("removeRole")]
        public async Task<IActionResult> RemoveRole(string id, Roles role)
        {
            var user = await _userService.GetAsync(id);
            if (user == null) return NotFound();

            user.Role &= ~role; // знімаємо прапорець
            await _userService.UpdateAsync(user);

            return Ok(new { username = user.Username, roles = user.Role, rolesValue = (int)user.Role });
        }
    }
}
