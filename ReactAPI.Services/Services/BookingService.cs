using ReactAPI.Core.Interfaces;
using ReactAPI.Core.Models;
using ReactAPI.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReactAPI.Services.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<List<Booking>> GetAllBookingsAsync()
        {
            return await _bookingRepository.GetAllBookingsAsync();
        }

        public async Task<Booking> AddBookingAsync(Booking booking)
        {
            return await _bookingRepository.AddBookingAsync(booking);
        }
    }
}