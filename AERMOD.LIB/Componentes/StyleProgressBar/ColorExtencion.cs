using System.Drawing;

namespace AERMOD.LIB.Componentes.StyleProgressBar
{
    public static class ColorExtencion
    {
        public static Color ToColor(this int argb)
        {
            return Color.FromArgb((argb & 0xff0000) >> 0x10, (argb & 0xff00) >> 8, argb & 0xff);
        }
    }
}
