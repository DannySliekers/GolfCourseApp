using GolfApp.Models;
using GolfApp.Services;
using GolfApp.ViewModels;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfAppTests.ViewModelTests
{
    public class ManageViewModelTests
    {
        private readonly IGolfCourseService _golfCourseService = Substitute.For<IGolfCourseService>();
        private readonly IBookingService _bookingService = Substitute.For<IBookingService>();
        private readonly ManageViewModel _viewModel;

        public ManageViewModelTests()
        {
            _viewModel = new ManageViewModel(_golfCourseService, _bookingService);
        }

        [Fact]
        public async Task GetManagedGolfCourses_ShouldFillManagedGolfCourses()
        {
            var mockCourses = new List<GolfCourse>
            {
                new GolfCourse { Id = 1, Name = "Course A" },
                new GolfCourse { Id = 2, Name = "Course B" }
            };

            _golfCourseService.GetManagedGolfCoursesAsync().Returns(mockCourses);

            await _viewModel.GetManagedGolfCourses();

            Assert.Equal(2, _viewModel.ManagedGolfCourses.Count);
            Assert.Contains(_viewModel.ManagedGolfCourses, c => c.Name == "Course A");
            Assert.Contains(_viewModel.ManagedGolfCourses, c => c.Name == "Course B");
        }

        [Fact]
        public async Task GetManagedBookings_ShouldFillManagedBookings()
        {
            _viewModel.ManagedGolfCourses.Add(new GolfCourse { Id = 10, Name = "Mock Course" });

            var mockBookings = new List<Booking>
            {
                new Booking { Id = 1, CreatedByUserId = 1 },
                new Booking { Id = 2, CreatedByUserId = 2 }
            };

            _bookingService.GetGolfCourseBookingsAsync(10).Returns(mockBookings);

            await _viewModel.GetManagedBookings();

            Assert.Equal(2, _viewModel.ManagedBookings.Count);
            Assert.Contains(_viewModel.ManagedBookings, b => b.Id == 1);
            Assert.Contains(_viewModel.ManagedBookings, b => b.Id == 2);
        }

    }
}
