using LW4_API.Data;
using LW4_API.Enums;
using LW4_API.Model.DTO;
using LW4_API.Model.Entity;
using LW4_API.Server.Interface;
using LW4_API.Server.Realizetion;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LW4_API.Controler
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _service;

        public ClientController(IClientService service)
        {
            _service = service;
        }
        [Authorize(Roles = "User,Manager,Admin")]
        [HttpGet("User")]
        public async Task<ActionResult> GetAll() =>
            Ok(await _service.GetAllAsync());
        [Authorize(Roles = "User,Manager,Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientDTO?>> GetById(int id)
        {
            try
            {
                return await _service.GetByIdAsync(id);
            }
            catch(NullReferenceException)
            {
                return NotFound();
            }
           
        }
        [Authorize(Roles = "Manager,Admin")]
        [HttpPost("Manager")]
        public async Task<ActionResult> Create(ClientDTO client)
        {
            await _service.CreateAsync(client);
            return Ok(client);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, ClientDTO updated)
        {
            await _service.UpdateAsync(id,updated);
            return Ok(updated);
        }

        [Authorize(Roles = "Manager,Admin")]
        [HttpDelete("(Admin){id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
