using Microsoft.AspNetCore.Mvc;
using ReactAPI.Core.Models;
using ReactAPI.Services.Interfaces;

namespace ReactAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IRegisterService _registerService;

        public RegisterController(IRegisterService registerService)
        {
            _registerService = registerService;
        }

        // GET: api/Register
        [HttpGet]
        public async Task<ActionResult<List<Register>>> GetRegisters()
        {
            var registers = await _registerService.GetAllRegistersAsync();
            return Ok(registers);
        }

        // GET: api/Register/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Register>> GetRegister(int id)
        {
            var register = await _registerService.GetRegisterByIdAsync(id);
            if (register == null)
            {
                return NotFound();
            }
            return Ok(register);
        }

        //// POST: api/Register
        ////[Route("AddRegister")]
        //[HttpPost("AddRegister")]
        /////public async Task<ActionResult<Register>> CreateRegister(Register register
        //public async Task<ActionResult<Register>> Register([FromBody] Register register)
        //{
        //    var createdRegister = await _registerService.AddRegisterAsync(register);
        //    return CreatedAtAction(nameof(GetRegister), new { id = createdRegister.Id }, createdRegister);
        //}

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] Register register)
        {
            if (register == null)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Invalid data",
                    data = (object)null
                });
            }

            try
            {
                var createdRegister = await _registerService.AddRegisterAsync(register);

                return CreatedAtAction(nameof(GetRegister), new { id = createdRegister.Id }, new
                {
                    success = true,
                    message = "Registration successful",
                    data = createdRegister
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Internal server error",
                    error = ex.Message
                });
            }
        }


        // PUT: api/Register/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRegister(int id, Register register)
        {
            if (id != register.Id)
            {
                return BadRequest();
            }

            await _registerService.UpdateRegisterAsync(register);
            return NoContent();
        }

        // DELETE: api/Register/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegister(int id)
        {
            await _registerService.DeleteRegisterAsync(id);
            return NoContent();
        }
    }
}