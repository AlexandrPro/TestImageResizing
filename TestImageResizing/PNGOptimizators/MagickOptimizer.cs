using ImageMagick;
using System.IO;

namespace TestImageResizing.PNGOptimizators
{
    public class MagickOptimizer
    {
        public static byte[] Use(byte[] bytes)
        {
            return UseCustom(bytes);
        }

        private static byte[] UseDefault(byte[] bytes)
        {
            var filename = "magick.png";
            File.WriteAllBytes(filename, bytes);

            ImageOptimizer optimizer = new ImageOptimizer();
            optimizer.LosslessCompress(filename);

            var outputBytes = ImageManager.ReadFromFileToByteArray(filename);
            File.Delete(filename);

            return outputBytes;
        }

        private static byte[] UseCustom(byte[] bytes)
        {
            var filename = "magick.png";
            File.WriteAllBytes(filename, bytes);

            var fileinfo = new FileInfo(filename);

            ImageOptimizer optimizer = new ImageOptimizer();
            optimizer.LosslessCompress(fileinfo);

            var outputBytes = ImageManager.ReadFromFileToByteArray(filename);
            File.Delete(filename);

            return outputBytes;
        }
    }
}
