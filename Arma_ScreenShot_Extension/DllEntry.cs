using RGiesecke.DllExport;
using System.Text;
using System.Runtime.InteropServices;

namespace Arma_ScreenShot_Extension
{
  public class DllEntry
  {
    private static readonly string SessionKey = "WWW";
    private static bool InitComplete = false;

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
      if (function == "init")
      {
        if (!InitComplete)
        {
          InitComplete = true;
          //Tools.Logger(null, "Initialized");

          output.Append(SessionKey);
        }
        else
          output.Append("Not Init");
      }
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
          outputSize--;
          try
          {
            output.Append("TEST TEST Try");
          }
          catch (Exception e)
          {
            output.Append("TEST TEST Catch");
          }
          return 1;
        }
    }
}
