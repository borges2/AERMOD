using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Design;
using System.Reflection;

namespace AERMOD.LIB.Componentes.Botao
{
    [DefaultEvent("Click"), ToolboxBitmap(typeof(Button))]
    //Remove a opção de Debug
    //[DebuggerStepThroughAttribute()]
    public class ButtonLIB : Control
    {
        #region Propriedades

        /// <summary>
        /// Time para repetição de tecla
        /// </summary>
        private System.Windows.Forms.Timer timerRepeat;        

        /// <summary>
        /// Indica se a tecla esta pressionada
        /// </summary>
        private bool down = false;

        /// <summary>
        /// Delay inicial
        /// </summary>
        private int delayInitial = 900;

        /// <summary>
        /// Get ou set o valor do delay inicial
        /// </summary>
        [DefaultValue(900)]
        public int DelayInitial
        {
            get
            {
                return delayInitial;
            }
            set
            {
                delayInitial = value;
                if (delayInitial < 100)
                    delayInitial = 100;
            }
        }

        /// <summary>
        /// Delay de repetição
        /// </summary>
        private int deleayRepeat = 100;

        /// <summary>
        /// Get ou set o delay de repetição
        /// </summary>
        [DefaultValue(100)]
        public int DelayRepeat
        {
            get
            {
                return deleayRepeat;
            }
            set
            {
                deleayRepeat = value;
                if (deleayRepeat < 100)
                    deleayRepeat = 100;
            }
        }

        /// <summary>
        /// Mouse leave
        /// </summary>
        private bool saiu = true;

        /// <summary>
        /// Get propriedades do objeto
        /// </summary>
        private MouseEventArgs mouseEvent;

        /// <summary>
        /// Get ou set a repetição do click quando se mantem pressionado o botão.
        /// </summary>
        [Description("Get ou set a repetição do click quando se mantem pressionado o botão."), DefaultValue(false)]
        public bool RepeatClick { get; set; }

        /// <summary>
        /// Efeito que sera aplicado ao botao
        /// </summary>
        private EfeitoColor efeitoColor = EfeitoColor.Normal;

        public new bool Enabled
        {
            get { return base.Enabled; }
            set
            {
                if (base.Enabled != value)
                {
                    base.Enabled = value;
                    this.Invalidate();                    
                }
                
                if (value == false && timerRepeat.Enabled)
                {
                    timerRepeat.Enabled = false;
                    down = false;
                }
            }
        }

        private float raio = 7;
        [Description("Get e Set o raio dos cantos."), DefaultValue(7)]
        public float Raio
        {
            get { return raio; }
            set
            {
                if (raio != value)
                {
                    raio = value;
                    this.Invalidate();
                }
            }
        }

        private Color color1 = Color.White;
        [Description("Get e Set a cor da parte superior."), DefaultValue(typeof(Color), "White")]
        public Color Color1
        {
            get { return color1; }
            set
            {
                if (color1 != value)
                {
                    color1 = value;
                    this.Invalidate();
                }
            }
        }

        private Color color2 = Color.Black;
        [Description("Get e Set a cor da parte inferior."), DefaultValue(typeof(Color), "Black")]
        public Color Color2
        {
            get { return color2; }
            set
            {
                if (color2 != value)
                {
                    color2 = value;
                    this.Invalidate();
                }
            }
        }

        private Int32 opacityColor1 = 100;
        [Description("Get e Set a opacidade da primeira cor."), DefaultValue(100)]
        public Int32 OpacityColor1
        {
            get { return opacityColor1; }
            set
            {
                if (opacityColor1 != value)
                {
                    opacityColor1 = value;
                    this.Invalidate();
                }
            }
        }

        private Int32 opacityColor2 = 110;
        [Description("Get e Set a opacidade da segunda cor."), DefaultValue(100)]
        public Int32 OpacityColor2
        {
            get { return opacityColor2; }
            set
            {
                if (opacityColor2 != value)
                {
                    opacityColor2 = value;
                    this.Invalidate();
                }
            }
        }

        private float angleColor = 90F;
        [Description("Get e Set o angulo da cor."), DefaultValue(90F)]
        public float AngleColor
        {
            get { return angleColor; }
            set
            {
                if (angleColor != value)
                {
                    angleColor = value;
                    this.Invalidate();
                }
            }
        }

        private Int32 opacityBorderColor1 = 255;
        [Description("Get e Set a opacidade da primeira cor."), DefaultValue(255)]
        public Int32 OpacityBorderColor1
        {
            get { return opacityBorderColor1; }
            set
            {
                if (opacityBorderColor1 != value)
                {
                    opacityBorderColor1 = value;
                    this.Invalidate();
                }
            }
        }

        private Int32 opacityBorderColor2 = 255;
        [Description("Get e Set a opacidade da segunda cor."), DefaultValue(255)]
        public Int32 OpacityBorderColor2
        {
            get { return opacityBorderColor2; }
            set
            {
                if (opacityBorderColor2 != value)
                {
                    opacityBorderColor2 = value;
                    this.Invalidate();
                }
            }
        }

        
        private string text = string.Empty;
        [DefaultValue(""), Description("Get e Set o texto."), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public override string Text
        {
            get { return text; }
            set
            {
                if (text != value)
                {
                    text = value;
                    this.Invalidate();
                }
            }
        }

        private Font font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        [DefaultValue(typeof(Font), "Microsoft Sans Serif, 8.25"), Description("Get e Set a fonte."), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override Font Font
        {
            get
            {
                return font;
            }
            set
            {
                if (font != value)
                {
                    font = value;
                    this.Invalidate();
                }
            }
        }

        private Image image = null;
        [Description("Get e Set a image."), DefaultValue(typeof(Image), "null")]
        public Image Image
        {
            get { return image; }
            set
            {
                if (image != value)
                {
                    image = value;
                    this.Invalidate();
                }
            }
        }

        private ContentAlignment imagePosition = ContentAlignment.MiddleCenter;
        [Description("Get e Set a posição da imagem."), DefaultValue(ContentAlignment.MiddleCenter)]
        public ContentAlignment ImagePosition
        {
            get { return imagePosition; }
            set
            {
                if (imagePosition != value)
                {
                    imagePosition = value;
                    this.Invalidate();
                }
            }
        }

        private ContentAlignment textAlignment = ContentAlignment.MiddleCenter;
        [Description("Get e Set o alinhamento do texto."), DefaultValue(ContentAlignment.MiddleCenter)]
        public ContentAlignment TextAlignment
        {
            get { return textAlignment; }
            set
            {
                if (textAlignment != value)
                {
                    textAlignment = value;
                    this.Invalidate();
                }
            }
        }

        private Color foreColor = Color.Black;
        [Description("Get e Set a cor do texto."), DefaultValue(typeof(Color), "Black"), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override Color ForeColor
        {
            get
            {
                return foreColor;
            }
            set
            {
                if (foreColor != value)
                {
                    foreColor = value;
                    this.Invalidate();
                }
            }
        }

        private Color colorFocused = Color.Black;
        [Description("Get e Set a cor quando focado."), DefaultValue(typeof(Color), "Black")]
        public Color ColorFocused
        {
            get { return colorFocused; }
            set
            {
                colorFocused = value;
            }
        }

        private Color colorMouseEnter1 = Color.White;
        [Description("Get e Set a primeira cor quando o mouse esta sobre o controle."), DefaultValue(typeof(Color), "White")]
        public Color ColorMouseEnter1
        {
            get { return colorMouseEnter1; }
            set
            {
                colorMouseEnter1 = value;
            }
        }

        private Color colorMouseEnter2 = Color.SkyBlue;
        [Description("Get e Set a segunda cor quando o mouse esta sobre o controle."), DefaultValue(typeof(Color), "SkyBlue")]
        public Color ColorMouseEnter2
        {
            get { return colorMouseEnter2; }
            set
            {
                colorMouseEnter2 = value;
            }
        }

        private Color colorClick1 = Color.Orange;
        [Description("Get e Set a primeira cor quando se clica."), DefaultValue(typeof(Color), "Orange")]
        public Color ColorClick1
        {
            get { return colorClick1; }
            set
            {
                colorClick1 = value;
            }
        }

        private Color colorClick2 = Color.Orange;
        [Description("Get e Set a segunda cor quando se clica"), DefaultValue(typeof(Color), "Orange")]
        public Color ColorClick2
        {
            get { return colorClick2; }
            set
            {
                colorClick2 = value;
            }
        }

        private GradienteMode gradientMode = GradienteMode.Linear;
        [Description("Get e Set o modo do gradiente."), DefaultValue(GradienteMode.Linear)]
        public GradienteMode GradientMode
        {
            get { return gradientMode; }
            set
            {
                if (gradientMode != value)
                {
                    gradientMode = value;
                    this.Invalidate();
                }
            }
        }

        private bool border = true;
        [Description("Get ou set a borda no botão."), DefaultValue(true)]
        public bool Border
        {
            get { return border; }
            set 
            {
                if (border != value)
                {
                    border = value;
                    this.Invalidate();
                }
            }
        }

        #endregion

        public ButtonLIB()
        {
            this.MouseUp += new MouseEventHandler(ButtonLIB_MouseUp);
            this.MouseDown += new MouseEventHandler(ButtonLIB_MouseDown);
            timerRepeat = new System.Windows.Forms.Timer();
            timerRepeat.Tick += new EventHandler(timerproc);
            timerRepeat.Enabled = false;

            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            base.SetStyle(ControlStyles.UserMouse, true);
            base.SetStyle(ControlStyles.StandardClick, true);
            base.SetStyle(ControlStyles.ResizeRedraw, true);
            this.BackColor = Color.Transparent;
        }

        #region Métodos

        /// <summary>
        /// Desenha um retangulo com cantos arredondados
        /// </summary>
        /// <param name="retangulo"></param>
        /// <param name="raio"></param>
        /// <returns></returns>
        private GraphicsPath GetRoundedRect(RectangleF retangulo, float raio)
        {
            //  Se o raio do arco for maior ou igual � metada da largura ou altura do ret�ngulo
            //  retorna um objeto no formato de capsula.
            if ((raio >= (Math.Min(retangulo.Width, retangulo.Height) / 2)))
            {
                return GetCapsule(retangulo);
            }
            else if (raio == 0)
            {
                GraphicsPath gp = new GraphicsPath();
                //Linha superior
                gp.AddRectangle(retangulo);
                gp.CloseFigure();

                return gp;
            }
            else
            {
                float diametro = (raio + raio);
                RectangleF arcRect = new RectangleF(retangulo.Location, new SizeF(diametro, diametro));
                GraphicsPath gp = new GraphicsPath();
                //  Arco superior esquerdo.
                gp.AddArc(arcRect, 180, 90);
                //  Arco superior direito.
                arcRect.X = (retangulo.Right - diametro);
                gp.AddArc(arcRect, 270, 90);
                //  Arco inferior direito.
                arcRect.Y = (retangulo.Bottom - diametro);
                gp.AddArc(arcRect, 0, 90);
                //  Arco inferior esquerdo
                arcRect.X = retangulo.Left;
                gp.AddArc(arcRect, 90, 90);
                gp.CloseFigure();

                return gp;
            }
        }

        /// <summary>
        /// Desenha uma elipse
        /// </summary>
        /// <param name="retangulo"></param>
        /// <returns></returns>
        private GraphicsPath GetCapsule(RectangleF retangulo)
        {
            float diametro;
            RectangleF arcRect;
            GraphicsPath gp = new GraphicsPath();
            try
            {
                if ((retangulo.Width > retangulo.Height))
                {
                    //  Capsula horizontal
                    diametro = retangulo.Height;
                    arcRect = new RectangleF(retangulo.Location, new SizeF(diametro, diametro));
                    gp.AddArc(arcRect, 90, 180);
                    arcRect.X = (retangulo.Right - diametro);
                    gp.AddArc(arcRect, 270, 180);
                }
                else if ((retangulo.Height > retangulo.Width))
                {
                    //  Capsula horizontal
                    diametro = retangulo.Width;
                    arcRect = new RectangleF(retangulo.Location, new SizeF(diametro, diametro));
                    gp.AddArc(arcRect, 180, 180);
                    arcRect.Y = (retangulo.Bottom - diametro);
                    gp.AddArc(arcRect, 0, 180);
                }
                else
                {
                    //  Circulo
                    gp.AddEllipse(retangulo);
                }
            }
            catch (Exception)
            {
                gp.AddEllipse(retangulo);
            }
            finally
            {
                gp.CloseFigure();
            }
            return gp;
        }

        /// <summary>
        /// Invocar click do mouse.
        /// </summary>
        public void PerformClick()
        {
            if (this.CanSelect)
            {
                base.OnClick(EventArgs.Empty);
            }
        }

        /// <summary>
        /// Get ContentAllignment do texto
        /// </summary>
        /// <param name="ca"></param>
        /// <returns></returns>
        private StringFormat GetStringFormatFromContentAllignment(ContentAlignment ca)
        {
            StringFormat format = new StringFormat();
            switch (ca)
            {
                case ContentAlignment.BottomCenter:
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Far;
                    break;
                case ContentAlignment.BottomLeft:
                    format.Alignment = StringAlignment.Near;
                    format.LineAlignment = StringAlignment.Far;
                    break;
                case ContentAlignment.BottomRight:
                    format.Alignment = StringAlignment.Far;
                    format.LineAlignment = StringAlignment.Far;
                    break;
                case ContentAlignment.MiddleCenter:
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Center;
                    break;
                case ContentAlignment.MiddleLeft:
                    format.Alignment = StringAlignment.Near;
                    format.LineAlignment = StringAlignment.Center;
                    break;
                case ContentAlignment.MiddleRight:
                    format.Alignment = StringAlignment.Far;
                    format.LineAlignment = StringAlignment.Center;
                    break;
                case ContentAlignment.TopCenter:
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Near;
                    break;
                case ContentAlignment.TopLeft:
                    format.Alignment = StringAlignment.Near;
                    format.LineAlignment = StringAlignment.Near;
                    break;
                case ContentAlignment.TopRight:
                    format.Alignment = StringAlignment.Far;
                    format.LineAlignment = StringAlignment.Near;
                    break;
            }
            return format;
        }

        /// <summary>
        /// Get ContentAllignment da image
        /// </summary>
        /// <returns></returns>
        private RectangleF GetPositionImage()
        {
            Rectangle rectImage = new Rectangle();
            switch (imagePosition)
            {
                case ContentAlignment.BottomCenter:
                    {
                        rectImage = new Rectangle(((this.Width) / 2) - (image.Size.Width / 2), this.Height - 4 - image.Size.Height, image.Size.Width, image.Size.Height);
                    }
                    break;
                case ContentAlignment.BottomLeft:
                    {
                        rectImage = new Rectangle(4, this.Height - 4 - image.Size.Height, image.Size.Width, image.Size.Height);
                    }
                    break;
                case ContentAlignment.BottomRight:
                    {
                        rectImage = new Rectangle(this.Width - 4 - image.Size.Width, this.Height - 4 - image.Size.Height, image.Size.Width, image.Size.Height);
                    }
                    break;
                case ContentAlignment.MiddleCenter:
                    {
                        rectImage = new Rectangle(((this.Width) / 2) - (image.Size.Width / 2), ((this.Height) / 2) - (image.Size.Height / 2), image.Size.Width, image.Size.Height);
                    }
                    break;
                case ContentAlignment.MiddleLeft:
                    {
                        rectImage = new Rectangle(4, ((this.Height) / 2) - (image.Size.Height / 2), image.Size.Width, image.Size.Height);
                    }
                    break;
                case ContentAlignment.MiddleRight:
                    {
                        rectImage = new Rectangle(this.Width - 4 - image.Size.Width, ((this.Height) / 2) - (image.Size.Height / 2), image.Size.Width, image.Size.Height);
                    }
                    break;
                case ContentAlignment.TopCenter:
                    {
                        rectImage = new Rectangle(((this.Width) / 2) - (image.Size.Width / 2), 4, image.Size.Width, image.Size.Height);
                    }
                    break;
                case ContentAlignment.TopLeft:
                    {
                        rectImage = new Rectangle(4, 4, image.Size.Width, image.Size.Height);
                    }
                    break;
                case ContentAlignment.TopRight:
                    {
                        rectImage = new Rectangle(this.Width - 4 - image.Size.Width, 4, image.Size.Width, image.Size.Height);
                    }
                    break;
            }

            return rectImage;
        }

        /// <summary>
        /// Set a imagem em escala cinza
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        private Bitmap SetGrayscale(Image image)
        {
            Bitmap temp = (Bitmap)image;
            Bitmap bmap = (Bitmap)temp.Clone();
            Color c;
            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    c = bmap.GetPixel(i, j);
                    if (c.Name != "ffffff")
                    {
                        byte gray = (byte)(.299 * c.R + .587 * c.G + .114 * c.B);
                        bmap.SetPixel(i, j, Color.FromArgb(gray, gray, gray));
                    }
                    else
                    {
                        bmap.SetPixel(i, j, c);
                    }
                }
            }
            return (Bitmap)bmap.Clone();
        }

        #endregion

        #region Eventos

        private void timerproc(object o1, EventArgs e1)
        {
            timerRepeat.Interval = deleayRepeat;
            if (down)
            {
                base.OnClick(EventArgs.Empty);
            }
        }        

        private void ButtonLIB_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (RepeatClick == true)
            {
                mouseEvent = e;
                timerRepeat.Interval = delayInitial;
                timerRepeat.Enabled = true;
                down = true;
            }
            efeitoColor = EfeitoColor.Click;
            this.Invalidate();
        }

        private void ButtonLIB_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (RepeatClick == true)
            {
                timerRepeat.Enabled = false;
                down = false;
            }

            if (saiu == false)
            {
                efeitoColor = EfeitoColor.MouseEnter;
            }
            this.Invalidate();

            //Tratamento para quando se clica e retira o mouse de cima do controle
            base.OnClick(e);
        }
        
        protected override void OnClick(EventArgs e)
        {            
            //base.OnClick(e);
        }

        private void ButtonLIB_Deactivate(object sender, EventArgs e)
        {
            if (RepeatClick == true && down == true)
            {
                this.OnMouseUp(mouseEvent);
            }
        }        

        protected override void OnEnter(EventArgs e)
        {
            this.Invalidate();
            base.OnEnter(e);
        }

        protected override void OnLeave(EventArgs e)
        {
            if (saiu == true)
            {
                efeitoColor = EfeitoColor.Normal;
            }
            this.Invalidate();
            base.OnLeave(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            this.Refresh();
            base.OnLostFocus(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            saiu = false;
            efeitoColor = EfeitoColor.MouseEnter;
            this.Invalidate();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            saiu = true;
            efeitoColor = EfeitoColor.Normal;
            this.Invalidate();
            base.OnMouseLeave(e);
        }

        protected override void OnParentChanged(EventArgs e)
        {
            if (this.FindForm() != null)
            {
                this.FindForm().Deactivate += new EventHandler(ButtonLIB_Deactivate);
            }

            base.OnParentChanged(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyData == Keys.Space)
            {
                efeitoColor = EfeitoColor.Click;
                this.Invalidate();
            }

            if (e.KeyData == Keys.Enter)
            {
                this.PerformClick();
            }

            base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (e.KeyData == Keys.Space)
            {
                if (saiu == true)
                {
                    efeitoColor = EfeitoColor.Normal;
                }
                else
                {
                    efeitoColor = EfeitoColor.MouseEnter;
                }
                this.Invalidate();
            }

            base.OnKeyUp(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (this.Width == 0 || this.Height == 0)
            {
                base.OnPaint(e);
                return;
            }

            #region Create variaveis e set values

            StringFormat strFormat = GetStringFormatFromContentAllignment(textAlignment);
            RectangleF rectangleBordaPreta;
            RectangleF rectangleBordaBranca;
            GraphicsPath gpPreto;
            GraphicsPath gpBranco;

            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

            rectangleBordaPreta = new RectangleF(0, 0, this.Width - 1, this.Height - 1);
            gpPreto = GetRoundedRect(rectangleBordaPreta, raio);

            rectangleBordaBranca = new RectangleF(1, 1, this.Width - 3, this.Height - 3);
            gpBranco = GetRoundedRect(rectangleBordaBranca, raio);

            #endregion

            #region Draw Fill

            Color cor1 = color1;
            Color cor2 = color2;

            if (efeitoColor == EfeitoColor.Click)
            {
                cor1 = colorClick1;
                cor2 = colorClick2;
            }
            else if (efeitoColor == EfeitoColor.MouseEnter)
            {
                cor1 = colorMouseEnter1;
                cor2 = colorMouseEnter2;
            }

            if (gradientMode == GradienteMode.Linear)
            {
                LinearGradientBrush myLinearGradientBrush = new LinearGradientBrush(rectangleBordaPreta,
                Color.FromArgb(opacityColor1, cor1),
                Color.FromArgb(opacityColor2, cor2), angleColor);
                if (border || efeitoColor == EfeitoColor.MouseEnter)
                {
                    if (Color.FromArgb(opacityColor1, cor1) != Color.FromArgb(opacityColor2, cor2))
                    {
                        e.Graphics.FillPath(myLinearGradientBrush, gpBranco);
                    }
                    else
                    {
                        e.Graphics.FillPath(new SolidBrush(Color.FromArgb(opacityColor1, cor1)), gpBranco);
                    }
                }
            }
            else if (gradientMode == GradienteMode.Radial)
            {
                PathGradientBrush pgb = new PathGradientBrush(gpBranco);
                pgb.CenterPoint = new PointF((this.ClientRectangle.Width - 3) / 2, (this.ClientRectangle.Height - 3) / 2);
                pgb.CenterColor = Color.FromArgb(opacityColor1, cor1);
                pgb.SurroundColors = new Color[] { Color.FromArgb(opacityColor2, cor2) };

                if (border || efeitoColor == EfeitoColor.MouseEnter)
                {
                    e.Graphics.FillPath(pgb, gpBranco);
                }
                pgb.Dispose();
            }
            else
            {
                PathGradientBrush pgb = new PathGradientBrush(gpBranco);

                pgb.CenterPoint = new PointF((this.ClientRectangle.Width - 3) / 2, (this.ClientRectangle.Height - 3) / 2);
                pgb.CenterColor = Color.FromArgb(opacityColor1, cor1);
                pgb.SurroundColors = new Color[] { Color.FromArgb(opacityColor2, cor2) };
                pgb.SetBlendTriangularShape(.5f, 1.0f);
                pgb.FocusScales = new PointF(0f, 0f);
                if (border || efeitoColor == EfeitoColor.MouseEnter)
                {
                    e.Graphics.FillPath(pgb, gpBranco);
                }
                pgb.Dispose();
            }

            #endregion

            #region Draw Image and Text

            strFormat.Trimming = StringTrimming.EllipsisCharacter;

            if (image != null)
            {
                if (Enabled == true)
                {
                    e.Graphics.DrawImage(image, GetPositionImage());
                }
                else
                {
                    var posicao = GetPositionImage();                    
                    ControlPaint.DrawImageDisabled(e.Graphics, image, (Int32)posicao.X, (Int32)posicao.Y, Color.Transparent);                    
                }
            }

            if (this.Focused)
            {
                RectangleF rtF = new RectangleF(2, 2, this.Width - 5, this.Height - 5);
                var gpF = GetRoundedRect(rtF, raio);
                Pen p = new Pen(colorFocused);
                p.DashStyle = DashStyle.Dash;
                e.Graphics.DrawPath(p, gpF);
            }

            strFormat.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;

            Rectangle rect = new Rectangle(0, 3, this.Width, this.Height - 6);  // this.ClientRectangle;//.Height > 35 ? Rectangle.Inflate(this.ClientRectangle,-7,-7) : this.ClientRectangle;
            //if (this.ClientRectangle.Height < 35)
            //{
            //    rect.Y += 1;
            //}
            //rect.Height -= 4;

            MethodInfo methodPaint = typeof(ControlPaint).GetMethod("CreateTextFormatFlags", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
            TextFormatFlags flags = (TextFormatFlags)methodPaint.Invoke(null, new object[] { this, textAlignment, false, true });

            if (Enabled == true)
            {
                //e.Graphics.DrawString(text, font, new SolidBrush(foreColor), rect, strFormat);

                TextRenderer.DrawText(e.Graphics, text, font, rect, foreColor, flags);

                if (border)
                {
                    e.Graphics.DrawPath(new Pen(Color.FromArgb(opacityBorderColor1, Color.Black)), gpPreto);
                }
            }
            else
            {
                //e.Graphics.DrawString(text, font, new SolidBrush(Color.Gray), rect, strFormat);

                MethodInfo methodRender = typeof(TextRenderer).GetMethod("DisabledTextColor", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
                Color disabledTextForeColor = (Color)methodRender.Invoke(null, new object[] { this.BackColor });

                TextRenderer.DrawText(e.Graphics, text, font, rect, disabledTextForeColor, flags);
                if (border)
                {
                    e.Graphics.DrawPath(new Pen(Color.Gray), gpPreto);
                }
            }

            if (border)
            {
                e.Graphics.DrawPath(new Pen(Color.FromArgb(opacityBorderColor2, Color.White)), gpBranco);
            }

            #endregion
        }

        protected override bool ProcessMnemonic(char charCode)
        {
            if (CanSelect && IsMnemonic(charCode, text) && (Control.ModifierKeys == Keys.Alt))
            {
                this.PerformClick();
                return true;
            }

            return base.ProcessMnemonic(charCode); 
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.timerRepeat.Dispose();
            }
            base.Dispose(disposing);
        }
    }

        #endregion

    /// <summary>
    /// Estilo do grandiente
    /// </summary>
    public enum GradienteMode
    {
        Linear,
        Radial,
        TriangularBlendShape
    }

    /// <summary>
    /// Efeito do botão
    /// </summary>
    public enum EfeitoColor
    {
        Normal,
        MouseEnter,
        Click
    }
}
