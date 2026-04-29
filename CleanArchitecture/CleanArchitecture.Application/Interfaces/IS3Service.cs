using System.IO;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Interfaces
{
    public interface IS3Service
    {
        /// <summary>Upload a file stream to S3 and return the S3 key.</summary>
        Task<string> UploadAsync(Stream fileStream, string fileName, string contentType);

        /// <summary>Delete an object from S3 by its key.</summary>
        Task DeleteAsync(string key);

        /// <summary>Get a presigned download URL for a key (short-lived).</summary>
        string GetPresignedUrl(string key, int expiryMinutes = 60);
    }
}
