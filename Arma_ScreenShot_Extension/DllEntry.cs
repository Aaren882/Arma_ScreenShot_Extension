using RGiesecke.DllExport;
using Maca134.Arma.DllExport;
using System.Text;
using System.Runtime.InteropServices;

namespace Arma_ScreenShot_Extension
{
    public class DllEntry
    {
      [ArmaDllExport]

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
    [MarshalAs(UnmanagedType.LPStr)] string function)
    {
      outputSize--;
      output.Append("ScreenShot TEST");
    }

#if IS_x64
    [DllExport("RVExtensionArgs", CallingConvention = CallingConvention.Winapi)]
#else
    [DllExport("_RVExtensionArgs@20", CallingConvention = CallingConvention.Winapi)]
#endif
    #endregion

    /*public static int RvExtensionArgs(StringBuilder output, int outputSize,
          [MarshalAs(UnmanagedType.LPStr)] string inputKey,
          [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 4)] string[] args, int argCount)
        {
            output.Append("ScreenShot Test");
            return 1;
        }*/
    }
}
