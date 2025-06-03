using GolfCourseWebAPI.Models;
using GolfCourseWebAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GolfCourseWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GolfCourseController : ControllerBase
    {
        private readonly ILogger<GolfCourseController> _logger;
        private readonly IGolfCourseRepository _repository;

        public GolfCourseController(ILogger<GolfCourseController> logger, IGolfCourseRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpDelete]
        [ProducesResponseType(200, Type = typeof(GolfCourse))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteGolfCourse(int id)
        {
            try
            {
                await _repository.DeleteGolfCourse(id);
                return Ok();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error while deleting GolfCourse");
                return BadRequest();
            }
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(GolfCourse))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddGolfCourse(GolfCourse golfCourse)
        {
            try
            {
                await _repository.AddGolfCourse(golfCourse);
                return Ok(golfCourse);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error while inserting GolfCourse");
                return BadRequest();
            }
        }

        [HttpPut]
        [ProducesResponseType(200, Type = typeof(GolfCourse))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateGolfCourse(GolfCourse golfCourse)
        {
            try
            {
                await _repository.UpdateGolfCourse(golfCourse);
                return Ok(golfCourse);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error while updating GolfCourse");
                return BadRequest();
            }
        }

        [HttpGet]
        public List<GolfCourse> GetGolfCourses()
        {
            return _repository.GetAll().ToList();
        }

        [HttpGet("{id}")]
        public GolfCourse GetById(int id)
        {
            return _repository.Get(id);
        }
    }
}
