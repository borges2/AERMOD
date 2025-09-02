#region Using

using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#endregion

namespace AERMOD.LIB.Componentes.MsgBox
{
    internal partial class FrmMessageBox : Form
    {
        #region Declarações

        /// <summary>
        /// Objeto TimerControl (Controle, Tempo, Texto).
        /// Utilizado para botão com timer de decremento habilitado.
        /// </summary>
        TimerControl timerControl = new TimerControl();

        /// <summary>
        /// Botão referenciado.
        /// </summary>
        private MessageBoxButton messageButton = new MessageBoxButton();

        /// <summary>
        /// Retorna o botão clicado.
        /// </summary>
        public MessageBoxButton GetMessageButton
        {
            get { return messageButton; }
        }

        /// <summary>
        /// Obrigatorio escolher um valor
        /// </summary>
        public bool ChooseValue { get; set; }

        public Color? ColorButton { get; set; }

        public string Caption
        {
            set
            {
                if (this.Text != value)
                {
                    this.Text = value;
                }
            }
        }

        public string Texto
        {
            set
            {
                if (lbText.Text != value)
                {
                    lbText.Text = value;                   
                }
            }
        }        

        /// <summary>
        /// Alterar fonte do texto.
        /// </summary>
        public Font FonteTexto
        {            
            set
            {
                if (lbText.Font != value)
                {
                    lbText.Font = value;
                }
            }
        }

        public Image Imagem
        {
            set
            {
                pictureBoxIcon.Visible = (value == null) ? false : true;
                pictureBoxIcon.Image = value;
            }
        }

        private MessageBoxButton[] boxButtons;

        public MessageBoxButton[] BoxButtons
        {
            set
            {
                boxButtons = value;

                Int32 index = value.Length;
                foreach (MessageBoxButton item in value)
                {
                    Button button = new Button();
                    button.Text = item.HabilitaTimer == false ? item.Texto : string.Format("{0} ({1})", item.Texto, item.TimerInicialDecremento);
                    button.Tag = item.Id;
                    button.MinimumSize = new System.Drawing.Size(90, 27);
                    button.MaximumSize = new System.Drawing.Size(0, 27);
                    button.AutoSize = true;
                    button.TabIndex = --index;
                    button.Click += new EventHandler(button_Click);
                    button.UseMnemonic = true;

                    if (ColorButton.HasValue == true)
                    {
                        button.BackColor = ColorButton.Value;
                    }

                    flowLayoutPanelBotton.Controls.Add(button);

                    if (item.HabilitaTimer)
                    {
                        timerControl.Controle = button;
                        timerControl.Tempo = item.TimerInicialDecremento;
                        timerControl.Texto = item.Texto;
                    }
                }
            }
            get
            {
                return boxButtons;
            }
        }

        #endregion

        #region Construtor

        public FrmMessageBox()
        {
            InitializeComponent();

            messageButton.Id = 0;
            messageButton.Texto = "None";
        }

        #endregion

        #region Eventos FrmMessageBox

        private void FrmMessageBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }

            #region ID do botão pressionado

            int keyVal = (int)e.KeyValue;
            int value = -1;
            if (keyVal >= (int)Keys.D0 && keyVal <= (int)Keys.D9)
            {
                value = (int)e.KeyValue - (int)Keys.D0;
            }
            else if (keyVal >= (int)Keys.NumPad0 && keyVal <= (int)Keys.NumPad9)
            {
                value = (int)e.KeyValue - (int)Keys.NumPad0;
            }

            if (value >= 0)
            {
                foreach (Control controle in flowLayoutPanelBotton.Controls)
                {
                    MessageBoxButton botao = BoxButtons.FirstOrDefault(I => I.Id == Convert.ToInt32(controle.Tag));

                    if (botao != null && botao.Id == value && botao.AtalhoNumerico)
                    {
                        ((Button)controle).PerformClick();
                    }
                }
            }

            #endregion
        }

        protected override void OnLoad(EventArgs e)
        {
            if (timerControl.Controle != null)
            {
                timerDecremento.Enabled = true;
            }

            base.OnLoad(e);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (ChooseValue == true && messageButton.Id == 0)
            {
                e.Cancel = true;
                return;
            }
            
            timerDecremento.Enabled = false;
            base.OnFormClosing(e);
        }

        #endregion

        #region Eventos button

        private void button_Click(object sender, EventArgs e)
        {
            messageButton.Id = Convert.ToInt32(((Button)sender).Tag);
            messageButton.Texto = ((Button)sender).Text;
            this.Close();
        }

        #endregion

        #region Eventos timerDecremento

        private void timerDecremento_Tick(object sender, EventArgs e)
        {
            Button button = (Button)timerControl.Controle;
            timerControl.Tempo--;
            if (timerControl.Tempo == -1)
            {
                timerDecremento.Enabled = false;
                button_Click(button, new EventArgs());
            }
            else
            {
                button.Text = string.Format("{0} ({1})", timerControl.Texto, timerControl.Tempo);
            }
        }

        #endregion
    }

    internal class TimerControl
    {
        /// <summary>
        /// Botão que possui o timer.
        /// </summary>
        public Control Controle { get; set; }

        /// <summary>
        /// Tempo para decremento.
        /// </summary>
        public int Tempo { get; set; }

        /// <summary>
        /// Texto do botão.
        /// </summary>
        public string Texto { get; set; }
    }
}
