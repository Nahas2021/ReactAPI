using Microsoft.EntityFrameworkCore;
using ReactAPI.Core.Interfaces;
using ReactAPI.Core.Models;
using ReactAPI.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReactAPI.Infrastructure.Repositories
{
    public class SlotRepository : ISlotRepository
    {
        private readonly EmployeeContext _context;

        public SlotRepository(EmployeeContext context)
        {
            _context = context;
        }

        public async Task<List<Slot>> GetAllSlotsAsync()
        {
            return await _context.Slots.ToListAsync();
        }

        public async Task<Slot> GetSlotByIdAsync(long id)
        {
            return await _context.Slots.FindAsync(id);
        }

        public async Task<Slot> AddSlotAsync(Slot slot)
        {
            _context.Slots.Add(slot);
            await _context.SaveChangesAsync();
            return slot;
        }

        public async Task UpdateSlotAvailabilityAsync(long id, bool isAvailable)
        {
            var slot = await _context.Slots.FindAsync(id);
            if (slot != null)
            {
                slot.IsAvailable = isAvailable;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteSlotAsync(long id)
        {
            var slot = await _context.Slots.FindAsync(id);
            if (slot != null)
            {
                _context.Slots.Remove(slot);
                await _context.SaveChangesAsync();
            }
        }
    }
}