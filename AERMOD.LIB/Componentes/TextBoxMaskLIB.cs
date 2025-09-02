using AERMOD.LIB.Formatacao;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AERMOD.LIB.Componentes
{
    [ToolboxBitmap(typeof(TextBox))]
    public class TextBoxMaskLIB : TextBox
    {
        #region Declarações

        /// <summary>
        /// Lista de ações do desfazer
        /// </summary>
        readonly List<Desfazer> listStorage = new List<Desfazer>();

        /// <summary>
        /// Controla e contextMenuStrip foi aberto
        /// </summary>
        bool openContextMenu = false;

        private Mask mascara = Mask.Default;

        /// <summary>
        /// Utilizado para habilitar/desabilitar o evento de TextChanged
        /// </summary>
        private bool EnabledTextChanged = true;

        #endregion

        #region Propriedades

        public enum Mask
        {
            Default,
            Decimal,
            Inteiro
        }

        public enum ValorInteiroDefault
        {
            Empty,
            Zero
        }

        /// <summary>
        /// Get ou set o nome da coluna em banco
        /// </summary>
        [DefaultValue("")]
        public string ColumnName { get; set; }

        [Description("Get ou set a mascara do componente"), DefaultValue(Mask.Default)]
        public Mask Mascara
        {
            get { return mascara; }
            set
            {
                if (mascara != value)
                {
                    mascara = value;
                    listStorage.Clear();

                    if (mascara == Mask.Decimal)
                    {
                        decimal valor = 0;
                        if (decimal.TryParse(base.Text, out valor) == true)
                        {
                            base.Text = valor.ToString("N" + decimais);
                        }
                        else
                        {
                            base.Text = "0," + new string('0', decimais);
                        }
                        this.TextAlign = HorizontalAlignment.Right;
                        this.Multiline = false;
                    }
                    else if (mascara == Mask.Inteiro)
                    {
                        decimal valor = 0;
                        if (decimal.TryParse(base.Text, out valor) == true)
                        {
                            base.Text = Convert.ToInt64(valor).ToString();
                        }
                        else
                        {
                            if (inteiroDefault == ValorInteiroDefault.Empty)
                            {
                                base.ResetText();
                            }
                            else
                            {
                                base.Text = "0";
                            }
                        }

                        this.TextAlign = HorizontalAlignment.Right;
                        this.Multiline = false;
                    }
                }
            }
        }

        private ValorInteiroDefault inteiroDefault;

        [DefaultValue(ValorInteiroDefault.Empty)]
        public ValorInteiroDefault InteiroDefault
        {
            get { return inteiroDefault; }
            set
            {
                if (inteiroDefault != value)
                {
                    inteiroDefault = value;
                    if (mascara == Mask.Inteiro)
                    {
                        if (inteiroDefault == ValorInteiroDefault.Empty)
                        {
                            base.ResetText();
                        }
                        else
                        {
                            Int64 valor = 0;
                            if (Int64.TryParse(base.Text, out valor) == true)
                            {
                                base.Text = valor.ToString();
                            }
                            else
                            {
                                base.Text = "0";
                            }
                        }
                    }
                }
            }
        }

        private bool inteiroZeroEsquerda;

        [Description("Permite zero a esquerda do valor ex:(001)")]
        [DefaultValue(false)]
        public bool InteiroZeroEsquerda
        {
            get
            {
                return inteiroZeroEsquerda;
            }
            set
            {
                if (value != inteiroZeroEsquerda)
                {
                    inteiroZeroEsquerda = value;
                }
            }
        }

        private int decimais = 2;

        [Description("Get ou set o número de casa decimais"), DefaultValue(2)]
        public int Decimais
        {
            get { return decimais; }
            set
            {
                if (decimais != value)
                {
                    if (value <= 0)
                    {
                        value = 1;
                    }
                    if (value > 10)
                    {
                        value = 10;
                    }

                    decimais = value;
                    if (base.Text != string.Empty)
                    {
                        if (mascara == Mask.Decimal)
                        {
                            base.Text = Convert.ToDecimal(base.Text).ToString("N" + decimais);
                        }
                    }
                }
            }
        }

        private int inteiro = 3;

        [Description("Get ou set o número de casas antes da virgula"), DefaultValue(3)]
        public int Inteiro
        {
            get { return inteiro; }
            set
            {
                if (inteiro != value)
                {
                    if (value <= 0)
                    {
                        value = 1;
                    }
                    if (value > 15)
                    {
                        value = 15;
                    }

                    inteiro = value;
                }
            }
        }

        /// <summary>
        /// Cor de fundo antes de estar com foco
        /// </summary>
        private Color backColor = Color.Empty;

        [Category("Appearance")]
        [Description("Indica se está sendo usado cor de fundo quando o controle está focado."), DefaultValue(false)]
        public bool FocusColorEnabled { get; set; }

        [Category("Appearance")]
        [Description("Cor de fundo quando o controle está focado."), DefaultValue(typeof(Color), "Empty")]
        public Color FocusColor { get; set; }

        /// <summary>
        /// Get se o número é negativo"
        /// </summary>
        [DefaultValue(false)]
        public bool Negativo
        {
            get
            {
                bool negativo = false;

                switch (Mascara)
                {
                    case Mask.Default:
                        {
                            negativo = false;
                        }
                        break;
                    case Mask.Decimal:
                        {
                            negativo = Convert.ToDecimal(base.Text) >= 0m ? false : true;
                        }
                        break;
                    case Mask.Inteiro:
                        {
                            negativo = base.Text == string.Empty || Convert.ToDecimal(base.Text) >= 0 ? false : true;
                        }
                        break;
                }

                return negativo;
            }
            set
            {

            }
        }

        private bool permiteNegativo = true;

        [Description("Permite valor negativo"), DefaultValue(true)]
        public bool PermiteNegativo
        {
            get { return permiteNegativo; }
            set
            {
                if (value == false)
                {
                    if (Negativo == true)
                    {
                        switch (Mascara)
                        {
                            case Mask.Decimal:
                            case Mask.Inteiro:
                                {
                                    base.Text = base.Text.Replace("-", "");
                                }
                                break;
                        }
                    }
                }
                permiteNegativo = value;
            }
        }

        private string GetSelectedText
        {
            get
            {                
                string value = base.SelectedText;                

                return value;
            }
        }

        private string valorEntrada = string.Empty;

        /// <summary>
        /// Get o valor de entrada do campo
        /// </summary>
        [DefaultValue(""), Browsable(false)]
        public string ValorEntrada
        {
            get { return valorEntrada; }
        }

        /// <summary>
        /// Valores para Duplicação, Incremento e Decremento.
        /// </summary>
        public string StrVALOR = string.Empty;

        /// <summary>
        /// Valores para Duplicação, Incremento e Decremento.
        /// </summary>
        public decimal? SglVALOR;

        private bool autoResize = false;

        [Description("A fonte se adapta a altura do componente"), DefaultValue(false)]
        public bool AutoResize
        {
            get { return autoResize; }
            set
            {
                if (value != autoResize)
                {
                    autoResize = value;

                    if (autoResize == true)
                    {
                        base.Multiline = true;
                    }
                    else
                    {
                        base.Multiline = false;
                        base.MinimumSize = new Size(0, 0);
                    }
                }
            }
        }

        #endregion

        #region Construtor

        public TextBoxMaskLIB()
        {

        }

        #endregion

        #region Text

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                if (mascara == Mask.Decimal)
                {
                    if (value == string.Empty)
                    {
                        value = "0";
                    }

                    decimal valor = 0;
                    if (decimal.TryParse(value, out valor))
                    {
                        if (valor >= 0)
                        {
                            base.Text = valor.ToString("N" + decimais);
                        }
                        else
                        {
                            if (permiteNegativo == false)
                            {
                                valor = valor * -1;
                            }

                            base.Text = valor.ToString("N" + decimais);
                        }
                    }
                }
                else if (mascara == Mask.Inteiro)
                {
                    if (value == string.Empty)
                    {
                        if (inteiroDefault == ValorInteiroDefault.Empty)
                        {
                            base.Text = value;
                        }
                        else
                        {
                            base.Text = "0";
                        }
                    }
                    else
                    {
                        decimal decimalValue = 0;

                        if (decimal.TryParse(value, out decimalValue) == true)
                        {
                            Int64 valor = decimal.ToInt64(decimalValue);

                            if (valor == 0 && inteiroDefault == ValorInteiroDefault.Empty)
                            {
                                base.Text = string.Empty;
                            }
                            else
                            {
                                if (valor < 0)
                                {
                                    if (permiteNegativo == false)
                                    {
                                        valor = valor * -1;
                                    }
                                }

                                if (inteiroZeroEsquerda == false)
                                {
                                    base.Text = valor.ToString();
                                }
                                else
                                {
                                    if (Int64.TryParse(value, out valor) == true)
                                    {
                                        if (valor < 0)
                                        {
                                            base.Text = "-" + value.Replace("-", "");
                                        }
                                        else
                                        {
                                            base.Text = value.Replace("-", "");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    base.Text = value;
                }

                if (this.FindForm() != null && this.FindForm().ActiveControl != this)
                {
                    StrVALOR = base.Text;
                }

                //Código Renato
                if (this.FindForm() != null && this.FindForm().ActiveControl != this)
                {
                    valorEntrada = base.Text;
                }
            }
        }

        #endregion

        #region TextAlign

        /// <summary>
        /// Get ou set o alinhamento horizontal do texto
        /// </summary>
        protected new HorizontalAlignment TextAlign
        {
            get
            {
                return base.TextAlign;
            }
            set
            {
                if (mascara == Mask.Decimal)
                {
                    base.TextAlign = HorizontalAlignment.Right;
                }
                else if (mascara == Mask.Inteiro)
                {
                    base.TextAlign = HorizontalAlignment.Right;
                }
                else
                {
                    base.TextAlign = value;
                }
            }
        }

        #endregion

        #region Multiline

        [DefaultValue(false)]
        public bool ForceMultiLine { get; set; }

        public override bool Multiline
        {
            get
            {
                return base.Multiline;
            }
            set
            {
                if (ForceMultiLine == true)
                {
                    base.Multiline = value;
                }
                else
                {
                    if (autoResize == true)
                    {
                        return;
                    }

                    if (mascara == Mask.Decimal || mascara == Mask.Inteiro)
                    {
                        value = false;
                    }
                    base.Multiline = value;
                }
            }
        }

        [Browsable(false)]
        public bool MultilineDock
        {
            set
            {
                base.Multiline = value;
            }
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Desfaz a 10 ultimas ações realizadas
        /// </summary>
        public void Desfazer()
        {
            if (listStorage.Count > 0)
            {
                for (int i = listStorage.Count - 1; i >= 0; i--)
                {
                    if (base.Text == listStorage[i].Texto)
                    {
                        listStorage.RemoveAt(i);
                    }
                    else
                    {
                        break;
                    }
                }

                if (listStorage.Count > 0)
                {
                    Desfazer Undo = new Desfazer();
                    Undo.Texto = listStorage.Last().Texto;
                    Undo.PosicaoCursor = listStorage.Last().PosicaoCursor;
                    Undo.SelecaoTexto = listStorage.Last().SelecaoTexto;
                    listStorage.Remove(listStorage.Last());

                    base.Text = Undo.Texto;
                    this.SelectionStart = Undo.PosicaoCursor;
                    this.SelectionLength = Undo.SelecaoTexto;

                    if (listStorage.Count > 0)
                    {
                        listStorage.Last().PosicaoCursor = Undo.PosicaoCursor;
                    }
                }
            }
        }

        /// <summary>
        /// Limpa a opçoes de desfazer
        /// </summary>
        public void ClearDesfazer()
        {
            listStorage.Clear();
        }

        /// <summary>
        /// Incrementa o valor
        /// </summary>
        public void Incrementar()
        {
            if (mascara == Mask.Inteiro)
            {
                Int64 valor = (base.Text == string.Empty) ? 0 : Convert.ToInt64(base.Text);
                if (valor < Convert.ToInt64("".PadRight(this.inteiro, '9')))
                {
                    base.Text = valor++.ToString();
                }
            }
            else if (mascara == Mask.Decimal)
            {
                decimal valor = Convert.ToDecimal(base.Text);
                if (valor < Convert.ToDecimal(string.Format("{0},{1}", "".PadLeft(this.inteiro, '9'), "".PadRight(this.decimais, '9'))))
                {
                    base.Text = valor++.ToString();
                }
            }
        }

        /// <summary>
        /// Decrementa o valor
        /// </summary>
        public void Decrementar()
        {
            if (mascara == Mask.Inteiro)
            {
                if (permiteNegativo == true)
                {
                    Int64 valor = (base.Text == string.Empty) ? 0 : Convert.ToInt64(base.Text);
                    if (valor > (Convert.ToInt64("".PadRight(this.inteiro, '9')) * -1))
                    {
                        base.Text = valor--.ToString();
                    }
                }
                else
                {
                    Int64 valor = (base.Text == string.Empty) ? 0 : Convert.ToInt64(base.Text);
                    if (valor > 0)
                    {
                        base.Text = valor--.ToString();
                    }
                }
            }
            else if (mascara == Mask.Decimal)
            {
                if (permiteNegativo == true)
                {
                    decimal valor = (base.Text == string.Empty) ? 0 : Convert.ToDecimal(base.Text);
                    if (valor > (Convert.ToDecimal(string.Format("{0},{1}", "".PadLeft(this.inteiro, '9'), "".PadRight(this.decimais, '9'))) * -1))
                    {
                        base.Text = valor--.ToString();
                    }
                }
                else
                {
                    decimal valor = (base.Text == string.Empty) ? 0 : Convert.ToDecimal(base.Text);
                    if (valor > 0)
                    {
                        base.Text = valor--.ToString();
                    }
                }
            }
        }

        #endregion

        #region OnEnter

        protected override void OnEnter(EventArgs e)
        {
            backColor = base.BackColor;

            //if (mascara == Mask.Inteiro)
            //{
            //    base.TextAlign = HorizontalAlignment.Left;
            //}

            if ((Control.MouseButtons ^ MouseButtons.Left) != 0 && openContextMenu == false)
            {
                this.SelectAll();
            }

            base.OnEnter(e);

            if (FocusColorEnabled == true && FocusColor != Color.Transparent)
            {
                base.BackColor = FocusColor;
            }
        }

        #endregion

        #region OnKeyDown

        protected override void OnKeyDown(KeyEventArgs e)
        {
            string[] arrayAjuda = new string[3];
            arrayAjuda[0] = "Atenção";            

            if (this.ReadOnly == true && e.KeyCode != Keys.Down && e.KeyCode != Keys.Up && e.KeyCode != Keys.Enter && e.KeyCode != Keys.Escape)
            {
                return;
            }

            if (e.KeyCode == Keys.Enter)
            {
                if (autoResize == true)
                {
                    e.SuppressKeyPress = true;
                    e.Handled = true;
                }
            }

            if (mascara == Mask.Decimal)
            {
                //Digitando Virgula e Ponto
                if (e.KeyValue == 188 || e.KeyValue == 190 || e.KeyValue == 110 || e.KeyValue == 194)
                {
                    #region Vírgula ou ponto

                    this.SelectionStart = base.Text.IndexOf(',') + 1;
                    this.SelectionLength = 0;

                    e.Handled = true;
                    e.SuppressKeyPress = true;

                    #endregion
                }
                //Digitando números
                else if ((e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9) || (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9))
                {
                    #region

                    if (this.SelectionStart > base.Text.IndexOf(','))
                    {
                        if (this.SelectionStart == base.Text.Length)
                        {
                            e.Handled = true;
                            e.SuppressKeyPress = true;
                        }
                        else
                        {
                            int index = this.SelectionStart;

                            string tecla = e.KeyCode.ToString().Replace("NumPad", "").Replace("D", "");

                            //EnabledTextChanged = false;                           
                            base.Text = Convert.ToDecimal(base.Text.Remove(index, (this.SelectionLength == 0) ? 1 : this.SelectionLength).Insert(index, tecla)).ToString("N" + decimais);
                            //EnabledTextChanged = true;

                            this.SelectionStart = ++index;

                            e.Handled = true;
                            e.SuppressKeyPress = true;
                        }
                    }
                    else
                    {
                        if (GetSelectedText.Contains(","))
                        {
                            if (!(GetSelectedText.Length == base.Text.Length))
                            {
                                e.Handled = true;
                                e.SuppressKeyPress = true;
                            }
                        }
                        else if (base.Text.Split(',')[0].Replace(".", "").Replace("-", "").Length >= inteiro)
                        {
                            if (GetSelectedText == string.Empty || GetSelectedText == "." || GetSelectedText == "-")
                            {
                                if (!(inteiro == 1 && base.SelectionStart == 1 && Convert.ToDecimal(base.Text.Split(',')[0]) == 0m))
                                {
                                    e.Handled = true;
                                    e.SuppressKeyPress = true;
                                }
                            }
                            else if (GetSelectedText == string.Empty && this.SelectionStart < base.Text.Length && base.Text[this.SelectionStart] == ',')
                            {
                                e.Handled = true;
                                e.SuppressKeyPress = true;
                            }
                        }
                        else if (base.Text.Contains("-") == true && this.SelectionStart == 0)
                        {
                            this.SelectionStart = 1;
                        }
                    }

                    #endregion
                }
                else if (e.KeyCode == Keys.Back || e.KeyCode == (Keys.Shift | Keys.Delete))
                {
                    #region

                    if (GetSelectedText.Contains(","))
                    {
                        if (base.Text == GetSelectedText)
                        {
                            base.ResetText();
                        }
                        else
                        {
                            e.Handled = true;
                            e.SuppressKeyPress = true;
                        }
                    }
                    else
                    {
                        if (this.SelectionStart > 0)
                        {
                            if (this.SelectionLength == 0)
                            {
                                if (base.Text[this.SelectionStart - 1] == ',')
                                {
                                    this.SelectionStart = this.SelectionStart - 1;

                                    e.Handled = true;
                                    e.SuppressKeyPress = true;
                                }
                                else if (base.Text[this.SelectionStart - 1] == '.')
                                {
                                    string indexOf = base.Text.Substring(this.SelectionStart);

                                    string valor = base.Text.Remove(this.SelectionStart - 2, 1);

                                    if (valor.StartsWith("."))
                                    {
                                        valor = valor.Remove(0, 1);
                                    }

                                    EnabledTextChanged = false;                                   
                                    base.Text = Convert.ToDecimal(valor).ToString("N" + decimais);                                    
                                    EnabledTextChanged = true;

                                    this.SelectionStart = (base.Text.IndexOf(indexOf) != -1) ? base.Text.IndexOf(indexOf) : 0;

                                    e.Handled = true;
                                    e.SuppressKeyPress = true;
                                }
                                else if (this.SelectionStart == 1 && base.Text[this.SelectionStart] == '.')
                                {
                                    string valor = base.Text.Remove(0, 2);

                                    EnabledTextChanged = false;                                   
                                    base.Text = Convert.ToDecimal(valor).ToString("N" + decimais);                                   
                                    EnabledTextChanged = true;

                                    this.SelectionStart = 0;

                                    e.Handled = true;
                                    e.SuppressKeyPress = true;
                                }
                            }
                        }
                    }

                    #endregion
                }
                else if ((e.Control == true && e.KeyCode == Keys.Back) || (e.Control == true && e.KeyCode == Keys.OemMinus))
                {
                    #region

                    e.Handled = true;
                    e.SuppressKeyPress = true;

                    #endregion
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    #region

                    if (GetSelectedText.Contains(","))
                    {
                        if (base.Text == GetSelectedText)
                        {
                            base.ResetText();
                        }
                        else
                        {
                            e.Handled = true;
                            e.SuppressKeyPress = true;
                        }
                    }
                    else
                    {
                        if (this.SelectionStart != base.Text.Length)
                        {
                            if (GetSelectedText == string.Empty)
                            {
                                if (base.Text[this.SelectionStart] == ',')
                                {
                                    this.SelectionStart = this.SelectionStart + 1;
                                    e.Handled = true;
                                    e.SuppressKeyPress = true;
                                }
                                else if (base.Text[this.SelectionStart] == '.')
                                {
                                    string indexOf = base.Text.Substring(this.SelectionStart + 2);

                                    string valor = base.Text.Remove(this.SelectionStart + 1, 1);

                                    EnabledTextChanged = false;                                    
                                    base.Text = Convert.ToDecimal(valor).ToString("N" + decimais);                                    
                                    EnabledTextChanged = true;

                                    Int32 index = 0;
                                    if (base.Text.Contains("."))
                                    {
                                        index = base.Text.IndexOf(indexOf) - 2;
                                        if (index < 0)
                                        {
                                            index = 0;
                                        }
                                    }
                                    else
                                    {
                                        index = base.Text.IndexOf(indexOf);
                                    }
                                    this.SelectionStart = index;

                                    e.Handled = true;
                                    e.SuppressKeyPress = true;
                                }
                                else if (this.SelectionStart == 0 && base.Text[this.SelectionStart + 1] == '.')
                                {
                                    string valor = base.Text.Remove(0, 2);

                                    EnabledTextChanged = false;                                    
                                    base.Text = Convert.ToDecimal(valor).ToString("N" + decimais);                                   
                                    EnabledTextChanged = true;

                                    this.SelectionStart = 0;

                                    e.Handled = true;
                                    e.SuppressKeyPress = true;
                                }
                            }
                        }
                    }

                    #endregion
                }
                else if (e.KeyCode == Keys.OemMinus || e.KeyValue == 109)
                {
                    #region

                    if (Convert.ToDecimal(base.Text) != 0m && PermiteNegativo == true)
                    {
                        if (Negativo == true)
                        {
                            var selectionIndex = this.SelectionStart;
                            base.Text = base.Text.Replace("-", "");

                            if (selectionIndex > 1)
                            {
                                this.SelectionStart = selectionIndex - 1;
                            }
                            else
                            {
                                this.SelectionStart = 0;
                            }
                        }
                        else
                        {
                            var selectionIndex = this.SelectionStart;
                            base.Text = "-" + base.Text;

                            this.SelectionStart = selectionIndex + 1;
                        }
                    }

                    e.Handled = true;
                    e.SuppressKeyPress = true;

                    #endregion
                }
                else if (e.KeyCode == (Keys.Control | Keys.Z))
                {
                    #region

                    Desfazer();

                    e.Handled = true;
                    e.SuppressKeyPress = true;

                    #endregion
                }
                else if ((e.Control == true && e.KeyCode == Keys.V) || (e.Shift == true && e.KeyCode == Keys.Insert))
                {
                    #region

                    decimal value = 0;

                    string texto = base.Text;
                    if (this.SelectionLength > 0)
                    {
                        texto = texto.Remove(this.SelectionStart, this.SelectionLength).Insert(this.SelectionStart, Clipboard.GetText());
                    }
                    else
                    {
                        texto = texto.Insert(this.SelectionStart, Clipboard.GetText());
                    }

                    if (!decimal.TryParse(texto, out value))
                    {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                    }
                    else
                    {
                        if (PermiteNegativo == false && value < 0m)
                        {
                            e.Handled = true;
                            e.SuppressKeyPress = true;
                        }
                        else
                        {
                            texto = value.ToString("N" + decimais);
                            if (texto.Split(',')[0].Replace("-", "").Replace(".", "").Length > Inteiro)
                            {
                                e.Handled = true;
                                e.SuppressKeyPress = true;
                            }
                        }
                    }

                    #endregion
                }
                else if (e.KeyCode == Keys.Up)
                {
                    #region

                    if (this.Multiline == false)
                    {
                        this.FindForm().SelectNextControl(this, false, true, true, true);
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                    }

                    #endregion
                }
                else if (e.KeyCode == Keys.Down)
                {
                    #region

                    if (this.Multiline == false)
                    {
                        this.FindForm().SelectNextControl(this, true, true, true, true);
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                    }

                    #endregion
                }
            }
            else if (mascara == Mask.Inteiro)
            {
                if ((e.Control == true && e.KeyCode == Keys.V) || (e.Shift == true && e.KeyCode == Keys.Insert))
                {
                    #region

                    Int64 value = 0;

                    string texto = base.Text;
                    if (this.SelectionLength > 0)
                    {
                        texto = texto.Remove(this.SelectionStart, this.SelectionLength).Insert(this.SelectionStart, Clipboard.GetText());
                    }
                    else
                    {
                        texto = texto.Insert(this.SelectionStart, Clipboard.GetText());
                    }

                    if (!Int64.TryParse(texto, out value))
                    {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                    }
                    else
                    {
                        if (PermiteNegativo == false && value < 0)
                        {
                            e.Handled = true;
                            e.SuppressKeyPress = true;
                        }
                        else
                        {
                            texto = InteiroZeroEsquerda == false ? value.ToString() : texto;
                            if (texto.Replace("-", "").Length > Inteiro)
                            {
                                e.Handled = true;
                                e.SuppressKeyPress = true;
                            }
                        }
                    }

                    #endregion
                }
                else if (e.KeyCode == Keys.Up)
                {
                    #region

                    if (this.Multiline == false)
                    {
                        this.FindForm().SelectNextControl(this, false, true, true, true);
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                    }

                    #endregion
                }
                else if (e.KeyCode == Keys.Down)
                {
                    #region

                    if (this.Multiline == false)
                    {
                        this.FindForm().SelectNextControl(this, true, true, true, true);
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                    }

                    #endregion
                }
                else if ((e.Control == true && e.KeyCode == Keys.Back) || (e.Control == true && e.KeyCode == Keys.OemMinus))
                {
                    #region

                    e.Handled = true;
                    e.SuppressKeyPress = true;

                    #endregion
                }
                else if (e.KeyCode == Keys.OemMinus || e.KeyValue == 109)
                {
                    #region

                    if (Convert.ToInt64(base.Text) != 0 && PermiteNegativo == true)
                    {
                        if (Negativo == true)
                        {
                            var selectionIndex = this.SelectionStart;
                            base.Text = base.Text.Replace("-", "");

                            if (selectionIndex > 1)
                            {
                                this.SelectionStart = selectionIndex - 1;
                            }
                            else
                            {
                                this.SelectionStart = 0;
                            }
                        }
                        else
                        {
                            var selectionIndex = this.SelectionStart;
                            base.Text = "-" + base.Text;

                            this.SelectionStart = selectionIndex + 1;
                        }
                    }

                    e.Handled = true;
                    e.SuppressKeyPress = true;

                    #endregion
                }
                else if (e.KeyCode == (Keys.Control | Keys.Z))
                {
                    #region

                    Desfazer();

                    e.Handled = true;
                    e.SuppressKeyPress = true;

                    #endregion
                }
            }
            else if (mascara == Mask.Default)
            {
                if (e.KeyCode == Keys.Up)
                {
                    #region

                    if (this.Multiline == false)
                    {
                        this.FindForm().SelectNextControl(this, false, true, true, true);
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                    }

                    #endregion
                }
                else if (e.KeyCode == Keys.Down)
                {
                    #region

                    if (this.Multiline == false)
                    {
                        this.FindForm().SelectNextControl(this, true, true, true, true);
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                    }

                    #endregion
                }
            }

            base.OnKeyDown(e);
        }

        #endregion

        #region OnKeyPress

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            bool keyInsert = false;

            if (mascara == Mask.Decimal)
            {
                if (char.IsLetter(e.KeyChar) || char.IsSymbol(e.KeyChar) || char.IsPunctuation(e.KeyChar) || e.KeyChar == ' ')
                {
                    e.Handled = true;
                }
                else if (keyInsert == true && char.IsNumber(e.KeyChar))
                {
                    if (GetSelectedText.Length == 0)
                    {
                        if (this.SelectionStart != base.Text.Length && base.Text[this.SelectionStart] == '.')
                        {
                            this.SelectionStart = this.SelectionStart + 1;
                        }

                        if (base.SelectedText == "-")
                        {
                            e.Handled = true;
                        }
                        else
                        {
                            if (base.Text.Contains("-") == true && this.SelectionStart == 0)
                            {
                                this.SelectionStart = 1;
                            }

                            if (base.Text[this.SelectionStart] != ',')
                            {
                                this.SelectionLength = 1;
                            }
                        }
                    }
                }
            }
            else if (mascara == Mask.Inteiro)
            {
                if (char.IsLetter(e.KeyChar) || char.IsSymbol(e.KeyChar) || char.IsPunctuation(e.KeyChar) || e.KeyChar == ' ')
                {
                    e.Handled = true;
                }
                else if (char.IsNumber(e.KeyChar))
                {
                    if (GetSelectedText.Length == 0)
                    {
                        if (keyInsert == true)
                        {
                            if (base.Text.Contains("-") == true && this.SelectionStart == 0)
                            {
                                this.SelectionStart = 1;
                            }
                            this.SelectionLength = 1;

                            if (this.SelectionLength == 0 && base.Text.Replace("-", "").Length == Inteiro)
                            {
                                e.Handled = true;
                            }
                        }
                        else if ((base.Text.Replace("-", "").Length - GetSelectedText.Length) >= inteiro)
                        {
                            if (!(base.Text.Replace("-", "").Length == inteiro && Convert.ToInt64(base.Text.Replace("-", "")) == 0L && this.SelectionStart == base.Text.Length))
                            {
                                e.Handled = true;
                            }
                        }
                        else if (base.Text.Contains("-") == true && this.SelectionStart == 0)
                        {
                            this.SelectionStart = 1;
                        }
                    }
                    else if (base.SelectedText == "-")
                    {
                        e.Handled = true;
                    }
                }
            }
            else if (mascara == Mask.Default)
            {
                if (keyInsert == true && GetSelectedText.Length == 0)
                {
                    this.SelectionLength = 1;
                }
            }

            base.OnKeyPress(e);
        }

        #endregion

        #region OnLeave

        protected override void OnLeave(EventArgs e)
        {
            if (mascara == Mask.Inteiro && inteiroDefault == ValorInteiroDefault.Empty)
            {
                if (base.Text.DeFormat() != string.Empty)
                {
                    if (Convert.ToInt64(base.Text) == 0)
                    {
                        this.Text = string.Empty;
                    }
                }
            }

            base.OnLeave(e);

            if (this.FindForm() != null && this.FindForm().ActiveControl != this)
            {
                StrVALOR = base.Text;

                //if (mascara == Mask.Inteiro)
                //{
                //    this.BeginInvoke((MethodInvoker)delegate { base.TextAlign = HorizontalAlignment.Right; });
                //}
            }

            //Código do Renato
            if (this.FindForm() != null && this.FindForm().ActiveControl != this)
            {
                valorEntrada = base.Text;

                //if (mascara == Mask.Inteiro)
                //{
                //    this.BeginInvoke((MethodInvoker)delegate { base.TextAlign = HorizontalAlignment.Right; });
                //}
            }

            if (FocusColorEnabled == true && FocusColor != Color.Transparent)
            {
                base.BackColor = backColor;
            }
        }

        #endregion

        #region TextChanged


        protected override void OnTextChanged(EventArgs e)
        {
            if (EnabledTextChanged == false)
            {
                return;
            }

            if (mascara == Mask.Decimal)
            {
                bool aposVirgula = false;

                //Teste - Colando -250,00
                string valor = base.Text.Contains(",") ? base.Text.Split(',')[0].Replace(",", ".") + "," + base.Text.Split(',')[1] : base.Text;
                if (valor != string.Empty)
                {
                    Int32 index = this.SelectionStart;
                    string indexOf = valor.Substring(index);

                    if (valor.StartsWith("."))
                    {
                        valor = valor.Remove(0, 1);
                    }
                    else if (valor.StartsWith("-."))
                    {
                        valor = valor.Remove(1, 1);
                    }

                    if (this.SelectionStart > base.Text.IndexOf(','))
                    {
                        aposVirgula = true;
                    }

                    EnabledTextChanged = false;                    
                    base.Text = Convert.ToDecimal(valor).ToString("N" + decimais);                    
                    EnabledTextChanged = true;

                    if (!base.Text.Contains(indexOf))
                    {
                        indexOf = indexOf.Remove(0, 1);
                    }

                    var str = Convert.ToDecimal(valor).ToString("N" + decimais);

                    if (aposVirgula == true)
                    {
                        this.SelectionStart = index;
                    }
                    else
                    {
                        //this.SelectionStart = base.Text.IndexOf(indexOf);

                        this.SelectionStart = (base.Text.IndexOf(indexOf) != -1) ? base.Text.IndexOf(indexOf) : 0;
                    }
                }
                else
                {
                    EnabledTextChanged = false;                   
                    base.Text = 0.00m.ToString("N" + decimais);                    
                    EnabledTextChanged = true;
                }

                if (Convert.ToDecimal(base.Text) == 0m)
                {
                    Negativo = false;
                }
            }
            else if (mascara == Mask.Inteiro)
            {
                if (base.Text == "-")
                {
                    EnabledTextChanged = false;
                    base.Text = "";
                    EnabledTextChanged = true;
                }

                if (base.Text == string.Empty)
                {
                    if (inteiroDefault == ValorInteiroDefault.Zero)
                    {
                        EnabledTextChanged = false;
                        base.Text = "0";
                        EnabledTextChanged = true;
                    }
                }
                else
                {
                    if (Convert.ToInt64(base.Text) == 0 && this.Focused == false && inteiroDefault == ValorInteiroDefault.Empty)
                    {
                        EnabledTextChanged = false;
                        base.Text = string.Empty;
                        EnabledTextChanged = true;
                    }
                    else if (base.Text.Contains("-") && Convert.ToInt64(base.Text) == 0)
                    {
                        EnabledTextChanged = false;
                        base.Text = base.Text.Replace("-", "");
                        EnabledTextChanged = true;
                    }
                }

                //Tratamento zero ao lado esquerdo do valor
                if (base.Text.Length > 1 && base.Text[(base.Text.Contains("-") ? 1 : 0)] == '0' && inteiroZeroEsquerda == false)
                {
                    Int32 index = this.SelectionStart - 1 >= 0 ? this.SelectionStart - 1 : 0;

                    EnabledTextChanged = false;
                    base.Text = Convert.ToInt64(base.Text).ToString();
                    EnabledTextChanged = true;

                    base.SelectionStart = index;
                }
            }

            if (listStorage.Count == 10)
            {
                listStorage.RemoveAt(0);
            }
            if (listStorage.Count == 0 || (listStorage.Count > 0 && listStorage.Last().Texto != base.Text))
            {
                listStorage.Add(new Desfazer() { Texto = base.Text, SelecaoTexto = this.SelectionLength, PosicaoCursor = this.SelectionStart });
            }

            if (this.Enabled == false)
            {
                this.DrawDefaultText();
            }


            base.OnTextChanged(e);
        }

        #endregion

        //Boolean CalculandoFonte = false;
        protected override void OnSizeChanged(EventArgs e)
        {
            if (autoResize == true)
            {
                //if (!CalculandoFonte)
                //{
                //    CalculandoFonte = true;
                //    try
                //    {
                //        System.Drawing.Font Fonte = Desenvolvimento.Funcoes.GetFontForTextBoxHeight(this.Height - 5, this.Font);
                //        //this.Font = Fonte;
                //    }
                //    finally
                //    {
                //        CalculandoFonte = false;
                //    }
                //}
            }
            else
            {
                base.OnResize(e);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.Z))
            {
                Desfazer();
                return true;
            }

            bool keyDown = (keyData == Keys.Down && this.GetType() == typeof(IDataGridViewEditingControl) && (this as IDataGridViewEditingControl).EditingControlDataGridView.Rows.Count > 0 && (this as IDataGridViewEditingControl).EditingControlDataGridView.CurrentRow == (this as IDataGridViewEditingControl).EditingControlDataGridView.Rows.Cast<DataGridViewRow>().Last());
            bool keyUp = (keyData == Keys.Up && this.GetType() == typeof(IDataGridViewEditingControl) && (this as IDataGridViewEditingControl).EditingControlDataGridView.Rows.Count > 0 && (this as IDataGridViewEditingControl).EditingControlDataGridView.CurrentRow == (this as IDataGridViewEditingControl).EditingControlDataGridView.Rows.Cast<DataGridViewRow>().First());

            if (this.GetType() == typeof(IDataGridViewEditingControl))
            {
                IDataGridViewEditingControl columnTextBox = (IDataGridViewEditingControl)this;

                if (keyDown || keyUp)
                {
                    MethodInfo[] processDownKey = columnTextBox.EditingControlDataGridView.GetType().GetMethods(BindingFlags.Instance | BindingFlags.NonPublic);

                    for (int i = 0; i < processDownKey.Length; i++)
                    {
                        if (processDownKey[i].Name == "OnCellValidating")
                        {
                            MethodInfo metodo = processDownKey[i];
                            if (metodo.ReturnType != typeof(void))
                            {
                                bool resultCancel = (bool)metodo.Invoke(columnTextBox.EditingControlDataGridView, new object[] { columnTextBox.EditingControlDataGridView.CurrentCell, columnTextBox.EditingControlDataGridView.CurrentCell.ColumnIndex, columnTextBox.EditingControlDataGridView.CurrentCell.RowIndex, DataGridViewDataErrorContexts.LeaveControl });
                                if (resultCancel == false)
                                {
                                    if (columnTextBox.EditingControlDataGridView.CurrentCell.IsInEditMode == true)
                                    {
                                        columnTextBox.EditingControlDataGridView.EndEdit(DataGridViewDataErrorContexts.LeaveControl);
                                    }
                                }
                                break;
                            }
                        }
                    }

                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }


        #region ContextMenuStrip

        private void context_Opened(object sender, EventArgs e)
        {
            openContextMenu = true;
            this.Focus();
            openContextMenu = false;
        }

        private void tsMenuItemSelecionar_Click(object sender, EventArgs e)
        {
            this.SelectAll();
        }

        private void tsMenuItemExcluir_Click(object sender, EventArgs e)
        {
            if (GetSelectedText != string.Empty)
            {                
                base.Text = base.Text.Remove(this.SelectionStart, this.SelectionLength);                
            }
        }

        private void tsMenuItemColar_Click(object sender, EventArgs e)
        {
            if (mascara == Mask.Decimal)
            {
                decimal value = 0;

                string texto = base.Text;
                if (this.SelectionLength > 0)
                {
                    texto = texto.Remove(this.SelectionStart, this.SelectionLength).Insert(this.SelectionStart, Clipboard.GetText());
                }
                else
                {
                    texto = texto.Insert(this.SelectionStart, Clipboard.GetText());
                }

                if (decimal.TryParse(texto, out value))
                {
                    if (PermiteNegativo == false && value < 0m)
                    {
                        return;
                    }

                    texto = value.ToString("N" + decimais);
                    if (texto.Split(',')[0].Replace("-", "").Replace(".", "").Length > Inteiro)
                    {
                        return;
                    }

                    this.Paste();
                }
            }
            else if (mascara == Mask.Inteiro)
            {
                Int64 value = 0;

                string texto = base.Text;
                if (this.SelectionLength > 0)
                {
                    texto = texto.Remove(this.SelectionStart, this.SelectionLength).Insert(this.SelectionStart, Clipboard.GetText());
                }
                else
                {
                    texto = texto.Insert(this.SelectionStart, Clipboard.GetText());
                }

                if (Int64.TryParse(texto, out value))
                {
                    if (PermiteNegativo == false && value < 0)
                    {
                        return;
                    }

                    texto = InteiroZeroEsquerda == false ? value.ToString() : texto;
                    if (texto.Replace("-", "").Length > Inteiro)
                    {
                        return;
                    }

                    this.Paste();
                }
            }
            else
            {
                this.Paste();
            }
        }

        private void tsMenuItemCopiar_Click(object sender, EventArgs e)
        {
            this.Copy();
        }

        private void tsMenuItemRecortar_Click(object sender, EventArgs e)
        {
            this.Cut();
        }

        private void tsMenuItemDesfazer_Click(object sender, EventArgs e)
        {
            Desfazer();
        }

        #endregion

        #region WndProc

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

        #endregion

        #region DrawDefaultText

        protected virtual void DrawDefaultText()
        {
            if (this.Created == true)
            {
                using (Graphics graphics = base.CreateGraphics())
                {
                    this.DrawDefaultText(graphics);
                }
            }
        }

        protected virtual void DrawDefaultText(Graphics g)
        {
            TextFormatFlags flags = TextFormatFlags.NoPadding | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix;

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
                TextRenderer.DrawText(g, this.Text, this.Font, clientRectangle, this.ForeColor, backColor, flags);
            }
        }

        #endregion

        #region OnPaint

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (this.Enabled == false)
            {
                this.DrawDefaultText(e.Graphics);
            }
        }

        #endregion

        #region OnCreateControl

        protected override void OnCreateControl()
        {
            #region ContextMenuStrip

            if (this.ContextMenuStrip == null)
            {
                ContextMenuStrip context = new ContextMenuStrip();
                context.Opened += context_Opened;

                ToolStripMenuItem tsMenuItemDesfazer = new ToolStripMenuItem { Text = "Desfazer" };
                tsMenuItemDesfazer.Click += tsMenuItemDesfazer_Click;
                context.Items.Add(tsMenuItemDesfazer);

                ToolStripMenuItem tsMenuItemRecortar = new ToolStripMenuItem { Text = "Recortar" };
                tsMenuItemRecortar.Click += tsMenuItemRecortar_Click;
                context.Items.Add(tsMenuItemRecortar);

                ToolStripMenuItem tsMenuItemCopiar = new ToolStripMenuItem { Text = "Copiar" };
                tsMenuItemCopiar.Click += tsMenuItemCopiar_Click;
                context.Items.Add(tsMenuItemCopiar);

                ToolStripMenuItem tsMenuItemColar = new ToolStripMenuItem { Text = "Colar" };
                tsMenuItemColar.Click += tsMenuItemColar_Click;
                context.Items.Add(tsMenuItemColar);

                ToolStripMenuItem tsMenuItemExcluir = new ToolStripMenuItem { Text = "Excluir" };
                tsMenuItemExcluir.Click += tsMenuItemExcluir_Click;
                context.Items.Add(tsMenuItemExcluir);

                ToolStripMenuItem tsMenuItemSelecionar = new ToolStripMenuItem { Text = "Selecionar tudo" };
                tsMenuItemSelecionar.Click += tsMenuItemSelecionar_Click;
                context.Items.Add(tsMenuItemSelecionar);

                this.ContextMenuStrip = context;
            }

            #endregion

            base.OnCreateControl();

            if (autoResize == true && this.DesignMode == false)
            {
                base.MinimumSize = base.Size;
                base.Multiline = false;
            }
        }

        #endregion

        #region Permissões

        /// <summary>
        /// Referente permissões, sinalizar chamadas (Habilitado/Desabilitado).
        /// </summary>
        public new bool Enabled
        {
            get
            {
                return base.Enabled;
            }
            set
            {
                if (!PodeVisualisar || !PodeAlterar || !CampoHabilitado)
                    value = false;

                base.Enabled = value;
            }
        }

        /// <summary>
        /// Faz controle de enabled dos campos qdo se pode ou não habilitar.
        /// </summary>
        private Boolean _CampoHabilitado = true;

        /// <summary>
        /// Faz controle de enabled dos campos qdo se pode ou não habilitar.
        /// </summary>
        public Boolean CampoHabilitado
        {
            get { return _CampoHabilitado; }
            set
            {
                _CampoHabilitado = value;
                this.Enabled = value;
            }
        }

        /// <summary>
        /// Referente permissões, sinalizar chamadas (Visualizar).
        /// </summary>
        private Boolean PodeVisualisar = true;

        /// <summary>
        /// Referente permissões, sinalizar chamadas (Alterar).
        /// </summary>
        private Boolean PodeAlterar = true;

        #endregion
    }

    internal class Desfazer
    {
        public string Texto { get; set; }
        public Int32 PosicaoCursor { get; set; }
        public Int32 SelecaoTexto { get; set; }
    }
}
