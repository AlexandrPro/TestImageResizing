using System.Collections.Generic;
using System.Drawing;
using System.IO;
using TestImageResizing.PNGOptimizators;

namespace TestImageResizing
{
    public class ImageManager
    {
        public delegate byte[] some_delegate(byte[] bytes);
        private Dictionary<string, some_delegate> methods = new Dictionary<string, some_delegate>();

        public ImageManager()
        {
            methods.Add("nQuant", nQuantOptimizer.Use);
            methods.Add("OptiPNG", OptiPNGOptimizer.Use);
            methods.Add("ImageMagick", MagickOptimizer.Use);
        }

        public void TryAllMethods(string fileName, byte[] inputFile)
        {
            foreach (var method in methods)
            {
                TryMethod(method, fileName, inputFile);
            }
        }
        
        private readonly byte[] signatureOfPNG = new byte[8] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };
        public bool CheckIsPDF(string filename)
        {
            using (var reader = new BinaryReader(File.Open(filename, FileMode.Open)))
            {
                byte[] binary = reader.ReadBytes(8);

                for (int i = 0; i < 7; i++)
                {
                    if (binary[i] != signatureOfPNG[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        private void TryMethod(KeyValuePair<string, some_delegate> metod, string fileName, byte[] bytes)
        {
            byte[] outputBytes = metod.Value.Invoke(bytes);
            string path = fileName.Replace(".png", "");
            path = path + "_" + metod.Key + ".png";
            File.WriteAllBytes(path, outputBytes);
        }

        public static byte[] ReadFromFileToByteArray(string fileName)
        {
            var bytes = File.ReadAllBytes(fileName);
            return bytes;
        }

        public static Bitmap ConvertByteToBitmap(byte[] bytes)
        {
            MemoryStream ms = new MemoryStream(bytes);
            Image img = Image.FromStream(ms);
            Bitmap btm = new Bitmap(img);
            return btm;
        }
    }
}
