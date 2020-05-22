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


    public class FfTaskGetOverlay : FfMpegTaskBase<int>
    {
        private readonly string _inputFilePath;
        private readonly string _outputFilePath;
        private readonly string _watermark;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="inputFilePath">Full path to the input video file.</param>
        /// <param name="outputFilePath">Full path to the output video file.</param>
        /// <param name="seekSpan">The frame timespan.</param>
        public FfTaskGetOverlay(string inputFilePath, string outputFilePath, string watermark)
        {
            this._inputFilePath = inputFilePath;
            this._outputFilePath = outputFilePath;
            this._watermark = watermark;
        }
        //ffmpeg -i input.mp4 -i logo.png -filter_complex "overlay=(main_w-overlay_w)/2:(main_h-overlay_h)/2" -codec:a copy output.mp4
        /// <summary>
        /// FfTaskBase.
        /// </summary>
        public override IList<string> CreateArguments()
        {
            var overlay = "overlay=x=(main_w-overlay_w)/2:y=(main_h-overlay_h)/2";
            var arguments = new[]
            {
                "-nostdin",
                "-y",
                "-loglevel",
                "info",
                "-i",
                $@"{this._inputFilePath}",
                "-i",
                $@"{this._watermark}",
                "-filter_complex",
                $"{overlay}",
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

    public class FfTaskReduceVideo : FfMpegTaskBase<int>
    {
        private readonly string _inputFilePath;
        private readonly string _outputFilePath;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="inputFilePath">Full path to the input video file.</param>
        /// <param name="outputFilePath">Full path to the output video file.</param>
        public FfTaskReduceVideo(string inputFilePath, string outputFilePath)
        {
            this._inputFilePath = inputFilePath;
            this._outputFilePath = outputFilePath;
        }
        //ffmpeg -i $infile -vf "scale=iw/2:ih/2" $outfile
        /// <summary>
        /// FfTaskBase.
        /// </summary>
        public override IList<string> CreateArguments()
        {
            var scale = "scale=iw/2:ih/2";
            var arguments = new[]
            {
                "-nostdin",
                "-y",
                "-loglevel",
                "info",
                "-i",
                $@"{this._inputFilePath}",
                "-vf",
                $"{scale}",
                $@"{this._outputFilePath}"
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