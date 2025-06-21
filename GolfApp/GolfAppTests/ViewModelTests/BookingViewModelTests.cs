using GolfApp.Models;
using GolfApp.Services;
using GolfApp.ViewModels;
using NSubstitute;

namespace GolfAppTests.ViewModelTests
{
    public class BookingViewModelTests
    {
        private readonly IBookingService _bookingService = Substitute.For<IBookingService>();
        private readonly IUserService _userService = Substitute.For<IUserService>();
        private readonly IGolfCourseService _golfCourseService = Substitute.For<IGolfCourseService>();
        private readonly BookingsViewModel _viewModel;

        public BookingViewModelTests()
        {
            _viewModel = new BookingsViewModel(_bookingService, _userService, _golfCourseService);
        }

        [Fact]
        public async Task InitializeAsync_LoadsBookingsCorrectly()
        {
            var mockBookings = new List<Booking>
            {
                new Booking { Id = 1, GolfCourseId = 100, CreatedByUserId = 200, StartTime = DateTime.Now }
            };
            var mockGolfCourse = new GolfCourse { Id = 100, Name = "Mock Course" };
            var mockUser = new User { Id = 200, UserName = "mockuser" };

            _bookingService.GetUserBookingsAsync().Returns(mockBookings);
            _golfCourseService.GetGolfCourseById(100).Returns(mockGolfCourse);
            _userService.GetUserById(200).Returns(mockUser);

            await _viewModel.InitializeAsync();

            var result = _viewModel.DisplayedBookings.First();
            Assert.Single(_viewModel.DisplayedBookings);
            Assert.Equal("Mock Course", result.GolfCourseName);
            Assert.Equal("mockuser", result.MainBooker);
        }
    }
}
