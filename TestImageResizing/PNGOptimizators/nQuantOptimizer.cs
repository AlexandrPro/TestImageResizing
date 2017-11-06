using System;
using nQuant;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace TestImageResizing.PNGOptimizators
{
    public static class nQuantOptimizer
    {
        public static byte[] Use(byte[] bytes)
        {
            return UseDefault(bytes);
        }

        private static byte[] UseDefault(byte[] bytes)
        {
            Bitmap image = ImageManager.ConvertByteToBitmap(bytes);
            string outputFileName = "nunit" + ".png";
            var quantizer = new WuQuantizer();
            try
            {
                var copy = new Bitmap(image);
                using (var quantized = quantizer.QuantizeImage(copy))
                {

                    quantized.Save(outputFileName, ImageFormat.Png);

                }
            }
            catch (QuantizationException ex)
            {
                Console.WriteLine(ex.Message);
                return bytes;
            }

            byte[] outputBytes = ImageManager.ReadFromFileToByteArray(outputFileName);
            File.Delete(outputFileName);

            return outputBytes;
        }

        private static byte[] UseCastom(byte[] bytes)
        {
            Bitmap image = ImageManager.ConvertByteToBitmap(bytes);
            string outputFileName = "nunit" + ".png";
            var quantizer = new WuQuantizer();
            try
            {
                var copy = new Bitmap(image);
                using (var quantized = quantizer.QuantizeImage(copy, 0, 1))
                {

                    quantized.Save(outputFileName, ImageFormat.Png);

                }
            }
            catch (QuantizationException ex)
            {
                Console.WriteLine(ex.Message);
                return bytes;
            }

            byte[] outputBytes = ImageManager.ReadFromFileToByteArray(outputFileName);
            File.Delete(outputFileName);

            return outputBytes;
        }
    }
}
