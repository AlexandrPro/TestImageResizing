using System;
using System.Drawing.Imaging;
using System.IO;
namespace TestImageResizing
{
    class Program
    {
        
        
        static void Main(string[] args)
        {
            ImageManager imageManager = new ImageManager();

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("------------------------");
                Console.WriteLine("Input image path");
                //var filename = Console.ReadLine();
                var fileName = i.ToString() + ".png";

                if (!imageManager.CheckIsPDF(fileName))
                {
                    Console.WriteLine("Not PNG signature");
                    continue;
                }

                byte[] inputFile = File.ReadAllBytes(fileName);

                imageManager.TryAllMethods(fileName, inputFile);
                
                Console.WriteLine("Done");
            }
            Console.WriteLine("End");
            Console.ReadKey();
        }

        public static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }

            return null;
        }
    }
}
