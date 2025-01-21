using Microsoft.AspNetCore.Mvc;
using Todo.Core.Interfaces;

namespace Todo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileController : ControllerBase
    {
        private readonly ILogger<FileController> _logger;
        private readonly IFileService _fileService;

        public FileController(ILogger<FileController> logger, IFileService fileService)
        {
            _logger = logger;
            _fileService = fileService;
        }

        [HttpPost]
        [Route(nameof(UploadFile))]
        public async Task<IActionResult> UploadFile(IFormFile file, [FromQuery] Guid todoId)
        {
            try
            {
                string test;

                string? test1 = null;

                test1.Contains("");
                string filePath = Path.Combine("", file.FileName);
                using var stream = file.OpenReadStream();
                await _fileService.UploadFile(filePath, stream, todoId);
                return Ok(new { message = "File uploaded successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to upload file");
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}