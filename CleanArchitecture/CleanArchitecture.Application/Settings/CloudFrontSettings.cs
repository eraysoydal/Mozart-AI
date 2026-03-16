namespace CleanArchitecture.Core.Settings
{
    public class CloudFrontSettings
    {
        public string Domain { get; set; }
        public string KeyPairId { get; set; }
        public string PrivateKeyPath { get; set; }
        public int ExpirationHours { get; set; }
    }
}
