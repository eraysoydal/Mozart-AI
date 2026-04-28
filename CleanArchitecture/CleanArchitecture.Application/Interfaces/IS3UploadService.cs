using System.Threading.Tasks;

namespace CleanArchitecture.Core.Interfaces
{
    public interface IS3UploadService
    {
        /// <summary>
        /// Generates a presigned PUT URL for uploading a file directly to S3.
        /// </summary>
        /// <param name="fileName">Original file name (e.g. "song.mp3")</param>
        /// <param name="contentType">MIME type (e.g. "audio/mpeg")</param>
        /// <param name="folder">S3 folder prefix (e.g. "tracks", "covers", "canvas")</param>
        /// <returns>Tuple of (presigned upload URL, S3 object key)</returns>
        Task<(string UploadUrl, string S3Key)> GeneratePresignedUploadUrlAsync(
            string fileName, string contentType, string folder);
    }
}
