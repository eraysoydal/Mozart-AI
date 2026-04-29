namespace CleanArchitecture.Core.Settings
{
    public class S3Settings
    {
        public string BucketName { get; set; }
        public string Region { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        /// <summary>Base folder prefix for uploaded tracks (e.g. "tracks/")</summary>
        public string KeyPrefix { get; set; } = "tracks/";
    }
}
