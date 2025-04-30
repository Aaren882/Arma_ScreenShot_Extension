using System;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing.Imaging;

namespace Arma_ScreenShot_Extension
{
    static class ScreenCapture
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT rect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        public static async Task<string> TakeScreenshot(string dir, string outputFilePath)
        {
            try
            {
                IntPtr handle = GetForegroundWindow();
                RECT rect;
                GetWindowRect(handle, out rect);

                int width = rect.Right - rect.Left;
                int height = rect.Bottom - rect.Top;

                using (Bitmap bitmap = new Bitmap(width, height))
                {
                    //- Get Format
                    ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
                    var myEncoder = System.Drawing.Imaging.Encoder.Quality;

                    //- Print Screen
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.CopyFromScreen(rect.Left, rect.Top, 0, 0, new Size(width, height));
                    }

                    //- Encoding Settings
                    EncoderParameters myEncoderParameters = new EncoderParameters(1);
                    EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 50L);
                    myEncoderParameters.Param[0] = myEncoderParameter;

                    //- Save the File
                    await Task.Delay(100);
                    bitmap.Save(outputFilePath, jpgEncoder, myEncoderParameters);

                    //- Check whether the folder is full
                    CheckMaxFile(dir);
                }
                return outputFilePath;
            }
            catch (Exception i)
            {
                return $"ERROR: \n{i}\n{outputFilePath}";
            }
        }
        static void CheckMaxFile(string folderPath)
        {
            try
            {
                // Get all pics files in the specified directory
                string[] jpgFiles = Directory.GetFiles(folderPath, "*.jpg");
                string[] pngFiles = Directory.GetFiles(folderPath, "*.png");

                // Combine all arrays
                string[] allFiles = jpgFiles.Concat(pngFiles).ToArray();

                double totalSizeBytes = 0;

                // List all jpg files
                foreach (string file in allFiles)
                {
                    totalSizeBytes = totalSizeBytes + new FileInfo(file).Length;
                }

                double totalSizeMB = totalSizeBytes / (1024.0 * 1024.0);
                if (totalSizeMB > DllEntry.MaxSize)
                {
                    File.Delete(allFiles.OrderBy(x => new FileInfo(x).CreationTime).FirstOrDefault());
                }
            }
            catch (Exception i)
            {
                Tools.Logger(i, i.ToString());
            }
        }
        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
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
