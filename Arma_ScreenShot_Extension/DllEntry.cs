using RGiesecke.DllExport;
using System;
using System.Drawing.Drawing2D;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace Arma_ScreenShot_Extension
{
    public class DllEntry
    {

        #region Misc RVExtension Requirements
#if IS_x64
        [DllExport("RVExtensionVersion", CallingConvention = CallingConvention.Winapi)]
#else
    [DllExport("_RVExtensionVersion@8", CallingConvention = CallingConvention.Winapi)]
#endif
        public static void RvExtensionVersion(StringBuilder output, int outputSize)
        {
            outputSize--;
            output.Append("1.0.0");
        }

#if IS_x64
        [DllExport("RVExtension", CallingConvention = CallingConvention.Winapi)]
#else
    [DllExport("_RVExtension@12", CallingConvention = CallingConvention.Winapi)]
#endif
        public static void RvExtension(StringBuilder output, int outputSize,
            [MarshalAs(UnmanagedType.LPStr)] string path)
        {
            outputSize--;
            if (path.IndexOf(@"\",0) < 0 && path.IndexOf("/", 0) < 0)
            {
                string dir = $@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\Screenshot";

                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                path = Path.Combine(dir, path);
            }
            output.Append(new ScreenCapture().TakeScreenshot(path));
        }

#if IS_x64
        [DllExport("RVExtensionArgs", CallingConvention = CallingConvention.Winapi)]
#else
    [DllExport("_RVExtensionArgs@20", CallingConvention = CallingConvention.Winapi)]
#endif
        #endregion
        public static int RvExtensionArgs(StringBuilder output, int outputSize,
            [MarshalAs(UnmanagedType.LPStr)] string inputKey,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 4)] string[] args, int argCount)
        {
            return 1;
        }
    }
}
