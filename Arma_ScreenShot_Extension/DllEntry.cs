using RGiesecke.DllExport;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Arma_ScreenShot_Extension
{
    public class DllEntry
    {
        public static int MaxSize = 25;

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
        public static async void RvExtension(StringBuilder output, int outputSize,
            [MarshalAs(UnmanagedType.LPStr)] string input)
        {
            outputSize--;
            string dir;

            // Convert each code point to a character and combine them into a single string
            string[] codePointStrings = Regex.Replace(input, @"[\[\]]", "").Split(',');
            string path = string.Concat(codePointStrings.Select(cp => char.ConvertFromUtf32(int.Parse(cp))));

            if (path.IndexOf(@"\",0) < 0 && path.IndexOf("/", 0) < 0)
            {
                dir = $@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\Screenshot";

                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                path = Path.Combine(dir, path);
            } else {
                string[] array = path.Split('\\');
                string removal = array[array.Length - 1];

                dir = string.Join(@"\", array.Where(val => val != removal).ToArray());
            }
            if (path.IndexOf(":") < 0)
            {
                output.Append("ERROR Invaild Directory");
                return;
            }

            int[] code32 = new int[path.Length];

            for (int i = 0; i < path.Length; i++)
            {
                // Get the Unicode code point of each character in the string
                code32[i] = char.ConvertToUtf32(path, i);

                // If the character is a surrogate pair, skip the next character
                if (char.IsHighSurrogate(path, i))
                {
                    i++;
                }
            }

            output.Append($"[{string.Join(",", code32)}]");
            await ScreenCapture.TakeScreenshot(dir,path);
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

            if (inputKey == "MaxSize")
            {
                MaxSize = int.Parse(args[0]);
                output.Append($"MaxSize success set as {MaxSize}");
                return MaxSize;
            }
            return -1;
        }
    }
}
