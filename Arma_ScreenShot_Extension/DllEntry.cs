using RGiesecke.DllExport;
using System.Text;
using System.Runtime.InteropServices;

namespace Arma_ScreenShot_Extension
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
  #endregion
}
