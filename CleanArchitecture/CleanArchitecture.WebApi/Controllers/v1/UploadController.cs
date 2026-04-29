using System.Threading.Tasks;
using CleanArchitecture.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize]
    public class UploadController : BaseApiController
    {
        private readonly IS3UploadService _s3UploadService;

        public UploadController(IS3UploadService s3UploadService)
        {
            _s3UploadService = s3UploadService;
        }

        /// <summary>
        /// Generate a presigned URL for uploading a file directly to S3.
        /// </summary>
        [HttpPost("presigned-url")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPresignedUrl([FromBody] PresignedUrlRequest request)
        {
            if (string.IsNullOrEmpty(request.FileName) || string.IsNullOrEmpty(request.ContentType))
            {
                return BadRequest("FileName and ContentType are required.");
            }

            // Validate folder
            var allowedFolders = new[] { "tracks", "covers", "canvas" };
            var folder = string.IsNullOrEmpty(request.Folder) ? "tracks" : request.Folder.ToLower();
            if (!System.Array.Exists(allowedFolders, f => f == folder))
            {
                return BadRequest($"Invalid folder. Allowed: {string.Join(", ", allowedFolders)}");
            }

            var (uploadUrl, s3Key) = await _s3UploadService.GeneratePresignedUploadUrlAsync(
                request.FileName, request.ContentType, folder);

            return Ok(new { uploadUrl, s3Key });
        }
    }

    public class PresignedUrlRequest
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public string Folder { get; set; }
    }
}
