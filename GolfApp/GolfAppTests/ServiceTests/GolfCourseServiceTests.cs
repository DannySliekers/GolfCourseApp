using GolfApp.Helpers;
using GolfApp.Models;
using GolfApp.Services;
using Microsoft.Maui.Storage;
using NSubstitute;
using System.Net;
using System.Text;
using System.Text.Json;

namespace GolfAppTests.ServiceTests
{
    public class GolfCourseServiceTests
    {
        private readonly ISecureStorageService _secureStorage;

        public GolfCourseServiceTests()
        {
            _secureStorage = Substitute.For<ISecureStorageService>();
            _secureStorage.GetAsync("jwt").Returns("mock_token");
        }

        [Fact]
        public async Task AddGolfCourseAsync_ReturnsCourseId_WhenSuccessful()
        {
            // Arrange
            var expectedCourse = new GolfCourse { Id = 42, Name = "Test Course" };
            var json = JsonSerializer.Serialize(expectedCourse);

            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            var mockHttpMessageHandler = Substitute.ForPartsOf<MockHttpMessageHandler>();

            mockHttpMessageHandler.MockSend(Arg.Any<HttpRequestMessage>(), Arg.Any<CancellationToken>())
                .Returns(responseMessage);

            var client = new HttpClient(mockHttpMessageHandler) { BaseAddress = new Uri("https://example.com") };
            var service = new GolfCourseService(client, _secureStorage);

            int result = await service.AddGolfCourseAsync(new GolfCourse());

            Assert.Equal(42, result);
        }

        [Fact]
        public async Task AddImageToGolfCourseAsync_ShouldReturnTrue_WhenResponseIsSuccessful()
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            var mockHandler = Substitute.ForPartsOf<MockHttpMessageHandler>();
            mockHandler.MockSend(Arg.Any<HttpRequestMessage>(), Arg.Any<CancellationToken>()).Returns(response);

            var client = new HttpClient(mockHandler) { BaseAddress = new Uri("https://example.com") };
            var service = new GolfCourseService(client, _secureStorage);

            // Act
            var result = await service.AddImageToGolfCourseAsync(1, "https://image.url");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteGolfCourseAsync_ShouldReturnTrue_WhenResponseIsSuccessful()
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            var mockHandler = Substitute.ForPartsOf<MockHttpMessageHandler>();
            mockHandler.MockSend(Arg.Any<HttpRequestMessage>(), Arg.Any<CancellationToken>()).Returns(response);

            var client = new HttpClient(mockHandler) { BaseAddress = new Uri("https://example.com") };
            var service = new GolfCourseService(client, _secureStorage);

            var result = await service.DeleteGolfCourseAsync(5);

            Assert.True(result);
        }

        [Fact]
        public async Task EditGolfCourseAsync_ShouldReturnTrue_WhenResponseIsSuccessful()
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            var mockHandler = Substitute.ForPartsOf<MockHttpMessageHandler>();
            mockHandler.MockSend(Arg.Any<HttpRequestMessage>(), Arg.Any<CancellationToken>()).Returns(response);

            var client = new HttpClient(mockHandler) { BaseAddress = new Uri("https://example.com") };
            var service = new GolfCourseService(client, _secureStorage);

            var result = await service.EditGolfCourseAsync(new GolfCourse { Id = 1 });

            Assert.True(result);
        }


        [Fact]
        public async Task GetAllGolfCoursesAsync_ShouldReturnCourses_WhenResponseIsValid()
        {
            var expectedCourses = new List<GolfCourse> { new() { Id = 1, Name = "Course A" } };
            var json = JsonSerializer.Serialize(expectedCourses);

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            var mockHandler = Substitute.ForPartsOf<MockHttpMessageHandler>();
            mockHandler.MockSend(Arg.Any<HttpRequestMessage>(), Arg.Any<CancellationToken>()).Returns(response);

            var client = new HttpClient(mockHandler) { BaseAddress = new Uri("https://example.com") };
            var service = new GolfCourseService(client, _secureStorage);

            var result = await service.GetAllGolfCoursesAsync();

            Assert.Single(result);
            Assert.Equal("Course A", result.First().Name);
        }


        [Fact]
        public async Task GetGolfCourseById_ShouldReturnCourse_WhenResponseIsValid()
        {
            var expectedCourse = new GolfCourse { Id = 42, Name = "Test Course" };
            var json = JsonSerializer.Serialize(expectedCourse);

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            var mockHandler = Substitute.ForPartsOf<MockHttpMessageHandler>();
            mockHandler.MockSend(Arg.Any<HttpRequestMessage>(), Arg.Any<CancellationToken>()).Returns(response);

            var client = new HttpClient(mockHandler) { BaseAddress = new Uri("https://example.com") };
            var service = new GolfCourseService(client, _secureStorage);

            var result = await service.GetGolfCourseById(42);

            Assert.Equal(42, result.Id);
        }
    }
}
