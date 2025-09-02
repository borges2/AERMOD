#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;
using AERMOD.LIB.Formatacao;
using Netsof.LIB.Componentes.Calendario;
using System.Reflection;
using AERMOD.LIB.Desenvolvimento;

#endregion

namespace AERMOD.LIB.Componentes
{
    [ToolboxBitmapAttribute(typeof(MaskedTextBox))]
    public partial class DataLIB : UserControl
    {
        #region Variáveis

        /// <summary>
        /// Pega data do controle Mcalendario.
        /// </summary>
        public String dataCalendario;

        /// <summary>
        /// Usado para posicionar cursor dentro do campo.
        /// </summary>
        /// <param name="posicao">Posição do cursor dentro do controle.</param>
        private delegate void PosicionaCursorDelegate(int posicao);

        /// <summary>
        /// Variável para indicar status do comando Overwrite/Insert.
        /// </summary>
        int _statusInsert = 0;

        [DefaultValue(typeof(KeysOpen), "F2")]
        public KeysOpen KeysOpenCalencar { get; set; }

        public enum KeysOpen
        {
            F2,
            Alt_F2
        }

        bool setFocus = false;
        public new bool Focus()
        {
            setFocus = true;
            bool result = base.Focus();
            setFocus = false;

            return result;
        }

        private bool abrirCalendarioMouseDown = true;

        [Browsable(false)]
        public bool AbrirCalendarioMouseDown
        {
            get { return abrirCalendarioMouseDown; }
            set { abrirCalendarioMouseDown = value; }
        }

        [DefaultValue(false)]
        public Boolean LeaveInvalidDate { get; set; }

        private ToolStripDropDown popup;

        #region Seguranca

        public String CodigoCampo { get; set; }

        #endregion

        #endregion

        #region Customiza controle

        #region Variáveis

        /// <summary>
        /// Estado da tecla Insert.
        /// </summary>
        bool bInserting;

        /// <summary>
        /// Dll externa para verificar o estado da tecla pressionada.
        /// </summary>
        /// <param name="nVirtkey">Código da tecla pressionada.</param>
        /// <returns>Retorna 1 para ativo e 0 para inativo.</returns>
        [DllImport("user32.dll")]
        static extern short GetKeyState(int nVirtkey);

        /// <summary>
        /// Invoca eventos de mouse como (MouseDown, MouseUp).
        /// </summary>
        /// <param name="dwFlags">Código do click MouseUp.</param>
        /// <param name="dx">Posição x do cursor no controle.</param>
        /// <param name="dy">Posição y do cursor no controle.</param>
        /// <param name="dwData">dwData do controle.</param>
        /// <param name="dwExtraInfo">dwExtraInfo.</param>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern void mouse_event(UInt32 dwFlags, UInt32 dx, UInt32 dy, UInt32 dwData, IntPtr dwExtraInfo);

        /// <summary>
        /// Código do click MouseUp.
        /// </summary>
        const UInt32 MOUSEEVENTF_LEFTUP = 0x4;

        /// <summary>
        /// Valores para Duplicação, Incremento e Decremento.
        /// </summary>
        public double? SglVALOR; public String StrVALOR;

        /// <summary>
        /// Timer para controlar componente piscando.
        /// </summary>
        System.Windows.Forms.Timer timerPiscar = new System.Windows.Forms.Timer();

        #endregion

        #region Enumeradores

        #region CTRL_Z

        /// <summary>
        /// Controla quando ocorre exceção na saída do controle,
        /// utilizado para tratamento da tecla CTRL_Z.
        /// </summary>
        bool exception = false;

        /// <summary>
        /// Controla quando evento MouseDown é chamado ou não.
        /// Corrige bug do evento MouseUp quando não dispara para posicionar o cursor.
        /// </summary>
        bool mouseDown;

        /// <summary>
        /// Valor digitado dentro do controle para tratamento da tecla (CTRL_Z).
        /// </summary>
        string digitado = string.Empty;

        /// <summary>
        /// Índice 0 = Captura valor de entrada do campo para tratamento da tecla (CTRL_Z).
        /// Índice 1 = Captura valor auxiliar digitado no campo para tratamento da tecla (CTRL_Z).
        /// A tecla CTRL_Z pode ter até 2 valores onde são alternados.
        /// </summary>
        public String[] ctrl_z = new String[] { string.Empty, string.Empty };

        /// <summary>
        /// Enumerador para controlar qual variável está ativa (ctrl_z ou ctrl_z_aux).
        /// </summary>
        public Ctrl_z_ativo ctrl_z_ativo;

        /// <summary>
        /// Enumerador para controlar qual variável está ativa (ctrl_z ou ctrl_z_aux).
        /// </summary>
        public enum Ctrl_z_ativo
        {
            nulo,
            indice_0,
            indice_1
        }

        #endregion

        #endregion

        #endregion

        #region Propriedades

        #region FocusColor

        /// <summary>
        /// Cor de fundo atual do controle.
        /// </summary>
        Color backColor;

        [Category("Appearance")]
        [Description("Indica se está sendo usado cor de fundo quando o controle está focado.")]
        public bool FocusColorEnabled { get; set; }

        [Category("Appearance")]
        [Description("Cor de fundo quando o controle está focado.")]
        public Color FocusColor { get; set; }

        #endregion

        #region saltar

        [DefaultValue(true)]
        [Category("Behavior")]
        [Description("Permite saltar o campo.")]
        public bool Saltar
        {
            get { return _Saltar; }
            set { _Saltar = value; }
        }

        protected bool _Saltar = true;

        #endregion

        #region Tabelado

        [Category("Behavior")]
        [Description("Indica se o controle é do tipo tabelado (descrição) ou não.")]
        [DefaultValue(false)]
        [Browsable(true)]
        public bool Tabelado { get; set; }

        #endregion

        #region Validação

        public enum ValidaçãoEnum
        {
            NORMAL = 0,
            CRIAÇÃO = 1,
            IDADE = 2
        }

        protected ValidaçãoEnum tipoValidacao;

        [DefaultValue(typeof(ValidaçãoEnum), "0")]
        [Category("Behavior")]
        [Description("Define tipo de validação para máscara do tipo data. Criação: válido até data atual. Idade: válido até data atual e menor que 115 anos.")]
        public ValidaçãoEnum Validação
        {
            get { return tipoValidacao; }
            set { tipoValidacao = value; }
        }

        #endregion

        #region Piscar

        [Category("Behavior")]
        [Browsable(false)]
        [Description("Componente fica piscando caso esteja ativo.")]
        public bool Piscar
        {
            set
            {
                if (value)
                {
                    timerPiscar.Interval = 200;
                    timerPiscar.Enabled = true;
                }
                else
                {
                    timerPiscar.Enabled = false;
                    this.mtbx.SetUserPaint(false);
                    this.mtbx.Tag = null;
                }
            }
        }

        void timerPiscar_Tick(object sender, EventArgs e)
        {
            if (this.mtbx.Tag == null)
            {
                this.mtbx.SetUserPaint(true);
                this.mtbx.Tag = this.Name;
            }
            else
            {
                this.mtbx.SetUserPaint(false);
                this.mtbx.Tag = null;
            }
        }

        #endregion

        #endregion

        #region Design

        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public MaskedTextBox mtbxZone
        {
            get { return mtbx; }
        }

        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public AERMOD.LIB.Componentes.Botao.ButtonLIB ButtonZone
        {
            get { return btnCalendario; }
        }

        #endregion

        #region Construtor

        public DataLIB()
        {
            InitializeComponent();

            bInserting = Convert.ToBoolean(GetKeyState(45));

            AERMOD.LIB.Desenvolvimento.Funcoes.SetStyle(btnCalendario, ControlStyles.Selectable, false);
        }

        #endregion

        #region Eventos DataLIB

        private void DataLIB_Resize(object sender, EventArgs e)
        {
            btnCalendario.Size = new Size(26, this.Height);
        }

        #endregion

        #region Eventos btnCalendario

        private void btnCalendario_MouseDown(object sender, MouseEventArgs e)
        {
            mtbx.Focus();
            if (mtbx.Focused == false)
            {
                return;
            }

            if (abrirCalendarioMouseDown)
            {
                if (this.TabStop == false) return;

                if (popup == null || popup.Tag == null)
                {
                    OpenCalendario();
                }
                else
                {
                    popup.Tag = null;
                }
            }
        }

        #endregion

        #region Eventos frmCalendario

        private void popup_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            if (e.CloseReason == ToolStripDropDownCloseReason.AppClicked)
            {
                if (this.btnCalendario.Bounds.Contains(this.PointToClient(MousePosition)))
                {
                    popup.Tag = true;
                    return;
                }
            }

            popup.Tag = null;
        }

        private void popup_Opened(object sender, EventArgs e)
        {
            MonthCalendario month = (popup.Items[0] as ToolStripControlHost).Control as MonthCalendario;
            month.Focus();
        }

        #endregion

        #region Metodos

        public void OpenCalendario()
        {
            MonthCalendario monthCalendario = new MonthCalendario();
            monthCalendario.BackColor = Color.White;
            monthCalendario.MinimumSize = new Size(233, 167);
            monthCalendario.Size = MinimumSize;
            monthCalendario.Location = new Point(5, 5);
            monthCalendario.KeyDown += monthCalendario_KeyDown;
            monthCalendario.DoubleClick += monthCalendario_DoubleClick;          

            if (this.mtbx.Text.DeFormat() != string.Empty && this.mtbx.Text.ValidarData() == true)
            {
                monthCalendario.CurrentDate = Convert.ToDateTime(this.mtbx.Text);
            }

            popup = new ToolStripDropDown();
            popup.AutoClose = true;
            popup.Margin = new System.Windows.Forms.Padding(0);
            popup.Padding = new System.Windows.Forms.Padding(0);
            popup.Opened += popup_Opened;
            popup.Closing += popup_Closing;

            ToolStripControlHost host = new ToolStripControlHost(monthCalendario);
            host.Margin = new System.Windows.Forms.Padding(0);
            host.Padding = new System.Windows.Forms.Padding(0);
            popup.Items.Add(host);

            Rectangle area = this.ClientRectangle;

            Point location = this.PointToScreen(new Point(area.Left - monthCalendario.Width + area.Width + 2, area.Top + area.Height + 2));
            Rectangle screen = Screen.FromControl(this).WorkingArea;
            if (location.X + this.Size.Width > (screen.Left + screen.Width))
            {
                location.X = (screen.Left + screen.Width) - this.Size.Width;
            }
            if (location.Y + this.Size.Height > (screen.Top + screen.Height))
            {
                location.Y -= this.Size.Height + area.Height;
            }
            location = this.PointToClient(location);

            popup.Show(this, location, ToolStripDropDownDirection.BelowRight);
        }

        #endregion

        #region Eventos monthCalendario        

        private void monthCalendario_DoubleClick(object sender, EventArgs e)
        {
            MonthCalendario monthCalendario = sender as MonthCalendario;
            dataCalendario = monthCalendario.CurrentDate.ToString("dd/MM/yyyy");

            popup.Close();

            DateSelectedEventsArgs selected = new DateSelectedEventsArgs();
            selected.DateIsSelected = monthCalendario.CurrentDate;

            OnDateSelectedChanged(selected);

            try
            {
                mtbx.Text = dataCalendario;
                mtbx.Focus();
                this.BeginInvoke(new PosicionaCursorDelegate(PosicionaCursor), new object[] { 0 });
            }
            catch (NullReferenceException) { }
        }

        private void monthCalendario_KeyDown(object sender, KeyEventArgs e)
        {
            MonthCalendario monthCalendario = sender as MonthCalendario;

            switch (e.KeyValue)
            {
                case (int)Keys.Enter:
                    dataCalendario = monthCalendario.CurrentDate.ToString("dd/MM/yyyy");
                    popup.Close(ToolStripDropDownCloseReason.CloseCalled);

                    DateSelectedEventsArgs selected = new DateSelectedEventsArgs();
                    selected.DateIsSelected = monthCalendario.CurrentDate;

                    OnDateSelectedChanged(selected);

                    try
                    {
                        mtbx.Text = dataCalendario;
                        mtbx.Focus();
                        this.BeginInvoke(new PosicionaCursorDelegate(PosicionaCursor), new object[] { 0 });
                    }
                    catch (NullReferenceException) { }

                    //this.Size = new System.Drawing.Size(117, 24);
                    break;
                //case (int)Keys.Escape:
                //    //this.Size = new System.Drawing.Size(117, 24);
                //    mtbx.Focus();
                //    this.BeginInvoke(new PosicionaCursorDelegate(PosicionaCursor), new object[] { 0 });
                //    break;
            }

        }

        #endregion

        #region Eventos mtbx

        void mtbx_GotFocus(object sender, EventArgs e)
        {
            this.BeginInvoke(new PosicionaCursorDelegate(PosicionaCursor), new object[] { 0 });
        }

        void mtbx_LostFocus(object sender, EventArgs e)
        {
            #region FocusColor

            if (FocusColorEnabled)
            {
                mtbx.BackColor = backColor;
            }

            #endregion
        }

        private void mtbx_Enter(object sender, EventArgs e)
        {
            #region FocusColor

            if (FocusColorEnabled)
            {
                backColor = mtbx.BackColor;
                mtbx.BackColor = FocusColor;
            }

            #endregion

            #region HotKeys

            StrVALOR = mtbx.Text;
            double _valor;
            SglVALOR = null;
            if (mtbx.Text.DeFormat().Length > 0)
            {
                if (double.TryParse(mtbx.Text.DeFormat(), out _valor)) SglVALOR = _valor;
            }

            #endregion

            #region Tecla ctrl_z

            if (exception == false)
            {
                #region Seta valor no array ctrl_z

                if (ctrl_z_ativo == Ctrl_z_ativo.nulo && mtbx.Text.DeFormat().Length > 0) ctrl_z[0] = mtbx.Text;
                else if (mtbx.Text.DeFormat() != ctrl_z[0].DeFormat() && mtbx.Text.DeFormat() != ctrl_z[1].DeFormat())
                {
                    if (ctrl_z[0].DeFormat().Length > 0 && ctrl_z[1].DeFormat().Length == 0)
                    {
                        ctrl_z[1] = mtbx.Text;
                        ctrl_z_ativo = Ctrl_z_ativo.indice_0;
                    }
                    else
                    {
                        if (ctrl_z_ativo == Ctrl_z_ativo.indice_0)
                        {
                            ctrl_z[0] = mtbx.Text;
                            ctrl_z_ativo = Ctrl_z_ativo.indice_1;
                        }
                        else if (ctrl_z_ativo == Ctrl_z_ativo.indice_1)
                        {
                            ctrl_z[1] = mtbx.Text;
                            ctrl_z_ativo = Ctrl_z_ativo.indice_0;
                        }
                    }
                }
                else if (digitado.DeFormat().Length > 0 && ctrl_z_ativo == Ctrl_z_ativo.indice_0 && digitado.DeFormat() == ctrl_z[1].DeFormat() ||
                         digitado.DeFormat().Length > 0 && ctrl_z_ativo == Ctrl_z_ativo.indice_1 && digitado.DeFormat() == ctrl_z[0].DeFormat())
                {
                    //Quando o valor digitado é igual o valor que estava antes
                    if (ctrl_z_ativo == Ctrl_z_ativo.indice_1) ctrl_z_ativo = Ctrl_z_ativo.indice_0;
                    else if (ctrl_z_ativo == Ctrl_z_ativo.indice_0) ctrl_z_ativo = Ctrl_z_ativo.indice_1;
                }

                #endregion

                #region Ctrl_z_nulo

                if (mtbx.Text.DeFormat().Length == 0 && ctrl_z_ativo == Ctrl_z_ativo.nulo && ctrl_z[0].DeFormat().Length == 0)
                {
                    ctrl_z_ativo = Ctrl_z_ativo.indice_1;
                }
                else if (mtbx.Text.DeFormat().Length > 0 && ctrl_z_ativo == Ctrl_z_ativo.nulo)
                {
                    ctrl_z_ativo = Ctrl_z_ativo.indice_0;
                }
                else if (ctrl_z_ativo == Ctrl_z_ativo.nulo) ctrl_z_ativo = Ctrl_z_ativo.indice_0;

                #endregion

                digitado = string.Empty;
            }

            #endregion

            #region Tecla Insert
            
            bInserting = Convert.ToBoolean(GetKeyState(45));
            if (bInserting == true)
            {
                _statusInsert = 1;
                statusInsert();
            }
            else
            {
                _statusInsert = 2;
                statusInsert();
            }

            #endregion


            if ((Control.MouseButtons & MouseButtons.Left) != 0 && setFocus == false && AERMOD.LIB.Desenvolvimento.GetControls.CheckMouseOverControl(this))
            {
                //Corrige bug do MouseUp quando entra no controle clicando e não dispara o evento MouseUp força o click do mouse.

                Int32 x = mtbx.PointToScreen(mtbx.Location).X + 1;
                Int32 y = mtbx.PointToScreen(mtbx.Location).Y + 1;

                mouse_event(MOUSEEVENTF_LEFTUP, (UInt32)x, (UInt32)y, 0, IntPtr.Zero);
            }
        }

        private void mtbx_Leave(object sender, EventArgs e)
        {
            exception = true;
            mouseDown = false;

            if (mtbx.Text.DeFormat().Trim() != "")
            {
                ValidaData(mtbx.Text);
            }
        }

        private void mtbx_KeyDown(object sender, KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if ((KeysOpenCalencar == KeysOpen.F2 && e.Alt == false) || (KeysOpenCalencar == KeysOpen.Alt_F2 && e.Alt == true))
            {
                if (e.KeyValue == (int)Keys.F2)
                {
                    #region Calendário

                    e.Handled = true;

                    OpenCalendario();

                    #endregion
                }
            }            

            switch (e.KeyData)
            {
                case Keys.Down:
                    #region Próximo controle

                    Form.ActiveForm.SelectNextControl(Form.ActiveForm.ActiveControl, true, true, true, true);

                    #endregion
                    break;
                case Keys.Up:
                    #region Controle anterior

                    Form.ActiveForm.SelectNextControl(Form.ActiveForm.ActiveControl, false, true, true, true);

                    #endregion
                    break;                
                case Keys.Space:
                    #region Tecla espaço

                    e.SuppressKeyPress = true;

                    #endregion
                    break;
            }

            #region Corrige qdo empurra valores pra frente

            //Apenas números
            if (mtbx.SelectionStart + 1 < mtbx.Text.Length && ((e.KeyValue > 47 && e.KeyValue < 58) || (e.KeyValue > 95 && e.KeyValue < 106)))
            {
                KeysConverter keyConverter = new KeysConverter();
                String caracter = keyConverter.ConvertToInvariantString(e.KeyData).Replace("NumPad", "");

                bInserting = Convert.ToBoolean(GetKeyState(45));
                if (bInserting == false && mtbx.SelectionLength == 0)
                {
                    #region Não selecionado

                    string[] valor = mtbx.Text.Split('/');
                    if (valor[0].Trim().Length < 2 || valor[1].Trim().Length < 2 || valor[2].Trim().Length < 4)
                    {
                        int posicaoInicial = mtbx.Text[mtbx.SelectionStart] != '/' ? mtbx.SelectionStart : mtbx.SelectionStart + 1;

                        if (mtbx.Text[posicaoInicial] != ' ')
                        {
                            if (posicaoInicial < 3)
                            {
                                //Dia
                                if (valor[0].Trim().Length < 2)
                                {
                                    mtbx.Text = mtbx.Text.Insert(posicaoInicial, caracter);
                                    mtbx.Text = mtbx.Text.Remove(3);
                                    mtbx.Text = mtbx.Text.Insert(3, valor[1] + "/" + valor[2]);
                                    mtbx.SelectionStart = posicaoInicial + 1;
                                }

                                e.SuppressKeyPress = true;
                            }
                            else if (posicaoInicial > 2 && posicaoInicial < 6)
                            {
                                //Mês
                                if (valor[1].Trim().Length < 2)
                                {
                                    mtbx.Text = mtbx.Text.Insert(posicaoInicial, caracter);
                                    if (valor[2].Trim().Length > 0)
                                    {
                                        mtbx.Text = mtbx.Text.Remove(6);
                                        mtbx.Text = mtbx.Text.Insert(6, valor[2]);
                                    }
                                    mtbx.SelectionStart = posicaoInicial + 1;
                                }

                                e.SuppressKeyPress = true;
                            }
                        }
                    }

                    #endregion
                }
                else if (mtbx.SelectionLength > 0 && mtbx.SelectionLength < mtbx.Text.Length && mtbx.SelectionStart < 4 && mtbx.SelectedText != "/")
                {
                    #region Selecionado

                    int posicaoInicial = mtbx.SelectionStart == 2 || mtbx.SelectionStart == 5 ? mtbx.SelectionStart + 1 : mtbx.SelectionStart;
                    int posicaoFinal = mtbx.SelectionStart == 2 || mtbx.SelectionStart == 5 ? posicaoInicial + mtbx.SelectionLength - 2 : posicaoInicial + mtbx.SelectionLength - 1;
                    string valorInicial = mtbx.Text;
                    string valorFinal = string.Empty;

                    for (int i = 0; i < valorInicial.Length; i++)
                    {
                        if (i >= posicaoInicial && i <= posicaoFinal)
                        {
                            if (valorInicial[i].ToString() != "/") valorFinal = valorFinal.Insert(i, " ");
                            else valorFinal = valorFinal.Insert(i, "/");
                        }
                        else
                        {
                            valorFinal = valorFinal.Insert(i, valorInicial[i].ToString());
                        }
                    }

                    valorFinal = valorFinal.Remove(posicaoInicial, 1).Insert(posicaoInicial, caracter);
                    if (valorFinal.Split('/')[0][0] == ' ') valorFinal = valorFinal.Remove(0, 1).Insert(1, " ");
                    if (posicaoFinal >= 3 && valorFinal.Split('/')[1][0] == ' ') valorFinal = valorFinal.Remove(3, 1).Insert(4, " ");
                    if (posicaoFinal >= 6 && valorFinal.Split('/')[2].Trim().Length > 0 && valorFinal.Split('/')[2].Contains(" "))
                    {
                        int qtdSelecionada = mtbx.SelectionStart + mtbx.SelectionLength;
                        valorFinal = valorFinal.Remove(6, qtdSelecionada - 6);
                    }

                    mtbx.ResetText();
                    mtbx.Text = valorFinal;
                    mtbx.SelectionStart = posicaoInicial + 1;

                    e.SuppressKeyPress = true;

                    #endregion
                }
            }

            #endregion
        }

        private void mtbx_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Ativa Funcionalidade da tecla Insert
            bInserting = Convert.ToBoolean(GetKeyState(45));

            if (bInserting == true)
            {
                _statusInsert = 1;
                statusInsert();
            }
            else
            {
                _statusInsert = 2;
                statusInsert();
            }

            base.OnKeyPress(e);
        }

        private void mtbx_Click(object sender, EventArgs e)
        {
            base.OnClick(e);
        }

        private void mtbx_MouseMove(object sender, MouseEventArgs e)
        {
            base.OnMouseMove(e);
        }

        private void mtbx_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;

            base.OnMouseDown(e);
        }

        private void mtbx_MouseUp(object sender, MouseEventArgs e)
        {
            if (mouseDown == false)
            {
                this.BeginInvoke(new PosicionaCursorDelegate(PosicionaCursor), new object[] { 0 });
            }

            base.OnMouseUp(e);
        }

        private void mtbx_TextChanged(object sender, EventArgs e)
        {
            base.OnTextChanged(e);

            OnText_Changed(new EventArgs());

            int _selst = mtbx.SelectionStart;
            if (this.mtbx.Focused && !this.mtbx.Text.Replace("/", "").Trim().Equals(String.Empty))
            {
                if (_selst > 0 && this.Text.Length > 0)
                {
                    if (!verTXT(this.Text[_selst - 1]))
                    {
                        this.Text.Remove(_selst - 1, 1);
                    }
                }
            }
            else
            {
                if (this.Text == "Back")
                {
                    this.Text = this.Text.Remove(this.Text.Length - 2);
                }
                else if (this.Focused && mtbx.Text.Replace("/", "").Trim().Length > 0)
                {
                    if (!verTXT(this.Text[this.Text.Length]))
                    {
                        this.Text = this.Text.Remove(this.Text.Length - 1, 1);
                    }
                }
            }

            #region Tecla ctrl_z

            if (ctrl_z_ativo != Ctrl_z_ativo.nulo) digitado = mtbx.Text;

            #endregion
        }

        private void mtbx_MouseEnter(object sender, EventArgs e)
        {
            base.OnMouseEnter(e);
        }

        private void mtbx_MouseHover(object sender, EventArgs e)
        {
            base.OnMouseHover(e);
        }

        private void mtbx_MouseLeave(object sender, EventArgs e)
        {
            base.OnMouseLeave(e);
        }

        #endregion

        #region Métodos controle dataLIB

        /// <summary>
        /// Posiciona cursor dentro do controle.
        /// </summary>
        /// <param name="posicao">Posição do cursor dentro do controle.</param>
        private void PosicionaCursor(int posicao)
        {
            mtbx.SelectionStart = posicao;
            if (mtbx.Text.DeFormat().Length > 0) mtbx.SelectionLength = mtbx.Text.Length;
            else mtbx.SelectionLength = 0;
        }

        #region ValidarData

        /// <summary>
        /// Método para validar data.
        /// </summary>
        /// <param name="data">Valor a ser validado.</param>
        private void ValidaData(String data)
        {
            ExceçãoEventArgs argumento = new ExceçãoEventArgs();
            DateTime result;
            if (DateTime.TryParse(data, out result))
            {
                mtbx.Text = Convert.ToDateTime(data).ToString("dd/MM/yyyy");

                if (tipoValidacao == ValidaçãoEnum.IDADE || tipoValidacao == ValidaçãoEnum.CRIAÇÃO)
                {
                    bool idade = tipoValidacao == ValidaçãoEnum.IDADE ? true : false;
                    if (AERMOD.LIB.Desenvolvimento.Funcoes.ValidarDataNasc(mtbx.Text, idade) == false)
                    {
                        argumento.TipoExceção = ExceçãoEnum.VALIDAÇÃO;
                        OnExceção(argumento);

                        if (LeaveInvalidDate == false)
                        {
                            mtbx.Focus();
                        }
                    }
                }
            }
            else
            {
                argumento.TipoExceção = ExceçãoEnum.FORMATO;
                OnExceção(argumento);

                if (LeaveInvalidDate == false)
                {
                    mtbx.Focus();
                }
            }
        }

        /// <summary>
        /// Método para validar data no tratamento da tecla ctrl_z.
        /// </summary>
        /// <param name="data">Valor a ser validado.</param>
        private void ValidaDataCTRL_Z(String data)
        {
            DateTime result;
            if (DateTime.TryParse(data, out result))
            {
                mtbx.Text = Convert.ToDateTime(data).ToString("dd/MM/yyyy");
            }
        }

        #endregion        

        /// <summary>
        /// Método para alternar a propriedade do controle entre (insert, overwright).
        /// </summary>
        private void statusInsert()
        {
            switch (_statusInsert)
            {
                case 1:
                    mtbx.InsertKeyMode = InsertKeyMode.Overwrite;
                    _statusInsert = 2;
                    break;
                case 2:
                    mtbx.InsertKeyMode = InsertKeyMode.Insert;
                    _statusInsert = 1;
                    break;
            }
        }

        /// <summary>
        /// Sobrescreve teclas padrões do windows e framework.
        /// </summary>
        /// <param name="msg">Mensagem referenciada.</param>
        /// <param name="keyData">Tecla pressionada.</param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Control | Keys.X:
                    #region Copiar

                    mtbx.Copy();

                    #endregion
                    break;
                case Keys.Control | Keys.C:
                    #region Copiar

                    mtbx.Copy();

                    #endregion
                    return true;
                case Keys.Control | Keys.V:
                    #region Colar

                    mtbx.Paste();

                    #endregion
                    return true;
                case Keys.Control | Keys.Z:
                    #region Control Z

                    //Quando não existe valor nas variáveis e no controle altera o enumerador
                    if (ctrl_z_ativo == Ctrl_z_ativo.indice_1 && ctrl_z[0].DeFormat().Length == 0 && ctrl_z[1].DeFormat().Length == 0 && mtbx.Text.DeFormat().Length == 0)
                    {
                        ctrl_z_ativo = Ctrl_z_ativo.indice_0;
                    }

                    //Faz tratamento da variável (ctrl_z/ctrl_z_aux) quando digita algum valor, o valor que estava antes é mostrado novamente
                    //Se o valor digitado for diferente do valor que estava antes
                    if (ctrl_z[0].DeFormat() != mtbx.Text.DeFormat() && ctrl_z[1].DeFormat() != mtbx.Text.DeFormat() && ctrl_z[0].DeFormat().Length > 0 && ctrl_z_ativo == Ctrl_z_ativo.indice_1 ||
                        ctrl_z[0].DeFormat() != mtbx.Text.DeFormat() && ctrl_z[1].DeFormat() != mtbx.Text.DeFormat() && ctrl_z[1].DeFormat().Length > 0 && ctrl_z_ativo == Ctrl_z_ativo.indice_0)
                    {
                        if (ctrl_z_ativo == Ctrl_z_ativo.indice_1 && mtbx.Text.DeFormat() != ctrl_z[0].DeFormat()) ctrl_z_ativo = Ctrl_z_ativo.indice_0;
                        else if (ctrl_z_ativo == Ctrl_z_ativo.indice_0 && mtbx.Text.DeFormat() != ctrl_z[1].DeFormat()) ctrl_z_ativo = Ctrl_z_ativo.indice_1;
                    }
                    else if (digitado.DeFormat().Length > 0 && ctrl_z_ativo == Ctrl_z_ativo.indice_0 && digitado.DeFormat() == ctrl_z[1].DeFormat() ||
                             digitado.DeFormat().Length > 0 && ctrl_z_ativo == Ctrl_z_ativo.indice_1 && digitado.DeFormat() == ctrl_z[0].DeFormat())
                    {
                        //Quando o valor digitado é igual o valor que estava antes
                        if (ctrl_z_ativo == Ctrl_z_ativo.indice_1) ctrl_z_ativo = Ctrl_z_ativo.indice_0;
                        else if (ctrl_z_ativo == Ctrl_z_ativo.indice_0) ctrl_z_ativo = Ctrl_z_ativo.indice_1;
                    }
                    //Quando o valor digitado e valor do controle são vazios e existe apenas um valor no array ctrl_z
                    else if (mtbx.Text.DeFormat().Length == 0 && digitado.DeFormat().Length == 0 && ctrl_z_ativo == Ctrl_z_ativo.indice_0 && ctrl_z[0].DeFormat().Length == 0 && ctrl_z[1].DeFormat().Length > 0) ctrl_z_ativo = Ctrl_z_ativo.indice_1;
                    else if (mtbx.Text.DeFormat().Length == 0 && digitado.DeFormat().Length == 0 && ctrl_z_ativo == Ctrl_z_ativo.indice_1 && ctrl_z[1].DeFormat().Length == 0 && ctrl_z[0].DeFormat().Length > 0) ctrl_z_ativo = Ctrl_z_ativo.indice_0;

                    switch (ctrl_z_ativo)
                    {
                        case Ctrl_z_ativo.indice_0:
                            ValidaDataCTRL_Z(mtbx.Text);
                            ctrl_z[1] = mtbx.Text;
                            mtbx.Text = ctrl_z[0];
                            if (mtbx.Text.DeFormat().Length > 0) mtbx.SelectAll();
                            else mtbx.SelectionStart = 0;
                            ctrl_z_ativo = Ctrl_z_ativo.indice_1;
                            digitado = string.Empty;
                            break;
                        case Ctrl_z_ativo.indice_1:
                            ValidaDataCTRL_Z(mtbx.Text);
                            ctrl_z[0] = mtbx.Text;
                            mtbx.Text = ctrl_z[1];
                            if (mtbx.Text.DeFormat().Length > 0) mtbx.SelectAll();
                            else mtbx.SelectionStart = 0;
                            ctrl_z_ativo = Ctrl_z_ativo.indice_0;
                            digitado = string.Empty;
                            break;
                    }

                    #endregion
                    break;                
                case Keys.Insert:
                    #region Tecla Insert

                    {
                        statusInsert();
                        typeof(Form).GetMethod("OnKeyDown", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(this.FindForm(), new object[] { new KeyEventArgs(Keys.Insert) });
                    }

                    #endregion
                    return true;
            }

            #region Backspace/Delete

            //quando tecla backapace ou delete é pressionada e mtbxData focado
            //não deixa o valor do mes e ano pular de posicao
            if (mtbx.Focused.Equals(true))
            {
                if (keyData == Keys.Back || keyData == Keys.Delete || keyData == (Keys.Control | Keys.X) || keyData == (Keys.Shift | Keys.Delete) || keyData == (Keys.Shift | Keys.Back) || (keyData == (Keys.Control | Keys.Delete) && mtbx.SelectionLength > 0))
                {
                    if (mtbx.SelectionLength == 0)
                    {
                        int posicao = 0;
                        if (keyData == Keys.Back || keyData == (Keys.Shift | Keys.Delete) || keyData == (Keys.Shift | Keys.Back)) posicao = mtbx.SelectionStart - 1;
                        else if (keyData == Keys.Delete || keyData == (Keys.Control | Keys.X)) posicao = mtbx.SelectionStart;

                        if (mtbx.SelectionStart <= 5 && posicao >= 0)
                        {
                            string valor = null;
                            if (posicao != 2 && posicao != 5)
                            {
                                valor = mtbx.Text.Remove(posicao, 1);
                                valor = valor.Insert(posicao + 1, " ");
                                mtbx.Text = "";
                                mtbx.Text = valor;
                                mtbx.SelectionStart = posicao;
                                return true;
                            }
                            else return false;
                        }
                    }
                    else
                    {
                        int selecao = mtbx.SelectionLength;
                        int posicaoInicial = mtbx.SelectionStart;
                        int posicaoFinal = posicaoInicial + selecao - 1;
                        string valorFinal = string.Empty;
                        string valorInicial = mtbx.Text;

                        for (int i = 0; i < valorInicial.Length; i++)
                        {
                            if (i >= posicaoInicial && i <= posicaoFinal)
                            {
                                if (valorInicial[i].ToString() != "/") valorFinal = valorFinal.Insert(i, " ");
                                else valorFinal = valorFinal.Insert(i, "/");
                            }
                            else
                            {
                                valorFinal = valorFinal.Insert(i, valorInicial[i].ToString());
                            }
                        }

                        if (posicaoInicial > 5 || valorFinal.DeFormat().Trim().Length == 0 || posicaoFinal > mtbx.Text.Length - 1) return false;

                        if (posicaoFinal + 1 == 1 || posicaoFinal + 1 == 4 || posicaoFinal + 1 > 6)
                        {
                            int t = mtbx.Text.Length - 1;
                            string numero = valorFinal.Substring(posicaoFinal + 1, t - posicaoFinal);
                            valorFinal = valorFinal.Remove(posicaoFinal, mtbx.Text.Length - posicaoFinal);
                            if (posicaoFinal > 5) valorFinal = valorFinal.Insert(6, numero);
                            else valorFinal = valorFinal.Insert(posicaoFinal, numero);

                            if (valorFinal.Length < mtbx.Text.Length)
                            {
                                string[] conteudo = valorFinal.Contains("/") ? valorFinal.Split('/') : valorFinal.Split(':');
                                for (int i = 0; i < conteudo.Length; i++)
                                {
                                    if (i == 0 && conteudo[i].Length == 1)
                                    {
                                        valorFinal = valorFinal.Insert(1, " ");
                                    }
                                    else if (i == 1 && conteudo[i].Length == 1)
                                    {
                                        valorFinal = valorFinal.Insert(4, " ");
                                    }
                                    else if (i == 2 && conteudo[2].Length > 0 && conteudo[2].Length != 4)
                                    {
                                        string e = string.Empty;
                                        e = e.PadLeft(4 - conteudo[2].Length, ' ');
                                        valorFinal = valorFinal.Insert(6 + conteudo[2].Length, e);
                                    }
                                }
                            }
                        }

                        mtbx.Text = "";
                        mtbx.Text = valorFinal;
                        mtbx.SelectionStart = posicaoInicial;
                        return true;
                    }
                }
            }

            #endregion

            #region Corrige Bug Framework

            if (keyData == (Keys.Control | Keys.Delete) && mtbx.SelectionLength == 0)
            {
                //Apaga todos os caracteres após o cursor.
                return false;
            }

            if (keyData == Keys.Enter || keyData == (Keys.Control | Keys.Enter) || keyData == (Keys.Shift | Keys.Enter))
            {
                base.OnKeyDown(new KeyEventArgs(keyData));
                base.OnKeyUp(new KeyEventArgs(keyData));
                base.OnKeyPress(new KeyPressEventArgs((char)keyData));

                if (this.FindForm() != null)
                {
                    if (this.FindForm().KeyPreview == true)
                    {
                        MethodInfo onKeyDown = FindForm().GetType().GetMethod("OnKeyDown", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                        onKeyDown.Invoke(FindForm(), new object[] { new KeyEventArgs(keyData) });
                    }
                }
                return true;
            }

            #endregion

            return base.ProcessCmdKey(ref msg, keyData);
        }

        #endregion

        #region Customiza Controle

        #region verTXT

        private bool verTXT(char TX)
        {
            char[] _matriz = @"0123456789!@#$%¨&*()_+-=/\|*.,ABCDEFGHIJKLMNOPQRSTUVXWYZÇabcdefghijklmnopqrstuvxwyzÂÃÁÀÊÉÈÎÍÌÔÕÓÒÛÚÙâãáàêéèîìíôõóòûúù`´{}[]ªº<>;:?°'""".ToCharArray();
            List<char> matriz = new List<char>();

            foreach (char c in _matriz)
            {
                matriz.Add(c);
            }

            //matriz.Exists(delegate(char _tx) { return _tx = TX[i]; });
            if (TX != 0)
            {
                if (matriz.Exists(delegate (char _tx) { return _tx == TX; }))
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region Focused

        public override bool Focused
        {
            get
            {
                return mtbx.Focused;
            }
        }

        #endregion

        #region OnValidating

        protected override void OnValidating(CancelEventArgs e)
        {
            exception = false;

            base.OnValidating(e);
        }

        #endregion

        #region OnCreateControl

        protected override void OnCreateControl()
        {
            //controla tecla insert
            if (bInserting == true)
            {
                _statusInsert = 1;
                statusInsert();
            }
            else
            {
                _statusInsert = 2;
                statusInsert();
            }

            mtbx.GotFocus += new EventHandler(mtbx_GotFocus);
            mtbx.LostFocus += mtbx_LostFocus;
            //timerPiscar.Tick += new EventHandler(timerPiscar_Tick);

            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            
            base.OnCreateControl();
        }

        #endregion

        #endregion

        #region Tratamento Exceção

        public delegate void ExceçãoEventHandler(object sender, ExceçãoEventArgs e);

        [DefaultValue(typeof(ExceçãoEnum), "0")]
        [Category("Behavior")]
        [Description("Define tratamento de exceções.")]
        public event ExceçãoEventHandler Exceção;

        public enum ExceçãoEnum
        {
            FORMATO = 0,
            VALIDAÇÃO = 1
        }

        public class ExceçãoEventArgs : EventArgs
        {
            public ExceçãoEnum TipoExceção { get; set; }
        }

        protected virtual void OnExceção(ExceçãoEventArgs e)
        {
            if (Exceção != null)
            {
                Exceção(this, e);
            }
        }

        #endregion

        #region Criação Evento TextChanged

        [Category("Behavior")]
        [Description("Evento gerado quando o valor da propriedade de texto for alterado no controle.")]
        public event System.EventHandler Text_Changed;

        protected virtual void OnText_Changed(System.EventArgs e)
        {
            if (Text_Changed != null)
            {
                Text_Changed(this, e);
            }
        }

        #endregion

        /// <summary>
        /// Ocorre seleciona uma data no MonthCalender
        /// </summary>
        [Description("Ocorre seleciona uma data no MonthCalender")]
        public event DateSelectedEventHandler DateSelectedChanged;
        public virtual void OnDateSelectedChanged(DateSelectedEventsArgs e)
        {
            if (DateSelectedChanged != null)
            {
                DateSelectedChanged(this, e);
            }
        }
    }

    public class DateSelectedEventsArgs : EventArgs
    {
        /// <summary>
        /// Data selecionada
        /// </summary>
        public DateTime DateIsSelected { get; set; }
    }

    public delegate void DateSelectedEventHandler(object sender, DateSelectedEventsArgs e);

    [ToolboxItem(false)]
    public class MaskedTextBoxSeguro : MaskedTextBox
    {
        public void SetUserPaint(bool value)
        {
            this.SetStyle(ControlStyles.UserPaint, value);
            this.Invalidate();
        }

        public MaskedTextBoxSeguro()
        {
            this.TextChanged += new EventHandler(MaskedTextBoxSeguro_TextChanged);
        }

        private void MaskedTextBoxSeguro_TextChanged(object sender, EventArgs e)
        {
            if (this.Enabled == false)
            {
                this.DrawDefaultText();
            }
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 8:
                    base.Invalidate();
                    break;
            }
            base.WndProc(ref m);
            if ((m.Msg == 15) && ((this.Enabled == false) && !base.GetStyle(ControlStyles.UserPaint)))
            {
                this.DrawDefaultText();
            }
        }

        protected virtual void DrawDefaultText()
        {
            using (Graphics graphics = base.CreateGraphics())
            {
                this.DrawDefaultText(graphics);
            }
        }

        protected virtual void DrawDefaultText(Graphics g)
        {
            TextFormatFlags flags = TextFormatFlags.NoPadding | TextFormatFlags.EndEllipsis;
            Rectangle clientRectangle = base.ClientRectangle;
            switch (base.TextAlign)
            {
                case HorizontalAlignment.Left:
                    clientRectangle.Offset(1, 1);
                    break;

                case HorizontalAlignment.Right:
                    flags |= TextFormatFlags.Right;
                    clientRectangle.Offset(0, 1);
                    break;

                case HorizontalAlignment.Center:
                    flags |= TextFormatFlags.HorizontalCenter;
                    clientRectangle.Offset(0, 1);
                    break;
            }

            Color backColor = this.Enabled ? this.BackColor : Color.FromArgb(255, 236, 233, 216);
            g.FillRectangle(new SolidBrush(backColor), base.ClientRectangle);

            if (base.GetStyle(ControlStyles.UserPaint) == false)
            {
                TextRenderer.DrawText(g, (this.Text == "  :  :") ? "  :  :  " : this.Text, this.Font, clientRectangle, this.ForeColor, backColor, flags);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (this.Enabled == false)
            {
                this.DrawDefaultText(e.Graphics);
            }
        }
    }
}
