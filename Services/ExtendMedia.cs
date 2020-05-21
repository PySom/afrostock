using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaToolkit.Core;

namespace MediaToolkit.Tasks
{
    /// <summary>
    /// The task saves the video thumbnail.
    /// The result is a dummy value.
    /// </summary>
    public class FfTaskGetVideoPortion : FfMpegTaskBase<int>
    {
        private readonly string _inputFilePath;
        private readonly string _outputFilePath;
        private readonly TimeSpan _seekSpan;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="inputFilePath">Full path to the input video file.</param>
        /// <param name="outputFilePath">Full path to the output video file.</param>
        /// <param name="seekSpan">The frame timespan.</param>
        public FfTaskGetVideoPortion(string inputFilePath, string outputFilePath, TimeSpan seekSpan)
        {
            this._inputFilePath = inputFilePath;
            this._outputFilePath = outputFilePath;
            this._seekSpan = seekSpan;
        }
        //ffmpeg -ss 00:01:00 -i input.mp4 -to 00:02:00 -c copy output.mp4
        /// <summary>
        /// FfTaskBase.
        /// </summary>
        public override IList<string> CreateArguments()
        {
            var arguments = new[]
            {
        "-ss",
        "00:00:00",
        "-i",
        $@"{this._inputFilePath}",
        "-to",
        this._seekSpan.TotalSeconds.ToString(),
        "-c",
        "copy",
        $@"{this._outputFilePath}",
      };

            return arguments;
        }

        /// <summary>
        /// FfTaskBase.
        /// </summary>
        public override async Task<int> ExecuteCommandAsync(IFfProcess ffProcess)
        {
            await ffProcess.Task;
            return 0;
        }
    }
}