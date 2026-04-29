using System;
using System.IO;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Settings;
using Microsoft.Extensions.Options;

namespace CleanArchitecture.Infrastructure.Services
{
    public class S3Service : IS3Service
    {
        private readonly S3Settings _settings;
        private readonly IAmazonS3 _s3Client;

        public S3Service(IOptions<S3Settings> settings)
        {
            _settings = settings.Value;

            _s3Client = new AmazonS3Client(
                _settings.AccessKey,
                _settings.SecretKey,
                RegionEndpoint.GetBySystemName(_settings.Region));
        }

        public async Task<string> UploadAsync(Stream fileStream, string fileName, string contentType)
        {
            var key = $"{_settings.KeyPrefix.TrimEnd('/')}/{Guid.NewGuid()}_{fileName}";

            var request = new PutObjectRequest
            {
                BucketName = _settings.BucketName,
                Key = key,
                InputStream = fileStream,
                ContentType = contentType,
                AutoCloseStream = false
            };

            await _s3Client.PutObjectAsync(request);

            return key;
        }

        public async Task DeleteAsync(string key)
        {
            var request = new DeleteObjectRequest
            {
                BucketName = _settings.BucketName,
                Key = key
            };

            await _s3Client.DeleteObjectAsync(request);
        }

        public string GetPresignedUrl(string key, int expiryMinutes = 60)
        {
            var request = new GetPreSignedUrlRequest
            {
                BucketName = _settings.BucketName,
                Key = key,
                Expires = DateTime.UtcNow.AddMinutes(expiryMinutes)
            };

            return _s3Client.GetPreSignedURL(request);
        }
    }
}
