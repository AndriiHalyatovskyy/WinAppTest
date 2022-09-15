using SharpAvi;
using SharpAvi.Codecs;
using SharpAvi.Output;
using System.Windows.Forms;

namespace WinAppTest.VideoRecorder
{
    public class RecorderParams
    {
        public RecorderParams(string filename, int FrameRate, FourCC Encoder, int Quality)
        {
            FileName = filename;
            FramesPerSecond = FrameRate;
            Codec = Encoder;
            this.Quality = Quality;

            Height = Screen.PrimaryScreen.Bounds.Height;
            Width = Screen.PrimaryScreen.Bounds.Width;
        }

        private string FileName;
        public int FramesPerSecond, Quality;
        FourCC Codec;

        public int Height { get; private set; }
        public int Width { get; private set; }

        public AviWriter CreateAviWriter()
        {
            return new AviWriter(FileName)
            {
                FramesPerSecond = FramesPerSecond,
                EmitIndex1 = true,
            };
        }

        public IAviVideoStream CreateVideoStream(AviWriter writer)
        {
            if (Codec == CodecIds.Uncompressed)
                return writer.AddUncompressedVideoStream(Width, Height);
            else if (Codec == CodecIds.MotionJpeg)
                return writer.AddMJpegWpfVideoStream(Width, Height, Quality);
            else
            {
                return writer.AddMpeg4VcmVideoStream(Width, Height, (double)writer.FramesPerSecond,
                    quality: Quality,
                    codec: Codec,
                    forceSingleThreadedAccess: true);
            }
        }
    }
}
