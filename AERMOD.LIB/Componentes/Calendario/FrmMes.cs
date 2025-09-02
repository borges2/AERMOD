using System;
using System.Windows.Forms;

namespace Netsof.LIB.Componentes.Calendario
{
    public partial class FrmMes : Form
    {
        public string mes = string.Empty;
        
        public FrmMes()
        {
            InitializeComponent();
        }

        private void FrmMes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnJaneiro_Click(object sender, EventArgs e)
        {
            mes = ((Control)sender).Text;
            this.Close();
        }       
        
    }
}
