using AERMOD.LIB;
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

namespace AERMOD
{
    public partial class FrmUsuario : Form
    {
        #region Instâncias e Propriedades

        #endregion

        #region Construtor

        public FrmUsuario()
        {
            InitializeComponent();

            this.Icon = Properties.Resources.pessoas.ConvertImageToIcon();
        }

        #endregion
    }
}
