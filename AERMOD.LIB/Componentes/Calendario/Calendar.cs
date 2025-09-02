using AERMOD.LIB.Desenvolvimento;
using Netsof.LIB.Componentes.Calendario;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace AERMOD.LIB.Componentes.Calendario
{
    public class Calendar : Control
    {
        private List<CalendarCell> cells = new List<CalendarCell>();

        private List<Evento> eventos = new List<Evento>();

        private List<EventoExclusao> eventosExclusao = new List<EventoExclusao>();

        private ToolStripDropDown popup;

        CultureInfo culture = new CultureInfo("pt-BR");

        private Size desiredSize = new Size(905, 585);
        public override Size MinimumSize
        {
            get { return desiredSize; }
            set { }
        }
        public override Size MaximumSize
        {
            get { return desiredSize; }
            set { }
        }

        private DateTime currentDate = DateTime.Today;

        /// <summary>
        /// Utilizado para a janela de ano
        /// </summary>
        Int32 anoSelecionado = 0;

        #region Eventos

        public event LoadEventosHandler LoadEvento;

        public virtual void OnLoadEventos(LoadEventosArgs e)
        {
            if (LoadEvento != null)
            {
                LoadEvento(this, e);
            }
        }

        public event NewEventoHandler NewEvento;

        public virtual void OnNewEvento(NewEventoArgs e)
        {
            if (NewEvento != null)
            {
                NewEvento(this, e);
            }
        }

        public event InfoEventoHandler InfoEvento;

        public virtual void OnInfoEvento(InfoEventoArgs e)
        {
            if (InfoEvento != null)
            {
                InfoEvento(this, e);
            }
        }

        public event ShowEventosHandler ShowEventos;

        public virtual void OnShowEventos(ShowEventosArgs e)
        {
            if (ShowEventos != null)
            {
                ShowEventos(this, e);
            }
        }

        public event ExcluirEventoHandler ExcluirEvento;

        public virtual void OnExcluirEvento(ExcluirEventoArgs e)
        {
            if (ExcluirEvento != null)
            {
                ExcluirEvento(this, e);
            }
        }

        #endregion

        public Calendar()
        {
            InitializeComponent();
        }


        #region Métodos

        private void InitializeComponent()
        {
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            base.SetStyle(ControlStyles.UserMouse, true);
            base.SetStyle(ControlStyles.StandardClick, true);
            base.SetStyle(ControlStyles.Selectable, true);

            this.BackColor = Color.White;
            this.currentDate = DateTime.Today;

            RepeatButton btnEsqMes = new RepeatButton();
            btnEsqMes.FlatStyle = FlatStyle.Flat;
            btnEsqMes.FlatAppearance.BorderSize = 0;
            btnEsqMes.BackColor = Color.FromArgb(244, 244, 244);
            btnEsqMes.Image = Properties.Resources.ladoEsquerdo;
            btnEsqMes.ImageAlign = ContentAlignment.MiddleCenter;
            btnEsqMes.Location = new Point(1, 1);
            btnEsqMes.Size = new Size(20, 38);
            btnEsqMes.MouseDown += BtnEsqMes_MouseDown;
            AERMOD.LIB.Desenvolvimento.Funcoes.SetStyle(btnEsqMes, ControlStyles.Selectable, false);
            this.Controls.Add(btnEsqMes);


            RepeatButton btnDirMes = new RepeatButton();
            btnDirMes.FlatStyle = FlatStyle.Flat;
            btnDirMes.FlatAppearance.BorderSize = 0;
            btnDirMes.BackColor = Color.FromArgb(244, 244, 244);
            btnDirMes.Image = Properties.Resources.ladoDireito;
            btnDirMes.ImageAlign = ContentAlignment.MiddleCenter;
            btnDirMes.Location = new Point(122, 1);
            btnDirMes.Size = new Size(20, 38);
            btnDirMes.MouseDown += BtnDirMes_MouseDown;
            AERMOD.LIB.Desenvolvimento.Funcoes.SetStyle(btnDirMes, ControlStyles.Selectable, false);
            this.Controls.Add(btnDirMes);


            RepeatButton btnEsqAno = new RepeatButton();
            btnEsqAno.FlatStyle = FlatStyle.Flat;
            btnEsqAno.FlatAppearance.BorderSize = 0;
            btnEsqAno.BackColor = Color.FromArgb(244, 244, 244);
            btnEsqAno.Image = Properties.Resources.ladoEsquerdo;
            btnEsqAno.ImageAlign = ContentAlignment.MiddleCenter;
            btnEsqAno.Location = new Point(791, 1);
            btnEsqAno.Size = new Size(20, 38);
            btnEsqAno.MouseDown += BtnEsqAno_MouseDown;
            AERMOD.LIB.Desenvolvimento.Funcoes.SetStyle(btnEsqAno, ControlStyles.Selectable, false);
            this.Controls.Add(btnEsqAno);


            RepeatButton btnDirAno = new RepeatButton();
            btnDirAno.FlatStyle = FlatStyle.Flat;
            btnDirAno.FlatAppearance.BorderSize = 0;
            btnDirAno.BackColor = Color.FromArgb(244, 244, 244);
            btnDirAno.Image = Properties.Resources.ladoDireito;
            btnDirAno.ImageAlign = ContentAlignment.MiddleCenter;
            btnDirAno.Location = new Point(884, 1);
            btnDirAno.Size = new Size(20, 38);
            btnDirAno.MouseDown += BtnDirAno_MouseDown;
            AERMOD.LIB.Desenvolvimento.Funcoes.SetStyle(btnDirAno, ControlStyles.Selectable, false);
            this.Controls.Add(btnDirAno);

            Int32 x = 1;
            Int32 y = 69;

            for (int i = 0; i < 6; i++)
            {
                CalendarCell cell = null;

                for (int z = 0; z < 7; z++)
                {
                    cell = new CalendarCell();
                    cell.Bounds = new Rectangle(x, y, 129, 85);
                    cell.Row = i;
                    cell.Column = "Column" + (z + 1);

                    cells.Add(cell);
                    x += cell.Bounds.Width;
                }

                y += cell.Bounds.Height + 1;
                x = 1;
            }

            PreencherCalendario();
            AtivarData(currentDate);
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

            PreencherCalendario();
            AtivarData(currentDate);

            popup.Close();
        }

        private void SelecionarAno(DataGridView dgvAno)
        {
            Panel panelTopo = dgvAno.Parent.Controls.Find("panelTopo", false).FirstOrDefault() as Panel;

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
            }

            PreencherCalendario();
            AtivarData(currentDate);

            popup.Close();
        }

        private void AtivarData(DateTime data)
        {
            LimparSelecao();

            var cellCurrent = cells.FirstOrDefault(f => f.Date.Value.Date == data.Date);
            if (cellCurrent != null)
            {
                cellCurrent.IsActive = true;
                this.Invalidate(cellCurrent.Bounds);
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

            if (coluna > 0)
            {
                DateTime mesAnterior = currentDate.AddMonths(-1);

                for (int i = (coluna - 1); i >= 0; i--)
                {
                    var item = cells.First(f => f.Row == 0 && f.Column == ("Column" + (i + 1)));

                    item.Date = new DateTime(mesAnterior.Year, mesAnterior.Month, diasMesAnterior);
                    item.Tag = 0;

                    diasMesAnterior--;
                }
            }

            Int32 tag = 1;

            for (int i = 0; i < 6; i++)
            {
                for (int y = coluna; y < 7; y++)
                {
                    var item = cells.First(f => f.Row == i && f.Column == ("Column" + (y + 1)));

                    if ((currentDate.Month == DateTime.MaxValue.Month && currentDate.Year == DateTime.MaxValue.Year) == false || tag == 1)
                    {
                        if (tag == 1)
                        {
                            item.Date = new DateTime(currentDate.Year, currentDate.Month, dia);
                        }
                        else
                        {
                            DateTime dateProximo = currentDate.AddMonths(1);
                            item.Date = new DateTime(dateProximo.Year, dateProximo.Month, dia);
                        }
                    }
                    else
                    {
                        item.Date = null;
                    }
                    item.Tag = tag;

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

            LimparEventos();
            CarregarEventos();

            this.Invalidate();
        }

        private void CarregarEventos()
        {
            LoadEventosArgs args = new LoadEventosArgs();
            args.CurrentDate = currentDate;
            OnLoadEventos(args);
        }

        private void LimparEventos()
        {
            foreach (var item in cells)
            {
                item.Clear();
            }
            eventos.Clear();
            eventosExclusao.Clear();
        }

        /// <summary>
        /// Adiciona o evento
        /// </summary>
        /// <param name="evento">Evento</param>
        public void AdicionarEvento(Evento evento)
        {
            eventos.Add(evento);

            var cell = cells.FirstOrDefault(f => f.Date.HasValue && f.Date.Value == evento.Data_Hora.Date);
            if (cell == null)
            {
                if (evento.Fixar == Fixar.Mensal)
                {
                    var listCells = cells.Where(f => f.Date.HasValue && f.Date.Value.Day == evento.Data_Hora.Date.Day);
                    foreach (var item in listCells)
                    {
                        if (item.Date.HasValue && eventosExclusao.Any(f => f.Codigo_Evento == evento.Codigo_Evento && f.DATA_EXCLUSAO == item.Date.Value) == false)
                        {
                            item.AddEvento(evento);
                        }
                    }
                }
                else if (evento.Fixar == Fixar.Anual)
                {
                    cell = cells.FirstOrDefault(f => f.Date.HasValue && f.Date.Value.Day == evento.Data_Hora.Day && f.Date.Value.Month == evento.Data_Hora.Month);
                    if (cell != null)
                    {
                        if (cell.Date.HasValue && eventosExclusao.Any(f => f.Codigo_Evento == evento.Codigo_Evento && f.DATA_EXCLUSAO == cell.Date.Value) == false)
                        {
                            cell.AddEvento(evento);
                        }
                    }
                }

                if (evento.Postergar == true && evento.Data_Hora.Date != DateTime.Today)
                {
                    cell = cells.FirstOrDefault(f => f.Date.HasValue && f.Date.Value.Date == DateTime.Today);
                    if (cell != null)
                    {
                        if (cell.Date.HasValue && eventosExclusao.Any(f => f.Codigo_Evento == evento.Codigo_Evento && f.DATA_EXCLUSAO == cell.Date.Value) == false)
                        {
                            cell.AddEvento(evento);
                        }
                    }
                }
            }
            else if (cell != null)
            {
                if (cell.Date.HasValue && eventosExclusao.Any(f => f.Codigo_Evento == evento.Codigo_Evento && f.DATA_EXCLUSAO == cell.Date.Value) == false)
                {
                    cell.AddEvento(evento);
                }

                if (evento.Fixar == Fixar.Mensal)
                {
                    var listCells = cells.Where(f => f.Date.HasValue && f.Date.Value.Day == evento.Data_Hora.Date.Day && f.Tag.ToString() == "0");
                    foreach (var item in listCells)
                    {
                        if (item.Date.HasValue && eventosExclusao.Any(f => f.Codigo_Evento == evento.Codigo_Evento && f.DATA_EXCLUSAO == item.Date.Value) == false)
                        {
                            item.AddEvento(evento);
                        }
                    }
                }
                else if (evento.Fixar == Fixar.Anual)
                {
                    cell = cells.FirstOrDefault(f => f.Date.HasValue && f.Date.Value.Day == evento.Data_Hora.Day && f.Date.Value.Month == evento.Data_Hora.Month && f.Tag.ToString() == "0");
                    if (cell != null)
                    {
                        if (cell.Date.HasValue && eventosExclusao.Any(f => f.Codigo_Evento == evento.Codigo_Evento && f.DATA_EXCLUSAO == cell.Date.Value) == false)
                        {
                            cell.AddEvento(evento);
                        }
                    }
                }

                if (evento.Postergar == true && evento.Data_Hora.Date != DateTime.Today)
                {
                    cell = cells.FirstOrDefault(f => f.Date.HasValue && f.Date.Value.Date == DateTime.Today && f.Date.Value > evento.Data_Hora.Date);
                    if (cell != null)
                    {
                        if (cell.Date.HasValue && eventosExclusao.Any(f => f.Codigo_Evento == evento.Codigo_Evento && f.DATA_EXCLUSAO == cell.Date.Value) == false)
                        {
                            cell.AddEvento(evento);
                        }
                    }
                }
            }

            if (evento.Dias_Antecipar > 0 && evento.Data_Hora.Date.AddDays(-evento.Dias_Antecipar) == DateTime.Today)
            {
                Evento eventoAntecipar = evento.CriarCopia();
                eventoAntecipar.Codigo_Evento = evento.Codigo_Evento * -1;
                eventoAntecipar.Data_Hora = evento.Data_Hora.AddDays(-evento.Dias_Antecipar);

                cell = cells.FirstOrDefault(f => f.Date.HasValue && f.Date.Value.Date == eventoAntecipar.Data_Hora.Date);
                if (cell != null)
                {
                    cell.AddEvento(eventoAntecipar);
                }
            }
        }

        /// <summary>
        /// Remove o evento
        /// </summary>
        /// <param name="evento">Evento</param>
        public void RemoverEvento(Evento evento)
        {
            eventos.RemoveAll(f => f.Codigo_Evento == evento.Codigo_Evento);
            var cell = cells.FirstOrDefault(f => f.Date.Value == evento.Data_Hora.Date);
            if (cell != null)
            {
                cell.RemoverEvento(evento);
            }
        }

        /// <summary>
        /// Adiciona o evento que não vai aparecer em determinada data
        /// </summary>
        /// <param name="eventoExclusao">Evento de exclusão</param>
        public void AdicionarEventoExclusao(EventoExclusao eventoExclusao)
        {
            eventosExclusao.Add(eventoExclusao);
        }

        /// <summary>
        ///Limpa a selecao das células
        /// </summary>
        private void LimparSelecao()
        {
            foreach (var item in cells)
            {
                if (item.IsActive == true)
                {
                    this.Invalidate(item.Bounds);
                }
                item.IsActive = false;
            }
        }

        /// <summary>
        /// Limpa todos os eventos
        /// </summary>
        public void ClearEvento()
        {
            foreach (var item in cells)
            {
                item.Clear();
            }

            eventos.Clear();
            eventosExclusao.Clear();
            this.Invalidate();
        }

        /// <summary>
        /// Seta a data selecionada
        /// </summary>
        /// <param name="value">Data</param>
        public void SetCurrentDate(DateTime value)
        {
            if (currentDate != value)
            {
                currentDate = value;

                PreencherCalendario();
                AtivarData(currentDate);
            }
        }

        /// <summary>
        /// Obtem a data selecionada
        /// </summary>
        /// <returns></returns>
        public DateTime GetCurrentDate()
        {
            return currentDate;
        }

        #endregion

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Down)
            {
                #region Down

                var cell = cells.FirstOrDefault(f => f.IsActive);
                if (cell != null && cell.Row < 5)
                {
                    cell = cells.FirstOrDefault(f => f.Row == cell.Row + 1 && f.Column == cell.Column);
                    if (cell != null)
                    {
                        if (cell.Date.HasValue)
                        {
                            currentDate = cell.Date.Value;
                        }

                        if (cell.Tag.ToString() == "0" && cell.Date.HasValue)
                        {
                            PreencherCalendario();
                        }
                        AtivarData(currentDate);
                    }
                }
                return true;

                #endregion
            }
            else if (keyData == Keys.Up)
            {
                #region Up

                var cell = cells.FirstOrDefault(f => f.IsActive);
                if (cell != null && cell.Row > 0)
                {
                    cell = cells.FirstOrDefault(f => f.Row == cell.Row - 1 && f.Column == cell.Column);
                    if (cell != null)
                    {
                        if (cell.Date.HasValue)
                        {
                            currentDate = cell.Date.Value;
                        }

                        if (cell.Tag.ToString() == "0" && cell.Date.HasValue)
                        {
                            PreencherCalendario();
                        }
                        AtivarData(currentDate);
                    }
                }
                return true;

                #endregion
            }
            else if (keyData == Keys.Left)
            {
                #region Left

                var cell = cells.FirstOrDefault(f => f.IsActive);
                if (cell != null && cell.Column != "Column1")
                {
                    cell = cells.FirstOrDefault(f => f.Row == cell.Row && f.ColumnIndex == (cell.ColumnIndex - 1));
                    if (cell != null)
                    {
                        if (cell.Date.HasValue)
                        {
                            currentDate = cell.Date.Value;
                        }

                        if (cell.Tag.ToString() == "0" && cell.Date.HasValue)
                        {
                            PreencherCalendario();
                        }
                        AtivarData(currentDate);
                    }
                }
                return true;

                #endregion
            }
            else if (keyData == Keys.Right)
            {
                #region Right

                var cell = cells.FirstOrDefault(f => f.IsActive);
                if (cell != null && cell.Column != "Column7")
                {
                    cell = cells.FirstOrDefault(f => f.Row == cell.Row && f.ColumnIndex == (cell.ColumnIndex + 1));
                    if (cell != null)
                    {
                        if (cell.Date.HasValue)
                        {
                            currentDate = cell.Date.Value;
                        }

                        if (cell.Tag.ToString() == "0" && cell.Date.HasValue)
                        {
                            PreencherCalendario();
                        }
                        AtivarData(currentDate);
                    }
                }
                return true;

                #endregion
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == MouseButtons.Left)
            {
                var rectHoje = new Rectangle(345, 9, 160, 21);
                var rectMes = new Rectangle(31, 9, 85, 21);
                var rectAno = new Rectangle(817, 9, 66, 21);

                if (rectHoje.Contains(e.Location) == true)
                {
                    #region Hoje

                    if (currentDate != DateTime.Today)
                    {
                        currentDate = DateTime.Today;
                        PreencherCalendario();
                        AtivarData(currentDate);
                    }

                    #endregion
                }
                else if (rectMes.Contains(e.Location) == true)
                {
                    if (popup == null || popup.Tag == null)
                    {
                        #region Mes

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
                        dgvMes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[]
                        {
                        ColumnMes1,
                        ColumnMes2,
                        ColumnMes3,
                        ColumnMes4
                        });
                        dgvMes.Dock = System.Windows.Forms.DockStyle.Fill;
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
                        dgvMes.Location = new Point(5, 5);
                        dgvMes.MinimumSize = new Size(250, 250);
                        dgvMes.Size = dgvMes.MinimumSize;
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

                        popup = new ToolStripDropDown();
                        popup.AutoClose = true;
                        popup.Margin = new System.Windows.Forms.Padding(0);
                        popup.Padding = new System.Windows.Forms.Padding(0);
                        popup.Opened += popup_Opened;
                        popup.Closing += popup_Closing;

                        ToolStripControlHost host = new ToolStripControlHost(dgvMes);
                        host.Margin = new System.Windows.Forms.Padding(0);
                        host.Padding = new System.Windows.Forms.Padding(1);
                        popup.Items.Add(host);

                        popup.Show(this, new Point(rectMes.Location.X, rectMes.Location.Y + rectMes.Height), ToolStripDropDownDirection.BelowRight);


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
                        DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                        string mesText = culture.TextInfo.ToTitleCase(dtfi.GetMonthName(currentDate.Month));
                        bool vBreak = false;

                        for (int i = 0; i < dgvMes.Rows.Count; i++)
                        {
                            for (int y = 0; y < dgvMes.Columns.Count; y++)
                            {
                                if (dgvMes.Rows[i].Cells[y].Value.ToString() == mesText.Substring(0, 3))
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

                        #endregion
                    }
                    else
                    {
                        popup.Tag = null;
                    }
                }
                else if (rectAno.Contains(e.Location) == true)
                {
                    if (popup == null || popup.Tag == null)
                    {
                        #region Ano

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
                        dgvAno.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[]
                        {
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
                        btnTelaAnoDireito.BackgroundImage = Properties.Resources.ladoDireito;
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
                        btnTelaAnoEsquerdo.BackgroundImage = Properties.Resources.ladoEsquerdo;
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
                        btnTelaAnoEsquerdo.Anchor = AnchorStyles.Left;
                        btnTelaAnoEsquerdo.MouseDown += BtnTelaAnoEsquerdo_MouseDown;

                        AERMOD.LIB.Desenvolvimento.Funcoes.SetStyle(btnTelaAnoDireito, ControlStyles.Selectable, false);
                        AERMOD.LIB.Desenvolvimento.Funcoes.SetStyle(btnTelaAnoEsquerdo, ControlStyles.Selectable, false);

                        #endregion

                        Panel panelFundo = new Panel();
                        panelFundo.Location = new Point(5, 5);
                        panelFundo.MinimumSize = new Size(265, 265);
                        panelFundo.Size = panelFundo.MinimumSize;

                        panelTopo.Controls.Add(btnTelaAnoDireito);
                        panelTopo.Controls.Add(btnTelaAnoEsquerdo);
                        panelTopo.Controls.Add(lbIntervaloAno);

                        panelFundo.Controls.Add(panelTopo);
                        panelFundo.Controls.Add(dgvAno);
                        dgvAno.BringToFront();

                        anoSelecionado = currentDate.Year;

                        popup = new ToolStripDropDown();
                        popup.AutoClose = true;
                        popup.Margin = new System.Windows.Forms.Padding(0);
                        popup.Padding = new System.Windows.Forms.Padding(0);
                        popup.Opened += popup_Opened;
                        popup.Closing += popup_Closing;

                        ToolStripControlHost host = new ToolStripControlHost(panelFundo);
                        host.Margin = new System.Windows.Forms.Padding(0);
                        host.Padding = new System.Windows.Forms.Padding(1);
                        popup.Items.Add(host);

                        popup.Show(this, new Point(rectAno.Location.X + rectAno.Width, rectAno.Location.Y + rectAno.Height), ToolStripDropDownDirection.BelowLeft);

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

                        #endregion
                    }
                    else
                    {
                        popup.Tag = null;
                    }
                }
                else
                {
                    #region Cell

                    var cell = cells.FirstOrDefault(f => f.Bounds.Contains(e.Location));
                    if (cell != null)
                    {
                        if (cell.Date.HasValue)
                        {
                            currentDate = cell.Date.Value;
                        }

                        LimparSelecao();
                        if (cell.Tag.ToString() == "0" && cell.Date.HasValue)
                        {
                            cell = cell.CriarCopia();

                            DateTime dataSelecionada = cell.Date.Value;
                            PreencherCalendario();

                            var novaCell = cells.First(f => f.Date.HasValue && f.Date.Value == dataSelecionada);
                            novaCell.IsActive = true;
                            this.Invalidate(novaCell.Bounds);
                        }
                        else
                        {
                            cell.IsActive = true;
                            this.Invalidate(cell.Bounds);
                        }

                        if (cell.Date.HasValue)
                        {
                            if (cell.listEventos.Count == 0)
                            {
                                NewEventoArgs args = new NewEventoArgs();
                                args.CurrentDate = cell.Date.Value;
                                OnNewEvento(args);
                            }
                            else
                            {
                                var rectTitulo = new Rectangle(cell.Bounds.X, cell.Bounds.Y, cell.Bounds.Width, 17);
                                var rect1 = new Rectangle(cell.Bounds.X, rectTitulo.Y + rectTitulo.Height, cell.Bounds.Width, 17);
                                var rect2 = new Rectangle(cell.Bounds.X, rect1.Y + rect1.Height, cell.Bounds.Width, 17);
                                var rect3 = new Rectangle(cell.Bounds.X, rect2.Y + rect2.Height, cell.Bounds.Width, 17);
                                var rectMais = new Rectangle(cell.Bounds.X, rect3.Y + rect3.Height, cell.Bounds.Width, 17);

                                if (rectTitulo.Contains(e.Location) == true)
                                {
                                    NewEventoArgs args = new NewEventoArgs();
                                    args.CurrentDate = cell.Date.Value;
                                    OnNewEvento(args);
                                }
                                else if (cell.listEventos.Count >= 1 && rect1.Contains(e.Location) == true)
                                {
                                    InfoEventoArgs args = new InfoEventoArgs();
                                    args.Evento = cell.listEventos[0];
                                    OnInfoEvento(args);
                                }
                                else if (cell.listEventos.Count >= 2 && rect2.Contains(e.Location) == true)
                                {
                                    InfoEventoArgs args = new InfoEventoArgs();
                                    args.Evento = cell.listEventos[1];
                                    OnInfoEvento(args);
                                }
                                else if (cell.listEventos.Count >= 3 && rect3.Contains(e.Location) == true)
                                {
                                    InfoEventoArgs args = new InfoEventoArgs();
                                    args.Evento = cell.listEventos[2];
                                    OnInfoEvento(args);
                                }
                                else if (cell.listEventos.Count > 3 && rectMais.Contains(e.Location) == true)
                                {
                                    ShowEventosArgs args = new ShowEventosArgs();
                                    args.CurrentDate = cell.Date.Value;
                                    OnShowEventos(args);
                                }
                                else if (rect1.Contains(e.Location) == true || rect2.Contains(e.Location) == true || rect3.Contains(e.Location) == true || rectMais.Contains(e.Location) == true)
                                {
                                    NewEventoArgs args = new NewEventoArgs();
                                    args.CurrentDate = cell.Date.Value;
                                    OnNewEvento(args);
                                }
                            }
                        }
                    }

                    #endregion
                }
            }
            else if(e.Button == MouseButtons.Right)
            {
                #region Exclusao

                var cell = cells.FirstOrDefault(f => f.Bounds.Contains(e.Location));
                if (cell != null && cell.Date.HasValue)
                {
                    var rectTitulo = new Rectangle(cell.Bounds.X, cell.Bounds.Y, cell.Bounds.Width, 17);
                    var rect1 = new Rectangle(cell.Bounds.X, rectTitulo.Y + rectTitulo.Height, cell.Bounds.Width, 17);
                    var rect2 = new Rectangle(cell.Bounds.X, rect1.Y + rect1.Height, cell.Bounds.Width, 17);
                    var rect3 = new Rectangle(cell.Bounds.X, rect2.Y + rect2.Height, cell.Bounds.Width, 17);

                    if (cell.listEventos.Count >= 1 && rect1.Contains(e.Location) == true)
                    {
                        ContextMenuStrip mnu = new ContextMenuStrip();
                        ToolStripMenuItem mnuNenhuma = new ToolStripMenuItem("Excluir");
                        mnuNenhuma.Click += delegate
                        {
                            ExcluirEventoArgs args = new ExcluirEventoArgs();
                            args.CurrentDate = cell.Date.Value;
                            args.Evento = cell.listEventos[0];
                            OnExcluirEvento(args);
                        };
                        mnu.Items.Add(mnuNenhuma);
                        mnu.Show(Cursor.Position);
                    }
                    else if (cell.listEventos.Count >= 2 && rect2.Contains(e.Location) == true)
                    {
                        ContextMenuStrip mnu = new ContextMenuStrip();
                        ToolStripMenuItem mnuNenhuma = new ToolStripMenuItem("Excluir");
                        mnuNenhuma.Click += delegate
                        {
                            ExcluirEventoArgs args = new ExcluirEventoArgs();
                            args.CurrentDate = cell.Date.Value;
                            args.Evento = cell.listEventos[1];
                            OnExcluirEvento(args);
                        };
                        mnu.Items.Add(mnuNenhuma);
                        mnu.Show(Cursor.Position);
                    }
                    else if (cell.listEventos.Count >= 3 && rect3.Contains(e.Location) == true)
                    {
                        ContextMenuStrip mnu = new ContextMenuStrip();
                        ToolStripMenuItem mnuNenhuma = new ToolStripMenuItem("Excluir");
                        mnuNenhuma.Click += delegate
                        {
                            ExcluirEventoArgs args = new ExcluirEventoArgs();
                            args.CurrentDate = cell.Date.Value;
                            args.Evento = cell.listEventos[2];
                            OnExcluirEvento(args);
                        };
                        mnu.Items.Add(mnuNenhuma);
                        mnu.Show(Cursor.Position);
                    }
                }

                #endregion
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (e.Location.Y > 68)
            {
                if (this.Cursor != Cursors.Hand)
                {
                    this.Cursor = Cursors.Hand;
                }
            }
            else
            {
                this.Cursor = Cursors.Default;
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            if (this.Cursor != Cursors.Default)
            {
                this.Cursor = Cursors.Default;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(244, 244, 244)), new Rectangle(0, 0, this.Width, 39));
            TextRenderer.DrawText(e.Graphics, $"Hoje: {DateTime.Today.ToString("dd/MM/yyyy")}", new Font("Segoe UI", 12f, FontStyle.Regular), new Rectangle(345, 9, 160, 21), ColorTranslator.FromHtml("#1e272e"), TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

            DateTimeFormatInfo dtfi = culture.DateTimeFormat;
            TextRenderer.DrawText(e.Graphics, culture.TextInfo.ToTitleCase(dtfi.GetMonthName(currentDate.Month)), new Font("Segoe UI", 12f, FontStyle.Regular), new Rectangle(31, 9, 85, 21), ColorTranslator.FromHtml("#1e272e"), TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            TextRenderer.DrawText(e.Graphics, currentDate.ToString("yyyy"), new Font("Segoe UI", 12f, FontStyle.Regular), new Rectangle(817, 9, 66, 21), ColorTranslator.FromHtml("#1e272e"), TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

            foreach (var item in cells)
            {
                Rectangle rectTitulo = new Rectangle(item.Bounds.X + 1, item.Bounds.Y + 1, 127, 17);
                if (item.Date.HasValue)
                {
                    TextFormatFlags flags = TextFormatFlags.VerticalCenter | TextFormatFlags.Left;
                    Color foreColor = item.Tag.ToString() == "1" ? Color.FromArgb(102, 102, 102) : ColorTranslator.FromHtml("#ced6e0");

                    TextRenderer.DrawText(e.Graphics, item.Date.Value.Day.ToString(), new System.Drawing.Font("Segoe UI", 9.75F), rectTitulo, foreColor, flags);
                }

                if (item.listEventos.Count > 0)
                {
                    TextFormatFlags flags = TextFormatFlags.VerticalCenter | TextFormatFlags.Left;
                    Rectangle rect1 = new Rectangle(item.Bounds.X + 1, rectTitulo.Y + rectTitulo.Height, 127, 17);
                    Rectangle rect2 = new Rectangle(item.Bounds.X + 1, rect1.Y + rect1.Height, 127, 17);
                    Rectangle rect3 = new Rectangle(item.Bounds.X + 1, rect2.Y + rect2.Height, 127, 17);

                    TextRenderer.DrawText(e.Graphics, string.Concat(item.listEventos[0].Data_Hora.ToString("HH:mm"), " ", item.listEventos[0].Titulo), new System.Drawing.Font("Segoe UI", 9.75F), rect1, Color.FromArgb(0, 120, 215), flags);
                    if (item.listEventos.Count >= 2)
                    {
                        TextRenderer.DrawText(e.Graphics, string.Concat(item.listEventos[1].Data_Hora.ToString("HH:mm"), " ", item.listEventos[1].Titulo), new System.Drawing.Font("Segoe UI", 9.75F), rect2, Color.FromArgb(0, 120, 215), flags);
                    }
                    if (item.listEventos.Count >= 3)
                    {
                        TextRenderer.DrawText(e.Graphics, string.Concat(item.listEventos[2].Data_Hora.ToString("HH:mm"), " ", item.listEventos[2].Titulo), new System.Drawing.Font("Segoe UI", 9.75F), rect3, Color.FromArgb(0, 120, 215), flags);

                        if (item.listEventos.Count > 3)
                        {
                            TextRenderer.DrawText(e.Graphics, "...", new System.Drawing.Font("Segoe UI", 9.75F), new Rectangle(item.Bounds.X + 57, rect3.Y + rect3.Height, 20, 17), Color.FromArgb(0, 120, 215), flags);
                        }
                    }
                }

                if (item.IsActive)
                {
                    Pen penCell = new Pen(ColorTranslator.FromHtml("#95afc0"));
                    e.Graphics.DrawRectangle(penCell, new Rectangle(item.Bounds.X, item.Bounds.Y, item.Bounds.Width - 1, item.Bounds.Height - 1));

                    penCell.Dispose();
                }
            }

            Pen pen = new Pen(Color.LightGray);
            e.Graphics.DrawLine(pen, 0, 39, this.Width, 39);
            e.Graphics.DrawLine(pen, 0, 68, this.Width, 68);
            e.Graphics.DrawLine(pen, 0, 154, this.Width, 154);
            e.Graphics.DrawLine(pen, 0, 240, this.Width, 240);
            e.Graphics.DrawLine(pen, 0, 326, this.Width, 326);
            e.Graphics.DrawLine(pen, 0, 412, this.Width, 412);
            e.Graphics.DrawLine(pen, 0, 498, this.Width, 498);

            TextRenderer.DrawText(e.Graphics, "Domingo", new Font("Segoe UI", 9.75f, FontStyle.Regular), new Rectangle(1, 39, 129, 28), Color.FromArgb(102, 102, 102), TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            TextRenderer.DrawText(e.Graphics, "Segunda-feira", new Font("Segoe UI", 9.75f, FontStyle.Regular), new Rectangle(130, 39, 129, 28), Color.FromArgb(102, 102, 102), TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            TextRenderer.DrawText(e.Graphics, "Terça-feira", new Font("Segoe UI", 9.75f, FontStyle.Regular), new Rectangle(259, 39, 129, 28), Color.FromArgb(102, 102, 102), TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            TextRenderer.DrawText(e.Graphics, "Quarta-feira", new Font("Segoe UI", 9.75f, FontStyle.Regular), new Rectangle(388, 39, 129, 28), Color.FromArgb(102, 102, 102), TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            TextRenderer.DrawText(e.Graphics, "Quinta-feira", new Font("Segoe UI", 9.75f, FontStyle.Regular), new Rectangle(517, 39, 129, 28), Color.FromArgb(102, 102, 102), TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            TextRenderer.DrawText(e.Graphics, "Sexta-feira", new Font("Segoe UI", 9.75f, FontStyle.Regular), new Rectangle(646, 39, 129, 28), Color.FromArgb(102, 102, 102), TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            TextRenderer.DrawText(e.Graphics, "Sábado", new Font("Segoe UI", 9.75f, FontStyle.Regular), new Rectangle(775, 39, 129, 28), Color.FromArgb(102, 102, 102), TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

            ControlPaint.DrawBorder(e.Graphics, new Rectangle(0, 0, this.Width, this.Height), ColorTranslator.FromHtml("#a5b1c2"), ButtonBorderStyle.Solid);

            pen.Dispose();
        }




        private void popup_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            if (e.CloseReason == ToolStripDropDownCloseReason.AppClicked)
            {
                var rectMes = new Rectangle(31, 9, 85, 21);
                var rectAno = new Rectangle(817, 9, 66, 21);

                if (rectMes.Contains(this.PointToClient(MousePosition)))
                {
                    popup.Tag = true;
                    return;
                }
                else if (rectAno.Contains(this.PointToClient(MousePosition)))
                {
                    popup.Tag = true;
                    return;
                }
            }

            popup.Tag = null;
        }

        private void popup_Opened(object sender, EventArgs e)
        {
            var control = (popup.Items[0] as ToolStripControlHost).Control;
            if (control is DataGridView)
            {
                DataGridView grid = control as DataGridView;
                grid.Focus();
            }
            else if (control is Panel)
            {
                Panel panel = control as Panel;
                DataGridView grid = (DataGridView)panel.Controls.Cast<Control>().First(f => f.GetType() == typeof(DataGridView));
                grid.Focus();
            }
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

        private void BtnEsqMes_MouseDown(object sender, MouseEventArgs e)
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
                AtivarData(currentDate);
            }
        }

        private void BtnDirMes_MouseDown(object sender, MouseEventArgs e)
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
                AtivarData(currentDate);
            }
        }

        private void BtnTelaAnoEsquerdo_MouseDown(object sender, MouseEventArgs e)
        {
            Control panelFundo = (sender as Control).Parent.Parent;

            DataGridView dgvAno = panelFundo.Controls.Find("dgvAno", false).FirstOrDefault() as DataGridView;

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
            Control panelFundo = (sender as Control).Parent.Parent;

            DataGridView dgvAno = panelFundo.Controls.Find("dgvAno", false).FirstOrDefault() as DataGridView;
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

        private void BtnEsqAno_MouseDown(object sender, MouseEventArgs e)
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
                AtivarData(currentDate);
            }
        }

        private void BtnDirAno_MouseDown(object sender, MouseEventArgs e)
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
                AtivarData(currentDate);
            }
        }

        private void Item_ShowEventos(object sender, EventArgs e)
        {
            //var item = sender as CalendarItem;
            //if (item.Tag.ToString() == "0")
            //{
            //    Int32 diaSelecionado = Convert.ToInt32(item.Dia);
            //    DateTime mesSelecionado = currentDate.AddMonths(diaSelecionado > 15 ? -1 : 1);

            //    currentDate = new DateTime(mesSelecionado.Year, mesSelecionado.Month, diaSelecionado);
            //    PreencherCalendario();
            //}
            //else
            //{
            //    currentDate = new DateTime(currentDate.Year, currentDate.Month, Convert.ToInt32(item.Dia));
            //}

            //if (item.Tag.ToString() == "0")
            //{
            //    AtivarData(currentDate.Day);
            //}

            //FrmListarEventos frmListar = new FrmListarEventos(data);
            //frmListar.Deactivate += FrmListar_Deactivate;
            //frmListar.Show(this);
        }

        /*
        private void Item_SelectedEventoChanged(object sender, SelectedEventoArgs e)
        {
            var item = sender as CalendarItem;
            if (item.Tag.ToString() == "0")
            {
                Int32 diaSelecionado = Convert.ToInt32(item.Dia);
                DateTime mesSelecionado = currentDate.AddMonths(diaSelecionado > 15 ? -1 : 1);

                currentDate = new DateTime(mesSelecionado.Year, mesSelecionado.Month, diaSelecionado);
                PreencherCalendario();
            }
            else
            {
                currentDate = new DateTime(currentDate.Year, currentDate.Month, Convert.ToInt32(item.Dia));
            }

            if (item.Tag.ToString() == "0")
            {
                AtivarData(currentDate.Day);
            }

            //FrmInfoEvento frmInfo = new FrmInfoEvento(e.Evento);
            //frmInfo.Deactivate += FrmInfo_Deactivate;
            //frmInfo.Show(this);
        }

        private void Item_NewEvento(object sender, EventArgs e)
        {
            var item = sender as CalendarItem;
            if (item.Tag.ToString() == "0")
            {
                Int32 diaSelecionado = Convert.ToInt32(item.Dia);
                DateTime mesSelecionado = currentDate.AddMonths(diaSelecionado > 15 ? -1 : 1);

                currentDate = new DateTime(mesSelecionado.Year, mesSelecionado.Month, diaSelecionado);
                PreencherCalendario();
            }
            else
            {
                currentDate = new DateTime(currentDate.Year, currentDate.Month, Convert.ToInt32(item.Dia));
            }

            if (item.Tag.ToString() == "0")
            {
                AtivarData(currentDate.Day);
            }

            //FrmNovoEvento frmNovo = new FrmNovoEvento(data);
            //frmNovo.ShowDialog(this);
        }

    */

        private void FrmListar_Deactivate(object sender, EventArgs e)
        {
            (sender as Form).Close();
        }

        private void FrmInfo_Deactivate(object sender, EventArgs e)
        {
            (sender as Form).Close();
        }
    }

    public delegate void LoadEventosHandler(object sender, LoadEventosArgs e);

    public class LoadEventosArgs : EventArgs
    {
        public DateTime CurrentDate { get; set; }
    }

    public delegate void NewEventoHandler(object sender, NewEventoArgs e);

    public class NewEventoArgs : EventArgs
    {
        public DateTime CurrentDate { get; set; }
    }

    public delegate void InfoEventoHandler(object sender, InfoEventoArgs e);

    public class InfoEventoArgs : EventArgs
    {
        public Evento Evento { get; set; }
    }

    public delegate void ShowEventosHandler(object sender, ShowEventosArgs e);

    public class ShowEventosArgs : EventArgs
    {
        public DateTime CurrentDate { get; set; }
    }

    public delegate void ExcluirEventoHandler(object sender, ExcluirEventoArgs e);

    public class ExcluirEventoArgs : EventArgs
    {
        public DateTime CurrentDate { get; set; }

        public Evento Evento { get; set; }
    }
}
