using System;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Settings;
using Microsoft.Extensions.Options;

namespace CleanArchitecture.Infrastructure.Services
{
    public class S3UploadService : IS3UploadService
    {
        private readonly S3Settings _settings;
        private readonly IAmazonS3 _s3Client;

        public S3UploadService(IOptions<S3Settings> settings)
        {
            _settings = settings.Value;

            var config = new AmazonS3Config
            {
                RegionEndpoint = RegionEndpoint.GetBySystemName(_settings.Region)
            };

            // Credentials are resolved automatically from the credential chain:
            // 1. Environment variables (AWS_ACCESS_KEY_ID, AWS_SECRET_ACCESS_KEY)
            // 2. AWS credentials file (~/.aws/credentials)
            // 3. IAM role (for EC2, ECS, Lambda)
            _s3Client = new AmazonS3Client(config);
        }

        public Task<(string UploadUrl, string S3Key)> GeneratePresignedUploadUrlAsync(
            string fileName, string contentType, string folder)
        {
            // Generate a unique key: folder/guid_originalname
            var sanitizedName = System.IO.Path.GetFileName(fileName);
            var s3Key = $"{folder}/{Guid.NewGuid()}_{sanitizedName}";

            var request = new GetPreSignedUrlRequest
            {
                BucketName = _settings.BucketName,
                Key = s3Key,
                Verb = HttpVerb.PUT,
                Expires = DateTime.UtcNow.AddMinutes(15),
                ContentType = contentType
            };

            // In AWSSDK v4, GetPreSignedURL is synchronous
            var uploadUrl = _s3Client.GetPreSignedURL(request);
            return Task.FromResult((uploadUrl, s3Key));
        }
    }
}
