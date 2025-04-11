using Microsoft.AspNetCore.Mvc;
using ReactAPI.Core.Models;
using ReactAPI.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReactAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlotController : ControllerBase
    {
        private readonly ISlotService _slotService;

        public SlotController(ISlotService slotService)
        {
            _slotService = slotService;
        }

        // GET: api/Slot
        [HttpGet]
        public async Task<ActionResult<List<Slot>>> GetSlots()
        {
            var slots = await _slotService.GetAllSlotsAsync();
            return Ok(slots);
        }

        // GET: api/Slot/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Slot>> GetSlot(long id)
        {
            var slot = await _slotService.GetSlotByIdAsync(id);
            if (slot == null)
            {
                return NotFound();
            }
            return Ok(slot);
        }

        // POST: api/Slot
        [HttpPost]
        public async Task<ActionResult<Slot>> CreateSlot(Slot slot)
        {
            var createdSlot = await _slotService.AddSlotAsync(slot);
            return CreatedAtAction(nameof(GetSlot), new { id = createdSlot.Id }, createdSlot);
        }

        // PUT: api/Slot/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSlotAvailability(long id, [FromBody] bool isAvailable)
        {
            await _slotService.UpdateSlotAvailabilityAsync(id, isAvailable);
            return NoContent();
        }

        // DELETE: api/Slot/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSlot(long id)
        {
            await _slotService.DeleteSlotAsync(id);
            return NoContent();
        }
    }
}