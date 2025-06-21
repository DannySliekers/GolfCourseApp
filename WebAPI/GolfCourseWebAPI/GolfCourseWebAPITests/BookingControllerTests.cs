using GolfCourseWebAPI.Controllers;
using GolfCourseWebAPI.Models;
using GolfCourseWebAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace GolfCourseWebAPITests
{
    [TestClass]
    public class BookingControllerTests
    {
        private IBookingRepository _repository;
        private ILogger<BookingController> _logger;
        private BookingController _controller;

        [TestInitialize]
        public void Setup()
        {
            _repository = Substitute.For<IBookingRepository>();
            _logger = Substitute.For<ILogger<BookingController>>();
            _controller = new BookingController(_logger, _repository);
        }

        [TestMethod]
        public async Task DeleteBooking_ReturnsOk_OnSuccess()
        {
            var result = await _controller.DeleteBooking(1);

            Assert.IsInstanceOfType(result, typeof(OkResult));
            await _repository.Received().DeleteBooking(1);
        }

        [TestMethod]
        public async Task DeleteBooking_ReturnsBadRequest_OnException()
        {
            _repository.DeleteBooking(Arg.Any<int>()).Throws(new Exception("fail"));

            var result = await _controller.DeleteBooking(1);

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task AddBooking_ReturnsOk_WithBooking()
        {
            var booking = new Booking { Id = 1 };

            var result = await _controller.AddBooking(booking) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(booking, result.Value);
        }

        [TestMethod]
        public async Task AddBooking_ReturnsBadRequest_OnException()
        {
            var booking = new Booking { Id = 1 };
            _repository.AddBooking(booking).Throws(new Exception("fail"));

            var result = await _controller.AddBooking(booking);

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task AddUserToBooking_ReturnsOk_WhenSuccess()
        {
            _repository.AddUserToBooking(1, 2).Returns(1);

            var result = await _controller.AddUserToBooking(1, 2) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("User added to booking.", result.Value);
        }

        [TestMethod]
        public async Task AddUserToBooking_ReturnsBadRequest_WhenLimitReached()
        {
            _repository.AddUserToBooking(1, 2).Returns(0);

            var result = await _controller.AddUserToBooking(1, 2);

            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task RemoveUserFromBooking_ReturnsOk_WhenSuccess()
        {
            _repository.RemoveUserFromBooking(1, 2).Returns(1);

            var result = await _controller.RemoveUserFromBooking(1, 2);

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task RemoveUserFromBooking_ReturnsBadRequest_WhenNotFound()
        {
            _repository.RemoveUserFromBooking(1, 2).Returns(0);

            var result = await _controller.RemoveUserFromBooking(1, 2);

            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task UpdateBooking_ReturnsOk_OnSuccess()
        {
            var booking = new Booking { Id = 1 };

            var result = await _controller.UpdateBooking(booking) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(booking, result.Value);
        }

        [TestMethod]
        public async Task UpdateBooking_ReturnsBadRequest_OnException()
        {
            var booking = new Booking { Id = 1 };
            _repository.UpdateBooking(booking).Throws(new Exception());

            var result = await _controller.UpdateBooking(booking);

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public void GetBookingUserIds_ReturnsList()
        {
            var expected = new List<int> { 1, 2 };
            _repository.GetUserIds(1).Returns(expected);

            var result = _controller.GetBookingUserIds(1);

            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetBookings_ReturnsAll()
        {
            var expected = new List<Booking> { new Booking { Id = 1 } };
            _repository.GetAll().Returns(expected);

            var result = _controller.GetBookings();

            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetById_ReturnsBooking()
        {
            var booking = new Booking { Id = 1 };
            _repository.Get(1).Returns(booking);

            var result = _controller.GetById(1);

            Assert.AreEqual(booking, result);
        }
    }
}
