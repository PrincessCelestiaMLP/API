using LW4_API.Data;
using LW4_API.Model.DTO;
using LW4_API.Model.Entity;
using LW4_API.Server.Realizetion;
using Microsoft.AspNetCore.Mvc;
using LW4_API.Server.Interface;

namespace LW4_API.Controler
{

    [ApiController]
    [Route("api/[controller]")]
    public class ParkingSpaceController : ControllerBase
    {
        private readonly IParkingService _service;

        public ParkingSpaceController(IParkingService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll() =>
            Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<ParkingSpaceDTO?>> GetById(int id)
        {
            try
            {
                return await _service.GetByIdAsync(id);
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }

        }

        [HttpPost]
        public async Task<ActionResult> Create(ParkingSpaceDTO spaceDto)
        {
            await _service.CreateAsync(spaceDto);
            return Ok(spaceDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, ParkingSpaceDTO spaceDto)
        {
            await _service.UpdateAsync(id,spaceDto);
            return Ok(spaceDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
