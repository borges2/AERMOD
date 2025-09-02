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

namespace AERMOD.LIB.Forms
{
    public partial class FrmFade : Form
    {
        #region Classes e Propriedades

        public double fator = 0.025;

        public System.Timers.Timer timerOpacidade = new System.Timers.Timer();

        #endregion

        #region Construtor padrão

        public FrmFade()
        {
            InitializeComponent();

            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        #endregion

        #region Eventos FrmFade

        protected override void OnLoad(EventArgs e)
        {
            timerOpacidade.Elapsed += new System.Timers.ElapsedEventHandler(timerOpacidade_Tick);
            timerOpacidade.Interval = 15;

            timerOpacidade.Enabled = true;

            base.OnLoad(e);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            timerOpacidade.Enabled = false;
            base.OnClosing(e);
        }

        #endregion

        #region Eventos timerOpacidade

        private void timerOpacidade_Tick(object sender, EventArgs e)
        {
            if (this.Opacity < 0.65)
            {
                try
                {
                    CrossThreadOperation.Invoke(this, delegate
                    {
                        this.Opacity += fator;
                    });
                }
                catch { }
            }
            else
            {
                timerOpacidade.Enabled = false;
            }
        }

        #endregion

        #region Métodos

        public void DoEvents()
        {
            CrossThreadOperation.Invoke(this, delegate
            {
                Application.DoEvents();
                this.Refresh();
            });
        }

        #endregion
    }
}
