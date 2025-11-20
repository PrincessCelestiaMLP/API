
using LW4_API.Model.DTO;
using LW4_API.Model.View;
using LW4_API.Server.Interface;
using LW4_API.Server.Realizetion;
using Microsoft.AspNetCore.Mvc;

namespace LW4_API.Controler
{
        [ApiController]
        [Route("api/[controller]")]
        public class RentController : ControllerBase
        {
        private readonly IRentService _service;

        public RentController(IRentService service)
            {
                _service = service;
            }

            [HttpGet]
            public async Task<ActionResult> GetAll() =>
                Ok(await _service.GetAllAsync());

            [HttpGet("{id}")]
            public async Task<ActionResult<RentView?>> GetById(int id)
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
            public async Task<ActionResult> Create(RentDTO rentDto)
            {
                await _service.CreateAsync(rentDto);
                return Ok(rentDto);
            }

            [HttpPut("{id}")]
            public async Task<ActionResult> Update(int id, RentDTO rentDto)
            {
                await _service.UpdateAsync(id,rentDto);
                return Ok(rentDto);
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                await _service.DeleteAsync(id);
                return NoContent();
            }
        }
    }
