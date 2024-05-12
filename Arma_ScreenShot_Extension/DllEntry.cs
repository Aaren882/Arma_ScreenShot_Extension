using Maca134.Arma.DllExport;
using System.Text;
using RGiesecke.DllExport;

namespace Arma_ScreenShot_Extension
{
    public class DllEntry
    {
        [ArmaDllExport]
       static void RvExtension(StringBuilder output, int outputSize)
       {
            output.Append("TEst DLL Entry");
       }
    }
}
