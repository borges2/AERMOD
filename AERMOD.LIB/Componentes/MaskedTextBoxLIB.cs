#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;
using AERMOD.LIB.Formatacao;
using AERMOD.LIB.Componentes.GridView;
using System.Reflection;
using AERMOD.LIB.Desenvolvimento;
using System.Threading;
using Netsof.LIB.Componentes.Calendario;
using System.Linq;

#endregion

namespace AERMOD.LIB.Componentes
{
    [ToolboxBitmapAttribute(typeof(MaskedTextBox))]
    public class MaskedTextBoxLIB : System.Windows.Forms.MaskedTextBox
    {
        #region Declarações

        #region Variáveis

        /// <summary>
        /// Estado da tecla Insert.
        /// </summary>
        bool bInserting;

        /// <summary>
        /// Indica estado da tecla Insert.
        /// </summary>
        /// <param name="nVirtkey">Código da tecla pressionada.</param>
        /// <returns>Retorna 1 para verdadeiro e 0 para falso.</returns>
        [DllImport("user32.dll")]
        static extern short GetKeyState(int nVirtkey);

        /// <summary>
        /// Valores para Duplicação, Incremento e Decremento.
        /// </summary>
        public decimal? SglVALOR; public String StrVALOR;

        /// <summary>
        /// Usado para posicionar cursor dentro do campo.
        /// </summary>
        /// <param name="posicao">Posição inicial do cursor dentro do controle.</param>
        private delegate void PosicionaCursorDelegate();

        /// <summary>
        /// Variável p/ indicar status do comando Overwrite/Insert.
        /// </summary>
        int _statusInsert = 0;

        /// <summary>
        /// Indica quando evento foi executado ou não referente a máscara (moeda).
        /// </summary>
        public bool eventoLeave;

        /// <summary>
        /// Indica tamanho do conteúdo do campo.
        /// </summary>
        public int tamanho = 0;

        /// <summary>
        /// Quando deve limpar o campo ou não.
        /// </summary>
        private bool limpaCampo = false;

        /// <summary>
        /// Guarda valor de saída do campo caso for
        /// do tipo moeda_porcentagem.
        /// </summary>
        public string texto = string.Empty;

        /// <summary>
        /// Label utilizado para inserção do sinal de subtração
        /// adicionado no controle.
        /// </summary>
        Label labelMenos;

        /// <summary>
        /// Label utilizado para inserção do símbolo monetário
        /// adicionado no controle.
        /// </summary>
        Label labelSimbolo;

        /// <summary>
        /// Utilizado para pressionamento click do mouse.
        /// </summary>
        Thread threadClick;

        /// <summary>
        /// Utilizado para pressionamento click do mouse.
        /// </summary>
        Boolean clickPressionado = false;

        /// <summary>
        /// Selecionar o texto ao entrar no campo
        /// </summary>
        public bool selecionarTextoEnter = true;

        #region Calendário

        /// <summary>
        /// Utilizado para abrir form. Calendário.
        /// </summary>
        private ToolStripDropDown popup;

        #endregion

        #endregion

        #region Enumeradores

        #region CTRL_Z

        /// <summary>
        /// Controla quando ocorre exceção na saída do controle,
        /// utilizado para tratamento da tecla CTRL_Z.
        /// </summary>
        bool exception = false;

        /// <summary>
        /// Valor digitado dentro do controle para tratamento da tecla (CTRL_Z).
        /// </summary>
        MaskedTextBox mtbxDigitado = new MaskedTextBox();

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
        //-----------------------------------------------------
        // Codigo Responsavel pelas Propriedades Customizadas |
        //-----------------------------------------------------

        [DefaultValue(CharacterCasing.Normal)]
        public CharacterCasing CharacterCasing { get; set; }

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

        #region Mascara

        public enum MascaraEnum
        {
            NORMAL = 0,
            DATA = 1,
            HORA_SHORT = 2,
            HORA_LONG = 3,
            MOEDA_PORCENTAGEM = 4,
            MES_ANO = 5,
            DATA_HORA = 6,
            PLACA = 7
        }

        protected MascaraEnum _Mascara;

        [DefaultValue(typeof(MascaraEnum), "0")]
        [Category("Behavior")]
        [Description("Define tipos de máscara.")]
        public MascaraEnum Mascara
        {
            get
            {
                return _Mascara;
            }
            set
            {
                _Mascara = value;

                Control btnCalendario = GetControls.GetAllControls(this.Controls).FirstOrDefault(I => I.GetType() == typeof(Button) && I.Name == "btnCalendario");
                if (btnCalendario != null)
                {
                    this.Controls.Remove(btnCalendario);
                }

                //Insere texto padrão para cada tipo de mascara
                switch (Mascara)
                {
                    case MascaraEnum.NORMAL:
                        {
                            this.Mask = "";
                        }
                        break;
                    case MascaraEnum.DATA:
                        {
                            this.TextAlign = HorizontalAlignment.Left;
                            DateTime data;
                            string _text = string.Empty;
                            if (DateTime.TryParse(this.Text, out data))
                            {
                                _text = data.ToString("dd/MM/yyyy");
                            }

                            this.Text = _text;
                            this.Mask = "00/00/0000";
                            this.PromptChar = ' ';
                            this.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                            Button _button = new Button { Cursor = Cursors.Default };
                            _button.Name = "btnCalendario";
                            _button.FlatStyle = FlatStyle.Flat;
                            _button.BackColor = Color.White;
                            _button.FlatAppearance.BorderSize = 0;
                            _button.SizeChanged += (o, e) => OnResize(e);
                            _button.Size = new Size(20, this.ClientSize.Height + 2);
                            _button.Location = new Point(this.ClientSize.Width - _button.Width, -1);
                            _button.Click += btnCalendario_Click;
                            _button.Paint += _button_Paint;

                            MethodInfo method = _button.GetType().GetMethod("SetStyle", BindingFlags.NonPublic | BindingFlags.Instance);

                            if (method != null)
                            {
                                object[] param = { ControlStyles.Selectable, false };
                                method.Invoke(_button, param);
                            }

                            this.Controls.Add(_button);
                        }
                        break;
                    case MascaraEnum.HORA_SHORT:
                        {
                            DateTime hora;
                            string _hora = string.Empty;
                            if (DateTime.TryParse(this.Text, out hora))
                            {
                                _hora = hora.ToString("HH:mm");
                            }
                            this.Text = _hora;
                            this.Mask = "00:00";
                            this.PromptChar = ' ';
                        }
                        break;
                    case MascaraEnum.HORA_LONG:
                        {
                            DateTime horaLong;
                            string _horaLong = string.Empty;
                            if (DateTime.TryParse(this.Text, out horaLong))
                            {
                                _horaLong = horaLong.ToString("HH:mm:ss");
                            }
                            this.Text = _horaLong;
                            this.Mask = "00:00:00";
                            this.PromptChar = ' ';
                        }
                        break;
                    case MascaraEnum.MOEDA_PORCENTAGEM:
                        {
                            this.PromptChar = ' ';
                            this.TextAlign = HorizontalAlignment.Right;
                            Decimal valor;
                            string _valor = string.Empty;
                            if (Decimal.TryParse(this.Text, out valor))
                            {
                                _valor = valor.ToString();
                            }
                            this.Text = _valor;

                            if (this.Text.DeFormat().Length == 0 || Convert.ToDecimal(this.Text.DeFormat()) == 0)
                            {
                                ZeroFill();
                            }
                        }
                        break;
                    case MascaraEnum.MES_ANO:
                        {
                            DateTime mes_ano;
                            string texto = string.Empty;
                            this.Text = this.Text.Insert(0, "01/");
                            if (DateTime.TryParse(this.Text, out mes_ano))
                            {
                                texto = mes_ano.ToString("MM/yyyy");
                            }

                            this.Text = texto;
                            this.Mask = "00/0000";
                            this.PromptChar = ' ';
                        }
                        break;
                    case MascaraEnum.DATA_HORA:
                        {
                            this.TextAlign = HorizontalAlignment.Left;
                            DateTime data;
                            string _text = string.Empty;
                            if (DateTime.TryParse(this.Text, out data))
                            {
                                _text = data.ToString("dd/MM/yyyy HH:mm:ss");
                            }

                            this.Text = _text;
                            this.Mask = "00/00/0000 00:00:00";
                            this.PromptChar = ' ';
                            this.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                            Button _button = new Button { Cursor = Cursors.Default };
                            _button.Name = "btnCalendario";
                            _button.FlatStyle = FlatStyle.Flat;
                            _button.BackColor = Color.White;
                            _button.FlatAppearance.BorderSize = 0;
                            _button.SizeChanged += (o, e) => OnResize(e);
                            _button.Size = new Size(20, this.ClientSize.Height + 2);
                            _button.Location = new Point(this.ClientSize.Width - _button.Width, -1);
                            _button.Click += btnCalendario_Click;
                            _button.Paint += _button_Paint;

                            MethodInfo method = _button.GetType().GetMethod("SetStyle", BindingFlags.NonPublic | BindingFlags.Instance);

                            if (method != null)
                            {
                                object[] param = { ControlStyles.Selectable, false };
                                method.Invoke(_button, param);
                            }

                            this.Controls.Add(_button);
                        }
                        break;
                    case MascaraEnum.PLACA:
                        {
                            //L - Letras a-z, A-Z [Requerido]
                            //0 - Digitar 0-9 [Requerido]
                            //A - Alfanumérico [Requerido]
                            this.Mask = "LLL0A00";
                            this.PromptChar = ' ';
                            this.CharacterCasing = CharacterCasing.Upper;
                        }
                        break;
                }
            }
        }

        #endregion

        #region Símbolo

        public enum SimboloEnum
        {
            [Description("NORMAL")]
            NORMAL = 0,

            [Description("REAL")]
            REAL = 1,

            [Description("DÓLAR AMERICANO")]
            DOLAR_AMERICANO = 2,

            [Description("GUARANI")]
            GUARANI = 3,

            [Description("PESO")]
            PESO = 4
        }

        protected SimboloEnum simbolo;

        [DefaultValue(typeof(MascaraEnum), "0")]
        [Category("Behavior")]
        [Description("Define tipo de simbolo monetário.")]
        public SimboloEnum Simbolo
        {
            get
            {
                return simbolo;
            }
            set
            {
                simbolo = value;

                //Insere texto padrão para cada tipo de mascara
                switch (Simbolo)
                {
                    case SimboloEnum.NORMAL:
                        if (Mascara == MascaraEnum.MOEDA_PORCENTAGEM) labelSimbolo.Visible = false;
                        break;
                    case SimboloEnum.REAL:
                        if (Mascara == MascaraEnum.MOEDA_PORCENTAGEM)
                        {
                            labelSimbolo.Text = "R$";
                            labelSimbolo.Visible = true;
                        }
                        break;
                    case SimboloEnum.DOLAR_AMERICANO:
                        if (Mascara == MascaraEnum.MOEDA_PORCENTAGEM)
                        {
                            labelSimbolo.Text = "US$";
                            labelSimbolo.Visible = true;
                        }
                        break;
                    case SimboloEnum.GUARANI:
                        if (Mascara == MascaraEnum.MOEDA_PORCENTAGEM)
                        {
                            labelSimbolo.Text = "₲$";
                            labelSimbolo.Visible = true;
                        }
                        break;
                    case SimboloEnum.PESO:
                        if (Mascara == MascaraEnum.MOEDA_PORCENTAGEM)
                        {
                            labelSimbolo.Text = "$";
                            labelSimbolo.Visible = true;
                        }
                        break;
                }
            }
        }

        #endregion

        #region Tamanho do campo (Moeda)

        [DefaultValue(15)]
        [Category("Behavior")]
        [Description("Tamanho máximo do campo moeda permitido = 15. Tamanho mínimo do campo moeda permitido = 1.")]
        public int TamanhoMoeda
        {
            get { return _TamanhoMoeda; }
            set
            {
                if (value > 15) value = 15;
                if (value < 1) value = 1;
                _TamanhoMoeda = value;

                if (_Mascara == MascaraEnum.MOEDA_PORCENTAGEM)
                {
                    if (this.Text.DeFormat().Length == 0 || Convert.ToDecimal(this.Text.DeFormat()) == 0)
                    {
                        ZeroFill();
                    }
                }
            }
        }

        /// <summary>
        /// Tamanho máximo do campo moeda permitido = 15. Tamanho mínimo do campo moeda permitido = 1.
        /// </summary>
        protected int _TamanhoMoeda = 15;

        #endregion

        #region casas decimais

        [DefaultValue(10)]
        [Category("Behavior")]
        [Description("Tamanho máximo de decimais do campo moeda permitido = 10. Tamanho mínimo de decimais do campo moeda permitido = 1.")]
        public int DecimaisMoeda
        {
            get { return _DecimaisMoeda; }
            set
            {
                if (value > 10) value = 10;
                if (value < 1) value = 1;
                _DecimaisMoeda = value;

                if (_Mascara == MascaraEnum.MOEDA_PORCENTAGEM)
                {
                    if (this.Text.DeFormat().Length == 0 || Convert.ToDecimal(this.Text.DeFormat()) == 0)
                    {
                        ZeroFill();
                    }
                }
            }
        }

        /// <summary>
        /// Tamanho máximo de decimais do campo moeda permitido = 8. Tamanho mínimo de decimais do campo moeda permitido = 1.
        /// </summary>
        protected int _DecimaisMoeda = 10;

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

        #region Tabelado

        [Category("Behavior")]
        [Description("Indica se o controle é do tipo tabelado (descrição) ou não.")]
        [DefaultValue(false)]
        [Browsable(true)]
        public bool Tabelado { get; set; }

        #endregion

        #region Menos

        [Browsable(false)]
        [Category("Behavior")]
        [Description("Utilizado para subtrair valores de elementos do tipo moeda_porcentagem.")]
        public bool Menos
        {
            get { return menos; }
            set
            {
                if (negativo == true)
                {
                    if (Simbolo != SimboloEnum.NORMAL) labelMenos.Location = new Point(this.ClientRectangle.Location.X + labelSimbolo.Width - 3, this.ClientRectangle.Location.Y);
                    labelMenos.Visible = value;
                    menos = value;
                }
            }
        }

        /// <summary>
        /// Utilizado para subtrair valores de elementos do tipo.
        /// moeda_porcentagem.
        /// </summary>
        bool menos;

        private bool negativo = true;
        [Description("Habilita/Desabilita valores negativos"), DefaultValue(true)]
        public bool Negativo
        {
            get { return negativo; }
            set
            {
                negativo = value;
                if (value == false)
                {
                    labelMenos.Visible = false;
                    menos = false;
                }
            }
        }


        #endregion

        #region Seguranca

        public String CodigoCampo { get; set; }

        #endregion

        [DefaultValue(false)]
        public Boolean LeaveInvalidDate { get; set; }

        #endregion

        #region Construtor

        public MaskedTextBoxLIB()
            : base()
        {
            //SetStyle(ControlStyles.UserPaint, true);

            #region LabelSimbolo

            labelSimbolo = new Label();
            labelSimbolo.AutoSize = true;
            labelSimbolo.TabIndex = this.TabIndex + 2;
            labelSimbolo.BackColor = Color.Transparent;
            labelSimbolo.Location = this.ClientRectangle.Location;
            labelSimbolo.ForeColor = this.ForeColor;
            labelSimbolo.Font = this.Font;
            labelSimbolo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            labelSimbolo.Visible = false;
            labelSimbolo.Text = "";
            labelSimbolo.Paint += LabelSimbolo_Paint;
            this.Controls.Add(labelSimbolo);

            #endregion

            #region LabelMenos

            labelMenos = new Label();
            labelMenos.AutoSize = true;
            labelMenos.TabIndex = this.TabIndex + 2;
            labelMenos.BackColor = Color.Transparent;
            labelMenos.Location = new Point(this.ClientRectangle.Location.X + labelSimbolo.Width, this.ClientRectangle.Location.Y);
            labelMenos.ForeColor = this.ForeColor;
            labelMenos.Font = this.Font;
            labelMenos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            labelMenos.Visible = false;
            labelMenos.Text = "-";
            labelMenos.Paint += LabelMenos_Paint;
            this.Controls.Add(labelMenos);

            #endregion

            bInserting = Convert.ToBoolean(GetKeyState(45));
        }

        #endregion

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

        #region Font

        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
                labelMenos.Font = value;
            }
        }

        #endregion

        #region OnText

        public override string Text
        {
            get
            {
                if (this.Mascara == MascaraEnum.MOEDA_PORCENTAGEM)
                {
                    if (base.Text.DeFormat().Trim().Length > 0)
                    {
                        if (Menos)
                        {
                            return "-" + base.Text.Trim();
                        }
                    }
                }

                return base.Text;
            }
            set
            {
                if (this.Mascara == MascaraEnum.MOEDA_PORCENTAGEM)
                {
                    if (value != null && value.Contains("-"))
                    {
                        this.Menos = true;
                        value = value.Replace("-", "");
                    }
                    else this.Menos = false;
                }

                if (_Mascara != MascaraEnum.MOEDA_PORCENTAGEM) base.Text = value;
                else
                {
                    if (value != null && this.GetType() == typeof(MaskedTextBoxLIB) && eventoLeave.Equals(false) && value.DeFormat().Length > 0 && Convert.ToDecimal(value.DeFormat()) >= 0)
                    {
                        base.Mask = "";
                        if (base.Focused) base.Text = Convert.ToString(decimal.Parse(value).ToString("N" + DecimaisMoeda)).Replace(".", "");
                        else base.Text = Convert.ToString(decimal.Parse(value).ToString("N" + DecimaisMoeda));
                    }
                }
            }
        }

        #endregion

        #region TextChanged

        protected override void OnTextChanged(EventArgs e)
        {
            if (this.Enabled == false)
            {
                this.DrawDefaultText();
            }

            int _selst = this.SelectionStart;
            if (this.Focused && !this.Text.Equals(String.Empty))
            {
                if (_selst > 0)
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
                else if (this.Focused && this.TextLength > 0)
                {
                    if (!verTXT(this.Text[this.Text.Length]))
                    {
                        this.Text = this.Text.Remove(this.Text.Length - 1, 1);
                    }
                }
            }

            #region Tecla ctrl_z

            if (ctrl_z_ativo != Ctrl_z_ativo.nulo && limpaCampo == false && eventoLeave == false) mtbxDigitado.Text = this.Text;

            #endregion

            base.OnTextChanged(e);
        }

        #endregion

        #region OnGotFocus

        bool selecionarTexto = true;

        protected override void OnGotFocus(EventArgs e)
        {
            if (Mascara != MascaraEnum.DATA && Mascara != MascaraEnum.DATA_HORA && Mascara != MascaraEnum.PLACA)
            {
                if (selecionarTexto == true)
                {
                    if (selecionarTextoEnter)
                    {
                        this.BeginInvoke(new PosicionaCursorDelegate(PosicionaCursor));
                    }
                }                
            }
            else
            {
                if ((Control.MouseButtons ^ MouseButtons.Left) != 0 && this.Text.Length > 0)
                {
                    this.BeginInvoke((MethodInvoker)delegate ()
                    {
                        base.SelectAll();
                    });
                }
            }

            labelSimbolo.BeginInvoke((MethodInvoker)delegate { labelSimbolo.Refresh(); });
            labelMenos.BeginInvoke((MethodInvoker)delegate { labelMenos.Refresh(); });

            StrVALOR = this.Text;
            tamanho = this.Text.DeFormat().Length;
            SglVALOR = null;
            if (this.Text.DeFormat().Length > 0)
            {
                decimal _valor;
                if (decimal.TryParse(this.Text.Trim(), out _valor))
                {
                    if (Mascara == MascaraEnum.MOEDA_PORCENTAGEM) SglVALOR = Convert.ToDecimal(this.Text.Replace(".", "").Replace(",", ""));
                    else SglVALOR = _valor;
                }
            }

            #region FocusColor

            if (FocusColorEnabled)
            {
                backColor = this.BackColor;
                this.BackColor = FocusColor;
            }

            #endregion

            base.OnGotFocus(e);
        }

        #endregion

        #region OnLostFocus

        protected override void OnLostFocus(EventArgs e)
        {
            #region FocusColor

            if (FocusColorEnabled)
            {
                this.BackColor = backColor;
            }

            #endregion

            base.OnLostFocus(e);
        }

        #endregion

        #region OnEnter

        /// <summary>
        /// Invoka o evento enter
        /// </summary>
        public void InvokeOnEnter()
        {
            this.OnEnter(EventArgs.Empty);
        }

        protected override void OnEnter(EventArgs e)
        {
            #region Moeda_porcentagem

            if (Mascara == MascaraEnum.MOEDA_PORCENTAGEM)
            {
                limpaCampo = true;

                string valor = string.Empty;                
                valor = this.Text.Trim();

                base.Text = string.Empty;
                base.Mask = string.Empty;
                base.Mask = base.Mask.PadLeft(_TamanhoMoeda, '0') + '.' + base.Mask.PadRight(_DecimaisMoeda, '0');

                if (valor.DeFormat() != "")
                {
                    String[] arrayValor = valor.Replace(".", "").Replace("-", "").Split(new char[] { ',' });

                    int qtd = _TamanhoMoeda - arrayValor[0].Trim().Length;
                    int qtdDecimais = _TamanhoMoeda + 1;

                    string teste = string.Empty;
                    //arruma valores antes da virgula
                    for (int i = 0; i < arrayValor[0].Trim().Length; i++)
                    {
                        if (selecionarTexto == true)
                        {
                            base.Text = base.Text.Insert(qtd + i, arrayValor[0][i].ToString());
                        }
                        else
                        {
                            base.Text = base.Text.Insert(0, arrayValor[0][i].ToString());
                        }
                    }
                    //arruma centavos
                    for (int i = 0; i < arrayValor[1].Trim().Length; i++)
                    {
                        base.Text = base.Text.Insert(qtdDecimais + i, arrayValor[1][i].ToString());
                    }
                }
            }

            #endregion

            #region Tecla ctrl_z

            if (ctrl_z_ativo == Ctrl_z_ativo.nulo) exception = false;
            if (exception == false)
            {
                if (Mascara == MascaraEnum.NORMAL)
                {
                    ////////Seta valor no array ctrl_z/////////////////

                    if (ctrl_z_ativo == Ctrl_z_ativo.nulo && this.Text.DeFormat().Length > 0) ctrl_z[0] = this.Text;
                    else if (this.Text.ToLower() != ctrl_z[0].ToLower() && this.Text.ToLower() != ctrl_z[1].ToLower())
                    {
                        if (ctrl_z[0].Length > 0 && ctrl_z[1].Length == 0)
                        {
                            ctrl_z[1] = this.Text;
                            ctrl_z_ativo = Ctrl_z_ativo.indice_0;
                        }
                        else
                        {
                            if (ctrl_z_ativo == Ctrl_z_ativo.indice_0)
                            {
                                ctrl_z[0] = this.Text;
                                ctrl_z_ativo = Ctrl_z_ativo.indice_1;
                            }
                            else if (ctrl_z_ativo == Ctrl_z_ativo.indice_1)
                            {
                                ctrl_z[1] = this.Text;
                                ctrl_z_ativo = Ctrl_z_ativo.indice_0;
                            }
                        }
                    }
                    else if (mtbxDigitado.Text.Length > 0 && ctrl_z_ativo == Ctrl_z_ativo.indice_0 && mtbxDigitado.Text.ToLower() == ctrl_z[1].ToLower() ||
                             mtbxDigitado.Text.Length > 0 && ctrl_z_ativo == Ctrl_z_ativo.indice_1 && mtbxDigitado.Text.ToLower() == ctrl_z[0].ToLower())
                    {
                        //Quando o valor digitado é igual o valor que estava antes
                        if (ctrl_z_ativo == Ctrl_z_ativo.indice_1) ctrl_z_ativo = Ctrl_z_ativo.indice_0;
                        else if (ctrl_z_ativo == Ctrl_z_ativo.indice_0) ctrl_z_ativo = Ctrl_z_ativo.indice_1;
                    }

                    /////ctrl_z_ativo nulo////////////////

                    if (this.Text.DeFormat().Length == 0 && ctrl_z_ativo == Ctrl_z_ativo.nulo && ctrl_z[0].Length == 0)
                    {
                        ctrl_z_ativo = Ctrl_z_ativo.indice_1;
                    }
                    else if (this.Text.DeFormat().Length > 0 && ctrl_z_ativo == Ctrl_z_ativo.nulo)
                    {
                        ctrl_z_ativo = Ctrl_z_ativo.indice_0;
                    }
                    else if (ctrl_z_ativo == Ctrl_z_ativo.nulo) ctrl_z_ativo = Ctrl_z_ativo.indice_0;
                }
                else
                {
                    ////////Seta valor no array ctrl_z/////////////////

                    //if(Mascara == MascaraEnum.MOEDA_PORCENTAGEM) metodoLeaveCTRL_Z(mtbxDigitado);

                    if (ctrl_z_ativo == Ctrl_z_ativo.nulo && this.Text.DeFormat().Length > 0) ctrl_z[0] = this.Text;
                    else if (this.Text.DeFormat() != ctrl_z[0].DeFormat() && this.Text.DeFormat() != ctrl_z[1].DeFormat())
                    {
                        if (ctrl_z[0].DeFormat().Length > 0 && ctrl_z[1].DeFormat().Length == 0)
                        {
                            ctrl_z[1] = this.Text;
                            ctrl_z_ativo = Ctrl_z_ativo.indice_0;
                        }
                        else
                        {
                            if (ctrl_z_ativo == Ctrl_z_ativo.indice_0)
                            {
                                ctrl_z[0] = this.Text;
                                ctrl_z_ativo = Ctrl_z_ativo.indice_1;
                            }
                            else if (ctrl_z_ativo == Ctrl_z_ativo.indice_1)
                            {
                                ctrl_z[1] = this.Text;
                                ctrl_z_ativo = Ctrl_z_ativo.indice_0;
                            }
                        }
                    }
                    else if (mtbxDigitado.Text.DeFormat().Length > 0 && ctrl_z_ativo == Ctrl_z_ativo.indice_0 && mtbxDigitado.Text.DeFormat() == ctrl_z[1].DeFormat() ||
                             mtbxDigitado.Text.DeFormat().Length > 0 && ctrl_z_ativo == Ctrl_z_ativo.indice_1 && mtbxDigitado.Text.DeFormat() == ctrl_z[0].DeFormat())
                    {
                        //Quando o valor digitado é igual o valor que estava antes
                        if (ctrl_z_ativo == Ctrl_z_ativo.indice_1) ctrl_z_ativo = Ctrl_z_ativo.indice_0;
                        else if (ctrl_z_ativo == Ctrl_z_ativo.indice_0) ctrl_z_ativo = Ctrl_z_ativo.indice_1;
                    }

                    /////ctrl_z_ativo nulo////////////////

                    if (this.Text.DeFormat().Length == 0 && ctrl_z_ativo == Ctrl_z_ativo.nulo && ctrl_z[0].DeFormat().Length == 0)
                    {
                        ctrl_z_ativo = Ctrl_z_ativo.indice_1;
                    }
                    else if (this.Text.DeFormat().Length > 0 && ctrl_z_ativo == Ctrl_z_ativo.nulo)
                    {
                        ctrl_z_ativo = Ctrl_z_ativo.indice_0;
                    }
                    else if (ctrl_z_ativo == Ctrl_z_ativo.nulo) ctrl_z_ativo = Ctrl_z_ativo.indice_0;
                }

                mtbxDigitado.Text = string.Empty;
                mtbxDigitado.Mask = "";
            }

            #endregion

            #region Tecla Insert

            bInserting = Convert.ToBoolean(GetKeyState(45));
            if (Mascara != MascaraEnum.NORMAL || string.IsNullOrEmpty(this.Mask) == false)
            {
                if (bInserting == true)
                {
                    _statusInsert = 1;
                    StatusInsert();
                }
                else
                {
                    _statusInsert = 2;
                    StatusInsert();
                }
            }

            #endregion

            base.OnEnter(e);
        }

        #endregion

        #region OnLeave

        protected override void OnLeave(EventArgs e)
        {
            exception = true;

            Validar();

            base.OnLeave(e);
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
            if (Funcoes.IsDesignTime() == false)
            {
                //controla tecla insert
                if (Mascara != MascaraEnum.NORMAL || string.IsNullOrEmpty(this.Mask) == false)
                {
                    if (bInserting == true)
                    {
                        _statusInsert = 1;
                        StatusInsert();
                    }
                    else
                    {
                        _statusInsert = 2;
                        StatusInsert();
                    }
                }

                this.PromptChar = ' ';
            }
            base.OnCreateControl();
        }

        #endregion

        #region OnKeyDown

        protected override void OnKeyDown(KeyEventArgs e)
        {
            string[] arrayAjuda = new string[3];
            arrayAjuda[0] = "Atenção";

            base.OnKeyDown(e);

            if (e.Alt) { }
            else if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.OemMinus:
                        #region Caracter estranho

                        e.SuppressKeyPress = true;

                        #endregion
                        break;
                }
            }
            else
            {
                switch (e.KeyCode)
                {
                    case Keys.Down:
                        #region Próximo controle

                        this.FindForm().SelectNextControl(this, true, true, true, true);
                        e.Handled = true;
                        e.SuppressKeyPress = true;

                        #endregion
                        break;
                    case Keys.Up:
                        #region Controle anterior

                        this.FindForm().SelectNextControl(this, false, true, true, true);
                        e.Handled = true;
                        e.SuppressKeyPress = true;

                        #endregion
                        break;
                    case Keys.F2:
                        #region Abrir calendário

                        e.SuppressKeyPress = true;
                        if (Mascara == MascaraEnum.DATA || Mascara == MascaraEnum.DATA_HORA)
                        {
                            OpenCalendario();
                        }

                        #endregion
                        break;                    
                    case Keys.F8:
                        #region Salta campo

                        arrayAjuda[1] = "Campo não pode ser saltado.";
                        arrayAjuda[2] = "Campo não permitido saltar. \nPode ser campo obrigatório.";

                        #endregion
                        break;
                    case Keys.F4:
                        #region Fixa campo

                        arrayAjuda[1] = arrayAjuda[1] = "Valor não pode ser fixado.";                        

                        #endregion
                        break;
                    case Keys.F5:
                        #region Incrementa campo

                        arrayAjuda[1] = arrayAjuda[1] = "Valor não pode ser incrementado.";
                        arrayAjuda[2] = arrayAjuda[1] = "Incremente valor ou data.";                        

                        #endregion
                        break;
                    case Keys.F6:
                        #region Decrementa campo

                        arrayAjuda[1] = arrayAjuda[1] = "Valor não pode ser decrementado.";
                        arrayAjuda[2] = arrayAjuda[1] = "Decremente valor ou data.";

                        #endregion
                        break;
                    case Keys.Space:
                        #region Tecla espaço

                        if (Mascara.Equals(MascaraEnum.DATA) || Mascara.Equals(MascaraEnum.HORA_SHORT) || Mascara.Equals(MascaraEnum.HORA_LONG) || Mascara.Equals(MascaraEnum.MES_ANO) || Mascara.Equals(MascaraEnum.DATA_HORA))
                        {
                            e.SuppressKeyPress = true;
                        }

                        #endregion
                        break;
                }
            }

            if (Mascara == MascaraEnum.MOEDA_PORCENTAGEM)
            {
                //não deixa apagar o conteúdo do textbox
                if (e.KeyValue == (int)Keys.Home || e.KeyValue == (int)Keys.End || e.KeyValue == (int)Keys.Left || e.KeyValue == (int)Keys.Right || e.KeyValue == (int)Keys.Back) limpaCampo = false;
            }

            #region Corrige qdo empurra valores pra frente

            if (Mascara == MascaraEnum.DATA)
            {
                #region Data

                //Apenas números
                if (this.SelectionStart + 1 < this.Text.Length && ((e.KeyValue > 47 && e.KeyValue < 58) || (e.KeyValue > 95 && e.KeyValue < 106)))
                {
                    KeysConverter keyConverter = new KeysConverter();
                    String caracter = keyConverter.ConvertToInvariantString(e.KeyData).Replace("NumPad", "");

                    bInserting = Convert.ToBoolean(GetKeyState(45));
                    if (bInserting == false && this.SelectionLength == 0)
                    {
                        #region Não selecionado

                        string[] valor = this.Text.Split('/');
                        if (valor[0].Trim().Length < 2 || valor[1].Trim().Length < 2 || valor[2].Trim().Length < 4)
                        {
                            int posicaoInicial = this.Text[this.SelectionStart] != '/' ? this.SelectionStart : this.SelectionStart + 1;

                            if (this.Text[posicaoInicial] != ' ')
                            {
                                if (posicaoInicial < 3)
                                {
                                    //Dia
                                    if (valor[0].Trim().Length < 2)
                                    {
                                        this.Text = this.Text.Insert(posicaoInicial, caracter);
                                        this.Text = this.Text.Remove(3);
                                        this.Text = this.Text.Insert(3, valor[1] + "/" + valor[2]);
                                        this.SelectionStart = posicaoInicial + 1;
                                    }

                                    e.SuppressKeyPress = true;
                                }
                                else if (posicaoInicial > 2 && posicaoInicial < 6)
                                {
                                    //Mês
                                    if (valor[1].Trim().Length < 2)
                                    {
                                        this.Text = this.Text.Insert(posicaoInicial, caracter);
                                        if (valor[2].Trim().Length > 0)
                                        {
                                            this.Text = this.Text.Remove(6);
                                            this.Text = this.Text.Insert(6, valor[2]);
                                        }
                                        this.SelectionStart = posicaoInicial + 1;
                                    }

                                    e.SuppressKeyPress = true;
                                }
                            }
                        }

                        #endregion
                    }
                    else if (this.SelectionLength > 0 && this.SelectionLength < this.Text.Length && this.SelectionStart < 4 && this.SelectedText != "/")
                    {
                        #region Selecionado

                        int posicaoInicial = this.SelectionStart == 2 || this.SelectionStart == 5 ? this.SelectionStart + 1 : this.SelectionStart;
                        int posicaoFinal = this.SelectionStart == 2 || this.SelectionStart == 5 ? posicaoInicial + this.SelectionLength - 2 : posicaoInicial + this.SelectionLength - 1;
                        string valorInicial = this.Text;
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
                            int qtdSelecionada = this.SelectionStart + this.SelectionLength;
                            valorFinal = valorFinal.Remove(6, qtdSelecionada - 6);
                        }

                        this.ResetText();
                        this.Text = valorFinal;
                        this.SelectionStart = posicaoInicial + 1;

                        e.SuppressKeyPress = true;

                        #endregion
                    }
                }

                #endregion
            }
            else if (Mascara == MascaraEnum.MES_ANO)
            {
                #region Mês/ano

                //Apenas números
                if (this.SelectionStart + 1 < this.Text.Length && ((e.KeyValue > 47 && e.KeyValue < 58) || (e.KeyValue > 95 && e.KeyValue < 106)))
                {
                    KeysConverter keyConverter = new KeysConverter();
                    String caracter = keyConverter.ConvertToInvariantString(e.KeyData).Replace("NumPad", "");

                    bInserting = Convert.ToBoolean(GetKeyState(45));
                    if (bInserting == false && this.SelectionLength == 0)
                    {
                        #region Não selecionado

                        string[] valor = this.Text.Split('/');
                        if (valor[0].Trim().Length < 2 || valor[1].Trim().Length < 4)
                        {
                            int posicaoInicial = this.Text[this.SelectionStart] != '/' ? this.SelectionStart : this.SelectionStart + 1;

                            if (this.Text[posicaoInicial] != ' ')
                            {
                                if (posicaoInicial < 3)
                                {
                                    //Dia
                                    if (valor[0].Trim().Length < 2)
                                    {
                                        this.Text = this.Text.Insert(posicaoInicial, caracter);
                                        this.Text = this.Text.Remove(3);
                                        this.Text = this.Text.Insert(3, valor[1]);
                                        this.SelectionStart = posicaoInicial + 1;
                                    }

                                    e.SuppressKeyPress = true;
                                }
                            }
                        }

                        #endregion
                    }
                    else if (this.SelectionLength > 0 && this.SelectionLength < this.Text.Length && this.SelectionStart < 1 && this.SelectedText != "/")
                    {
                        #region Selecionado

                        int posicaoInicial = this.SelectionStart == 2 ? this.SelectionStart + 1 : this.SelectionStart;
                        int posicaoFinal = this.SelectionStart == 2 ? posicaoInicial + this.SelectionLength - 2 : posicaoInicial + this.SelectionLength - 1;
                        string valorInicial = this.Text;
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
                        if (posicaoFinal >= 3 && valorFinal.Split('/')[1].Trim().Length > 0 && valorFinal.Split('/')[1].Contains(" "))
                        {
                            int qtdSelecionada = this.SelectionStart + this.SelectionLength;
                            valorFinal = valorFinal.Remove(3, qtdSelecionada - 3);
                        }

                        this.ResetText();
                        this.Text = valorFinal;
                        this.SelectionStart = posicaoInicial + 1;

                        e.SuppressKeyPress = true;

                        #endregion
                    }
                }

                #endregion
            }
            else if (Mascara == MascaraEnum.HORA_LONG)
            {
                #region Hora long

                //Apenas números
                if (this.SelectionStart + 1 < this.Text.Length && ((e.KeyValue > 47 && e.KeyValue < 58) || (e.KeyValue > 95 && e.KeyValue < 106)))
                {
                    KeysConverter keyConverter = new KeysConverter();
                    String caracter = keyConverter.ConvertToInvariantString(e.KeyData).Replace("NumPad", "");

                    bInserting = Convert.ToBoolean(GetKeyState(45));
                    if (bInserting == false && this.SelectionLength == 0)
                    {
                        #region Não selecionado

                        string[] valor = this.Text.Split(':');
                        if (valor[0].Trim().Length < 2 || valor[1].Trim().Length < 2 || valor[2].Trim().Length < 2)
                        {
                            int posicaoInicial = this.Text[this.SelectionStart] != ':' ? this.SelectionStart : this.SelectionStart + 1;

                            if (this.Text[posicaoInicial] != ' ')
                            {
                                if (posicaoInicial < 3)
                                {
                                    //Hora
                                    if (valor[0].Trim().Length < 2)
                                    {
                                        this.Text = this.Text.Insert(posicaoInicial, caracter);
                                        this.Text = this.Text.Remove(3);
                                        this.Text = this.Text.Insert(3, valor[1] + ":" + valor[2]);
                                        this.SelectionStart = posicaoInicial + 1;
                                    }

                                    e.SuppressKeyPress = true;
                                }
                                else if (posicaoInicial > 2 && posicaoInicial < 6)
                                {
                                    //Minuto
                                    if (valor[1].Trim().Length < 2)
                                    {
                                        this.Text = this.Text.Insert(posicaoInicial, caracter);
                                        if (valor[2].Trim().Length > 0)
                                        {
                                            this.Text = this.Text.Remove(6);
                                            this.Text = this.Text.Insert(6, valor[2]);
                                        }
                                        this.SelectionStart = posicaoInicial + 1;
                                    }

                                    e.SuppressKeyPress = true;
                                }
                            }
                        }

                        #endregion
                    }
                    else if (this.SelectionLength > 0 && this.SelectionLength < this.Text.Length && this.SelectionStart < 4 && this.SelectedText != ":")
                    {
                        #region Selecionado

                        int posicaoInicial = this.SelectionStart == 2 || this.SelectionStart == 5 ? this.SelectionStart + 1 : this.SelectionStart;
                        int posicaoFinal = this.SelectionStart == 2 || this.SelectionStart == 5 ? posicaoInicial + this.SelectionLength - 2 : posicaoInicial + this.SelectionLength - 1;
                        string valorInicial = this.Text;
                        string valorFinal = string.Empty;

                        for (int i = 0; i < valorInicial.Length; i++)
                        {
                            if (i >= posicaoInicial && i <= posicaoFinal)
                            {
                                if (valorInicial[i].ToString() != ":") valorFinal = valorFinal.Insert(i, " ");
                                else valorFinal = valorFinal.Insert(i, ":");
                            }
                            else
                            {
                                valorFinal = valorFinal.Insert(i, valorInicial[i].ToString());
                            }
                        }

                        valorFinal = valorFinal.Remove(posicaoInicial, 1).Insert(posicaoInicial, caracter);
                        if (valorFinal.Split(':')[0][0] == ' ') valorFinal = valorFinal.Remove(0, 1).Insert(1, " ");
                        if (posicaoFinal >= 3 && valorFinal.Split(':')[1][0] == ' ') valorFinal = valorFinal.Remove(3, 1).Insert(4, " ");
                        if (posicaoFinal >= 6 && valorFinal.Split(':')[2].Trim().Length > 0 && valorFinal.Split(':')[2].Contains(" "))
                        {
                            int qtdSelecionada = this.SelectionStart + this.SelectionLength;
                            valorFinal = valorFinal.Remove(6, qtdSelecionada - 6);
                        }

                        this.ResetText();
                        this.Text = valorFinal;
                        this.SelectionStart = posicaoInicial + 1;

                        e.SuppressKeyPress = true;

                        #endregion
                    }
                }

                #endregion
            }
            else if (Mascara == MascaraEnum.HORA_SHORT)
            {
                #region Hora short

                //Apenas números
                if (this.SelectionStart + 1 < this.Text.Length && ((e.KeyValue > 47 && e.KeyValue < 58) || (e.KeyValue > 95 && e.KeyValue < 106)))
                {
                    KeysConverter keyConverter = new KeysConverter();
                    String caracter = keyConverter.ConvertToInvariantString(e.KeyData).Replace("NumPad", "");

                    bInserting = Convert.ToBoolean(GetKeyState(45));
                    if (bInserting == false && this.SelectionLength == 0)
                    {
                        #region Não selecionado

                        string[] valor = this.Text.Split(':');
                        if (valor[0].Trim().Length < 2 || valor[1].Trim().Length < 2)
                        {
                            int posicaoInicial = this.Text[this.SelectionStart] != ':' ? this.SelectionStart : this.SelectionStart + 1;

                            if (this.Text[posicaoInicial] != ' ')
                            {
                                if (posicaoInicial < 3)
                                {
                                    //Hora
                                    if (valor[0].Trim().Length < 2)
                                    {
                                        this.Text = this.Text.Insert(posicaoInicial, caracter);

                                        if (this.Text.Length > 3)
                                            this.Text = this.Text.Remove(3);

                                        this.Text = this.Text.Insert(3, valor[1]);
                                        this.SelectionStart = posicaoInicial + 1;
                                    }

                                    e.SuppressKeyPress = true;
                                }
                            }
                        }

                        #endregion
                    }
                    else if (this.SelectionLength > 0 && this.SelectionLength < this.Text.Length && this.SelectionStart < 1 && this.SelectedText != ":")
                    {
                        #region Selecionado

                        int posicaoInicial = this.SelectionStart == 2 ? this.SelectionStart + 1 : this.SelectionStart;
                        int posicaoFinal = this.SelectionStart == 2 ? posicaoInicial + this.SelectionLength - 2 : posicaoInicial + this.SelectionLength - 1;
                        string valorInicial = this.Text;
                        string valorFinal = string.Empty;

                        for (int i = 0; i < valorInicial.Length; i++)
                        {
                            if (i >= posicaoInicial && i <= posicaoFinal)
                            {
                                if (valorInicial[i].ToString() != ":") valorFinal = valorFinal.Insert(i, " ");
                                else valorFinal = valorFinal.Insert(i, ":");
                            }
                            else
                            {
                                valorFinal = valorFinal.Insert(i, valorInicial[i].ToString());
                            }
                        }

                        valorFinal = valorFinal.Remove(posicaoInicial, 1).Insert(posicaoInicial, caracter);
                        if (valorFinal.Split(':')[0][0] == ' ') valorFinal = valorFinal.Remove(0, 1).Insert(1, " ");
                        if (posicaoFinal >= 3 && valorFinal.Split(':')[1].Trim().Length > 0 && valorFinal.Split(':')[1].Contains(" ")) valorFinal = valorFinal.Remove(3).Insert(3, valorFinal.Split(':')[1].Replace(" ", ""));

                        this.ResetText();
                        this.Text = valorFinal;
                        this.SelectionStart = posicaoInicial + 1;

                        e.SuppressKeyPress = true;

                        #endregion
                    }
                }

                #endregion
            }
            else if (Mascara == MascaraEnum.MOEDA_PORCENTAGEM)
            {
                #region Moeda/Porcentagem

                string mtbxTexto = this.Text;

                if (this.Text.Contains("-"))
                {
                    mtbxTexto = mtbxTexto.Replace("-", string.Empty.PadLeft(TamanhoMoeda - mtbxTexto.Split(',')[0].Length + 1));
                }

                //Apenas números
                if (this.SelectionStart + 1 < mtbxTexto.Length && ((e.KeyValue > 47 && e.KeyValue < 58) || (e.KeyValue > 95 && e.KeyValue < 106)))
                {
                    KeysConverter keyConverter = new KeysConverter();
                    String caracter = keyConverter.ConvertToInvariantString(e.KeyData).Replace("NumPad", "");

                    bInserting = Convert.ToBoolean(GetKeyState(45));

                    if (bInserting == false && this.SelectionLength == 0)
                    {
                        #region Não selecionado

                        string[] valor = mtbxTexto.Split(',');

                        int posicaoInicial = mtbxTexto[this.SelectionStart] != ',' ? this.SelectionStart : this.SelectionStart + 1;

                        if (mtbxTexto[posicaoInicial] != ' ')
                        {
                            if (posicaoInicial < mtbxTexto.IndexOf(','))
                            {
                                if (valor[1].Trim().Length > 0)
                                {
                                    //Ajusta decimais
                                    if (valor[0].Trim().Length < TamanhoMoeda && mtbxTexto[mtbxTexto.IndexOf(',') - 1] == ' ')
                                    {
                                        base.Text = base.Text.Insert(posicaoInicial, caracter);
                                        base.Text = base.Text.Remove(base.Text.IndexOf(',') + 1);
                                        base.Text = base.Text.Insert(base.Text.IndexOf(',') + 1, valor[1]);
                                        base.SelectionStart = posicaoInicial + 1;
                                    }

                                    e.SuppressKeyPress = true;
                                }
                                else if (mtbxTexto[mtbxTexto.IndexOf(',') - 1] != ' ')
                                {
                                    e.SuppressKeyPress = true;
                                }
                            }
                        }

                        #endregion
                    }
                    else if (this.SelectionLength > 0 && this.SelectionLength < this.Text.Replace("-", "").Length && this.SelectionStart < mtbxTexto.IndexOf(',') - 1 && this.SelectedText != ",")
                    {
                        #region Selecionado

                        string valorInicial = this.Text;
                        string valorFinal = string.Empty;

                        if (valorInicial.Contains("-"))
                        {
                            valorInicial = mtbxTexto.Insert(0, "-");
                            this.SelectionStart++;
                        }

                        int posicaoInicial = this.SelectionStart == mtbxTexto.IndexOf(',') ? this.SelectionStart + 1 : this.SelectionStart;
                        int posicaoFinal = this.SelectionStart == mtbxTexto.IndexOf(',') ? posicaoInicial + this.SelectionLength - 2 : posicaoInicial + this.SelectionLength - 1;

                        for (int i = 0; i < valorInicial.Length; i++)
                        {
                            if (i >= posicaoInicial && i <= posicaoFinal)
                            {
                                if (valorInicial[i].ToString() != ",") valorFinal = valorFinal.Insert(i, " ");
                                else valorFinal = valorFinal.Insert(i, ",");
                            }
                            else
                            {
                                valorFinal = valorFinal.Insert(i, valorInicial[i].ToString());
                            }
                        }

                        valorFinal = valorFinal.Remove(posicaoInicial, 1).Insert(posicaoInicial, caracter);
                        if (valorFinal.Split(',')[0].Contains(" "))
                        {
                            //Ajusta unidades
                            int qtdSelecionado = this.SelectedText.Split(',')[0].Length > 0 ? this.SelectedText.Split(',')[0].Length - 1 : 0;
                            if (qtdSelecionado > 0)
                            {
                                valorFinal = valorFinal.Remove(posicaoInicial + 1, qtdSelecionado);
                                valorFinal = valorFinal.Insert(valorFinal.IndexOf(','), " ".PadRight(qtdSelecionado));
                            }
                        }
                        if (valorFinal.Split(',')[1].Trim().Length > 0 && valorFinal.Split(',')[1].Contains(" "))
                        {
                            //Ajusta decimais
                            int qtdSelecionado = this.SelectionStart + this.SelectionLength;
                            if (qtdSelecionado > 0)
                            {
                                int index = valorFinal.IndexOf(',') + 1;
                                valorFinal = valorFinal.Remove(index, qtdSelecionado - index);
                            }
                        }

                        base.ResetText();
                        base.Text = valorFinal;
                        base.SelectionStart = posicaoInicial + 1;
                        if (valorFinal.Contains("-"))
                        {
                            base.SelectionStart = posicaoInicial;
                            Menos = true;
                        }

                        e.SuppressKeyPress = true;

                        #endregion
                    }
                }

                #endregion
            }
            else if (Mascara == MascaraEnum.DATA_HORA)
            {
                #region Data/Hora

                //Apenas números
                if (this.SelectionStart + 1 < this.Text.Length && ((e.KeyValue > 47 && e.KeyValue < 58) || (e.KeyValue > 95 && e.KeyValue < 106)))
                {
                    KeysConverter keyConverter = new KeysConverter();
                    String caracter = keyConverter.ConvertToInvariantString(e.KeyData).Replace("NumPad", "");

                    bInserting = Convert.ToBoolean(GetKeyState(45));
                    if (bInserting == false && this.SelectionLength == 0)
                    {
                        #region Não selecionado

                        string conteudo = this.Text;
                        conteudo = conteudo.Remove(10, 1).Insert(10, "E");

                        string[] valor = conteudo.Split('/');
                        string[] valorHora = conteudo.Split(':');
                        valor[2] = valor[2].Split('E')[0];
                        valorHora[0] = valorHora[0].Split('E')[1];

                        if (valor[0].Trim().Length < 2 || valor[1].Trim().Length < 2 || valor[2].Trim().Length < 4 ||
                            valorHora[0].Trim().Length < 2 || valorHora[1].Trim().Length < 2 || valorHora[2].Trim().Length < 2)
                        {
                            int posicaoInicial = this.Text[this.SelectionStart] != '/' && this.Text[this.SelectionStart] != ':' ? this.SelectionStart : this.SelectionStart + 1;

                            if (this.Text[posicaoInicial] != ' ')
                            {
                                if (posicaoInicial < 3)
                                {
                                    #region Dia

                                    if (valor[0].Trim().Length < 2)
                                    {
                                        this.Text = this.Text.Insert(posicaoInicial, caracter);
                                        this.Text = this.Text.Remove(3);
                                        this.Text = this.Text.Insert(3, $"{valor[1]}/{valor[2]} {valorHora[0]}:{valorHora[1]}:{valorHora[2]}");
                                        this.SelectionStart = posicaoInicial + 1;
                                    }

                                    e.SuppressKeyPress = true;

                                    #endregion
                                }
                                else if (posicaoInicial > 2 && posicaoInicial < 6)
                                {
                                    #region Mês

                                    if (valor[1].Trim().Length < 2)
                                    {
                                        this.Text = this.Text.Insert(posicaoInicial, caracter);
                                        if (valor[2].Trim().Length > 0)
                                        {
                                            this.Text = this.Text.Remove(6);
                                            this.Text = this.Text.Insert(6, $"{valor[2]} {valorHora[0]}:{valorHora[1]}:{valorHora[2]}");
                                        }

                                        this.SelectionStart = posicaoInicial + 1;
                                    }

                                    e.SuppressKeyPress = true;

                                    #endregion
                                }
                                else if (posicaoInicial > 5 && posicaoInicial < 10)
                                {
                                    #region Ano

                                    if (valor[2].Trim().Length < 4)
                                    {
                                        this.Text = this.Text.Insert(posicaoInicial, caracter);
                                        this.Text = this.Text.Remove(11);
                                        this.Text = this.Text.Insert(11, $"{valorHora[0]}:{valorHora[1]}:{valorHora[2]}");
                                        this.SelectionStart = posicaoInicial + 1;
                                    }

                                    e.SuppressKeyPress = true;

                                    #endregion
                                }
                                else if (posicaoInicial > 10 && posicaoInicial < 14)
                                {
                                    #region Hora

                                    if (valorHora[0].Trim().Length < 2)
                                    {
                                        this.Text = this.Text.Insert(posicaoInicial, caracter);
                                        this.Text = this.Text.Remove(14);
                                        this.Text = this.Text.Insert(14, $"{valorHora[1]}:{valorHora[2]}");
                                        this.SelectionStart = posicaoInicial + 1;
                                    }

                                    e.SuppressKeyPress = true;

                                    #endregion
                                }
                                else if (posicaoInicial > 13 && posicaoInicial < 17)
                                {
                                    #region Minuto

                                    if (valorHora[1].Trim().Length < 2)
                                    {
                                        this.Text = this.Text.Insert(posicaoInicial, caracter);
                                        if (valorHora[2].Trim().Length > 0)
                                        {
                                            this.Text = this.Text.Remove(17);
                                            this.Text = this.Text.Insert(17, $"{valorHora[2]}");
                                        }
                                        this.SelectionStart = posicaoInicial + 1;
                                    }

                                    e.SuppressKeyPress = true;

                                    #endregion
                                }
                            }
                            else if (posicaoInicial == 10 && valorHora[0].Trim().Length > 0)
                            {
                                #region Espaço entre data e hora

                                if (valorHora[0].Trim().Length < 2)
                                {
                                    valorHora[0] = valorHora[0].Insert(0, caracter).Remove(2);
                                    this.Text = $"{valor[0]}/{valor[1]}/{valor[2]} {valorHora[0]}:{valorHora[1]}:{valorHora[2]}";
                                    this.SelectionStart = posicaoInicial + 1;

                                    e.SuppressKeyPress = true;
                                }
                                else
                                {
                                    e.SuppressKeyPress = true;
                                }

                                #endregion
                            }
                        }

                        #endregion
                    }
                    else if (this.SelectionLength > 0 && this.SelectionLength < this.Text.Length)
                    {
                        #region Selecionado

                        if (this.SelectionStart == 10 && this.SelectionLength == 1)
                        {
                            return;
                        }

                        int posicaoInicial = this.SelectionStart == 2 || this.SelectionStart == 5 || this.SelectionStart == 10 || this.SelectionStart == 13 || this.SelectionStart == 16 ? this.SelectionStart + 1 : this.SelectionStart;
                        int posicaoFinal = this.SelectionStart == 2 || this.SelectionStart == 5 || this.SelectionStart == 10 || this.SelectionStart == 13 || this.SelectionStart == 16 ? posicaoInicial + this.SelectionLength - 2 : posicaoInicial + this.SelectionLength - 1;
                        string valorInicial = this.Text.Remove(10, 1).Insert(10, "E");
                        string valorFinal = string.Empty;

                        for (int i = 0; i < valorInicial.Length; i++)
                        {
                            if (i >= posicaoInicial && i <= posicaoFinal)
                            {
                                if (valorInicial[i].ToString() == "/")
                                {
                                    valorFinal = valorFinal.Insert(i, "/");
                                }
                                else if (valorInicial[i].ToString() == ":")
                                {
                                    valorFinal = valorFinal.Insert(i, ":");
                                }
                                else if (valorInicial[i].ToString() == "E")
                                {
                                    valorFinal = valorFinal.Insert(i, "E");
                                }
                                else
                                {
                                    valorFinal = valorFinal.Insert(i, " ");
                                }
                            }
                            else
                            {
                                valorFinal = valorFinal.Insert(i, valorInicial[i].ToString());
                            }
                        }

                        valorFinal = valorFinal.Remove(posicaoInicial, 1).Insert(posicaoInicial, caracter);

                        #region Dia

                        if (valorFinal.Split('/')[0][0] == ' ')
                        {
                            valorFinal = valorFinal.Remove(0, 1).Insert(1, " ");
                        }

                        #endregion

                        #region Mês

                        if (posicaoFinal >= 3 && valorFinal.Split('/')[1][0] == ' ')
                        {
                            valorFinal = valorFinal.Remove(3, 1).Insert(4, " ");
                        }

                        #endregion

                        #region Ano

                        if (posicaoFinal >= 6)
                        {
                            string valorAno = valorFinal.Split('/')[2].Split('E')[0];

                            if (string.IsNullOrEmpty(valorAno) == false && valorAno.Trim().Length != 4)
                            {
                                int qtd = valorAno.Length;
                                valorAno = valorAno.Replace(" ", "");
                                valorAno = valorAno.PadRight(4, ' ');
                                valorFinal = valorFinal.Remove(6, qtd).Insert(6, valorAno);
                            }
                        }

                        #endregion

                        #region Hora

                        string valorHora = valorFinal.Split(':')[0].Split('E')[1];

                        if (valorHora[0] == ' ')
                        {
                            valorFinal = valorFinal.Remove(11, 1).Insert(12, " ");
                        }

                        #endregion

                        #region Minutos

                        if (posicaoFinal >= 14 && valorFinal.Split(':')[1][0] == ' ')
                        {
                            valorFinal = valorFinal.Remove(14, 1).Insert(15, " ");
                        }

                        #endregion

                        #region Segundos

                        if (posicaoFinal >= 17 && valorFinal.Split(':')[2][0] == ' ')
                        {
                            valorFinal = valorFinal.Remove(17, 1).Insert(18, " ");
                        }

                        #endregion

                        this.ResetText();
                        this.Text = valorFinal;
                        this.SelectionStart = posicaoInicial + 1;

                        e.SuppressKeyPress = true;

                        #endregion
                    }
                }

                #endregion
            }

            #endregion
        }

        #endregion

        #region OnKeyPress

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            switch (CharacterCasing)
            {
                case CharacterCasing.Lower:
                    e.KeyChar = Char.ToLower(e.KeyChar, CultureInfo.InvariantCulture);
                    break;
                case CharacterCasing.Normal:
                    break;
                case CharacterCasing.Upper:
                    e.KeyChar = Char.ToUpper(e.KeyChar, CultureInfo.InvariantCulture);
                    break;
                default:
                    break;
            }

            if (this.ReadOnly)
            {
                e.Handled = true;
                return;
            }

            #region Tecla Insert

            bInserting = Convert.ToBoolean(GetKeyState(45));
            // Ativa Funcionalidade da tecla Insert
            if (Mascara == MascaraEnum.NORMAL && string.IsNullOrEmpty(this.Mask))
            {
                if (bInserting && e.KeyChar != (char)8 && this.SelectionLength == 0)
                {
                    this.SelectionLength = 1;
                }
            }
            else
            {
                if (bInserting == true && this.SelectionLength == 0)
                {
                    _statusInsert = 1;
                    StatusInsert();
                }
                else
                {
                    _statusInsert = 2;
                    StatusInsert();
                }
            }

            #endregion

            #region Operador -/+

            if (Mascara.Equals(MascaraEnum.MOEDA_PORCENTAGEM))
            {
                if (e.KeyChar == '-' && this.Text.DeFormat().Trim().Length > 0 && Convert.ToDecimal(this.Text.DeFormat()) > 0)
                {
                    if (this.SelectionLength > 0) SendKeys.Send("{DELETE}");

                    limpaCampo = false;
                    Menos = true;
                    e.Handled = true;
                    return;
                }
                else if (e.KeyChar == '+')
                {
                    if (this.SelectionLength > 0) SendKeys.Send("{DELETE}");

                    limpaCampo = false;
                    Menos = false;
                    e.Handled = true;
                    return;
                }
            }

            #endregion

            #region Apenas Números

            if (Mascara.Equals(MascaraEnum.DATA) || Mascara.Equals(MascaraEnum.HORA_SHORT) || Mascara.Equals(MascaraEnum.HORA_LONG) || Mascara.Equals(MascaraEnum.MOEDA_PORCENTAGEM) || Mascara.Equals(MascaraEnum.MES_ANO))
            {
                ///Para utilizar CTRL + X, CTRL + V .....
                if (Form.ModifierKeys != Keys.Control && !Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8) //permite apenas inserção de números  (char)8) = tecla backapace
                {
                    e.Handled = true;
                }
            }

            #endregion

            #region Moeda_porcentagem

            if (Mascara.Equals(MascaraEnum.MOEDA_PORCENTAGEM) && Form.ModifierKeys != Keys.Control)
            {
                if ((e.KeyChar < '0' || e.KeyChar > '9') && (e.KeyChar != '-' && e.KeyChar != ',' && e.KeyChar != '.' && e.KeyChar != (char)13 && e.KeyChar != (char)8))
                {
                    e.KeyChar = (char)0;
                    limpaCampo = false;
                }
                else
                {
                    if (this.SelectionStart == 0 && limpaCampo.Equals(true))
                    {
                        limpaCampo = false;
                        base.Text = "";
                    }
                    else limpaCampo = false;

                    if (e.KeyChar == '.' || e.KeyChar == ',')
                    {
                        if (!this.Text.Contains(","))
                        {
                            e.KeyChar = ',';
                        }
                        else
                        {
                            if (this.SelectionStart <= _TamanhoMoeda) MascaraMoedaEnter();
                        }
                    }
                }
            }

            #endregion

            base.OnKeyPress(e);
        }

        #endregion

        #region OnFontChanged

        protected override void OnFontChanged(EventArgs e)
        {
            labelSimbolo.Font = this.Font;
            labelMenos.Font = this.Font;

            base.OnFontChanged(e);
        }

        #endregion

        #region OnForeColorChanged

        protected override void OnForeColorChanged(EventArgs e)
        {
            labelSimbolo.ForeColor = this.ForeColor;
            labelMenos.ForeColor = this.ForeColor;

            base.OnForeColorChanged(e);
        }

        #endregion

        #region OnMouseDown

        protected override void OnMouseDown(MouseEventArgs e)
        {
            clickPressionado = true;
            threadClick = new Thread(delegate ()
            {
                while (clickPressionado)
                {
                    CrossThreadOperation.Invoke(this, delegate { labelSimbolo.Refresh(); });
                    CrossThreadOperation.Invoke(this, delegate { labelMenos.Refresh(); });

                    Thread.Sleep(1);
                }
                Thread.CurrentThread.Abort();
            });
            //Multi thread apartment (ambiente onde várias thread podem ser executadas
            threadClick.SetApartmentState(ApartmentState.MTA);
            threadClick.Start();

            base.OnMouseDown(e);
        }

        #endregion

        #region OnMouseUp

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            clickPressionado = false;

            base.OnMouseUp(mevent);
        }

        #endregion

        #region HotKeys

        #region LimpaTexto

        #region Incrementar

        /// <summary>
        /// Função de incrementar o valor do campo.
        /// </summary>
        public void Incrementar()
        {
            switch (Mascara)
            {
                case MascaraEnum.MOEDA_PORCENTAGEM:
                    {
                        #region Moeda_Porcentagem

                        if (this.Focused.Equals(false))
                        {
                            if (this.Text.DeFormat().Length > 0) this.SglVALOR = Convert.ToDecimal(this.Text.Replace(".", "").Replace(",", ""));
                            else this.SglVALOR = null;
                        }

                        if (this.SglVALOR != null)
                        {
                            this.eventoLeave = true;
                            this.SglVALOR = this.SglVALOR + 1;

                            Int64 tamanhoCampo = this.TamanhoMoeda + this.DecimaisMoeda;
                            if (SglVALOR.ToString().DeFormat().Length > tamanhoCampo) this.SglVALOR = this.SglVALOR - 1;

                            bool negativo = SglVALOR.ToString().Contains("-") ? true : false;
                            SglVALOR = decimal.Parse(SglVALOR.ToString().Replace("-", ""));
                            string valor = Convert.ToString(this.SglVALOR).PadLeft(this.tamanho, '0');
                            valor = valor.Insert(valor.Length - this.DecimaisMoeda, ",");
                            String[] digitado = valor.Split(new char[] { ',' });

                            if (digitado[0].Length > this.TamanhoMoeda) return;

                            digitado[0] = digitado[0].Replace(".", "");
                            base.Mask = string.Empty.PadLeft(TamanhoMoeda, '0') + '.' + string.Empty.PadLeft(DecimaisMoeda, '0');
                            base.Text = string.Empty;
                            int qtd = this.TamanhoMoeda - digitado[0].Trim().Length;
                            int qtdDecimais = this.TamanhoMoeda + 1;

                            //arruma valores antes da virgula
                            for (int i = 0; i < digitado[0].Trim().Length; i++)
                            {
                                base.Text = base.Text.Insert(qtd + i, digitado[0][i].ToString());
                            }
                            //arruma centavos
                            for (int i = 0; i < digitado[1].Trim().Length; i++)
                            {
                                base.Text = base.Text.Insert(qtdDecimais + i, digitado[1][i].ToString());
                            }
                            this.MascaraMoedaLeave();
                            if (negativo == false) Menos = false;
                        }
                        else
                        {
                            base.Mask = "0." + base.Mask.PadLeft(DecimaisMoeda, '0');
                            base.Text = base.Text.Insert(0, "0");
                            for (int i = 0; i < DecimaisMoeda - 1; i++)
                            {
                                base.Text = base.Text.Insert(2 + i, "0");
                            }
                            base.Text = base.Text.Insert(2 + (DecimaisMoeda - 1), Convert.ToString(SglVALOR));
                            this.tamanho = this.Text.DeFormat().Length;
                        }

                        this.eventoLeave = false;

                        #endregion
                    }
                    break;
                case MascaraEnum.DATA:
                    {
                        #region Data

                        string dataString = string.Empty;
                        if (this.Focused.Equals(false) && this.Text.DeFormat().Length > 0) dataString = this.Text;
                        else if (this.Focused && this.SglVALOR > 0)
                        {
                            if (Convert.ToString(this.SglVALOR).Length == 8) dataString = Convert.ToString(this.SglVALOR).FormatDATA();
                            else
                            {
                                dataString = Convert.ToString(this.SglVALOR).Insert(0, "0");
                                dataString = dataString.FormatDATA();
                            }
                        }

                        if (dataString.Length > 0)
                        {
                            DateTime _date = DateTime.Parse(dataString);

                            if (_date != DateTime.MaxValue)
                            {
                                _date = _date.AddDays(1);
                                this.Text = _date.ToShortDateString();
                            }
                        }

                        #endregion
                    }
                    break;
                case MascaraEnum.HORA_SHORT:
                    {
                        #region Hora_short

                        string horaString = string.Empty;
                        if (this.Focused.Equals(false) && this.Text.DeFormat().Length > 0) horaString = this.Text;
                        else if (this.Focused && this.SglVALOR > 0)
                        {
                            if (Convert.ToString(this.SglVALOR).Length == 4) horaString = Convert.ToString(this.SglVALOR).Insert(2, ":");
                            else
                            {
                                horaString = Convert.ToString(this.SglVALOR).Insert(0, "0");
                                horaString = horaString.Insert(2, ":");
                            }
                        }

                        if (horaString.Length > 0)
                        {
                            DateTime _hora = DateTime.Parse(horaString);

                            if (_hora != DateTime.MaxValue)
                            {
                                _hora = _hora.AddMinutes(1);
                                this.Text = _hora.ToLongTimeString();
                            }
                        }

                        #endregion
                    }
                    break;
                case MascaraEnum.HORA_LONG:
                    {
                        #region Hora_long

                        string horaString = string.Empty;
                        if (this.Focused.Equals(false) && this.Text.DeFormat().Length > 0) horaString = this.Text;
                        else if (this.Focused && this.SglVALOR > 0)
                        {
                            if (Convert.ToString(this.SglVALOR).Length == 6) horaString = Convert.ToString(this.SglVALOR).Insert(2, ":").Insert(5, ":");
                            else
                            {
                                horaString = Convert.ToString(this.SglVALOR).Insert(0, "0");
                                horaString = horaString.Insert(2, ":").Insert(5, ":");
                            }
                        }

                        if (horaString.Length > 0)
                        {
                            DateTime _hora = DateTime.Parse(horaString);

                            if (_hora != DateTime.MaxValue)
                            {
                                _hora = _hora.AddSeconds(1);
                                this.Text = _hora.ToLongTimeString();
                            }
                        }

                        #endregion
                    }
                    break;
                case MascaraEnum.MES_ANO:
                    {
                        #region Mes_Ano

                        string dataString = string.Empty;
                        if (this.Focused.Equals(false) && this.Text.DeFormat().Trim().Length > 0) dataString = this.Text;
                        else if (this.Focused && this.SglVALOR > 0)
                        {
                            if (Convert.ToString(this.SglVALOR).Length == 8) dataString = Convert.ToString(this.SglVALOR).FormatMesAno();
                            else
                            {
                                dataString = Convert.ToString(this.SglVALOR).Insert(0, "0");
                                dataString = dataString.FormatMesAno();
                            }
                        }

                        if (dataString.Length > 0)
                        {
                            DateTime _date = DateTime.Parse(dataString);

                            if (_date != DateTime.MaxValue)
                            {
                                _date = _date.AddMonths(1);
                                this.Text = _date.Date.ToString("MM/yyyy");
                            }
                        }

                        #endregion
                    }
                    break;
                case MascaraEnum.DATA_HORA:
                    {
                        #region Data_Hora

                        string dataString = this.Text;

                        if (dataString.Length > 0)
                        {
                            DateTime result;
                            if (DateTime.TryParse(dataString, out result))
                            {
                                if (result != DateTime.MaxValue)
                                {
                                    this.Text = result.AddSeconds(1).ToString("dd/MM/yyyy HH:mm:ss");
                                }
                            }
                        }

                        #endregion
                    }
                    break;
            }
        }

        #endregion

        #region Decrementar

        /// <summary>
        /// Função de decrementar o valor do campo.
        /// </summary>
        public void Decrementar()
        {
            if (this.Text.DeFormat().Length > 0)
            {
                switch (Mascara)
                {
                    case MascaraEnum.MOEDA_PORCENTAGEM:
                        {
                            #region Moeda_Porcentagem

                            if (this.Focused.Equals(false))
                            {
                                if (this.Text.DeFormat().Length > 0) this.SglVALOR = Convert.ToDecimal(this.Text.Replace(".", "").Replace(",", ""));
                                else this.SglVALOR = null;
                            }

                            if (SglVALOR != null)
                            {
                                eventoLeave = true;
                                SglVALOR = SglVALOR - 1;

                                Int64 tamanhoCampo = TamanhoMoeda + DecimaisMoeda;
                                if (SglVALOR.ToString().DeFormat().Length > tamanhoCampo || (Negativo == false && Convert.ToDecimal(SglVALOR) < 0)) SglVALOR = SglVALOR + 1;

                                bool negativo = SglVALOR.ToString().Contains("-") ? true : false;
                                SglVALOR = decimal.Parse(SglVALOR.ToString().Replace("-", ""));
                                string valor = Convert.ToString(SglVALOR).PadLeft(tamanho, '0');
                                valor = valor.Insert(valor.Length - DecimaisMoeda, ",");
                                String[] digitado = valor.Split(new char[] { ',' });

                                if (digitado[0].Length > TamanhoMoeda) return;

                                digitado[0] = digitado[0].Replace(".", "");
                                base.Mask = string.Empty.PadLeft(TamanhoMoeda, '0') + '.' + string.Empty.PadLeft(DecimaisMoeda, '0');
                                base.Text = string.Empty;
                                int qtd = TamanhoMoeda - digitado[0].Trim().Length;
                                int qtdDecimais = TamanhoMoeda + 1;

                                //arruma valores antes da virgula
                                for (int i = 0; i < digitado[0].Trim().Length; i++)
                                {
                                    base.Text = base.Text.Insert(qtd + i, digitado[0][i].ToString());
                                }
                                //arruma centavos
                                for (int i = 0; i < digitado[1].Trim().Length; i++)
                                {
                                    base.Text = base.Text.Insert(qtdDecimais + i, digitado[1][i].ToString());
                                }
                                MascaraMoedaLeave();
                                if (negativo) Menos = true;
                            }
                            else
                            {
                                base.Mask = null;
                                base.Mask = "0." + base.Mask.PadLeft(DecimaisMoeda, '0');
                                base.Text = base.Text.Insert(0, "0");
                                for (int i = 0; i < DecimaisMoeda - 1; i++)
                                {
                                    base.Text = base.Text.Insert(2 + i, "0");
                                }
                                base.Text = base.Text.Insert(2 + (DecimaisMoeda - 1), Convert.ToString(SglVALOR));
                                tamanho = this.Text.DeFormat().Length;
                            }

                            eventoLeave = false;

                            #endregion
                        }
                        break;
                    case MascaraEnum.DATA:
                        {
                            #region Data

                            string dataString = string.Empty;
                            if (this.Focused.Equals(false) && this.Text.DeFormat().Length > 0) dataString = this.Text;
                            else if (this.Focused && SglVALOR > 0)
                            {
                                if (Convert.ToString(SglVALOR).Length == 8) dataString = Convert.ToString(SglVALOR).FormatDATA();
                                else
                                {
                                    dataString = Convert.ToString(SglVALOR).Insert(0, "0");
                                    dataString = dataString.FormatDATA();
                                }
                            }

                            if (dataString.Length > 0)
                            {
                                DateTime _date = DateTime.Parse(dataString);

                                if (_date != DateTime.MinValue)
                                {
                                    _date = _date.AddDays(-1);
                                    this.Text = _date.ToShortDateString();
                                }
                            }

                            #endregion
                        }
                        break;
                    case MascaraEnum.HORA_SHORT:
                        {
                            #region Hora_short

                            string horaString = string.Empty;
                            if (this.Focused.Equals(false) && this.Text.DeFormat().Length > 0) horaString = this.Text;
                            else if (this.Focused && SglVALOR > 0)
                            {
                                if (Convert.ToString(SglVALOR).Length == 4) horaString = Convert.ToString(SglVALOR).Insert(2, ":");
                                else
                                {
                                    horaString = Convert.ToString(SglVALOR).Insert(0, "0");
                                    horaString = horaString.Insert(2, ":");
                                }
                            }

                            if (horaString.Length > 0)
                            {
                                DateTime _hora = DateTime.Parse(horaString);

                                if (_hora != DateTime.MinValue)
                                {
                                    _hora = _hora.AddMinutes(-1);
                                    this.Text = _hora.ToLongTimeString();
                                }
                            }

                            #endregion
                        }
                        break;
                    case MascaraEnum.HORA_LONG:
                        {
                            #region Hora_long

                            string horaString = string.Empty;
                            if (this.Focused.Equals(false) && this.Text.DeFormat().Length > 0) horaString = this.Text;
                            else if (this.Focused && SglVALOR > 0)
                            {
                                if (Convert.ToString(SglVALOR).Length == 6) horaString = Convert.ToString(SglVALOR).Insert(2, ":").Insert(5, ":");
                                else
                                {
                                    horaString = Convert.ToString(SglVALOR).Insert(0, "0");
                                    horaString = horaString.Insert(2, ":").Insert(5, ":");
                                }
                            }

                            if (horaString.Length > 0)
                            {
                                DateTime _hora = DateTime.Parse(horaString);

                                if (_hora != DateTime.MinValue)
                                {
                                    _hora = _hora.AddSeconds(-1);
                                    this.Text = _hora.ToLongTimeString();
                                }
                            }

                            #endregion
                        }
                        break;
                    case MascaraEnum.MES_ANO:
                        {
                            #region Mes_Ano

                            string dataString = string.Empty;
                            if (this.Focused.Equals(false) && this.Text.DeFormat().Length > 0) dataString = this.Text;
                            else if (this.Focused && SglVALOR > 0)
                            {
                                if (Convert.ToString(SglVALOR).Length == 8) dataString = Convert.ToString(SglVALOR).FormatMesAno();
                                else
                                {
                                    dataString = Convert.ToString(SglVALOR).Insert(0, "0");
                                    dataString = dataString.FormatMesAno();
                                }
                            }

                            if (dataString.Length > 0)
                            {
                                DateTime _date = DateTime.Parse(dataString);

                                if (_date != DateTime.MinValue)
                                {
                                    _date = _date.AddMonths(-1);
                                    this.Text = _date.Date.ToString("MM/yyyy");
                                }
                            }

                            #endregion
                        }
                        break;
                    case MascaraEnum.DATA_HORA:
                        {
                            #region Data_Hora

                            string dataString = this.Text;

                            if (dataString.Length > 0)
                            {
                                DateTime result;
                                if (DateTime.TryParse(dataString, out result))
                                {
                                    if (result != DateTime.MinValue)
                                    {
                                        this.Text = result.AddSeconds(-1).ToString("dd/MM/yyyy HH:mm:ss");
                                    }
                                }
                            }

                            #endregion
                        }
                        break;
                }
            }
        }

        #endregion

        #endregion

        #endregion

        #region Métodos

        #region Métodos Ref. Moeda

        #region Entrada

        /// <summary>
        /// Ajusta valor de entrada do controle quando
        /// entra através do click.
        /// </summary>
        /// <param name="mtbx">Controle a ser ajustado.</param>
        private void MascaraMoedaEnter()
        {
            //quando está focado arruma valor conforme máscara
            //pega valor digitado
            String[] digitado = base.Text.Split(new char[] { ',' });
            String[] _digitado;
            string valor;
            bool ajustaCentavos = false;
            while (digitado[0].StartsWith(" ") && digitado[0].Trim().Length > 0) digitado[0] = digitado[0].Remove(0, 1);
            if (digitado[0].Contains(" "))
            {
                _digitado = digitado[0].Split(new char[] { ' ' });
                valor = _digitado[0].ToString();
                ajustaCentavos = false;
            }
            else
            {
                valor = digitado[0].ToString();
                ajustaCentavos = true;
            }

            //remove zero a esquerda
            while (valor.Trim().StartsWith("0") && valor.Trim().Length > 1) valor = valor.Remove(0, 1);

            valor = valor.Replace(" ", "");
            int qtdDigito = valor.Length;
            int posicao = _TamanhoMoeda + 1;

            //arruma valores antes da virgula conforme máscara
            base.Text = "";
            for (int i = 0; i < valor.Length; i++)
            {
                //arruma valores antes da virgula
                base.Text = base.Text.Insert((_TamanhoMoeda - qtdDigito) + i, valor[i].ToString());
            }

            //arruma valores após a vírgula conforme máscara
            if (qtdDigito == 0)
            {
                base.Text = base.Text.Insert(_TamanhoMoeda - 1, "0");

                if (digitado[1].Length > 0)
                {
                    if (ajustaCentavos)
                    {
                        for (int i = 0; i < _DecimaisMoeda; i++)
                        {
                            try { base.Text = base.Text.Insert(posicao + i, digitado[1][i].ToString()); }
                            catch (IndexOutOfRangeException) { base.Text = base.Text.Insert(posicao + i, "0"); }
                        }
                    }
                }
                base.SelectionStart = posicao;
            }
            else if (qtdDigito > 0)
            {
                if (digitado[1].Length > 0)
                {
                    if (ajustaCentavos)
                    {
                        if (digitado[1].StartsWith(" "))
                        {
                            for (int i = 0; i < _DecimaisMoeda; i++)
                            {
                                base.Text = base.Text.Insert(posicao + i, "0");
                            }
                        }
                        else
                        {
                            for (int i = 0; i < _DecimaisMoeda; i++)
                            {
                                try { base.Text = base.Text.Insert(posicao + i, digitado[1][i].ToString()); }
                                catch (IndexOutOfRangeException) { base.Text = base.Text.Insert(posicao + i, "0"); }
                            }
                        }
                    }
                }
                base.SelectionStart = posicao;
            }
        }

        /// <summary>
        /// Ajusta valor de entrada do controle quando utiliza
        /// teclas de atalho (F4, F5, F6, F8, F9).
        /// </summary>
        public void MetodoEnterHotKeys()
        {
            //Ref. máscara moeda
            string valor = this.Text.Trim();

            base.Text = string.Empty;
            base.Mask = string.Empty;
            base.Mask = base.Mask.PadLeft(_TamanhoMoeda, '0') + '.' + base.Mask.PadRight(_DecimaisMoeda, '0');

            if (valor.DeFormat() != "")
            {
                String[] arrayValor = valor.Replace(".", "").Replace("-", "").Split(new char[] { ',' });

                int qtd = _TamanhoMoeda - arrayValor[0].Trim().Length;
                int qtdDecimais = _TamanhoMoeda + 1;
                //arruma valores antes da virgula
                for (int i = 0; i < arrayValor[0].Trim().Length; i++)
                {
                    base.Text = base.Text.Insert(qtd + i, arrayValor[0][i].ToString());
                }
                //arruma centavos
                for (int i = 0; i < arrayValor[1].Trim().Length; i++)
                {
                    base.Text = base.Text.Insert(qtdDecimais + i, arrayValor[1][i].ToString());
                }
            }
        }

        #endregion

        #region Saída

        /// <summary>
        /// Ajusta valor de saída do controle.
        /// Método é executado dentro do método (metodoLeave()).
        /// </summary>
        private void MascaraMoedaLeave()
        {
            bool ajustaCentavos = false;
            string antesVirgula = base.Text.Split(',')[0].TrimStart();
            string decimais = base.Text.Split(',')[1];

            //remove zero a esquerda
            while (antesVirgula.StartsWith("0") && antesVirgula.Trim().Length > 1)
            {
                antesVirgula = antesVirgula.Remove(0, 1);
            }

            if (antesVirgula.Contains(" "))
            {
                antesVirgula = antesVirgula.Split(' ')[0];
                ajustaCentavos = false;
            }
            else if (decimais.Trim().Length > 0)
            {
                ajustaCentavos = true;
            }

            base.Text = "";

            #region Ajusta Máscara

            base.Mask = string.Empty;
            base.Mask = string.Empty.PadLeft(antesVirgula.Length, '0') + '.' + base.Mask.PadRight(_DecimaisMoeda, '0');

            #region Antes da vírgula

            for (int i = 0; i < antesVirgula.Length; i++)
            {
                base.Text = base.Text.Insert(i, antesVirgula[i].ToString());
            }

            #endregion

            #region Centavos

            //ajusta valores após a virgula
            for (int i = 1; i <= _DecimaisMoeda; i++)
            {
                base.Text = base.Text.Insert(antesVirgula.Length + i, "0");
            }
            if (decimais.Trim().Length > 0 && ajustaCentavos)
            {
                for (int i = 0; i < decimais.Length; i++)
                {
                    base.Text = base.Text.Insert(antesVirgula.Length + i + 1, decimais[i].ToString());
                }
            }

            #endregion

            #region Milhares

            int milhar = 3;
            int cont = 0;
            for (int i = antesVirgula.Length - 1; i > 0; i--)
            {
                cont++;

                if (cont == milhar)
                {
                    base.Mask = base.Mask.Insert(i, ",");
                    milhar += 3;
                }
            }

            #endregion

            #endregion
        }

        /// <summary>
        /// Ajusta valor de saída do controle.
        /// </summary>
        public void MetodoLeave()
        {
            // Aplica a Mascara ao sair do controle ref. máscara moeda
            //diferente de vazio
            if (base.Text.DeFormat() != "" && Convert.ToDecimal(base.Text.DeFormat()) > 0)
            {
                eventoLeave = true; //indica que evento está sendo executado...

                decimal soma = Convert.ToDecimal(base.Text.Replace(" ", "")) + Convert.ToDecimal(base.Text.Replace(" ", ""));

                //igual a vazio, insere valor vazio
                if (soma == 0)
                {
                    base.Mask = "0." + string.Empty.PadLeft(_DecimaisMoeda, '0');
                    base.Text = this.Text.Insert(0, "0").Insert(1, string.Empty.PadRight(_DecimaisMoeda, '0'));
                    return;
                }

                String[] valores = this.Text.Split(new char[] { ',' });

                if (valores[0].Trim().Length == 0)
                {
                    base.Text = base.Text.Remove(base.Text.IndexOf(',') - 1, 1);
                    base.Text = base.Text.Insert(base.Text.IndexOf(',') - 1, "0");
                }

                if (valores[1].Trim().Length > 0)
                {
                    if (valores[1][0].ToString() == " ")
                    {
                        int posicao = valores[0].Length + 1;
                        for (int i = 0; i < _DecimaisMoeda; i++)
                        {
                            base.Text = base.Text.Insert(posicao + i, "0");
                        }
                    }
                }

                MascaraMoedaLeave();
            }
            else
            {
                //Insere valor vazio
                base.Mask = "0." + string.Empty.PadLeft(_DecimaisMoeda, '0');
                base.Text = base.Text.Insert(0, "0").Insert(1, string.Empty.PadRight(_DecimaisMoeda, '0'));
            }

            eventoLeave = false;

            //igual a vazio
            if (base.Text.DeFormat() != "" && Convert.ToDecimal(base.Text.DeFormat()) == 0)
            {
                base.Mask = "0." + string.Empty.PadLeft(_DecimaisMoeda, '0');
                base.Text = base.Text.Insert(0, "0").Insert(1, string.Empty.PadRight(_DecimaisMoeda, '0'));
                Menos = false;
            }
        }

        #endregion

        #region CTRL_Z

        /// <summary>
        /// Ajusta valor de saída do controle mtbxDigitado.
        /// Método é executado dentro do método (metodoLeaveCTRL_Z()).
        /// </summary>
        /// <param name="mtbx">Controle a ser ajustado.</param>
        public void MascaraMoedaLeaveCTRL_Z(MaskedTextBox mtbx)
        {
            String[] digitado = mtbx.Text.Split(new char[] { ',' });
            String[] _digitado;
            string valor;
            bool ajustaCentavos = false;
            while (digitado[0].StartsWith(" ") && digitado[0].Trim().Length > 0) digitado[0] = digitado[0].Remove(0, 1);

            if (digitado[0].Contains(" "))
            {
                _digitado = digitado[0].Split(new char[] { ' ' });
                valor = _digitado[0].ToString();
                ajustaCentavos = false;
            }
            else
            {
                valor = digitado[0].ToString();
                ajustaCentavos = true;
            }

            //caso valor igual a vazio
            if (valor.Replace(" ", "").Trim() == "")
            {
                if (mtbx.Text.Replace(",", "").Trim() != "") valor = mtbx.Text;
            }

            //remove zero a esquerda
            String[] zeroEsquerda = valor.Split(new char[] { ',' });
            while (zeroEsquerda[0].Trim().StartsWith("0") && zeroEsquerda[0].Trim().Length > 1)
            {
                valor = valor.Remove(0, 1);
                zeroEsquerda[0] = valor;
            }

            valor = valor.Replace(" ", "");

            mtbx.Text = "";

            //quando perde foco arruma valor conforme máscara
            String[] separador = valor.Split(new char[] { ',' });
            int qtdDigito = separador[0].Trim().Length;

            switch (qtdDigito)
            {
                case 0:
                    mtbx.Text = mtbx.Text.Insert(0, "0");

                    mtbx.Mask = "0." + string.Empty.PadLeft(_DecimaisMoeda, '0');

                    //ajusta valores após a virgula
                    if (digitado[1].Length == 0)
                    {
                        for (int i = 0; i < _DecimaisMoeda; i++)
                        {
                            mtbx.Text = mtbx.Text.Insert(2 + i, "0");
                        }
                    }
                    else
                    {
                        if (ajustaCentavos)
                        {
                            for (int i = 0; i < _DecimaisMoeda; i++)
                            {
                                try { mtbx.Text = mtbx.Text.Insert(2 + i, digitado[1][i].ToString()); }
                                catch (IndexOutOfRangeException) { mtbx.Text = mtbx.Text.Insert(2 + i, "0"); }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < _DecimaisMoeda; i++)
                            {
                                mtbx.Text = mtbx.Text.Insert(2 + i, "0");
                            }
                        }
                    }
                    break;
                case 1:
                    mtbx.Mask = "0." + string.Empty.PadLeft(_DecimaisMoeda, '0');

                    //ajusta valores antes da virgula
                    for (int i = 0; i < 1; i++)
                    {
                        mtbx.Text = mtbx.Text.Insert(i, valor[i].ToString());
                    }

                    //ajusta valores após a virgula
                    if (digitado[1].Length == 0)
                    {
                        for (int i = 0; i < _DecimaisMoeda; i++)
                        {
                            mtbx.Text = mtbx.Text.Insert(2 + i, "0");
                        }
                    }
                    else
                    {
                        if (ajustaCentavos)
                        {
                            for (int i = 0; i < _DecimaisMoeda; i++)
                            {
                                try { mtbx.Text = mtbx.Text.Insert(2 + i, digitado[1][i].ToString()); }
                                catch (IndexOutOfRangeException) { mtbx.Text = mtbx.Text.Insert(2 + i, "0"); }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < _DecimaisMoeda; i++)
                            {
                                mtbx.Text = mtbx.Text.Insert(2 + i, "0");
                            }
                        }
                    }
                    break;
                case 2:
                    mtbx.Mask = "00." + string.Empty.PadLeft(_DecimaisMoeda, '0');

                    //ajusta valores antes da virgula
                    for (int i = 0; i < 2; i++)
                    {
                        mtbx.Text = mtbx.Text.Insert(i, valor[i].ToString());
                    }

                    //ajusta valores após a virgula
                    if (digitado[1].Length == 0)
                    {
                        for (int i = 0; i < _DecimaisMoeda; i++)
                        {
                            mtbx.Text = mtbx.Text.Insert(3 + i, "0");
                        }
                    }
                    else
                    {
                        if (ajustaCentavos)
                        {
                            for (int i = 0; i < _DecimaisMoeda; i++)
                            {
                                try { mtbx.Text = mtbx.Text.Insert(3 + i, digitado[1][i].ToString()); }
                                catch (IndexOutOfRangeException) { mtbx.Text = mtbx.Text.Insert(3 + i, "0"); }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < _DecimaisMoeda; i++)
                            {
                                mtbx.Text = mtbx.Text.Insert(3 + i, "0");
                            }
                        }
                    }

                    break;
                case 3:
                    mtbx.Mask = "000." + string.Empty.PadLeft(_DecimaisMoeda, '0');

                    //ajusta valores antes da virgula
                    for (int i = 0; i < 3; i++)
                    {
                        mtbx.Text = mtbx.Text.Insert(i, valor[i].ToString());
                    }

                    //ajusta valores após a virgula
                    if (digitado[1].Length == 0)
                    {
                        for (int i = 0; i < _DecimaisMoeda; i++)
                        {
                            mtbx.Text = mtbx.Text.Insert(4 + i, "0");
                        }
                    }
                    else
                    {
                        if (ajustaCentavos)
                        {
                            for (int i = 0; i < _DecimaisMoeda; i++)
                            {
                                try { mtbx.Text = mtbx.Text.Insert(4 + i, digitado[1][i].ToString()); }
                                catch (IndexOutOfRangeException) { mtbx.Text = mtbx.Text.Insert(4 + i, "0"); }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < _DecimaisMoeda; i++)
                            {
                                mtbx.Text = mtbx.Text.Insert(4 + i, "0");
                            }
                        }
                    }
                    break;
                case 4:
                    mtbx.Text = mtbx.Text.Insert(1, " ");
                    mtbx.Mask = "0,000." + string.Empty.PadLeft(_DecimaisMoeda, '0');

                    //ajusta valores antes da virgula
                    for (int i = 0; i < 5; i++)
                    {
                        if (i != 1) mtbx.Text = mtbx.Text.Insert(i, valor[i].ToString());
                        else valor = valor.Insert(i, ",");
                    }

                    //ajusta valores após a virgula
                    if (digitado[1].Length == 0)
                    {
                        for (int i = 0; i < _DecimaisMoeda; i++)
                        {
                            mtbx.Text = mtbx.Text.Insert(6 + i, "0");
                        }
                    }
                    else
                    {
                        if (ajustaCentavos)
                        {
                            for (int i = 0; i < _DecimaisMoeda; i++)
                            {
                                try { mtbx.Text = mtbx.Text.Insert(6 + i, digitado[1][i].ToString()); }
                                catch (IndexOutOfRangeException) { mtbx.Text = mtbx.Text.Insert(6 + i, "0"); }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < _DecimaisMoeda; i++)
                            {
                                mtbx.Text = mtbx.Text.Insert(6 + i, "0");
                            }
                        }
                    }
                    break;
                case 5:
                    mtbx.Text = mtbx.Text.Insert(2, " ");
                    mtbx.Mask = "00,000." + string.Empty.PadLeft(_DecimaisMoeda, '0');

                    //ajusta valores antes da virgula
                    for (int i = 0; i < 6; i++)
                    {
                        if (i != 2) mtbx.Text = mtbx.Text.Insert(i, valor[i].ToString());
                        else valor = valor.Insert(i, ",");
                    }

                    //ajusta valores após a virgula
                    if (digitado[1].Length == 0)
                    {
                        for (int i = 0; i < _DecimaisMoeda; i++)
                        {
                            mtbx.Text = mtbx.Text.Insert(7 + i, "0");
                        }
                    }
                    else
                    {
                        if (ajustaCentavos)
                        {
                            for (int i = 0; i < _DecimaisMoeda; i++)
                            {
                                try { mtbx.Text = mtbx.Text.Insert(7 + i, digitado[1][i].ToString()); }
                                catch (IndexOutOfRangeException) { mtbx.Text = mtbx.Text.Insert(7 + i, "0"); }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < _DecimaisMoeda; i++)
                            {
                                mtbx.Text = mtbx.Text.Insert(7 + i, "0");
                            }
                        }
                    }
                    break;
                case 6:
                    mtbx.Text = mtbx.Text.Insert(3, " ");
                    mtbx.Mask = "000,000." + string.Empty.PadLeft(_DecimaisMoeda, '0');

                    //ajusta valores antes da virgula
                    for (int i = 0; i < 7; i++)
                    {
                        if (i != 3) mtbx.Text = mtbx.Text.Insert(i, valor[i].ToString());
                        else valor = valor.Insert(i, ",");
                    }

                    //ajusta valores após a virgula
                    if (digitado[1].Length == 0)
                    {
                        for (int i = 0; i < _DecimaisMoeda; i++)
                        {
                            mtbx.Text = mtbx.Text.Insert(8 + i, "0");
                        }
                    }
                    else
                    {
                        if (ajustaCentavos)
                        {
                            for (int i = 0; i < _DecimaisMoeda; i++)
                            {
                                try { mtbx.Text = mtbx.Text.Insert(8 + i, digitado[1][i].ToString()); }
                                catch (IndexOutOfRangeException) { mtbx.Text = mtbx.Text.Insert(8 + i, "0"); }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < _DecimaisMoeda; i++)
                            {
                                mtbx.Text = mtbx.Text.Insert(8 + i, "0");
                            }
                        }
                    }
                    break;
                case 7:
                    mtbx.Text = mtbx.Text.Insert(1, " ");
                    mtbx.Text = mtbx.Text.Insert(5, " ");
                    mtbx.Mask = "0,000,000." + string.Empty.PadLeft(_DecimaisMoeda, '0');

                    //ajusta valores antes da virgula
                    for (int i = 0; i < 9; i++)
                    {
                        if (i != 1 && i != 5) mtbx.Text = mtbx.Text.Insert(i, valor[i].ToString());
                        else valor = valor.Insert(i, ",");
                    }

                    //ajusta valores após a virgula
                    if (digitado[1].Length == 0)
                    {
                        for (int i = 0; i < _DecimaisMoeda; i++)
                        {
                            mtbx.Text = mtbx.Text.Insert(10 + i, "0");
                        }
                    }
                    else
                    {
                        if (ajustaCentavos)
                        {
                            for (int i = 0; i < _DecimaisMoeda; i++)
                            {
                                try { mtbx.Text = mtbx.Text.Insert(10 + i, digitado[1][i].ToString()); }
                                catch (IndexOutOfRangeException) { mtbx.Text = mtbx.Text.Insert(10 + i, "0"); }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < _DecimaisMoeda; i++)
                            {
                                mtbx.Text = mtbx.Text.Insert(10 + i, "0");
                            }
                        }
                    }
                    break;
                case 8:
                    mtbx.Text = mtbx.Text.Insert(2, " ");
                    mtbx.Text = mtbx.Text.Insert(6, " ");
                    mtbx.Mask = "00,000,000." + string.Empty.PadLeft(_DecimaisMoeda, '0');

                    //ajusta valores antes da virgula
                    for (int i = 0; i < 10; i++)
                    {
                        if (i != 2 && i != 6) mtbx.Text = mtbx.Text.Insert(i, valor[i].ToString());
                        else valor = valor.Insert(i, ",");
                    }

                    //ajusta valores após a virgula
                    if (digitado[1].Length == 0)
                    {
                        for (int i = 0; i < _DecimaisMoeda; i++)
                        {
                            mtbx.Text = mtbx.Text.Insert(11 + i, "0");
                        }
                    }
                    else
                    {
                        if (ajustaCentavos)
                        {
                            for (int i = 0; i < _DecimaisMoeda; i++)
                            {
                                try { mtbx.Text = mtbx.Text.Insert(11 + i, digitado[1][i].ToString()); }
                                catch (IndexOutOfRangeException) { mtbx.Text = mtbx.Text.Insert(11 + i, "0"); }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < _DecimaisMoeda; i++)
                            {
                                mtbx.Text = mtbx.Text.Insert(11 + i, "0");
                            }
                        }
                    }
                    break;
                case 9:
                    mtbx.Text = mtbx.Text.Insert(3, " ");
                    mtbx.Text = mtbx.Text.Insert(7, " ");
                    mtbx.Mask = "000,000,000." + string.Empty.PadLeft(_DecimaisMoeda, '0');

                    //ajusta valores antes da virgula
                    for (int i = 0; i < 11; i++)
                    {
                        if (i != 3 && i != 7) mtbx.Text = mtbx.Text.Insert(i, valor[i].ToString());
                        else valor = valor.Insert(i, ",");
                    }

                    //ajusta valores após a virgula
                    if (digitado[1].Length == 0)
                    {
                        for (int i = 0; i < _DecimaisMoeda; i++)
                        {
                            mtbx.Text = mtbx.Text.Insert(12 + i, "0");
                        }
                    }
                    else
                    {
                        if (ajustaCentavos)
                        {
                            for (int i = 0; i < _DecimaisMoeda; i++)
                            {
                                try { mtbx.Text = mtbx.Text.Insert(12 + i, digitado[1][i].ToString()); }
                                catch (IndexOutOfRangeException) { mtbx.Text = mtbx.Text.Insert(12 + i, "0"); }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < _DecimaisMoeda; i++)
                            {
                                mtbx.Text = mtbx.Text.Insert(12 + i, "0");
                            }
                        }
                    }
                    break;
                case 10:
                    mtbx.Text = mtbx.Text.Insert(1, " ");
                    mtbx.Text = mtbx.Text.Insert(5, " ");
                    mtbx.Text = mtbx.Text.Insert(9, " ");
                    mtbx.Mask = "0,000,000,000." + string.Empty.PadLeft(_DecimaisMoeda, '0');

                    //ajusta valores antes da virgula
                    for (int i = 0; i < 13; i++)
                    {
                        if (i != 1 && i != 5 && i != 9) mtbx.Text = mtbx.Text.Insert(i, valor[i].ToString());
                        else valor = valor.Insert(i, ",");
                    }

                    //ajusta valores após a virgula
                    if (digitado[1].Length == 0)
                    {
                        for (int i = 0; i < _DecimaisMoeda; i++)
                        {
                            mtbx.Text = mtbx.Text.Insert(14 + i, "0");
                        }
                    }
                    else
                    {
                        if (ajustaCentavos)
                        {
                            for (int i = 0; i < _DecimaisMoeda; i++)
                            {
                                try { mtbx.Text = mtbx.Text.Insert(14 + i, digitado[1][i].ToString()); }
                                catch (IndexOutOfRangeException) { mtbx.Text = mtbx.Text.Insert(14 + i, "0"); }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < _DecimaisMoeda; i++)
                            {
                                mtbx.Text = mtbx.Text.Insert(14 + i, "0");
                            }
                        }
                    }
                    break;
                case 11:
                    mtbx.Text = mtbx.Text.Insert(2, " ");
                    mtbx.Text = mtbx.Text.Insert(6, " ");
                    mtbx.Text = mtbx.Text.Insert(10, " ");
                    mtbx.Mask = "00,000,000,000." + string.Empty.PadLeft(_DecimaisMoeda, '0');

                    //ajusta valores antes da virgula
                    for (int i = 0; i < 14; i++)
                    {
                        if (i != 2 && i != 6 && i != 10) mtbx.Text = mtbx.Text.Insert(i, valor[i].ToString());
                        else valor = valor.Insert(i, ",");
                    }

                    //ajusta valores após a virgula
                    if (digitado[1].Length == 0)
                    {
                        for (int i = 0; i < _DecimaisMoeda; i++)
                        {
                            mtbx.Text = mtbx.Text.Insert(15 + i, "0");
                        }
                    }
                    else
                    {
                        if (ajustaCentavos)
                        {
                            for (int i = 0; i < _DecimaisMoeda; i++)
                            {
                                try { mtbx.Text = mtbx.Text.Insert(15 + i, digitado[1][i].ToString()); }
                                catch (IndexOutOfRangeException) { mtbx.Text = mtbx.Text.Insert(15 + i, "0"); }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < _DecimaisMoeda; i++)
                            {
                                mtbx.Text = mtbx.Text.Insert(15 + i, "0");
                            }
                        }
                    }
                    break;
                case 12:
                    mtbx.Text = mtbx.Text.Insert(3, " ");
                    mtbx.Text = mtbx.Text.Insert(7, " ");
                    mtbx.Text = mtbx.Text.Insert(11, " ");
                    mtbx.Mask = "000,000,000,000." + string.Empty.PadLeft(_DecimaisMoeda, '0');

                    //ajusta valores antes da virgula
                    for (int i = 0; i < 15; i++)
                    {
                        if (i != 3 && i != 7 && i != 11) mtbx.Text = mtbx.Text.Insert(i, valor[i].ToString());
                        else valor = valor.Insert(i, ",");
                    }

                    //ajusta valores após a virgula
                    if (digitado[1].Length == 0)
                    {
                        for (int i = 0; i < _DecimaisMoeda; i++)
                        {
                            mtbx.Text = mtbx.Text.Insert(16 + i, "0");
                        }
                    }
                    else
                    {
                        if (ajustaCentavos)
                        {
                            for (int i = 0; i < _DecimaisMoeda; i++)
                            {
                                try { mtbx.Text = mtbx.Text.Insert(16 + i, digitado[1][i].ToString()); }
                                catch (IndexOutOfRangeException) { mtbx.Text = mtbx.Text.Insert(16 + i, "0"); }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < _DecimaisMoeda; i++)
                            {
                                mtbx.Text = mtbx.Text.Insert(16 + i, "0");
                            }
                        }
                    }
                    break;
                case 13:
                    mtbx.Text = mtbx.Text.Insert(1, " ");
                    mtbx.Text = mtbx.Text.Insert(5, " ");
                    mtbx.Text = mtbx.Text.Insert(9, " ");
                    mtbx.Text = mtbx.Text.Insert(13, " ");
                    mtbx.Mask = "0,000,000,000,000." + string.Empty.PadLeft(_DecimaisMoeda, '0');

                    //ajusta valores antes da virgula
                    for (int i = 0; i < 17; i++)
                    {
                        if (i != 1 && i != 5 && i != 9 && i != 13) mtbx.Text = mtbx.Text.Insert(i, valor[i].ToString());
                        else valor = valor.Insert(i, ",");
                    }

                    //ajusta valores após a virgula
                    if (digitado[1].Length == 0)
                    {
                        for (int i = 0; i < _DecimaisMoeda; i++)
                        {
                            mtbx.Text = mtbx.Text.Insert(18 + i, "0");
                        }
                    }
                    else
                    {
                        if (ajustaCentavos)
                        {
                            for (int i = 0; i < _DecimaisMoeda; i++)
                            {
                                try { mtbx.Text = mtbx.Text.Insert(18 + i, digitado[1][i].ToString()); }
                                catch (IndexOutOfRangeException) { mtbx.Text = mtbx.Text.Insert(18 + i, "0"); }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < _DecimaisMoeda; i++)
                            {
                                mtbx.Text = mtbx.Text.Insert(18 + i, "0");
                            }
                        }
                    }
                    break;
                case 14:
                    mtbx.Text = mtbx.Text.Insert(2, " ");
                    mtbx.Text = mtbx.Text.Insert(6, " ");
                    mtbx.Text = mtbx.Text.Insert(10, " ");
                    mtbx.Text = mtbx.Text.Insert(14, " ");
                    mtbx.Mask = "00,000,000,000,000." + string.Empty.PadLeft(_DecimaisMoeda, '0');

                    //ajusta valores antes da virgula
                    for (int i = 0; i < 18; i++)
                    {
                        if (i != 2 && i != 6 && i != 10 && i != 14) mtbx.Text = mtbx.Text.Insert(i, valor[i].ToString());
                        else valor = valor.Insert(i, ",");
                    }

                    //ajusta valores após a virgula
                    if (digitado[1].Length == 0)
                    {
                        for (int i = 0; i < _DecimaisMoeda; i++)
                        {
                            mtbx.Text = mtbx.Text.Insert(19 + i, "0");
                        }
                    }
                    else
                    {
                        if (ajustaCentavos)
                        {
                            for (int i = 0; i < _DecimaisMoeda; i++)
                            {
                                try { mtbx.Text = mtbx.Text.Insert(19 + i, digitado[1][i].ToString()); }
                                catch (IndexOutOfRangeException) { mtbx.Text = mtbx.Text.Insert(19 + i, "0"); }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < _DecimaisMoeda; i++)
                            {
                                mtbx.Text = mtbx.Text.Insert(19 + i, "0");
                            }
                        }
                    }
                    break;
                case 15:
                    mtbx.Text = mtbx.Text.Insert(3, " ");
                    mtbx.Text = mtbx.Text.Insert(7, " ");
                    mtbx.Text = mtbx.Text.Insert(11, " ");
                    mtbx.Text = mtbx.Text.Insert(15, " ");
                    mtbx.Mask = "000,000,000,000,000." + string.Empty.PadLeft(_DecimaisMoeda, '0');

                    //ajusta valores antes da virgula
                    for (int i = 0; i < 19; i++)
                    {
                        if (i != 3 && i != 7 && i != 11 && i != 15) mtbx.Text = mtbx.Text.Insert(i, valor[i].ToString());
                        else valor = valor.Insert(i, ",");
                    }

                    //ajusta valores após a virgula
                    if (digitado[1].Length == 0)
                    {
                        for (int i = 0; i < _DecimaisMoeda; i++)
                        {
                            mtbx.Text = mtbx.Text.Insert(20 + i, "0");
                        }
                    }
                    else
                    {
                        if (ajustaCentavos)
                        {
                            for (int i = 0; i < _DecimaisMoeda; i++)
                            {
                                try { mtbx.Text = mtbx.Text.Insert(20 + i, digitado[1][i].ToString()); }
                                catch (IndexOutOfRangeException) { mtbx.Text = mtbx.Text.Insert(20 + i, "0"); }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < _DecimaisMoeda; i++)
                            {
                                mtbx.Text = mtbx.Text.Insert(20 + i, "0");
                            }
                        }
                    }
                    break;
            }
        }

        #endregion

        #region ZeroFill

        /// <summary>
        /// Preenche com zero o campo do tipo moeda_porcentagem caso for vazio.
        /// </summary>
        public void ZeroFill()
        {
            texto = this.Text;

            base.Mask = string.Empty;
            base.Mask = base.Mask.PadLeft(_TamanhoMoeda, '0') + '.' + base.Mask.PadRight(_DecimaisMoeda, '0');
            //arruma valores antes e após a vírgula
            base.Text = string.Empty;
            base.Text = base.Text.Insert(_TamanhoMoeda - 1, "0").Insert(_TamanhoMoeda, string.Empty.PadRight(_DecimaisMoeda, '0'));
            labelMenos.Visible = false;
        }

        #endregion

        #endregion

        #region Métodos Ref. Data

        /// <summary>
        /// Abrir form calendário.
        /// </summary>
        public void OpenCalendario()
        {
            MonthCalendario monthCalendario = new MonthCalendario();
            monthCalendario.BackColor = Color.White;
            monthCalendario.MinimumSize = new Size(233, 167);
            monthCalendario.Size = MinimumSize;
            monthCalendario.Location = new Point(5, 5);
            monthCalendario.KeyDown += monthCalendario_KeyDown;
            monthCalendario.DoubleClick += monthCalendario_DoubleClick;

            if (this.Text.DeFormat() != string.Empty && this.Text.ValidarData() == true)
            {
                monthCalendario.CurrentDate = Convert.ToDateTime(this.Text);
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

        #region Método Override

        /// <summary>
        /// Sobrescreve eventos de teclas padrões do windows e framework.
        /// </summary>
        /// <param name="msg">Mensagem referenciada.</param>
        /// <param name="keyData">Tecla pressionada.</param>
        /// <returns>Retorno do evento sobrescrito.</returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (ReadOnly && (keyData == Keys.Back || keyData == Keys.Delete))
                return false;

            switch (keyData)
            {
                case Keys.Control | Keys.Z:
                    #region Control Z

                    if (Mascara == MascaraEnum.NORMAL)
                    {
                        //Quando não existe valor nas variáveis e no controle altera o enumerador
                        if (ctrl_z_ativo == Ctrl_z_ativo.indice_1 && ctrl_z[0].Length == 0 && ctrl_z[1].Length == 0 && this.Text.DeFormat().Length == 0)
                        {
                            ctrl_z_ativo = Ctrl_z_ativo.indice_0;
                        }

                        //Faz tratamento da variável (ctrl_z/ctrl_z_aux) quando digita algum valor, o valor que estava antes é mostrado novamente
                        //Se o valor digitado for diferente do valor que estava antes
                        if (ctrl_z[0].ToLower() != this.Text.ToLower() && ctrl_z[1].ToLower() != this.Text.ToLower() && ctrl_z[0].Length > 0 && ctrl_z_ativo == Ctrl_z_ativo.indice_1 ||
                            ctrl_z[0].ToLower() != this.Text.ToLower() && ctrl_z[1].ToLower() != this.Text.ToLower() && ctrl_z[1].Length > 0 && ctrl_z_ativo == Ctrl_z_ativo.indice_0)
                        {
                            if (ctrl_z_ativo == Ctrl_z_ativo.indice_1 && this.Text.ToLower() != ctrl_z[0].ToLower()) ctrl_z_ativo = Ctrl_z_ativo.indice_0;
                            else if (ctrl_z_ativo == Ctrl_z_ativo.indice_0 && this.Text.ToLower() != ctrl_z[1].ToLower()) ctrl_z_ativo = Ctrl_z_ativo.indice_1;
                        }
                        else if (mtbxDigitado.Text.Length > 0 && ctrl_z_ativo == Ctrl_z_ativo.indice_0 && mtbxDigitado.Text.ToLower() == ctrl_z[1].ToLower() ||
                                 mtbxDigitado.Text.Length > 0 && ctrl_z_ativo == Ctrl_z_ativo.indice_1 && mtbxDigitado.Text.ToLower() == ctrl_z[0].ToLower())
                        {
                            //Quando o valor digitado é igual o valor que estava antes
                            if (ctrl_z_ativo == Ctrl_z_ativo.indice_1) ctrl_z_ativo = Ctrl_z_ativo.indice_0;
                            else if (ctrl_z_ativo == Ctrl_z_ativo.indice_0) ctrl_z_ativo = Ctrl_z_ativo.indice_1;
                        }
                        //Quando o valor digitado e valor do controle são vazios e existe apenas um valor no array ctrl_z
                        else if (this.Text.DeFormat().Length == 0 && mtbxDigitado.Text.Length == 0 && ctrl_z_ativo == Ctrl_z_ativo.indice_0 && ctrl_z[0].Length == 0 && ctrl_z[1].Length > 0) ctrl_z_ativo = Ctrl_z_ativo.indice_1;
                        else if (this.Text.DeFormat().Length == 0 && mtbxDigitado.Text.Length == 0 && ctrl_z_ativo == Ctrl_z_ativo.indice_1 && ctrl_z[1].Length == 0 && ctrl_z[0].Length > 0) ctrl_z_ativo = Ctrl_z_ativo.indice_0;
                    }
                    else
                    {
                        //Quando não existe valor nas variáveis e no controle altera o enumerador
                        if (ctrl_z_ativo == Ctrl_z_ativo.indice_1 && ctrl_z[0].DeFormat().Length == 0 && ctrl_z[1].DeFormat().Length == 0 && this.Text.DeFormat().Length == 0)
                        {
                            ctrl_z_ativo = Ctrl_z_ativo.indice_0;
                        }

                        //Faz tratamento da variável (ctrl_z/ctrl_z_aux) quando digita algum valor, o valor que estava antes é mostrado novamente
                        //Se o valor digitado for diferente do valor que estava antes
                        if (ctrl_z[0].DeFormat() != this.Text.DeFormat() && ctrl_z[1].DeFormat() != this.Text.DeFormat() && ctrl_z[0].DeFormat().Length > 0 && ctrl_z_ativo == Ctrl_z_ativo.indice_1 ||
                            ctrl_z[0].DeFormat() != this.Text.DeFormat() && ctrl_z[1].DeFormat() != this.Text.DeFormat() && ctrl_z[1].DeFormat().Length > 0 && ctrl_z_ativo == Ctrl_z_ativo.indice_0)
                        {
                            if (ctrl_z_ativo == Ctrl_z_ativo.indice_1 && this.Text.DeFormat() != ctrl_z[0].DeFormat()) ctrl_z_ativo = Ctrl_z_ativo.indice_0;
                            else if (ctrl_z_ativo == Ctrl_z_ativo.indice_0 && this.Text.DeFormat() != ctrl_z[1].DeFormat()) ctrl_z_ativo = Ctrl_z_ativo.indice_1;
                        }
                        else if (mtbxDigitado.Text.DeFormat().Length > 0 && ctrl_z_ativo == Ctrl_z_ativo.indice_0 && mtbxDigitado.Text.DeFormat() == ctrl_z[1].DeFormat() ||
                                 mtbxDigitado.Text.DeFormat().Length > 0 && ctrl_z_ativo == Ctrl_z_ativo.indice_1 && mtbxDigitado.Text.DeFormat() == ctrl_z[0].DeFormat())
                        {
                            //Quando o valor digitado é igual o valor que estava antes
                            if (ctrl_z_ativo == Ctrl_z_ativo.indice_1) ctrl_z_ativo = Ctrl_z_ativo.indice_0;
                            else if (ctrl_z_ativo == Ctrl_z_ativo.indice_0) ctrl_z_ativo = Ctrl_z_ativo.indice_1;
                        }
                        //Quando o valor digitado e valor do controle são vazios e existe apenas um valor no array ctrl_z
                        else if (this.Text.DeFormat().Length == 0 && mtbxDigitado.Text.DeFormat().Length == 0 && ctrl_z_ativo == Ctrl_z_ativo.indice_0 && ctrl_z[0].DeFormat().Length == 0 && ctrl_z[1].DeFormat().Length > 0) ctrl_z_ativo = Ctrl_z_ativo.indice_1;
                        else if (this.Text.DeFormat().Length == 0 && mtbxDigitado.Text.DeFormat().Length == 0 && ctrl_z_ativo == Ctrl_z_ativo.indice_1 && ctrl_z[1].DeFormat().Length == 0 && ctrl_z[0].DeFormat().Length > 0) ctrl_z_ativo = Ctrl_z_ativo.indice_0;
                    }

                    switch (ctrl_z_ativo)
                    {
                        case Ctrl_z_ativo.indice_0:
                            if (Mascara == MascaraEnum.NORMAL) ctrl_z[1] = this.Text;
                            else
                            {
                                if (Mascara == MascaraEnum.MOEDA_PORCENTAGEM) MetodoLeave();
                                else if (Mascara == MascaraEnum.DATA || Mascara == MascaraEnum.MES_ANO) ValidaDataCTRL_Z(this.Text);
                                else if (Mascara == MascaraEnum.HORA_LONG || Mascara == MascaraEnum.HORA_SHORT) ValidaHoraCTRL_Z(this.Text);
                                ctrl_z[1] = this.Text;
                            }
                            this.Clear();
                            if (Mascara == MascaraEnum.MOEDA_PORCENTAGEM)
                            {
                                base.Mask = "";

                                #region Arruma máscara de entrada

                                if (ctrl_z[0].DeFormat().Length == 0)
                                {
                                    limpaCampo = true;
                                    string valor = this.Text.Trim();

                                    base.Text = string.Empty;
                                    base.Mask = string.Empty;
                                    base.Mask = base.Mask.PadLeft(_TamanhoMoeda, '0') + '.' + base.Mask.PadRight(_DecimaisMoeda, '0');

                                    if (valor.DeFormat() != "")
                                    {
                                        String[] arrayValor = valor.Replace(".", "").Replace("-", "").Split(new char[] { ',' });

                                        int qtd = _TamanhoMoeda - arrayValor[0].Trim().Length;
                                        int qtdDecimais = _TamanhoMoeda + 1;
                                        //arruma valores antes da virgula
                                        for (int i = 0; i < arrayValor[0].Trim().Length; i++)
                                        {
                                            base.Text = base.Text.Insert(qtd + i, arrayValor[0][i].ToString());
                                        }
                                        //arruma centavos
                                        for (int i = 0; i < arrayValor[1].Trim().Length; i++)
                                        {
                                            base.Text = base.Text.Insert(qtdDecimais + i, arrayValor[1][i].ToString());
                                        }
                                    }
                                }

                                #endregion
                            }

                            this.Text = ctrl_z[0];

                            if (Mascara == MascaraEnum.MOEDA_PORCENTAGEM)
                            {
                                #region Arruma máscara de entrada

                                limpaCampo = true;
                                string valor = this.Text.Trim();

                                base.Text = string.Empty;
                                base.Mask = string.Empty;
                                base.Mask = base.Mask.PadLeft(_TamanhoMoeda, '0') + '.' + base.Mask.PadRight(_DecimaisMoeda, '0');

                                if (valor.DeFormat() != "")
                                {
                                    String[] arrayValor = valor.Replace(".", "").Replace("-", "").Split(new char[] { ',' });

                                    int qtd = _TamanhoMoeda - arrayValor[0].Trim().Length;
                                    int qtdDecimais = _TamanhoMoeda + 1;
                                    //arruma valores antes da virgula
                                    for (int i = 0; i < arrayValor[0].Trim().Length; i++)
                                    {
                                        base.Text = base.Text.Insert(qtd + i, arrayValor[0][i].ToString());
                                    }
                                    //arruma centavos
                                    for (int i = 0; i < arrayValor[1].Trim().Length; i++)
                                    {
                                        base.Text = base.Text.Insert(qtdDecimais + i, arrayValor[1][i].ToString());
                                    }
                                }

                                #endregion
                            }
                            if (this.Text.DeFormat().Length > 0) this.SelectAll();
                            ctrl_z_ativo = Ctrl_z_ativo.indice_1;
                            mtbxDigitado.Text = string.Empty;
                            mtbxDigitado.Mask = "";
                            break;
                        case Ctrl_z_ativo.indice_1:
                            if (Mascara == MascaraEnum.NORMAL) ctrl_z[0] = this.Text;
                            else
                            {
                                if (Mascara == MascaraEnum.MOEDA_PORCENTAGEM) MetodoLeave();
                                else if (Mascara == MascaraEnum.DATA || Mascara == MascaraEnum.MES_ANO) ValidaDataCTRL_Z(this.Text);
                                else if (Mascara == MascaraEnum.HORA_LONG || Mascara == MascaraEnum.HORA_SHORT) ValidaHoraCTRL_Z(this.Text);
                                ctrl_z[0] = this.Text;
                            }
                            this.Clear();
                            if (Mascara == MascaraEnum.MOEDA_PORCENTAGEM)
                            {
                                base.Mask = "";

                                #region Arruma máscara de entrada

                                if (ctrl_z[1].DeFormat().Length == 0)
                                {
                                    limpaCampo = true;
                                    string valor = this.Text.Trim();

                                    base.Text = string.Empty;
                                    base.Mask = string.Empty;
                                    base.Mask = base.Mask.PadLeft(_TamanhoMoeda, '0') + '.' + base.Mask.PadRight(_DecimaisMoeda, '0');

                                    if (valor.DeFormat() != "")
                                    {
                                        String[] arrayValor = valor.Replace(".", "").Replace("-", "").Split(new char[] { ',' });

                                        int qtd = _TamanhoMoeda - arrayValor[0].Trim().Length;
                                        int qtdDecimais = _TamanhoMoeda + 1;
                                        //arruma valores antes da virgula
                                        for (int i = 0; i < arrayValor[0].Trim().Length; i++)
                                        {
                                            base.Text = base.Text.Insert(qtd + i, arrayValor[0][i].ToString());
                                        }
                                        //arruma centavos
                                        for (int i = 0; i < arrayValor[1].Trim().Length; i++)
                                        {
                                            base.Text = base.Text.Insert(qtdDecimais + i, arrayValor[1][i].ToString());
                                        }
                                    }
                                }

                                #endregion
                            }

                            this.Text = ctrl_z[1];

                            if (Mascara == MascaraEnum.MOEDA_PORCENTAGEM)
                            {
                                #region Arruma máscara de entrada

                                limpaCampo = true;
                                string valor = this.Text.Trim();

                                base.Text = string.Empty;
                                base.Mask = string.Empty;
                                base.Mask = base.Mask.PadLeft(_TamanhoMoeda, '0') + '.' + base.Mask.PadRight(_DecimaisMoeda, '0');

                                if (valor.DeFormat() != "")
                                {
                                    String[] arrayValor = valor.Replace(".", "").Replace("-", "").Split(new char[] { ',' });

                                    int qtd = _TamanhoMoeda - arrayValor[0].Trim().Length;
                                    int qtdDecimais = _TamanhoMoeda + 1;
                                    //arruma valores antes da virgula
                                    for (int i = 0; i < arrayValor[0].Trim().Length; i++)
                                    {
                                        base.Text = base.Text.Insert(qtd + i, arrayValor[0][i].ToString());
                                    }
                                    //arruma centavos
                                    for (int i = 0; i < arrayValor[1].Trim().Length; i++)
                                    {
                                        base.Text = base.Text.Insert(qtdDecimais + i, arrayValor[1][i].ToString());
                                    }
                                }

                                #endregion
                            }

                            if (this.Text.DeFormat().Length > 0) this.SelectAll();
                            ctrl_z_ativo = Ctrl_z_ativo.indice_0;
                            mtbxDigitado.Text = string.Empty;
                            mtbxDigitado.Mask = "";
                            break;
                    }

                    #endregion
                    break;                
                case Keys.Insert:
                    #region Controla tecla insert

                    StatusInsert();

                    #endregion
                    return true;
                case Keys.Control | Keys.Back:
                    //Caracter especial.
                    return true;
            }

            //Copia valor selecionado antes de efetuar backspace/delete
            if (keyData == (Keys.Control | Keys.X)) this.Copy();

            #region Backspace/Delete (Data/Hora_Long)/Ctrl X

            if (Mascara == MascaraEnum.DATA || Mascara == MascaraEnum.HORA_LONG)
            {
                if (keyData == Keys.Back || keyData == Keys.Delete || keyData == (Keys.Control | Keys.X) || keyData == (Keys.Shift | Keys.Delete) || keyData == (Keys.Shift | Keys.Back) || (keyData == (Keys.Control | Keys.Delete) && this.SelectionLength > 0))
                {
                    if (this.SelectionLength == 0)
                    {
                        int posicao = 0;
                        if (keyData == Keys.Back || keyData == (Keys.Shift | Keys.Delete) || keyData == (Keys.Shift | Keys.Back)) posicao = this.SelectionStart - 1;
                        else if (keyData == Keys.Delete || keyData == (Keys.Control | Keys.X)) posicao = this.SelectionStart;

                        if (this.SelectionStart <= 5 && posicao >= 0)
                        {
                            string valor = null;
                            if (posicao != 2 && posicao != 5)
                            {
                                valor = this.Text.Remove(posicao, 1);
                                valor = valor.Insert(posicao + 1, " ");
                                this.Text = "";
                                this.Text = valor;
                                this.SelectionStart = posicao;
                                return true;
                            }
                            else return false;
                        }
                    }
                    else
                    {
                        int selecao = this.SelectionLength;
                        int posicaoInicial = this.SelectionStart;
                        int posicaoFinal = posicaoInicial + selecao - 1;
                        string valorFinal = string.Empty;
                        string valorInicial = this.Text;

                        for (int i = 0; i < valorInicial.Length; i++)
                        {
                            if (i >= posicaoInicial && i <= posicaoFinal)
                            {
                                if (Mascara == MascaraEnum.DATA)
                                {
                                    if (valorInicial[i].ToString() != "/") valorFinal = valorFinal.Insert(i, " ");
                                    else valorFinal = valorFinal.Insert(i, "/");
                                }
                                else
                                {
                                    if (valorInicial[i].ToString() != ":") valorFinal = valorFinal.Insert(i, " ");
                                    else valorFinal = valorFinal.Insert(i, ":");
                                }
                            }
                            else
                            {
                                valorFinal = valorFinal.Insert(i, valorInicial[i].ToString());
                            }
                        }

                        if (posicaoInicial > 5 || valorFinal.DeFormat().Trim().Length == 0 || posicaoFinal > this.Text.Length - 1) return false;

                        if (posicaoFinal + 1 == 1 || posicaoFinal + 1 == 4 || posicaoFinal + 1 > 6)
                        {
                            int t = this.Text.Length - 1;
                            string numero = valorFinal.Substring(posicaoFinal + 1, t - posicaoFinal);
                            valorFinal = valorFinal.Remove(posicaoFinal, this.Text.Length - posicaoFinal);
                            if (posicaoFinal > 5) valorFinal = valorFinal.Insert(6, numero);
                            else valorFinal = valorFinal.Insert(posicaoFinal, numero);

                            if (valorFinal.Length < this.Text.Length)
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
                                    else if (Mascara == MascaraEnum.DATA && i == 2 && conteudo[2].Length > 0 && conteudo[2].Length != 4)
                                    {
                                        string e = string.Empty;
                                        e = e.PadLeft(4 - conteudo[2].Length, ' ');
                                        valorFinal = valorFinal.Insert(6 + conteudo[2].Length, e);
                                    }
                                    else if (Mascara == MascaraEnum.HORA_LONG && i == 2 && conteudo[2].Length > 0 && conteudo[2].Length != 2)
                                    {
                                        string e = string.Empty;
                                        e = e.PadLeft(2 - conteudo[2].Length, ' ');
                                        valorFinal = valorFinal.Insert(6 + conteudo[2].Length, e);
                                    }
                                }
                            }
                        }

                        this.Text = "";
                        this.Text = valorFinal;
                        this.SelectionStart = posicaoInicial;
                        return true;
                    }
                }
            }

            #endregion

            #region Backspace/Delete Hora_Short/Ctrl X

            if (Mascara == MascaraEnum.HORA_SHORT)
            {
                if (keyData == Keys.Back || keyData == Keys.Delete || keyData == (Keys.Control | Keys.X) || keyData == (Keys.Shift | Keys.Delete) || keyData == (Keys.Shift | Keys.Back) || (keyData == (Keys.Control | Keys.Delete) && this.SelectionLength > 0))
                {
                    if (this.SelectionLength == 0)
                    {
                        int posicao = 0;
                        if (keyData == Keys.Back || keyData == (Keys.Shift | Keys.Delete) || keyData == (Keys.Shift | Keys.Back)) posicao = this.SelectionStart - 1;
                        else if (keyData == Keys.Delete || keyData == (Keys.Control | Keys.X)) posicao = this.SelectionStart;

                        if (this.SelectionStart <= 2 && posicao >= 0)
                        {
                            string valor = null;
                            if (posicao != 2)
                            {
                                valor = this.Text.Remove(posicao, 1);
                                valor = valor.Insert(posicao + 1, " ");
                                this.Text = "";
                                this.Text = valor;
                                this.SelectionStart = posicao;
                                return true;
                            }
                            else return false;
                        }
                    }
                    else
                    {
                        int selecao = this.SelectionLength;
                        int posicaoInicial = this.SelectionStart;
                        int posicaoFinal = posicaoInicial + selecao - 1;
                        string valorFinal = string.Empty;
                        string valorInicial = this.Text;

                        for (int i = 0; i < valorInicial.Length; i++)
                        {
                            if (i >= posicaoInicial && i <= posicaoFinal)
                            {
                                if (valorInicial[i].ToString() != ":") valorFinal = valorFinal.Insert(i, " ");
                                else valorFinal = valorFinal.Insert(i, ":");
                            }
                            else
                            {
                                valorFinal = valorFinal.Insert(i, valorInicial[i].ToString());
                            }
                        }

                        if (posicaoInicial > 2 || valorFinal.DeFormat().Trim().Length == 0 || posicaoFinal > this.Text.Length - 1) return false;

                        if (posicaoFinal + 1 == 1 || posicaoFinal + 1 > 3)
                        {
                            int t = this.Text.Length - 1;
                            string numero = valorFinal.Substring(posicaoFinal + 1, t - posicaoFinal);
                            valorFinal = valorFinal.Remove(posicaoFinal, this.Text.Length - posicaoFinal);
                            if (posicaoFinal > 2) valorFinal = valorFinal.Insert(3, numero);
                            else valorFinal = valorFinal.Insert(posicaoFinal, numero);

                            if (valorFinal.Length < this.Text.Length)
                            {
                                string[] conteudo = valorFinal.Split(':');
                                for (int i = 0; i < conteudo.Length; i++)
                                {
                                    if (i == 0 && conteudo[i].Length == 1)
                                    {
                                        valorFinal = valorFinal.Insert(1, " ");
                                    }
                                    else if (i == 1 && conteudo[1].Length > 0 && conteudo[1].Length != 2)
                                    {
                                        string e = string.Empty;
                                        e = e.PadLeft(2 - conteudo[1].Length, ' ');
                                        valorFinal = valorFinal.Insert(3 + conteudo[1].Length, e);
                                    }
                                }
                            }
                        }

                        this.Text = "";
                        this.Text = valorFinal;
                        this.SelectionStart = posicaoInicial;
                        return true;
                    }
                }
            }

            #endregion

            #region Backspace/Delete Mês/Ano/Ctrl X

            if (Mascara == MascaraEnum.MES_ANO)
            {
                if (keyData == Keys.Back || keyData == Keys.Delete || keyData == (Keys.Control | Keys.X) || keyData == (Keys.Shift | Keys.Delete) || keyData == (Keys.Shift | Keys.Back) || (keyData == (Keys.Control | Keys.Delete) && this.SelectionLength > 0))
                {
                    if (this.SelectionLength == 0)
                    {
                        int posicao = 0;
                        if (keyData == Keys.Back || keyData == (Keys.Shift | Keys.Delete) || keyData == (Keys.Shift | Keys.Back)) posicao = this.SelectionStart - 1;
                        else if (keyData == Keys.Delete || keyData == (Keys.Control | Keys.X)) posicao = this.SelectionStart;

                        if (this.SelectionStart <= 2 && posicao >= 0)
                        {
                            string valor = null;
                            if (posicao != 2)
                            {
                                valor = this.Text.Remove(posicao, 1);
                                valor = valor.Insert(posicao + 1, " ");
                                this.Text = "";
                                this.Text = valor;
                                this.SelectionStart = posicao;
                                return true;
                            }
                            else return false;
                        }
                    }
                    else
                    {
                        int selecao = this.SelectionLength;
                        int posicaoInicial = this.SelectionStart;
                        int posicaoFinal = posicaoInicial + selecao - 1;
                        string valorFinal = string.Empty;
                        string valorInicial = this.Text;

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

                        if (posicaoInicial > 2 || valorFinal.DeFormat().Trim().Length == 0 || posicaoFinal > this.Text.Length - 1) return false;

                        if (posicaoFinal + 1 == 1 || posicaoFinal + 1 > 3)
                        {
                            int t = this.Text.Length - 1;
                            string numero = valorFinal.Substring(posicaoFinal + 1, t - posicaoFinal);
                            valorFinal = valorFinal.Remove(posicaoFinal, this.Text.Length - posicaoFinal);
                            if (posicaoFinal > 2) valorFinal = valorFinal.Insert(3, numero);
                            else valorFinal = valorFinal.Insert(posicaoFinal, numero);


                            if (valorFinal.Length < this.Text.Length)
                            {
                                string[] conteudo = valorFinal.Split('/');
                                for (int i = 0; i < conteudo.Length; i++)
                                {
                                    if (i == 0 && conteudo[i].Length == 1)
                                    {
                                        valorFinal = valorFinal.Insert(1, " ");
                                    }
                                    else if (i == 1 && conteudo[1].Length > 0 && conteudo[1].Length != 4)
                                    {
                                        string e = string.Empty;
                                        e = e.PadLeft(4 - conteudo[1].Length, ' ');
                                        valorFinal = valorFinal.Insert(3 + conteudo[1].Length, e);
                                    }
                                }
                            }
                        }

                        this.Text = "";
                        this.Text = valorFinal;
                        this.SelectionStart = posicaoInicial;
                        return true;
                    }
                }
            }

            #endregion

            #region Backspace/Delete Moeda_Porcentagem/Ctrl X

            //Quando tecla backapace ou delete é pressionada e campo moeda focado
            //não deixa os centavos pular de posicao
            if (base.Focused.Equals(true) && Mascara == MascaraEnum.MOEDA_PORCENTAGEM)
            {
                if (keyData == Keys.Back || keyData == Keys.Delete || keyData == (Keys.Control | Keys.X) || keyData == (Keys.Shift | Keys.Delete) || keyData == (Keys.Shift | Keys.Back) || (keyData == (Keys.Control | Keys.Delete) && this.SelectionLength > 0))
                {
                    if (base.SelectionLength == 0)
                    {
                        int posicao = 0;
                        if (keyData == Keys.Back || keyData == (Keys.Shift | Keys.Delete) || keyData == (Keys.Shift | Keys.Back)) posicao = base.SelectionStart - 1;
                        else if (keyData == Keys.Delete || keyData == (Keys.Control | Keys.X)) posicao = base.SelectionStart;

                        if (base.Mask != string.Empty.PadLeft(_TamanhoMoeda, '0') + '.' + string.Empty.PadLeft(_DecimaisMoeda, '0')) return false;

                        if ((keyData == Keys.Back || keyData == (Keys.Shift | Keys.Delete) || keyData == (Keys.Shift | Keys.Back)) && base.SelectionStart <= _TamanhoMoeda && posicao >= 0 || ((keyData == Keys.Delete || keyData == (Keys.Control | Keys.X)) && base.SelectionStart <= _TamanhoMoeda - 1 && posicao >= 0))
                        {
                            string valor = base.Text.Remove(posicao, 1);
                            valor = valor.Trim();
                            valor = valor.Insert(valor.IndexOf(','), " ");
                            int indice = base.Text.Length - valor.Length;
                            base.Text = "";

                            string[] arrayValor = valor.Split(',');
                            if (arrayValor[0].Length < TamanhoMoeda) arrayValor[0] = string.Empty.PadLeft(TamanhoMoeda - valor.Split(',')[0].Length) + arrayValor[0];
                            if (arrayValor[1].Length < DecimaisMoeda) arrayValor[1] = arrayValor[1] + string.Empty.PadRight(DecimaisMoeda - valor.Split(',')[1].Length);

                            base.Text = arrayValor[0] + "," + arrayValor[1];
                            base.SelectionStart = posicao;
                            return true;
                        }
                        else return false;
                    }
                    else
                    {
                        int posicao = 0;
                        if (keyData == Keys.Back || keyData == (Keys.Shift | Keys.Delete) || keyData == (Keys.Shift | Keys.Back)) posicao = base.SelectionStart;
                        else if (keyData == Keys.Delete || keyData == (Keys.Control | Keys.X) || keyData == (Keys.Control | Keys.Delete)) posicao = base.SelectionStart;

                        if (base.Mask != string.Empty.PadLeft(_TamanhoMoeda, '0') + '.' + string.Empty.PadLeft(_DecimaisMoeda, '0')) return false;

                        if ((keyData == Keys.Back || keyData == (Keys.Shift | Keys.Delete) || keyData == (Keys.Shift | Keys.Back)) && base.SelectionStart <= _TamanhoMoeda && posicao >= 0 || ((keyData == Keys.Delete || keyData == (Keys.Control | Keys.X) || keyData == (Keys.Control | Keys.Delete)) && base.SelectionStart <= _TamanhoMoeda - 1 && posicao >= 0))
                        {
                            string valor = string.Empty;
                            //Seleção está antes da virgula
                            if (posicao + base.SelectionLength <= TamanhoMoeda)
                            {
                                valor = base.Text.Remove(posicao, base.SelectionLength);

                                string espaco = string.Empty;
                                espaco = espaco.PadLeft(base.SelectionLength, ' ');
                                valor = valor.Insert(valor.IndexOf(','), espaco);
                                int indice = base.Text.Length - valor.Length;
                                base.Text = "";

                                string[] arrayValor = valor.Split(',');
                                if (arrayValor[0].Length < TamanhoMoeda) arrayValor[0] = string.Empty.PadLeft(TamanhoMoeda - valor.Split(',')[0].Length) + arrayValor[0];
                                if (arrayValor[1].Length < DecimaisMoeda) arrayValor[1] = arrayValor[1] + string.Empty.PadRight(DecimaisMoeda - valor.Split(',')[1].Length);

                                base.Text = arrayValor[0] + "," + arrayValor[1];
                                base.SelectionStart = posicao;
                            }
                            else
                            {
                                //Seleção está após a virgula
                                for (int i = 0; i < base.Text.Length; i++)
                                {
                                    if (base.Text[i].ToString() != ",")
                                    {
                                        if (i < posicao || i >= posicao + base.SelectionLength)
                                        {
                                            valor = valor + base.Text[i].ToString();
                                        }
                                    }
                                    else
                                    {
                                        valor = valor.Insert(valor.Length, ",");
                                    }
                                }

                                string espaco = string.Empty;
                                espaco = espaco.PadLeft(TamanhoMoeda - posicao, ' ');
                                valor = valor.Insert(valor.IndexOf(','), espaco);
                                int indice = base.Text.Length - valor.Length - 1;
                                base.Text = "";
                                for (int i = 0; i < valor.Length; i++)
                                {
                                    if (valor[i].ToString() == " ")
                                    {
                                        base.Text = base.Text.Insert(i, " ");
                                    }
                                    else
                                    {
                                        base.Text = base.Text.Insert(i, valor[i].ToString());
                                    }
                                }

                                base.SelectionStart = posicao;
                            }
                            return true;
                        }
                        else return false;
                    }
                }
            }

            #endregion

            #region Backspace/Delete Data_Hora/Ctrl X

            if (Mascara == MascaraEnum.DATA_HORA)
            {
                if (keyData == Keys.Back || keyData == Keys.Delete || keyData == (Keys.Control | Keys.X) || keyData == (Keys.Shift | Keys.Delete) || keyData == (Keys.Shift | Keys.Back) || (keyData == (Keys.Control | Keys.Delete) && this.SelectionLength > 0))
                {
                    if (this.SelectionLength == 0)
                    {
                        #region Não selecionado

                        int posicao = 0;
                        if (keyData == Keys.Back || keyData == (Keys.Shift | Keys.Delete) || keyData == (Keys.Shift | Keys.Back)) posicao = this.SelectionStart - 1;
                        else if (keyData == Keys.Delete || keyData == (Keys.Control | Keys.X)) posicao = this.SelectionStart;

                        if (this.SelectionStart <= 16 && posicao >= 0)
                        {
                            string valor = null;
                            if (posicao != 2 && posicao != 5 && posicao != 10 && posicao != 13 && posicao != 16)
                            {
                                if (posicao >= 6 && posicao <= 10)
                                {
                                    #region Ano

                                    valor = this.Text.Remove(posicao, 1);
                                    int count = 10 - (posicao + 1);
                                    valor = valor.Insert(posicao + count, " ");

                                    #endregion
                                }
                                else
                                {
                                    #region Outros

                                    valor = this.Text.Remove(posicao, 1);
                                    valor = valor.Insert(posicao + 1, " ");

                                    #endregion
                                }

                                this.Text = "";
                                this.Text = valor;
                                this.SelectionStart = posicao;
                                return true;
                            }
                            else return false;
                        }

                        #endregion
                    }
                    else
                    {
                        #region Selecionado

                        int selecao = this.SelectionLength;
                        int posicaoInicial = this.SelectionStart;
                        int posicaoFinal = posicaoInicial + selecao - 1;
                        string valorFinal = string.Empty;
                        string valorInicial = this.Text.Remove(10, 1).Insert(10, "E");

                        for (int i = 0; i < valorInicial.Length; i++)
                        {
                            if (i >= posicaoInicial && i <= posicaoFinal)
                            {
                                if (valorInicial[i].ToString() == "/")
                                {
                                    valorFinal = valorFinal.Insert(i, "/");
                                }
                                else if (valorInicial[i].ToString() == ":")
                                {
                                    valorFinal = valorFinal.Insert(i, ":");
                                }
                                else if (valorInicial[i].ToString() == "E")
                                {
                                    valorFinal = valorFinal.Insert(i, "E");
                                }
                                else
                                {
                                    valorFinal = valorFinal.Insert(i, " ");
                                }
                            }
                            else
                            {
                                valorFinal = valorFinal.Insert(i, valorInicial[i].ToString());
                            }
                        }

                        if (posicaoInicial > 16 || valorFinal.DeFormat().Trim().Length == 0 || posicaoFinal > this.Text.Length - 1) return false;

                        if (posicaoFinal + 1 == 1 || posicaoFinal + 1 == 4 || posicaoFinal + 1 > 6)
                        {
                            int t = this.Text.Length - 1;
                            string numero = valorFinal.Substring(posicaoFinal + 1, t - posicaoFinal);
                            valorFinal = valorFinal.Remove(posicaoFinal, this.Text.Length - posicaoFinal);

                            if (posicaoFinal > 16)
                            {
                                valorFinal = valorFinal.Insert(17, numero);
                            }
                            else
                            {
                                valorFinal = valorFinal.Insert(posicaoFinal, posicaoFinal == 10 ? $"E{numero}" : numero);
                            }

                            if (valorFinal.Length < this.Text.Length)
                            {
                                #region Dia

                                if (valorFinal.Split('/')[0].Length == 1)
                                {
                                    valorFinal = valorFinal.Insert(1, " ");
                                }

                                #endregion

                                #region Mês

                                if (valorFinal.Split('/')[1].Length == 1)
                                {
                                    valorFinal = valorFinal.Insert(4, " ");
                                }

                                #endregion

                                #region Ano

                                string valorAno = valorFinal.Split('/')[2].Split('E')[0];

                                if (string.IsNullOrEmpty(valorAno) == false && valorAno.Trim().Length != 4)
                                {
                                    int qtd = valorAno.Length;
                                    valorAno = valorAno.Replace(" ", "");
                                    valorAno = valorAno.PadRight(4, ' ');
                                    valorFinal = valorFinal.Remove(6, qtd).Insert(6, valorAno);
                                }

                                #endregion

                                #region Hora

                                string valorHora = valorFinal.Split(':')[0].Split('E')[1];

                                if (valorHora.Length == 1)
                                {
                                    valorFinal = valorFinal.Insert(12, " ");
                                }

                                #endregion

                                #region Minutos

                                if (valorFinal.Split(':')[1].Length == 1)
                                {
                                    valorFinal = valorFinal.Insert(15, " ");
                                }

                                #endregion

                                #region Segundos

                                if (valorFinal.Split(':')[2].Length == 1)
                                {
                                    valorFinal = valorFinal.Insert(18, " ");
                                }

                                #endregion
                            }
                        }

                        this.Text = "";
                        this.Text = valorFinal;
                        this.SelectionStart = posicaoInicial;
                        return true;

                        #endregion
                    }
                }
            }

            #endregion

            #region Corrige Bug ctrl_C qdo insert está ativo

            if (keyData == (Keys.Control | Keys.C))
            {
                if (Convert.ToBoolean(GetKeyState(45)))
                {
                    this.Copy();
                    return true;
                }
            }

            #endregion

            #region Corrigue Bug ctrl_X qdo insert está ativo

            if (keyData == (Keys.Control | Keys.X))
            {
                if (Convert.ToBoolean(GetKeyState(45)))
                {
                    this.Copy();
                    int posicao = this.SelectionStart;
                    this.Text = this.Text.Remove(this.SelectionStart, this.SelectionLength);
                    this.SelectionStart = posicao;
                    return true;
                }
            }

            #endregion

            #region Corrige Bug ENTER Framework / Down e Up finalizar edição DataGridView

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

        #region Validação

        /// <summary>
        /// Faz validação do valor de saída referente
        /// máscara definida.
        /// </summary>
        /// <returns>True para válido e False para inválido.</returns>
        public Boolean Validar()
        {
            Boolean Retorno = true;
            switch (Mascara)
            {
                case MascaraEnum.DATA:
                    if (this.Text.DeFormat().Length > 0)
                    {
                        Retorno = ValidaData(this.Text);
                    }
                    break;
                case MascaraEnum.HORA_LONG:
                    if (this.Text.DeFormat().Length > 0)
                    {
                        Retorno = ValidaHora(this.Text);
                    }
                    break;
                case MascaraEnum.HORA_SHORT:
                    if (this.Text.DeFormat().Length > 0)
                    {
                        Retorno = ValidaHora(this.Text);
                    }
                    break;
                case MascaraEnum.MOEDA_PORCENTAGEM:
                    if (this.Text.DeFormat().Trim().Length == 0) ZeroFill();
                    MetodoLeave();
                    break;
                case MascaraEnum.MES_ANO:
                    if (this.Text.DeFormat().Trim().Length > 0)
                    {
                        string data = "01/" + this.Text;
                        Retorno = ValidaData(data);
                    }
                    break;
                case MascaraEnum.DATA_HORA:
                    if (this.Text.DeFormat().Length > 0)
                    {
                        if (this.Text.EndsWith(" :  :"))
                        {
                            this.Text = this.Text.Insert(11, "00:00:00");
                        }

                        Retorno = ValidaData(this.Text);
                    }
                    break;
                case MascaraEnum.PLACA:
                    if (this.Text.DeFormat().Length > 0 && this.Text.DeFormat().Length != 7)
                    {
                        Retorno = false;
                        ExceçãoEventArgs argumento = new ExceçãoEventArgs();
                        argumento.TipoCampo = MascaraEnum.PLACA;
                        argumento.TipoExceção = ExceçãoEnum.FORMATO;
                        OnExceção(argumento);

                        this.Focus();
                    }
                    break;
            }

            return Retorno;
        }

        #region ValidaData

        /// <summary>
        /// Método para validar data
        /// </summary>
        /// <param name="data">Data a ser validado.</param>
        private Boolean ValidaData(String data)
        {
            Boolean Retorno = true;

            ExceçãoEventArgs argumento = new ExceçãoEventArgs();
            DateTime result;
            if (DateTime.TryParse(data, out result))
            {
                switch (Mascara)
                {
                    case MascaraEnum.DATA:
                        this.Text = Convert.ToDateTime(data).ToString("dd/MM/yyyy");
                        break;
                    case MascaraEnum.MES_ANO:
                        this.Text = Convert.ToDateTime(data).ToString("MM/yyyy");
                        break;
                    case MascaraEnum.DATA_HORA:
                        this.Text = Convert.ToDateTime(data).ToString("dd/MM/yyyy HH:mm:ss");
                        break;
                }

                if (tipoValidacao == ValidaçãoEnum.IDADE || tipoValidacao == ValidaçãoEnum.CRIAÇÃO)
                {
                    bool idade = tipoValidacao == ValidaçãoEnum.IDADE ? true : false;
                    if (AERMOD.LIB.Desenvolvimento.Funcoes.ValidarDataNasc(this.Text, idade) == false)
                    {
                        Retorno = false;
                        argumento.TipoCampo = MascaraEnum.DATA;
                        argumento.TipoExceção = ExceçãoEnum.VALIDAÇÃO;
                        OnExceção(argumento);

                        if (LeaveInvalidDate == false)
                        {
                            this.Focus();
                        }
                    }
                }
            }
            else
            {
                Retorno = false;
                argumento.TipoCampo = MascaraEnum.DATA;
                argumento.TipoExceção = ExceçãoEnum.FORMATO;
                OnExceção(argumento);

                if (LeaveInvalidDate == false)
                {
                    this.Focus();
                }
            }

            return Retorno;
        }

        /// <summary>
        /// Método para validar data para tratamento da tecla ctrl_z.
        /// </summary>
        /// <param name="data">Data a ser validado.</param>
        private void ValidaDataCTRL_Z(String data)
        {
            if (Mascara == MascaraEnum.MES_ANO && data.DeFormat().Trim().Length > 0) data = data.Insert(0, "01/");
            DateTime result;
            if (DateTime.TryParse(data, out result))
            {
                this.Text = Mascara == MascaraEnum.DATA ? Convert.ToDateTime(data).ToString("dd/MM/yyyy") : Convert.ToDateTime(data).ToString("MM/yyyy");
            }
        }

        #endregion

        #region ValidaHora

        /// <summary>
        /// Método para validar hora.
        /// </summary>
        /// <param name="hora"></param>
        private Boolean ValidaHora(String hora)
        {
            Boolean Retorno = true;

            DateTime result;
            if (DateTime.TryParse(hora, out result))
            {
                this.Text = Convert.ToDateTime(hora).ToString("HH:mm:ss");
            }
            else
            {
                Retorno = false;
                ExceçãoEventArgs argumento = new ExceçãoEventArgs();
                argumento.TipoCampo = MascaraEnum.HORA_LONG;
                argumento.TipoExceção = ExceçãoEnum.FORMATO;
                OnExceção(argumento);

                if (LeaveInvalidDate == false)
                {
                    this.Focus();
                }
            }

            return Retorno;
        }

        /// <summary>
        /// Método para validar hora.
        /// </summary>
        /// <param name="hora"></param>
        private void ValidaHoraCTRL_Z(String hora)
        {
            DateTime result;
            if (DateTime.TryParse(hora, out result))
            {
                this.Text = Convert.ToDateTime(hora).ToString("HH:mm:ss");
            }
        }

        #endregion

        #endregion

        #region PosicionaCursor

        /// <summary>
        /// Posiciona o cursor dentro do campo de acordo com valor informado.
        /// </summary>
        private void PosicionaCursor()
        {
            base.SelectAll();
        }

        #endregion

        #region StatusInsert

        /// <summary>
        /// Método para alternar o controle do form. entre(insert,overwright).
        /// </summary>
        private void StatusInsert()
        {
            switch (_statusInsert)
            {
                case 1:
                    this.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
                    _statusInsert = 2;
                    break;
                case 2:
                    this.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Insert;
                    _statusInsert = 1;
                    break;
            }
        }

        #endregion

        #endregion

        #region Eventos btnCalendario

        private void btnCalendario_Click(object sender, EventArgs e)
        {
            this.Focus();

            if (this.Focused == true)
            {
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

        private void _button_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            Control _button = GetControls.GetAllControls(this.Controls).FirstOrDefault(I => I.GetType() == typeof(Button) && I.Name == "btnCalendario");

            if (_button != null)
            {
                e.Graphics.DrawImage(AERMOD.LIB.Properties.Resources.calendar, new Rectangle(1, 1, _button.Width - 2, _button.Height - 2));
            }
        }

        protected override void OnResize(EventArgs e)
        {
            Control _button = GetControls.GetAllControls(this.Controls).FirstOrDefault(I => I.GetType() == typeof(Button) && I.Name == "btnCalendario");

            if (_button != null)
            {
                _button.Size = new Size(_button.Width, this.ClientSize.Height + 2);
                _button.Location = new Point(this.ClientSize.Width - _button.Width, -1);
            }

            base.OnResize(e);
        }

        #endregion

        #region Eventos monthCalendario        

        private void monthCalendario_DoubleClick(object sender, EventArgs e)
        {
            MonthCalendario monthCalendario = sender as MonthCalendario;
            string dataCalendario = monthCalendario.CurrentDate.ToString("dd/MM/yyyy");

            popup.Close();

            try
            {
                if (Mascara == MascaraEnum.DATA)
                {
                    this.Text = dataCalendario;
                }
                else
                {
                    string[] texto = this.Text.Split(' ');

                    if (texto[1].DeFormat().Trim().Length == 0)
                    {
                        texto[1] = "00:00:00";
                    }

                    this.Text = $"{dataCalendario} {texto[1]}";
                }

                this.Focus();
                this.BeginInvoke(new PosicionaCursorDelegate(PosicionaCursor));
            }
            catch (NullReferenceException) { }
        }

        private void monthCalendario_KeyDown(object sender, KeyEventArgs e)
        {
            MonthCalendario monthCalendario = sender as MonthCalendario;

            switch (e.KeyValue)
            {
                case (int)Keys.Enter:
                    string dataCalendario = monthCalendario.CurrentDate.ToString("dd/MM/yyyy");
                    popup.Close(ToolStripDropDownCloseReason.CloseCalled);

                    try
                    {
                        if (Mascara == MascaraEnum.DATA)
                        {
                            this.Text = dataCalendario;
                        }
                        else
                        {
                            string[] texto = this.Text.Split(' ');

                            if (texto[1].DeFormat().Trim().Length == 0)
                            {
                                texto[1] = "00:00:00";
                            }

                            this.Text = $"{dataCalendario} {texto[1]}";
                        }

                        this.Focus();
                        this.BeginInvoke(new PosicionaCursorDelegate(PosicionaCursor));
                    }
                    catch (NullReferenceException) { }

                    break;
            }
        }

        #endregion

        #region Eventos popup

        private void popup_Opened(object sender, EventArgs e)
        {
            MonthCalendario month = (popup.Items[0] as ToolStripControlHost).Control as MonthCalendario;
            month.Focus();
        }

        private void popup_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            Control _button = GetControls.GetAllControls(this.Controls).FirstOrDefault(I => I.GetType() == typeof(Button) && I.Name == "btnCalendario");

            if (_button != null && e.CloseReason == ToolStripDropDownCloseReason.AppClicked)
            {
                if (_button.Bounds.Contains(this.PointToClient(MousePosition)))
                {
                    popup.Tag = true;
                    return;
                }
            }

            popup.Tag = null;
        }

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
            public MascaraEnum TipoCampo { get; set; }
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

        #region OnPaint

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
            TextFormatFlags flags = TextFormatFlags.NoPadding;
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

            Color backColor = this.Enabled ? this.backColor : Color.FromArgb(255, 236, 233, 216);
            g.FillRectangle(new SolidBrush(backColor), base.ClientRectangle);

            if (base.GetStyle(ControlStyles.UserPaint) == false)
            {
                TextRenderer.DrawText(g, base.Text, this.Font, clientRectangle, this.ForeColor, backColor, flags);

                //if (labelMenos.Visible)
                //{
                //    Color foreColorLabelMenos = labelMenos.ForeColor;
                //    labelMenos.ForeColor = backColor;
                //    Rectangle clientRectangleMenos = labelMenos.ClientRectangle;
                //    clientRectangleMenos.Offset(3, 0);
                //    TextRenderer.DrawText(g, "-", labelMenos.Font, clientRectangleMenos, foreColorLabelMenos, TextFormatFlags.NoPadding);
                //}
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

        #endregion

        #region OnPaint LabelMenos

        private void LabelMenos_Paint(object sender, PaintEventArgs e)
        {
            if (this.Enabled == false)
            {
                Rectangle clientRectangleMenos = labelMenos.ClientRectangle;
                clientRectangleMenos.Offset(3, 0);
                TextRenderer.DrawText(e.Graphics, labelMenos.Text, labelMenos.Font, clientRectangleMenos, labelMenos.ForeColor, TextFormatFlags.NoPadding);
            }
        }

        #endregion

        #region OnPaint LabelSimbolo

        private void LabelSimbolo_Paint(object sender, PaintEventArgs e)
        {
            if (this.Enabled == false)
            {
                Rectangle clientRectangleSimbolo = labelSimbolo.ClientRectangle;
                clientRectangleSimbolo.Offset(3, 0);
                TextRenderer.DrawText(e.Graphics, labelSimbolo.Text, labelSimbolo.Font, clientRectangleSimbolo, labelSimbolo.ForeColor, TextFormatFlags.NoPadding);
            }
        }

        #endregion
    }
}
