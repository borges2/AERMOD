using AERMOD.LIB.Desenvolvimento;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AERMOD.CamadaApresentacao
{
    public partial class FrmNomeArquivo : Form
    {
        #region Instâncias e Propriedades

        public string NomeArquivo { get; private set; }

        #endregion

        #region Construtor

        public FrmNomeArquivo()
        {
            InitializeComponent();
        }

        #endregion

        #region Eventos FrmNomeArquivo

        private void FrmNomeArquivo_Load(object sender, EventArgs e)
        {
            tbxNomeArquivo.Text = "SAMSON";
        }

        private void FrmNomeArquivo_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    e.SuppressKeyPress = true;
                    this.Close();
                    break;
                case Keys.Enter:
                    if (string.IsNullOrEmpty(tbxNomeArquivo.Text.Trim()) == false)
                    {
                        string nomeArquivo = tbxNomeArquivo.Text.RemoverCaracterEspecial();
                        if (nomeArquivo == tbxNomeArquivo.Text)
                        {
                            NomeArquivo = tbxNomeArquivo.Text;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show(this, "Nome inválido.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show(this, "Nome inválido.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    break;
            }
        }

        #endregion
    }
}
