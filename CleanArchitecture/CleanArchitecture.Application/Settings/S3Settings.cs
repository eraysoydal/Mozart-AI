namespace CleanArchitecture.Core.Settings
{
    public class S3Settings
    {
        public string BucketName { get; set; }
        public string Region { get; set; }
        // Credentials are loaded from environment variables or IAM roles
        // Do NOT store AccessKey/SecretKey in appsettings.json
    }
}
