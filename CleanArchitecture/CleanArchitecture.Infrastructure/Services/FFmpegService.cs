using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using CleanArchitecture.Core.Interfaces;

namespace CleanArchitecture.Infrastructure.Services
{
    /// <summary>
    /// FFmpeg-based audio processing service.
    /// Requires ffmpeg to be installed and accessible via PATH (or /usr/bin/ffmpeg on Linux).
    /// </summary>
    public class FFmpegService : IFFmpegService
    {
        private const string FfmpegBinary = "ffmpeg";

        public async Task<string> SegmentToHlsAsync(
            string sourceFilePath,
            string outputDirectory,
            string outputNamePrefix)
        {
            if (!File.Exists(sourceFilePath))
                throw new FileNotFoundException($"Source file not found: {sourceFilePath}");

            Directory.CreateDirectory(outputDirectory);

            var playlistPath = Path.Combine(outputDirectory, $"{outputNamePrefix}.m3u8");
            var segmentPattern = Path.Combine(outputDirectory, $"{outputNamePrefix}_%03d.ts");

            // ffmpeg -i <source> -c:a aac -b:a 128k -hls_time 10 -hls_list_size 0
            //        -hls_segment_filename <segments> <playlist>
            var args = $"-i \"{sourceFilePath}\" " +
                       $"-c:a aac -b:a 128k " +
                       $"-hls_time 10 -hls_list_size 0 " +
                       $"-hls_segment_filename \"{segmentPattern}\" " +
                       $"\"{playlistPath}\" -y";

            await RunFfmpegAsync(args);

            return playlistPath;
        }

        public async Task<string> ConvertAsync(
            string sourceFilePath,
            string targetFormat,
            string outputDirectory)
        {
            if (!File.Exists(sourceFilePath))
                throw new FileNotFoundException($"Source file not found: {sourceFilePath}");

            Directory.CreateDirectory(outputDirectory);

            var outputFileName = $"{Path.GetFileNameWithoutExtension(sourceFilePath)}.{targetFormat.TrimStart('.')}";
            var outputPath = Path.Combine(outputDirectory, outputFileName);

            var args = $"-i \"{sourceFilePath}\" \"{outputPath}\" -y";

            await RunFfmpegAsync(args);

            return outputPath;
        }

        private static Task RunFfmpegAsync(string arguments)
        {
            return Task.Run(() =>
            {
                var psi = new ProcessStartInfo
                {
                    FileName = FfmpegBinary,
                    Arguments = arguments,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using var process = new Process { StartInfo = psi };
                process.Start();
                process.WaitForExit();

                if (process.ExitCode != 0)
                {
                    var error = process.StandardError.ReadToEnd();
                    throw new InvalidOperationException($"FFmpeg failed (exit {process.ExitCode}): {error}");
                }
            });
        }
    }
}
