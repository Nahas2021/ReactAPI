using ReactAPI.Core.Interfaces;
using ReactAPI.Core.Models;
using ReactAPI.Services.Interfaces;


namespace ReactAPI.Services.Services
{
    public class SlotService : ISlotService
    {
        private readonly ISlotRepository _slotRepository;

        public SlotService(ISlotRepository slotRepository)
        {
            _slotRepository = slotRepository;
        }

        public async Task<List<Slot>> GetAllSlotsAsync()
        {
            return await _slotRepository.GetAllSlotsAsync();
        }

        public async Task<Slot> GetSlotByIdAsync(long id)
        {
            return await _slotRepository.GetSlotByIdAsync(id);
        }

        public async Task<Slot> AddSlotAsync(Slot slot)
        {
            return await _slotRepository.AddSlotAsync(slot);
        }

        public async Task UpdateSlotAvailabilityAsync(long id, bool isAvailable)
        {
            await _slotRepository.UpdateSlotAvailabilityAsync(id, isAvailable);
        }

        public async Task DeleteSlotAsync(long id)
        {
            await _slotRepository.DeleteSlotAsync(id);
        }
    }
}