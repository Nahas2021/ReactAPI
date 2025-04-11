using ReactAPI.Core.Models;

namespace ReactAPI.Services.Interfaces
{
    public interface IBookingService
    {
        Task<List<Booking>> GetAllBookingsAsync();
        Task<Booking> AddBookingAsync(Booking booking);
    }
}