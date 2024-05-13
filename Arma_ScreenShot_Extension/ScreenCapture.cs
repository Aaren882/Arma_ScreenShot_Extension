using System;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;

namespace Arma_ScreenShot_Extension
{
    internal class ScreenCapture
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

        public string TakeScreenshot(string outputFilePath)
        {
            try
            {
                IntPtr handle = GetForegroundWindow();
                RECT rect;
                GetWindowRect(handle, out rect);

                int width = rect.Right - rect.Left;
                int height = rect.Bottom - rect.Top;
                //outputFilePath = outputFilePath.Trim('"',' ').Replace("/", @"\");

                using (Bitmap bitmap = new Bitmap(width, height))
                {
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.CopyFromScreen(rect.Left, rect.Top, 0, 0, new Size(width, height));
                    }

                    bitmap.Save(outputFilePath);
                }
                return outputFilePath;
            }
            catch (Exception i)
            {
                return $"Error: \n{i}\n{outputFilePath}";
            }
        }
    }
}
