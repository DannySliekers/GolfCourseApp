using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GolfCourseWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly string _imageDirectory = Path.Combine(Directory.GetCurrentDirectory(), "UploadedImages");

        public UploadController()
        {
            if (!Directory.Exists(_imageDirectory))
            {
                Directory.CreateDirectory(_imageDirectory);
            }
        }

        [HttpPost("image")]
        public async Task<IActionResult> UploadImage(IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            if (!image.ContentType.StartsWith("image/"))
            {
                return BadRequest("Only image files are allowed.");
            }

            var fileExtension = Path.GetExtension(image.FileName);
            var fileName = $"{Guid.NewGuid()}{fileExtension}";
            var filePath = Path.Combine(_imageDirectory, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            var imageUrl = $"{Request.Scheme}://{Request.Host}/UploadedImages/{fileName}";

            return Ok(new { fileName, imageUrl });
        }
    }
}
