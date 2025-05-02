using ReactAPI.Core.Models;
using ReactAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        // GET: api/bookings
        [HttpGet]
        public async Task<ActionResult<List<Booking>>> GetBookings()
        {
            var bookings = await _bookingService.GetAllBookingsAsync();
            return Ok(bookings);
        }

        // POST: api/bookings
        [HttpPost]
        public async Task<ActionResult<Booking>> PostBooking(Booking booking)
        {
            var createdBooking = await _bookingService.AddBookingAsync(booking);
            return CreatedAtAction(nameof(GetBookings), new { id = createdBooking.Id }, createdBooking);
        }
    }
}