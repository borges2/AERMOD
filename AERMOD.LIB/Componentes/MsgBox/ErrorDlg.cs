using global::System.Windows.Forms;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;

namespace AERMOD.LIB.Componentes.MsgBox
{
    public class ErrorDlg : Form
    {
        private TableLayoutPanel buttonTableLayoutPanel;
        private Button okBtn;
        private Bitmap collapseImage;
        private TextBox details;
        private DetailsButton detailsBtn;
        private bool detailsButtonExpanded;
        private Bitmap expandImage;
        private Label lblMessage;
        private TableLayoutPanel overarchingTableLayoutPanel;
        private PictureBox pictureBox;
        private TableLayoutPanel pictureLabelTableLayoutPanel;

        private Image imagem = null;

        public Image Imagem
        {
            get 
            { 
                if(imagem == null)
                {
                    imagem = SystemIcons.Error.ToBitmap();
                }

                return imagem; 
            }
            set 
            { 
                imagem = value;
                this.pictureBox.Image = imagem;
            }
        }


        public ErrorDlg()
        {
            this.expandImage = new Bitmap(typeof(ThreadExceptionDialog), "down.bmp");
            this.expandImage.MakeTransparent();
            if (IsScalingRequired())
            {
                ScaleBitmapLogicalToDevice(ref this.expandImage, 0);
            }
            this.collapseImage = new Bitmap(typeof(ThreadExceptionDialog), "up.bmp");
            this.collapseImage.MakeTransparent();
            if (IsScalingRequired())
            {
                ScaleBitmapLogicalToDevice(ref this.collapseImage, 0);
            }
            this.InitializeComponent();
            this.KeyPreview = true;
            foreach (Control control in base.Controls)
            {
                if (SupportsUseCompatibleTextRendering(control))
                {
                    UseCompatibleTextRenderingInt(control, false);
                }
            }
            this.pictureBox.Image = this.Imagem;
            this.detailsBtn.Text = " " + GetString("ExDlgShowDetails");
            this.details.AccessibleName = GetString("ExDlgDetailsText");
            this.okBtn.Text = GetString("ExDlgOk");
            this.detailsBtn.Image = this.expandImage;
        }

        private void InitializeComponent()
        {
            if (IsRTLResources)
            {
                this.RightToLeft = RightToLeft.Yes;
            }
            this.detailsBtn = new DetailsButton(this);
            this.overarchingTableLayoutPanel = new TableLayoutPanel();
            this.buttonTableLayoutPanel = new TableLayoutPanel();
            this.okBtn = new Button();
            this.pictureLabelTableLayoutPanel = new TableLayoutPanel();
            this.lblMessage = new Label();
            this.pictureBox = new PictureBox();
            this.details = new TextBox();
            this.overarchingTableLayoutPanel.SuspendLayout();
            this.buttonTableLayoutPanel.SuspendLayout();
            this.pictureLabelTableLayoutPanel.SuspendLayout();
            ((ISupportInitialize)this.pictureBox).BeginInit();
            base.SuspendLayout();
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new Point(0x49, 30);
            this.lblMessage.Margin = new Padding(3, 30, 3, 0);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new Size(0xd0, 0x2b);
            this.lblMessage.TabIndex = 0;
            this.pictureBox.Location = new Point(3, 3);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new Size(0x40, 0x40);
            this.pictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
            this.pictureBox.TabIndex = 5;
            this.pictureBox.TabStop = false;
            this.pictureBox.AutoSize = true;
            this.detailsBtn.ImageAlign = ContentAlignment.MiddleLeft;
            this.detailsBtn.Location = new Point(3, 3);
            this.detailsBtn.Margin = new Padding(12, 3, 0x1d, 3);
            this.detailsBtn.Name = "detailsBtn";
            this.detailsBtn.Size = new Size(100, 0x17);
            this.detailsBtn.TabIndex = 4;
            this.detailsBtn.Click += new EventHandler(this.DetailsClick);
            this.overarchingTableLayoutPanel.AutoSize = true;
            this.overarchingTableLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.overarchingTableLayoutPanel.ColumnCount = 1;
            this.overarchingTableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
            this.overarchingTableLayoutPanel.Controls.Add(this.buttonTableLayoutPanel, 0, 1);
            this.overarchingTableLayoutPanel.Controls.Add(this.pictureLabelTableLayoutPanel, 0, 0);
            this.overarchingTableLayoutPanel.Location = new Point(1, 0);
            this.overarchingTableLayoutPanel.MinimumSize = new Size(0x117, 50);
            this.overarchingTableLayoutPanel.Name = "overarchingTableLayoutPanel";
            this.overarchingTableLayoutPanel.RowCount = 2;
            this.overarchingTableLayoutPanel.RowStyles.Add(new RowStyle());
            this.overarchingTableLayoutPanel.RowStyles.Add(new RowStyle());
            this.overarchingTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
            this.overarchingTableLayoutPanel.Size = new Size(290, 0x6c);
            this.overarchingTableLayoutPanel.TabIndex = 6;
            this.buttonTableLayoutPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.buttonTableLayoutPanel.AutoSize = true;
            this.buttonTableLayoutPanel.ColumnCount = 3;
            this.overarchingTableLayoutPanel.SetColumnSpan(this.buttonTableLayoutPanel, 2);
            this.buttonTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            this.buttonTableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
            this.buttonTableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
            this.buttonTableLayoutPanel.Controls.Add(this.okBtn, 2, 0);
            this.buttonTableLayoutPanel.Controls.Add(this.detailsBtn, 0, 0);
            this.buttonTableLayoutPanel.Location = new Point(0, 0x4f);
            this.buttonTableLayoutPanel.Name = "buttonTableLayoutPanel";
            this.buttonTableLayoutPanel.RowCount = 1;
            this.buttonTableLayoutPanel.RowStyles.Add(new RowStyle());
            this.buttonTableLayoutPanel.Size = new Size(290, 0x1d);
            this.buttonTableLayoutPanel.TabIndex = 8;
            this.okBtn.AutoSize = true;
            this.okBtn.DialogResult = DialogResult.Cancel;
            this.okBtn.Location = new Point(0xd4, 3);
            this.okBtn.Margin = new Padding(0, 3, 3, 3);
            this.okBtn.Name = "cancelBtn";
            this.okBtn.Size = new Size(0x4b, 0x17);
            this.okBtn.TabIndex = 2;
            this.okBtn.Click += new EventHandler(this.OnButtonClick);
            this.pictureLabelTableLayoutPanel.AutoSize = true;
            this.pictureLabelTableLayoutPanel.AutoSizeMode = AutoSizeMode.GrowOnly;
            this.pictureLabelTableLayoutPanel.ColumnCount = 2;
            this.pictureLabelTableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
            this.pictureLabelTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            this.pictureLabelTableLayoutPanel.Controls.Add(this.lblMessage, 1, 0);
            this.pictureLabelTableLayoutPanel.Controls.Add(this.pictureBox, 0, 0);
            this.pictureLabelTableLayoutPanel.Dock = DockStyle.Fill;
            this.pictureLabelTableLayoutPanel.Location = new Point(3, 3);
            this.pictureLabelTableLayoutPanel.Name = "pictureLabelTableLayoutPanel";
            this.pictureLabelTableLayoutPanel.RowCount = 1;
            this.pictureLabelTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.pictureLabelTableLayoutPanel.Size = new Size(0x11c, 0x49);
            this.pictureLabelTableLayoutPanel.TabIndex = 4;
            this.details.Location = new Point(4, 0x72);
            this.details.Multiline = true;
            this.details.Name = "details";
            this.details.ReadOnly = true;
            this.details.ScrollBars = ScrollBars.Vertical;
            this.details.Size = new Size(0x111, 100);
            this.details.TabIndex = 3;
            this.details.TabStop = false;
            this.details.Visible = false;
            base.AcceptButton = this.okBtn;
            base.CancelButton = this.okBtn;
            this.AutoSize = true;
            base.AutoScaleMode = AutoScaleMode.Font;
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.ClientSize = new Size(0x12b, 0x71);
            base.Controls.Add(this.details);
            base.Controls.Add(this.overarchingTableLayoutPanel);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "Form1";
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterParent;
            this.overarchingTableLayoutPanel.ResumeLayout(false);
            this.overarchingTableLayoutPanel.PerformLayout();
            this.buttonTableLayoutPanel.ResumeLayout(false);
            this.buttonTableLayoutPanel.PerformLayout();
            this.pictureLabelTableLayoutPanel.ResumeLayout(false);
            ((ISupportInitialize)this.pictureBox).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void DetailsClick(object sender, EventArgs devent)
        {
            int num = this.details.Height + 8;
            if (this.details.Visible)
            {
                this.detailsBtn.Image = this.expandImage;
                this.detailsButtonExpanded = false;
                base.Height -= num;
            }
            else
            {
                this.detailsBtn.Image = this.collapseImage;
                this.detailsButtonExpanded = true;
                this.details.Width = this.overarchingTableLayoutPanel.Width - this.details.Margin.Horizontal;
                base.Height += num;
            }
            this.details.Visible = !this.details.Visible;

            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
        }

        private void OnButtonClick(object s, EventArgs e)
        {
            base.DialogResult = ((Button)s).DialogResult;
            base.Close();
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            if (base.Visible)
            {
                using (Graphics graphics = base.CreateGraphics())
                {
                    int num3 = (int)Math.Ceiling((double)MeasureTextHelper.MeasureText(graphics, this.detailsBtn.Text, this.detailsBtn.Font).Width);
                    num3 += this.detailsBtn.Image.Width;
                    this.detailsBtn.Width = (int)Math.Ceiling((double)(num3 * (1.4f)));
                    this.detailsBtn.Height = this.okBtn.Height;
                }
                int x = this.details.Location.X;
                int y = (this.detailsBtn.Location.Y + this.detailsBtn.Height) + this.detailsBtn.Margin.Bottom;
                for (Control control = this.detailsBtn.Parent; (control != null) && !(control is Form); control = control.Parent)
                {
                    y += control.Location.Y;
                }
                this.details.Location = new Point(x, y);
                if (this.details.Visible)
                {
                    this.DetailsClick(this.details, EventArgs.Empty);
                }
            }
            this.okBtn.Focus();
        }

        /// <summary>
        /// Detalhes do erro
        /// </summary>
        public string Details
        {
            set
            {
                this.details.Text = value;
            }
        }

        public bool DetailsButtonExpanded => this.detailsButtonExpanded;

        private static bool IsRTLResources => (GetString2("RTL") != "RTL_False");

        /// <summary>
        /// Menssagem
        /// </summary>
        public string Message
        {
            set
            {
                this.lblMessage.Text = value;
            }
        }

        public Exception Exception
        {
            set
            {
                Exception ex = value;
                if (ex != null)
                {
                    this.details.Text = AERMOD.LIB.Desenvolvimento.Log.EscreverException(ex);
                }
            }
        }

        private string GetString(string sr)
        {
            Type myType = Type.GetType("System.Windows.Forms.SR, System.Windows.Forms, Version = 2.0.0.0, Culture = neutral, PublicKeyToken = b77a5c561934e089");

            MethodInfo[] methods = myType.GetMethods(BindingFlags.Public | BindingFlags.Static);
            foreach (MethodInfo method in methods)
            {
                if (method.Name == "GetString")
                {
                    if (method.GetParameters().Length == 1)
                    {
                        return (string)method.Invoke(null, new object[] { sr });
                    }
                }
            }

            return string.Empty;
        }

        private static string GetString2(string sr)
        {
            Type myType = Type.GetType("System.Windows.Forms.SR, System.Windows.Forms, Version = 2.0.0.0, Culture = neutral, PublicKeyToken = b77a5c561934e089");

            MethodInfo[] methods = myType.GetMethods(BindingFlags.Public | BindingFlags.Static);
            foreach (MethodInfo method in methods)
            {
                if (method.Name == "GetString")
                {
                    if (method.GetParameters().Length == 1)
                    {
                        return (string)method.Invoke(null, new object[] { sr });
                    }
                }
            }

            return string.Empty;
        }

        private bool IsScalingRequired()
        {
            Type myType = Type.GetType("System.Windows.Forms.DpiHelper, System.Windows.Forms, Version = 2.0.0.0, Culture = neutral, PublicKeyToken = b77a5c561934e089");
            PropertyInfo pi = myType.GetProperty("IsScalingRequired", BindingFlags.Static | BindingFlags.Public);

            return Convert.ToBoolean(pi.GetValue(null, null));
        }

        private void ScaleBitmapLogicalToDevice(ref Bitmap logicalBitmap, int deviceDpi = 0)
        {
            Type myType = Type.GetType("System.Windows.Forms.DpiHelper, System.Windows.Forms, Version = 2.0.0.0, Culture = neutral, PublicKeyToken = b77a5c561934e089");
            var method = myType.GetMethod("ScaleBitmapLogicalToDevice", BindingFlags.Static | BindingFlags.Public);
            method.Invoke(null, new object[] { logicalBitmap, deviceDpi });
        }

        private bool SupportsUseCompatibleTextRendering(Control control)
        {
            Type myType = typeof(Control);
            PropertyInfo pi = myType.GetProperty("SupportsUseCompatibleTextRendering", BindingFlags.NonPublic | BindingFlags.Instance);
            return Convert.ToBoolean(pi.GetValue(control, null));
        }

        private void UseCompatibleTextRenderingInt(Control control, bool value)
        {
            Type myType = typeof(Control);
            PropertyInfo pi = myType.GetProperty("UseCompatibleTextRenderingInt", BindingFlags.NonPublic | BindingFlags.Instance);
            pi.SetValue(control, value, null);
        }
    }

    internal static class MeasureTextHelper
    {
        public static TextFormatFlags GetTextRendererFlags() =>
            (TextFormatFlags.PreserveGraphicsTranslateTransform | TextFormatFlags.PreserveGraphicsClipping);

        public static SizeF MeasureText(Graphics g, string text, Font font) =>
            MeasureTextSimple(g, text, font, new SizeF(0f, 0f));

        public static SizeF MeasureText(Graphics g, string text, Font font, SizeF size)
        {
            TextFormatFlags flags = ((GetTextRendererFlags() | TextFormatFlags.LeftAndRightPadding) | TextFormatFlags.WordBreak) | TextFormatFlags.NoFullWidthCharacterBreak;
            return (SizeF)TextRenderer.MeasureText(g, text, font, Size.Ceiling(size), flags);
        }

        public static SizeF MeasureText(Graphics g, string text, Font font, int width) =>
            MeasureText(g, text, font, new SizeF((float)width, 999999f));

        public static SizeF MeasureTextSimple(Graphics g, string text, Font font, SizeF size)
        {
            return (SizeF)TextRenderer.MeasureText(g, text, font, Size.Ceiling(size), GetTextRendererFlags());
        }
    }

    [ToolboxItem(false)]
    internal class DetailsButton : Button
    {
        private ErrorDlg parent;

        public DetailsButton(ErrorDlg form)
        {
            this.parent = form;
        }

        protected override AccessibleObject CreateAccessibilityInstance()
        {
            return base.CreateAccessibilityInstance();
        }

        public bool Expanded => this.parent.DetailsButtonExpanded;
    }
}
