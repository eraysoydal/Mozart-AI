using System.Threading.Tasks;

namespace CleanArchitecture.Core.Interfaces
{
    public interface IFFmpegService
    {
        /// <summary>
        /// Segments the source audio file into HLS format (.m3u8 + .ts chunks).
        /// Returns the path to the generated .m3u8 playlist file.
        /// </summary>
        Task<string> SegmentToHlsAsync(string sourceFilePath, string outputDirectory, string outputNamePrefix);

        /// <summary>
        /// Converts an audio file to the specified format and returns the output file path.
        /// </summary>
        Task<string> ConvertAsync(string sourceFilePath, string targetFormat, string outputDirectory);
    }
}
