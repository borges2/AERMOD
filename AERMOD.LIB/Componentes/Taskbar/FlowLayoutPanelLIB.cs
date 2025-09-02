using System.Windows.Forms;
using System.ComponentModel;

namespace AERMOD.LIB.Componentes.Taskbar
{
    [ToolboxItem(true)]
    public class FlowLayoutPanelLIB : FlowLayoutPanel
    {
        /// <summary>
        /// Utilizado para retirar o refresh da imagem de fundo
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        public FlowLayoutPanelLIB()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, false);
            this.UpdateStyles();
        }
    }
}
