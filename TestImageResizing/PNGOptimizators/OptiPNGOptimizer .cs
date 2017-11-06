using System.Diagnostics;
using System.IO;

namespace TestImageResizing.PNGOptimizators
{
    public class OptiPNGOptimizer
    {
        public static byte[] Use(byte[] bytes)
        {
            return UseDefault(bytes);
        }

        private static byte[] UseDefault(byte[] bytes)
        {
            var filename = "optipng.png";
            File.WriteAllBytes(filename, bytes);

            int optimizationLevel = 1;
            UseOptiPNG(filename, optimizationLevel);
            
            var outputBytes = ImageManager.ReadFromFileToByteArray(filename);
            File.Delete(filename);

            return outputBytes;
        }

        private static void UseOptiPNG(string imputFilePath, int optimizationLevel)
        {
            string parameters = "-o" + optimizationLevel + " " + imputFilePath;
            var optipngPath = "optipng.exe";

            var process = new Process();
            process.StartInfo.FileName = optipngPath;
            process.StartInfo.Arguments = parameters;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;

            process.Start();
            process.WaitForExit();
            process.Dispose();
        }
    }
}
