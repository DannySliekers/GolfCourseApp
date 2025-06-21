using GolfCourseWebAPI.Controllers;
using GolfCourseWebAPI.Models;
using GolfCourseWebAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace GolfCourseWebAPITests
{
    [TestClass]
    public class GolfCourseControllerTests
    {
        private IGolfCourseRepository _repository;
        private ILogger<GolfCourseController> _logger;
        private GolfCourseController _controller;

        [TestInitialize]
        public void Setup()
        {
            _repository = Substitute.For<IGolfCourseRepository>();
            _logger = Substitute.For<ILogger<GolfCourseController>>();
            _controller = new GolfCourseController(_logger, _repository);
        }

        [TestMethod]
        public async Task AddGolfCourse_ReturnsOkResult_WhenSuccessful()
        {
            var course = new GolfCourse { Id = 1, Name = "Test Course" };

            var result = await _controller.AddGolfCourse(course) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(course, result.Value);
            await _repository.Received(1).AddGolfCourse(course);
        }

        [TestMethod]
        public async Task DeleteGolfCourse_ReturnsOkResult_WhenSuccessful()
        {
            var result = await _controller.DeleteGolfCourse(1) as OkResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            await _repository.Received(1).DeleteGolfCourse(1);
        }

        [TestMethod]
        public async Task AddGolfCourseImage_ReturnsNotFound_WhenCourseDoesNotExist()
        {
            var image = new GolfCourseImage { GolfCourseId = 999 };
            _repository.AddGolfCourseImage(image).Returns(-1);

            var result = await _controller.AddGolfCourseImage(image) as NotFoundObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);
            StringAssert.Contains(result.Value.ToString(), "does not exist");
        }
    }
}
