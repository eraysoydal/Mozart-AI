namespace CleanArchitecture.Core.Interfaces
{
    public interface ICloudFrontService
    {
        string GetSignedUrl(string fileName);
    }
}
