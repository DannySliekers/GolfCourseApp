using GolfCourseWebAPI.Models;
using GolfCourseWebAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GolfCourseWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly ILogger<BookingController> _logger;
        private readonly IBookingRepository _repository;

        public BookingController(ILogger<BookingController> logger, IBookingRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpDelete]
        [ProducesResponseType(200, Type = typeof(Booking))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            try
            {
                await _repository.DeleteBooking(id);
                return Ok();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error while deleting Booking");
                return BadRequest();
            }
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(Booking))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddBooking(Booking booking)
        {
            try
            {
                await _repository.AddBooking(booking);
                return Ok(booking);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error while inserting Booking");
                return BadRequest();
            }
        }

        [HttpPost("{bookingId}/users/{userId}")]
        public async Task<IActionResult> AddUserToBooking([FromRoute] int bookingId, [FromRoute] int userId)
        {
            var result = await _repository.AddUserToBooking(bookingId, userId);

            if (result == 0)
            {
                return BadRequest("Booking already has maximum number of users.");
            }

            return Ok("User added to booking.");
        }

        [HttpPut]
        [ProducesResponseType(200, Type = typeof(Booking))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateBooking(Booking booking)
        {
            try
            {
                await _repository.UpdateBooking(booking);
                return Ok(booking);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error while updating Booking");
                return BadRequest();
            }
        }

        [HttpGet("{bookingId}/users")]
        public List<int> GetBookingUserIds(int bookingId)
        {
            return _repository.GetUserIds(bookingId).ToList();
        }

        [HttpGet]
        public List<Booking> GetBookings()
        {
            return _repository.GetAll().ToList();
        }

        [HttpGet("user/{userId}")]
        public List<Booking> GetUserBookings(int userId)
        {
            return _repository.GetAllUserBookings(userId).ToList();
        }

        [HttpGet("{id}")]
        public Booking GetById(int id)
        {
            return _repository.Get(id);
        }
    }
}
