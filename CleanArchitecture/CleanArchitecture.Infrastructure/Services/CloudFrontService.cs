using System;
using System.IO;
using Amazon.CloudFront;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Settings;
using Microsoft.Extensions.Options;

namespace CleanArchitecture.Infrastructure.Services
{
    public class CloudFrontService : ICloudFrontService
    {
        private readonly CloudFrontSettings _settings;

        public CloudFrontService(IOptions<CloudFrontSettings> settings)
        {
            _settings = settings.Value;
        }

        public string GetSignedUrl(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return fileName;
            
            // If it's already a full URL, we might want to extract the path or just return it.
            // For this implementation, we assume fileName is just the key/path.
            
            // Ensure fileName starts with a leading slash or not, depending on how it's stored
            string urlObject = fileName.StartsWith("/") ? fileName.Substring(1) : fileName;
            string fullUrl = $"{_settings.Domain.TrimEnd('/')}/{urlObject}";

            try
            {
                // Ensure the private key file exists
                if (!File.Exists(_settings.PrivateKeyPath))
                {
                    // Fallback to unstructured or log an error; for now return the unsigned URL
                    return fullUrl;
                }

                // Parse the private key
                using var streamReader = new StreamReader(_settings.PrivateKeyPath);
                
                DateTime expiration = DateTime.UtcNow.AddHours(_settings.ExpirationHours);

                string signedUrl = AmazonCloudFrontUrlSigner.GetCannedSignedURL(
                    AmazonCloudFrontUrlSigner.Protocol.https,
                    _settings.Domain.Replace("https://", "").Replace("http://", "").TrimEnd('/'),
                    new StreamReader(_settings.PrivateKeyPath),
                    urlObject,
                    _settings.KeyPairId,
                    expiration);

                return signedUrl;
            }
            catch (Exception)
            {
                // Log exception if needed, fallback to returning the unsigned URL
                return fullUrl;
            }
        }
    }
}
