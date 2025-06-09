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

        [HttpGet]
        public List<Booking> GetBookings()
        {
            return _repository.GetAll().ToList();
        }

        [HttpGet("{id}")]
        public Booking GetById(int id)
        {
            return _repository.Get(id);
        }
    }
}
