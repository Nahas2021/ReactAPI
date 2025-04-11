using ReactAPI.Core.Interfaces;
using ReactAPI.Core.Models;
using ReactAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReactAPI.Infrastructure.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly EmployeeContext _context;

        public BookingRepository(EmployeeContext context)
        {
            _context = context;
        }

        public async Task<List<Booking>> GetAllBookingsAsync()
        {
            return await _context.Bookings.ToListAsync();
        }

        public async Task<Booking> AddBookingAsync(Booking booking)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            return booking;
        }
    }
}