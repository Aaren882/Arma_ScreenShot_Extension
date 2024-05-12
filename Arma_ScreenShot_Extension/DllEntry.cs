using Maca134.Arma.DllExport;
using System.Text;

namespace Arma_ScreenShot_Extension
{
    public class DllEntry
    {
        [ArmaDllExport]
       static void RvExtension(StringBuilder output, int outputSize)
       {
            return;
       }
    }
}
