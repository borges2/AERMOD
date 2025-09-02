using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Globalization;
using System.Reflection;
using System.Collections.Generic;

namespace Netsof.LIB.Componentes.Calendario
{
    public partial class MonthCalendario : UserControl
    {
        #region Propriedades

        private bool execute_SelectionChanged = true;

        private DateTime currentDate = DateTime.Today;

        /// <summary>
        /// Get ou set a data atual (Data selecionada)
        /// </summary>
        public DateTime CurrentDate
        {
            get { return currentDate; }
            set
            {
                if (currentDate != value)
                {
                    currentDate = value;

                    PreencherCalendario();
                    AtivarData(currentDate.Day);

                    CurrentDateEventArgs eventChanged = new CurrentDateEventArgs();
                    eventChanged.CurrentDate = currentDate;
                    OnCurrentDateChanged(eventChanged);
                }
            }
        }

        private List<DateTime> listAnualDatas = new List<DateTime>();

        /// <summary>
        /// Get ou set o dia, mês e ano a ser marcado no calendario(dd-MM-aaaa)
        /// </summary>
        public List<DateTime> ListAnualDatas
        {
            get { return listAnualDatas; }
            set
            {
                listAnualDatas = value;
            }
        }

        private List<DateTime> listMesDatas = new List<DateTime>();

        /// <summary>
        /// Get ou set o dia e o mês a ser marcado no calendario (dd-MM)
        /// </summary>
        public List<DateTime> ListMesDatas
        {
            get { return listMesDatas; }
            set { listMesDatas = value; }
        }

        private List<DateTime> listDiaDatas = new List<DateTime>();

        /// <summary>
        /// Get ou set a o dia a ser marcado no calendario (dd)
        /// </summary>
        public List<DateTime> ListDiaDatas
        {
            get { return listDiaDatas; }
            set { listDiaDatas = value; }
        }

        /// <summary>
        /// Melhora o desempenho na pintura do form.
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        /// <summary>
        /// Utilizado para a janela de ano
        /// </summary>
        Int32 anoSelecionado = 0;

        #endregion

        #region Eventos próprios

        public event CurrentDateEventHandler CurrentDateChanged;

        public virtual void OnCurrentDateChanged(CurrentDateEventArgs e)
        {
            if (CurrentDateChanged != null)
            {
                CurrentDateChanged(this, e);
            }
        }

        #endregion

        public MonthCalendario()
        {
            InitializeComponent();
           
            SetStyleEx(btnMesEsquerdo, ControlStyles.Selectable, false);
            SetStyleEx(btnMesDireito, ControlStyles.Selectable, false);
            SetStyleEx(btnAnoEsquerdo, ControlStyles.Selectable, false);
            SetStyleEx(btnAnoDireito, ControlStyles.Selectable, false);

            dgvDias.Rows.Add(6);

            lbHoje.Text = $"Hoje: {DateTime.Today.ToString("dd/MM/yyyy")}";
            PreencherCalendario();
            AtivarData(currentDate.Day);
        }

        #region Metodos

        public static void SetStyleEx(Control control, ControlStyles style, bool value)
        {
            // Prevent flickering, only if our assembly 
            // has reflection permission. 
            Type type = control.GetType();
            BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance;
            MethodInfo method = type.GetMethod("SetStyle", flags);

            if (method != null)
            {
                object[] param = { style, value };
                method.Invoke(control, param);
            }
        }

        private void AtivarData(Int32 dia)
        {
            for (int i = 0; i < 6; i++)
            {
                for (int y = 0; y < 7; y++)
                {
                    if (Convert.ToInt32(dgvDias.Rows[i].Cells[y].Value) == dia && Convert.ToInt32(dgvDias.Rows[i].Cells[y].Tag) == 1)
                    {
                        execute_SelectionChanged = false;
                        dgvDias.CurrentCell = dgvDias.Rows[i].Cells[y];
                        execute_SelectionChanged = true;
                        return;
                    }
                }
            }
        }

        private void PreencherCalendario()
        {
            Int32 coluna = (Int32)new DateTime(currentDate.Year, currentDate.Month, 1).DayOfWeek;
            Int32 diasMesAnterior = 0;

            if ((currentDate.Month == DateTime.MinValue.Month && currentDate.Year == DateTime.MinValue.Year) == false)
            {
                DateTime mesAnterior = currentDate.AddMonths(-1);
                diasMesAnterior = DateTime.DaysInMonth(mesAnterior.Year, mesAnterior.Month);
            }

            Int32 diasMes = DateTime.DaysInMonth(currentDate.Year, currentDate.Month);
            Int32 dia = 1;

            CultureInfo culture = new CultureInfo("pt-BR");
            DateTimeFormatInfo dtfi = culture.DateTimeFormat;

            lbMes.Text = culture.TextInfo.ToTitleCase(dtfi.GetMonthName(currentDate.Month));
            lbAno.Text = currentDate.Year.ToString().PadLeft(4, '0');

            if (coluna > 0)
            {
                for (int i = (coluna - 1); i >= 0; i--)
                {
                    dgvDias.Rows[0].Cells["Column" + (i + 1)].Value = diasMesAnterior;
                    dgvDias.Rows[0].Cells["Column" + (i + 1)].Tag = 0;

                    diasMesAnterior--;
                }
            }

            Int32 tag = 1;

            for (int i = 0; i < 6; i++)
            {
                for (int y = coluna; y < 7; y++)
                {
                    if ((currentDate.Month == DateTime.MaxValue.Month && currentDate.Year == DateTime.MaxValue.Year) == false || tag == 1)
                    {
                        dgvDias.Rows[i].Cells["Column" + (y + 1)].Value = dia;
                    }
                    else
                    {
                        dgvDias.Rows[i].Cells["Column" + (y + 1)].Value = 0;
                    }
                    dgvDias.Rows[i].Cells["Column" + (y + 1)].Tag = tag;

                    if (dia == diasMes)
                    {
                        dia = 1;
                        tag = 0;
                    }
                    else
                    {
                        dia++;
                    }

                    if (y == 6)
                    {
                        coluna = 0;
                    }
                }
            }
        }

        private void SelecionarMes(DataGridView dgvMes)
        {
            var cell = dgvMes.CurrentCell;
            Int32 mesSelecionado = 0;
            Int32 dia = currentDate.Day;

            if (cell.RowIndex == 0)
            {
                mesSelecionado = cell.ColumnIndex + 1;
                if (dia > DateTime.DaysInMonth(currentDate.Year, mesSelecionado))
                {
                    dia = DateTime.DaysInMonth(currentDate.Year, mesSelecionado);
                }

                currentDate = new DateTime(currentDate.Year, mesSelecionado, dia);
            }
            else
            {
                mesSelecionado = ((cell.RowIndex) * 4) + cell.ColumnIndex + 1;
                if (dia > DateTime.DaysInMonth(currentDate.Year, mesSelecionado))
                {
                    dia = DateTime.DaysInMonth(currentDate.Year, mesSelecionado);
                }

                currentDate = new DateTime(currentDate.Year, mesSelecionado, dia);
            }

            dgvMes.Visible = false;
            panelCabecalho.Visible = true;
            dgvDias.Visible = true;
            panelRodape.Visible = true;

            PreencherCalendario();
            AtivarData(currentDate.Day);

            CurrentDateEventArgs eventChanged = new CurrentDateEventArgs();
            eventChanged.CurrentDate = currentDate;
            OnCurrentDateChanged(eventChanged);

            dgvMes.Dispose();
        }

        private void SelecionarAno(DataGridView dgvAno)
        {
            Panel panelTopo = this.Controls.Find("panelTopo", false).FirstOrDefault() as Panel;

            if (Convert.ToInt32(dgvAno.CurrentCell.Value) != 0)
            {
                Int32 ano = Convert.ToInt32(dgvAno.CurrentCell.Value);
                if (currentDate.Day > DateTime.DaysInMonth(ano, currentDate.Month))
                {
                    currentDate = new DateTime(ano, currentDate.Month, DateTime.DaysInMonth(currentDate.Year, currentDate.Month));
                }
                else
                {
                    currentDate = new DateTime(ano, currentDate.Month, currentDate.Day);
                }

                dgvAno.Visible = false;
                panelTopo.Visible = false;
                panelCabecalho.Visible = true;
                dgvDias.Visible = true;
                panelRodape.Visible = true;

                PreencherCalendario();
                AtivarData(currentDate.Day);

                CurrentDateEventArgs eventChanged = new CurrentDateEventArgs();
                eventChanged.CurrentDate = currentDate;
                OnCurrentDateChanged(eventChanged);
            }
            else
            {
                dgvAno.Visible = false;
                panelTopo.Visible = false;
                panelCabecalho.Visible = true;
                dgvDias.Visible = true;
                panelRodape.Visible = true;
            }

            dgvAno.Dispose();
            panelTopo.Dispose();
        }

        #endregion

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            //Fundo
            e.Graphics.FillRectangle(Brushes.White, new RectangleF(0, 0, this.Width - 1, this.Height - 1));
            e.Graphics.DrawRectangle(Pens.Black, new Rectangle(0, 0, this.Width - 1, this.Height - 1));
        }


        private void panelCabecalho_Click(object sender, EventArgs e)
        {
            if (this.Focused == false)
            {
                OnClick(e);
            }

            dgvDias.Focus();
        }

        private void btnMesEsquerdo_MouseDown(object sender, MouseEventArgs e)
        {
            if ((currentDate.Month == DateTime.MinValue.Month && currentDate.Year == DateTime.MinValue.Year) == false)
            {
                Int32 dia = currentDate.Day;
                currentDate = currentDate.AddMonths(-1);
                if (dia > DateTime.DaysInMonth(currentDate.Year, currentDate.Month))
                {
                    currentDate = new DateTime(currentDate.Year, currentDate.Month, DateTime.DaysInMonth(currentDate.Year, currentDate.Month));
                }

                PreencherCalendario();
                AtivarData(currentDate.Day);

                CurrentDateEventArgs eventChanged = new CurrentDateEventArgs();
                eventChanged.CurrentDate = currentDate;
                OnCurrentDateChanged(eventChanged);
            }

            if (this.Focused == false)
            {
                OnClick(e);
            }

            dgvDias.Focus();
        }

        private void btnMesDireito_MouseDown(object sender, MouseEventArgs e)
        {
            if ((currentDate.Month == DateTime.MaxValue.Month && currentDate.Year == DateTime.MaxValue.Year) == false)
            {
                Int32 dia = currentDate.Day;
                currentDate = currentDate.AddMonths(1);
                if (dia > DateTime.DaysInMonth(currentDate.Year, currentDate.Month))
                {
                    currentDate = new DateTime(currentDate.Year, currentDate.Month, DateTime.DaysInMonth(currentDate.Year, currentDate.Month));
                }

                PreencherCalendario();
                AtivarData(currentDate.Day);

                CurrentDateEventArgs eventChanged = new CurrentDateEventArgs();
                eventChanged.CurrentDate = currentDate;
                OnCurrentDateChanged(eventChanged);
            }

            if (this.Focused == false)
            {
                OnClick(e);
            }

            dgvDias.Focus();
        }

        private void lbMes_Click(object sender, EventArgs e)
        {
            //DataGridView
            System.Windows.Forms.DataGridView dgvMes = new DataGridView();
            System.Windows.Forms.DataGridViewTextBoxColumn ColumnMes1 = new DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn ColumnMes2 = new DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn ColumnMes3 = new DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn ColumnMes4 = new DataGridViewTextBoxColumn();

            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();


            dgvMes.AllowUserToAddRows = false;
            dgvMes.AllowUserToDeleteRows = false;
            dgvMes.AllowUserToResizeColumns = false;
            dgvMes.AllowUserToResizeRows = false;
            dgvMes.BackgroundColor = System.Drawing.Color.White;
            dgvMes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dgvMes.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dgvMes.ColumnHeadersVisible = false;
            dgvMes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvMes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvMes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            ColumnMes1,
            ColumnMes2,
            ColumnMes3,
            ColumnMes4
            });
            dgvMes.Dock = System.Windows.Forms.DockStyle.Fill;
            dgvMes.Location = new System.Drawing.Point(1, 30);
            dgvMes.MultiSelect = false;
            dgvMes.Name = "dgvMes";
            dgvMes.ReadOnly = true;
            dgvMes.RowHeadersVisible = false;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dgvMes.RowsDefaultCellStyle = dataGridViewCellStyle9;
            dgvMes.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            dgvMes.RowTemplate.Height = 18;
            dgvMes.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            dgvMes.ScrollBars = System.Windows.Forms.ScrollBars.None;
            dgvMes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            dgvMes.Size = new System.Drawing.Size(225, 113);
            dgvMes.StandardTab = true;
            dgvMes.TabIndex = 0;
            dgvMes.KeyDown += DgvMes_KeyDown;
            dgvMes.CellDoubleClick += DgvMes_CellDoubleClick;

            // 
            // ColumnMes1
            // 

            ColumnMes1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            ColumnMes1.DefaultCellStyle = dataGridViewCellStyle2;
            ColumnMes1.Name = "ColumnMes1";
            ColumnMes1.ReadOnly = true;
            // 
            // ColumnMes2
            // 
            ColumnMes2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            ColumnMes2.DefaultCellStyle = dataGridViewCellStyle3;
            ColumnMes2.Name = "ColumnMes2";
            ColumnMes2.ReadOnly = true;
            // 
            // ColumnMes3
            // 
            ColumnMes3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            ColumnMes3.DefaultCellStyle = dataGridViewCellStyle4;
            ColumnMes3.Name = "ColumnMes3";
            ColumnMes3.ReadOnly = true;
            // 
            // ColumnMes4
            // 
            ColumnMes4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            ColumnMes4.DefaultCellStyle = dataGridViewCellStyle5;
            ColumnMes4.Name = "ColumnMes4";
            ColumnMes4.ReadOnly = true;

            dgvMes.Rows.Add("Jan", "Fev", "Mar", "Abr");
            dgvMes.Rows.Add("Mai", "Jun", "Jul", "Ago");
            dgvMes.Rows.Add("Set", "Out", "Nov", "Dez");

            panelCabecalho.Visible = false;
            dgvDias.Visible = false;
            panelRodape.Visible = false;

            this.Controls.Add(dgvMes);

            if (this.Focused == false)
            {
                OnClick(e);
            }

            dgvMes.Focus();

            Int32 tamanho = dgvMes.ClientRectangle.Height;
            Int32 altura = (tamanho / dgvMes.Rows.Count);
            Int32 somaAltura = 0;

            foreach (DataGridViewRow row in dgvMes.Rows)
            {
                if (row == dgvMes.Rows[dgvMes.Rows.Count - 1])
                {
                    row.Height = tamanho - somaAltura;
                }
                else
                {
                    row.Height = altura;
                    somaAltura += altura;
                }
            }

            //Seta o Mês atual
            bool vBreak = false;
            for (int i = 0; i < dgvMes.Rows.Count; i++)
            {
                for (int y = 0; y < dgvMes.Columns.Count; y++)
                {
                    if (dgvMes.Rows[i].Cells[y].Value.ToString() == lbMes.Text.Substring(0, 3))
                    {
                        dgvMes.CurrentCell = dgvMes.Rows[i].Cells[y];

                        vBreak = true;
                        break;
                    }
                }

                if (vBreak == true)
                {
                    break;
                }
            }
        }


        private void btnAnoEsquerdo_MouseDown(object sender, MouseEventArgs e)
        {
            if (currentDate.Year != DateTime.MinValue.Year)
            {
                Int32 dia = currentDate.Day;
                currentDate = currentDate.AddYears(-1);
                if (dia > DateTime.DaysInMonth(currentDate.Year, currentDate.Month))
                {
                    currentDate = new DateTime(currentDate.Year, currentDate.Month, DateTime.DaysInMonth(currentDate.Year, currentDate.Month));
                }

                PreencherCalendario();
                AtivarData(currentDate.Day);

                CurrentDateEventArgs eventChanged = new CurrentDateEventArgs();
                eventChanged.CurrentDate = currentDate;
                OnCurrentDateChanged(eventChanged);
            }

            if (this.Focused == false)
            {
                OnClick(e);
            }

            dgvDias.Focus();
        }

        private void btnAnoDireito_MouseDown(object sender, MouseEventArgs e)
        {
            if (currentDate.Year != DateTime.MaxValue.Year)
            {
                Int32 dia = currentDate.Day;
                currentDate = currentDate.AddYears(1);
                if (dia > DateTime.DaysInMonth(currentDate.Year, currentDate.Month))
                {
                    currentDate = new DateTime(currentDate.Year, currentDate.Month, DateTime.DaysInMonth(currentDate.Year, currentDate.Month));
                }

                PreencherCalendario();
                AtivarData(currentDate.Day);

                CurrentDateEventArgs eventChanged = new CurrentDateEventArgs();
                eventChanged.CurrentDate = currentDate;
                OnCurrentDateChanged(eventChanged);
            }

            if (this.Focused == false)
            {
                OnClick(e);
            }

            dgvDias.Focus();
        }

        private void lbAno_Click(object sender, EventArgs e)
        {
            #region  DataGridView

            System.Windows.Forms.DataGridView dgvAno = new DataGridView();
            System.Windows.Forms.DataGridViewTextBoxColumn ColumnAno1 = new DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn ColumnAno2 = new DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn ColumnAno3 = new DataGridViewTextBoxColumn();
            System.Windows.Forms.DataGridViewTextBoxColumn ColumnAno4 = new DataGridViewTextBoxColumn();

            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();


            dgvAno.AllowUserToAddRows = false;
            dgvAno.AllowUserToDeleteRows = false;
            dgvAno.AllowUserToResizeColumns = false;
            dgvAno.AllowUserToResizeRows = false;
            dgvAno.BackgroundColor = System.Drawing.Color.White;
            dgvAno.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dgvAno.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dgvAno.ColumnHeadersVisible = false;
            dgvAno.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvAno.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvAno.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            ColumnAno1,
            ColumnAno2,
            ColumnAno3,
            ColumnAno4
            });
            dgvAno.Dock = System.Windows.Forms.DockStyle.Fill;
            dgvAno.Location = new System.Drawing.Point(1, 30);
            dgvAno.MultiSelect = false;
            dgvAno.Name = "dgvAno";
            dgvAno.ReadOnly = true;
            dgvAno.RowHeadersVisible = false;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dgvAno.RowsDefaultCellStyle = dataGridViewCellStyle9;
            dgvAno.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            dgvAno.RowTemplate.Height = 18;
            dgvAno.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            dgvAno.ScrollBars = System.Windows.Forms.ScrollBars.None;
            dgvAno.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            dgvAno.Size = new System.Drawing.Size(225, 113);
            dgvAno.StandardTab = true;
            dgvAno.TabIndex = 0;
            dgvAno.KeyDown += DgvAno_KeyDown;
            dgvAno.CellDoubleClick += DgvAno_CellDoubleClick;
            dgvAno.CellFormatting += DgvAno_CellFormatting;
            dgvAno.SelectionChanged += DgvAno_SelectionChanged;

            // 
            // ColumnMes1
            // 

            ColumnAno1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            ColumnAno1.DefaultCellStyle = dataGridViewCellStyle2;
            ColumnAno1.Name = "ColumnMes1";
            ColumnAno1.ReadOnly = true;
            // 
            // ColumnMes2
            // 
            ColumnAno2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            ColumnAno2.DefaultCellStyle = dataGridViewCellStyle3;
            ColumnAno2.Name = "ColumnMes2";
            ColumnAno2.ReadOnly = true;
            // 
            // ColumnMes3
            // 
            ColumnAno3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            ColumnAno3.DefaultCellStyle = dataGridViewCellStyle4;
            ColumnAno3.Name = "ColumnMes3";
            ColumnAno3.ReadOnly = true;
            // 
            // ColumnMes4
            // 
            ColumnAno4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            ColumnAno4.DefaultCellStyle = dataGridViewCellStyle5;
            ColumnAno4.Name = "ColumnMes4";
            ColumnAno4.ReadOnly = true;

            dgvAno.Rows.Add(3);
            Int32 ano = currentDate.Year;

            for (int i = 0; i < 3; i++)
            {
                for (int y = 0; y < 4; y++)
                {
                    if (ano <= DateTime.MaxValue.Year)
                    {
                        dgvAno.Rows[i].Cells[y].Value = ano;

                        if (i != 2 || y != 3)
                        {
                            ano++;
                        }
                    }
                    else
                    {
                        dgvAno.Rows[i].Cells[y].Value = 0;
                    }
                }
            }

            #endregion

            #region Panel

            Panel panelTopo = new Panel();
            panelTopo.BackColor = System.Drawing.Color.Gainsboro;
            panelTopo.Dock = System.Windows.Forms.DockStyle.Top;
            panelTopo.Location = new System.Drawing.Point(1, 1);
            panelTopo.Name = "panelTopo";
            panelTopo.Size = new System.Drawing.Size(225, 29);
            panelTopo.TabIndex = 1;

            #endregion

            #region Label

            Label lbIntervaloAno = new Label();
            lbIntervaloAno.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbIntervaloAno.ForeColor = System.Drawing.Color.Black;
            lbIntervaloAno.Size = new System.Drawing.Size(85, 21);
            lbIntervaloAno.Location = new System.Drawing.Point((panelTopo.Width / 2) - (lbIntervaloAno.Width / 2), 4);
            lbIntervaloAno.Name = "lbIntervaloAno";
            lbIntervaloAno.TabIndex = 1;
            lbIntervaloAno.Text = $"{dgvAno.Rows[0].Cells[0].Value} - {ano}";
            lbIntervaloAno.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lbIntervaloAno.Anchor = AnchorStyles.Left | AnchorStyles.Right;

            #endregion

            #region Button

            RepeatButton btnTelaAnoDireito = new RepeatButton();
            btnTelaAnoDireito.BackgroundImage = AERMOD.LIB.Properties.Resources.ladoDireito;
            btnTelaAnoDireito.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            btnTelaAnoDireito.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            btnTelaAnoDireito.FlatAppearance.BorderSize = 0;
            btnTelaAnoDireito.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnTelaAnoDireito.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            btnTelaAnoDireito.Size = new System.Drawing.Size(10, 10);
            btnTelaAnoDireito.Location = new System.Drawing.Point(lbIntervaloAno.Location.X + lbIntervaloAno.Width + 5, 9);
            btnTelaAnoDireito.Name = "btnTelaAnoDireito";
            btnTelaAnoDireito.RepeatInterval = 350;
            btnTelaAnoDireito.TabIndex = 2;
            btnTelaAnoDireito.TabStop = false;
            btnTelaAnoDireito.UseVisualStyleBackColor = true;
            btnTelaAnoDireito.Anchor = AnchorStyles.Right;
            btnTelaAnoDireito.MouseDown += BtnTelaAnoDireito_MouseDown;

            RepeatButton btnTelaAnoEsquerdo = new RepeatButton();
            btnTelaAnoEsquerdo.BackgroundImage = AERMOD.LIB.Properties.Resources.ladoEsquerdo;
            btnTelaAnoEsquerdo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            btnTelaAnoEsquerdo.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            btnTelaAnoEsquerdo.FlatAppearance.BorderSize = 0;
            btnTelaAnoEsquerdo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnTelaAnoEsquerdo.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            btnTelaAnoEsquerdo.Size = new System.Drawing.Size(10, 10);
            btnTelaAnoEsquerdo.Location = new System.Drawing.Point(lbIntervaloAno.Location.X - btnTelaAnoEsquerdo.Width - 5, 9);
            btnTelaAnoEsquerdo.Name = "btnTelaAnoEsquerdo";
            btnTelaAnoEsquerdo.RepeatInterval = 350;
            btnTelaAnoEsquerdo.TabIndex = 1;
            btnTelaAnoEsquerdo.TabStop = false;
            btnTelaAnoEsquerdo.UseVisualStyleBackColor = true;
            btnTelaAnoEsquerdo.Anchor = AnchorStyles.Right;
            btnTelaAnoEsquerdo.MouseDown += BtnTelaAnoEsquerdo_MouseDown;

            SetStyleEx(btnTelaAnoDireito, ControlStyles.Selectable, false);
            SetStyleEx(btnTelaAnoEsquerdo, ControlStyles.Selectable, false);

            #endregion

            panelCabecalho.Visible = false;
            dgvDias.Visible = false;
            panelRodape.Visible = false;

            this.Controls.Add(panelTopo);
            panelTopo.Controls.Add(btnTelaAnoDireito);
            panelTopo.Controls.Add(btnTelaAnoEsquerdo);
            panelTopo.Controls.Add(lbIntervaloAno);

            this.Controls.Add(dgvAno);
            dgvAno.BringToFront();

            if (this.Focused == false)
            {
                OnClick(e);
            }

            anoSelecionado = currentDate.Year;
            dgvAno.Focus();

            #region Tamanho das linhas

            Int32 tamanho = dgvAno.ClientRectangle.Height;
            Int32 altura = (tamanho / dgvAno.Rows.Count);
            Int32 somaAltura = 0;

            foreach (DataGridViewRow row in dgvAno.Rows)
            {
                if (row == dgvAno.Rows[dgvAno.Rows.Count - 1])
                {
                    row.Height = tamanho - somaAltura;
                }
                else
                {
                    row.Height = altura;
                    somaAltura += altura;
                }
            }

            #endregion
        }

        private void dgvDias_Click(object sender, EventArgs e)
        {
            if (this.Focused == false)
            {
                OnClick(e);
            }
        }

        private void dgvDias_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                OnDoubleClick(e);
            }
        }

        private void dgvDias_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (Convert.ToInt32(e.Value) == 0)
                {
                    e.Value = "";
                }

                if (Convert.ToInt32(dgvDias.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag) == 0)
                {
                    e.CellStyle.ForeColor = Color.LightGray;
                }
                else
                {
                    e.CellStyle.ForeColor = Color.Black;
                }

                if (e.Value.ToString() != "" && Convert.ToInt32(e.Value) != 0 && Convert.ToInt32(dgvDias.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag) != 0)
                {
                    DateTime data = new DateTime(currentDate.Year, currentDate.Month, Convert.ToInt32(e.Value));

                    if (listAnualDatas.Any(f => f.Day == data.Day && f.Month == data.Month && f.Year == data.Year) == true  ||
                        listMesDatas.Any(f => f.Day == data.Day && f.Month == data.Month) == true ||
                        listDiaDatas.Any(f => f.Day == data.Day) == true)
                    {
                        e.CellStyle.ForeColor = Color.OrangeRed;
                        e.CellStyle.SelectionForeColor = Color.LightSalmon;
                    }
                    else
                    {
                        e.CellStyle.ForeColor = Color.Black;
                        e.CellStyle.SelectionForeColor = Color.White;
                    }
                }
            }
        }

        private void dgvDias_KeyDown(object sender, KeyEventArgs e)
        {
            OnKeyDown(e);

            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        private void dgvDias_SizeChanged(object sender, EventArgs e)
        {
            Int32 tamanho = dgvDias.ClientRectangle.Height - dgvDias.ColumnHeadersHeight;
            Int32 altura = (tamanho / dgvDias.Rows.Count);
            Int32 somaAltura = 0;

            foreach (DataGridViewRow row in dgvDias.Rows)
            {
                if (row == dgvDias.Rows[dgvDias.Rows.Count - 1])
                {
                    row.Height = tamanho - somaAltura;
                }
                else
                {
                    row.Height = altura;
                    somaAltura += altura;
                }
            }
        }

        private void dgvDias_SelectionChanged(object sender, EventArgs e)
        {
            if (execute_SelectionChanged == false)
            {
                return;
            }

            if (dgvDias.CurrentRow != null)
            {
                if (dgvDias.Rows[dgvDias.CurrentRow.Index].Cells[dgvDias.CurrentCell.ColumnIndex].FormattedValue.ToString() != "")
                {
                    if (Convert.ToInt32(dgvDias.Rows[dgvDias.CurrentRow.Index].Cells[dgvDias.CurrentCell.ColumnIndex].Tag) == 0)
                    {
                        Int32 diaSelecionado = Convert.ToInt32(dgvDias.Rows[dgvDias.CurrentRow.Index].Cells[dgvDias.CurrentCell.ColumnIndex].Value);
                        DateTime mesSelecionado = currentDate.AddMonths(diaSelecionado > 15 ? -1 : 1);

                        currentDate = new DateTime(mesSelecionado.Year, mesSelecionado.Month, diaSelecionado);
                        PreencherCalendario();

                        execute_SelectionChanged = false;
                        AtivarData(diaSelecionado);
                        execute_SelectionChanged = true;
                    }
                    else
                    {
                        Int32 diaSelecionado = Convert.ToInt32(dgvDias.Rows[dgvDias.CurrentRow.Index].Cells[dgvDias.CurrentCell.ColumnIndex].Value);
                        currentDate = new DateTime(currentDate.Year, currentDate.Month, diaSelecionado);
                    }
                }
                else
                {
                    execute_SelectionChanged = false;
                    AtivarData(currentDate.Day);
                    execute_SelectionChanged = true;
                }
            }
        }


        private void panelRodape_Click(object sender, EventArgs e)
        {
            if (this.Focused == false)
            {
                OnClick(e);
            }

            dgvDias.Focus();
        }

        private void lbHoje_Click(object sender, EventArgs e)
        {
            lbHoje.Text = $"Hoje: {DateTime.Today.ToString("dd/MM/yyyy")}";

            if (currentDate != DateTime.Today)
            {
                currentDate = DateTime.Today;
                PreencherCalendario();
                AtivarData(currentDate.Day);

                CurrentDateEventArgs eventChanged = new CurrentDateEventArgs();
                eventChanged.CurrentDate = currentDate;
                OnCurrentDateChanged(eventChanged);
            }

            if (this.Focused == false)
            {
                OnClick(e);
            }

            dgvDias.Focus();
        }

        private void lbHoje_DoubleClick(object sender, EventArgs e)
        {
            OnDoubleClick(e);
        }


        private void DgvMes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                SelecionarMes(sender as DataGridView);
            }
        }

        private void DgvMes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            SelecionarMes(sender as DataGridView);
        }


        private void DgvAno_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (Convert.ToInt32(e.Value) == 0)
                {
                    e.Value = "";
                }
            }
        }

        private void DgvAno_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            SelecionarAno(sender as DataGridView);
        }

        private void DgvAno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                SelecionarAno(sender as DataGridView);
            }
        }

        private void DgvAno_SelectionChanged(object sender, EventArgs e)
        {
            var dgvAno = sender as DataGridView;

            if (dgvAno.CurrentRow != null)
            {
                if (Convert.ToInt32(dgvAno.CurrentCell.Value) == 0)
                {
                    for (int i = 0; i < dgvAno.Rows.Count; i++)
                    {
                        for (int y = 0; y < dgvAno.Columns.Count; y++)
                        {
                            if (Convert.ToInt32(dgvAno.Rows[i].Cells[y].Value) == anoSelecionado)
                            {
                                dgvAno.SelectionChanged -= DgvAno_SelectionChanged;
                                dgvAno.CurrentCell = dgvAno.Rows[i].Cells[y];
                                dgvAno.SelectionChanged += DgvAno_SelectionChanged;
                                return;
                            }
                        }
                    }
                }
                else
                {
                    anoSelecionado = Convert.ToInt32(dgvAno.CurrentCell.Value);
                }
            }
        }


        private void BtnTelaAnoEsquerdo_MouseDown(object sender, MouseEventArgs e)
        {
            DataGridView dgvAno = this.Controls.Find("dgvAno", false).FirstOrDefault() as DataGridView;

            if (Convert.ToInt32(dgvAno.Rows[0].Cells[0].Value) != DateTime.MinValue.Year)
            {
                Int32 ultimoAno = Convert.ToInt32(dgvAno.Rows[0].Cells[0].Value);
                for (int i = 11; i >= 0; i--)
                {
                    if (ultimoAno - (i + 1) < DateTime.MinValue.Year)
                    {
                        continue;
                    }
                    else
                    {
                        ultimoAno -= (i + 1);
                        break;
                    }
                }

                for (int i = 0; i < dgvAno.Rows.Count; i++)
                {
                    for (int y = 0; y < dgvAno.Columns.Count; y++)
                    {
                        dgvAno.Rows[i].Cells[y].Value = ultimoAno;
                        ultimoAno++;
                    }
                }
            }

            Label lbIntervaloAno = (sender as RepeatButton).Parent.Controls.Find("lbIntervaloAno", false).FirstOrDefault() as Label;
            lbIntervaloAno.Text = $"{dgvAno.Rows[0].Cells[0].Value} - {dgvAno.Rows[2].Cells[3].Value}";
        }

        private void BtnTelaAnoDireito_MouseDown(object sender, MouseEventArgs e)
        {
            DataGridView dgvAno = this.Controls.Find("dgvAno", false).FirstOrDefault() as DataGridView;
            Int32 ultimoAno = 0;

            for (int i = 0; i < dgvAno.Rows.Count; i++)
            {
                for (int y = 0; y < dgvAno.Columns.Count; y++)
                {
                    if (Convert.ToInt32(dgvAno.Rows[i].Cells[y].Value) != 0)
                    {
                        ultimoAno = Convert.ToInt32(dgvAno.Rows[i].Cells[y].Value);
                    }
                    else
                    {
                        break;
                    }
                }
            }

            if (ultimoAno != DateTime.MaxValue.Year)
            {
                for (int i = 0; i < dgvAno.Rows.Count; i++)
                {
                    for (int y = 0; y < dgvAno.Columns.Count; y++)
                    {
                        if (ultimoAno < DateTime.MaxValue.Year)
                        {
                            dgvAno.Rows[i].Cells[y].Value = ++ultimoAno;
                        }
                        else
                        {
                            dgvAno.Rows[i].Cells[y].Value = 0;
                        }
                    }
                }
            }

            Label lbIntervaloAno = (sender as RepeatButton).Parent.Controls.Find("lbIntervaloAno", false).FirstOrDefault() as Label;
            lbIntervaloAno.Text = $"{dgvAno.Rows[0].Cells[0].Value} - {ultimoAno}";
        }
    }

    public delegate void CurrentDateEventHandler(object sender, CurrentDateEventArgs e);

    public class CurrentDateEventArgs : EventArgs
    {
        public DateTime CurrentDate { get; set; }
    }

    [ToolboxItem(false)]
    internal class RepeatButton : Button
    {
        private Timer timerRepeater;
        private IContainer components;

        public RepeatButton() : base()
        {
            InitializeComponent();

            InitialDelay = 400;
            RepeatInterval = 350;
        }

        [DefaultValue(400)]
        public int InitialDelay { set; get; }

        [DefaultValue(62)]
        public int RepeatInterval { set; get; }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timerRepeater = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            this.timerRepeater.Tick += new System.EventHandler(this.timerRepeater_Tick);
            this.ResumeLayout(false);
        }

        MouseEventArgs MouseDownArgs = null;
        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            //Save arguments
            MouseDownArgs = mevent;
            timerRepeater.Enabled = false;
            timerRepeater_Tick(null, EventArgs.Empty);
        }

        private void timerRepeater_Tick(object sender, EventArgs e)
        {
            base.OnMouseDown(MouseDownArgs);
            if (timerRepeater.Enabled)
                timerRepeater.Interval = RepeatInterval;
            else
                timerRepeater.Interval = InitialDelay;

            timerRepeater.Enabled = true;
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            timerRepeater.Enabled = false;
        }
    }
}
