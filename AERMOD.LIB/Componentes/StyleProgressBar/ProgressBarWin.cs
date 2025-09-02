using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AERMOD.LIB.Componentes.StyleProgressBar
{
    public class ProgressBarWin : ProgressBar
    {
        private Cores.Natural fundo = Cores.Natural.A700azul;
        private int maqe = 0;
        private Pen pen = new Pen(Color.FromArgb(0xba, 0xba, 0xba));
        private int rest;
        private Timer timer = new Timer();

        public ProgressBarWin()
        {
            this.timer.Tick += new EventHandler(this.Timer_Tick);
            this.timer.Interval = 1;
            this.timer.Enabled = true;
            this.timer.Start();
            this.BackColor = SystemColors.ControlLight;
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.MarqueeAnimationSpeed = 1;
            base.Width = 100;
            base.Height = 5;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (base.Style != ProgressBarStyle.Marquee)
            {
                if (base.Maximum > 0)
                {
                    int width = (int)(e.ClipRectangle.Width * (((double)base.Value) / ((double)base.Maximum)));
                    e.Graphics.FillRectangle(new SolidBrush(((int)this.fundo).ToColor()), 0, 0, width, e.ClipRectangle.Height);
                    e.Graphics.FillRectangle((this.fundo != Cores.Natural.Defaul) ? new SolidBrush(SystemColors.ControlLight) : new SolidBrush(Color.FromArgb(240, 240, 240)), width, 0, e.ClipRectangle.Width, e.ClipRectangle.Height);
                    e.Graphics.DrawLine(this.pen, new System.Drawing.Point(width, 0), new System.Drawing.Point(base.Width - 1, 0));
                    if (base.Value <= ((0x63 * base.Maximum) / 100))
                    {
                        e.Graphics.DrawLine(this.pen, new System.Drawing.Point(base.Width - 1, 0), new System.Drawing.Point(base.Width - 1, base.Height - 1));
                    }
                    e.Graphics.DrawLine(this.pen, new System.Drawing.Point(base.Width - 1, base.Height - 1), new System.Drawing.Point(width, base.Height - 1));
                    if (base.Value == 0)
                    {
                        e.Graphics.DrawLine(this.pen, new System.Drawing.Point(0, 0), new System.Drawing.Point(0, base.Height - 1));
                    }
                }
            }
            else
            {
                int num2 = (int)(e.ClipRectangle.Width * 0.3);
                this.rest = num2;
                e.Graphics.FillRectangle((this.fundo != Cores.Natural.Defaul) ? new SolidBrush(SystemColors.ControlLight) : new SolidBrush(Color.FromArgb(240, 240, 240)), 0, 0, base.Width, e.ClipRectangle.Height);
                e.Graphics.FillRectangle(new SolidBrush(((int)this.fundo).ToColor()), this.maqe, 0, num2, e.ClipRectangle.Height);
                e.Graphics.FillRectangle((this.fundo != Cores.Natural.Defaul) ? new SolidBrush(SystemColors.ControlLight) : new SolidBrush(Color.FromArgb(240, 240, 240)), num2 + this.maqe, 0, e.ClipRectangle.Width, e.ClipRectangle.Height);
                if (this.maqe > 1)
                {
                    e.Graphics.DrawLine(this.pen, new System.Drawing.Point(0, 0), new System.Drawing.Point(this.maqe - 1, 0));
                    e.Graphics.DrawLine(this.pen, new System.Drawing.Point(0, base.Height - 1), new System.Drawing.Point(this.maqe - 1, base.Height - 1));
                    e.Graphics.DrawLine(this.pen, new System.Drawing.Point(0, 0), new System.Drawing.Point(0, base.Height - 1));
                }
                if ((this.maqe + this.rest) <= base.Width)
                {
                    e.Graphics.DrawLine(this.pen, new System.Drawing.Point(this.maqe + this.rest, 0), new System.Drawing.Point(base.Width - 1, 0));
                    e.Graphics.DrawLine(this.pen, new System.Drawing.Point(this.maqe + this.rest, base.Height - 1), new System.Drawing.Point(base.Width - 1, base.Height - 1));
                    e.Graphics.DrawLine(this.pen, new System.Drawing.Point(base.Width - 1, 0), new System.Drawing.Point(base.Width - 1, base.Height - 1));
                }
            }
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            base.SetBoundsCore(x, y, width, height, specified);
        }

        protected override void Dispose(bool disposing)
        {
            this.timer.Stop();
            this.timer.Dispose();

            base.Dispose(disposing);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (base.Style == ProgressBarStyle.Marquee && this.DesignMode == false)
            {
                this.maqe += this.MarqueeAnimationSpeed;
                if (this.maqe > base.Width)
                {
                    this.maqe = this.rest * -1;
                }
                base.Invalidate(false);
            }
        }

        [Browsable(false)]
        public int Depth { get; set; }

        [DefaultValue(1)]
        public int MarqueeAnimationSpeed
        {
            get
            {
                return base.MarqueeAnimationSpeed;
            }
            set
            {
                base.MarqueeAnimationSpeed = value;
            }
        }

        [Category("Aparência"), DefaultValue(typeof(Cores.Natural), "A700azul"), Description("Cor enche o progresso deste controle.")]
        public Cores.Natural ProgressColor
        {
            get
            {
                return this.fundo;
            }
            set
            {
                this.fundo = value;
                base.Invalidate();
            }
        }
    }
}
