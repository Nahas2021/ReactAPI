using ReactAPI.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReactAPI.Services.Interfaces
{
    public interface ISlotService
    {
        Task<List<Slot>> GetAllSlotsAsync();
        Task<Slot> GetSlotByIdAsync(long id);
        Task<Slot> AddSlotAsync(Slot slot);
        Task UpdateSlotAvailabilityAsync(long id, bool isAvailable);
        Task DeleteSlotAsync(long id);
    }
}