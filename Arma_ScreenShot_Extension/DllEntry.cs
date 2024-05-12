using RGiesecke.DllExport;
using System.Text;
using System.Runtime.InteropServices;

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
          Tools.Logger(null, "Attempted re-initialization");
      }
    }
    #endregion
  }
}
