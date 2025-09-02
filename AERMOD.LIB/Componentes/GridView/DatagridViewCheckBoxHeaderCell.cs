using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AERMOD.LIB.Componentes.GridView
{
    public class DatagridViewCheckBoxHeaderCell : DataGridViewColumnHeaderCell
    {
        #region Variáveis

        Point checkBoxLocation;
        Size checkBoxSize;
        bool _checked = false;
        Point _cellLocation = new Point();
        System.Windows.Forms.VisualStyles.CheckBoxState _cbState = System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal;
        public event CheckBoxClickedHandler OnCheckBoxClicked;

        public bool executarMouseClick = true;

        #endregion

        #region Propriedade

        /// <summary>
        /// Propriedade para saber se HeaderCell está checado ou não.
        /// Setar check (true/false) no HeaderCell qdo todas as linhas estão marcadas ou não.
        /// </summary>
        public bool Checked
        {
            get { return _checked; }
            set
            {
                _checked = value;
                HeaderClicado = false;
                SetarCheckbox();
            }
        }

        private bool showHeader = true;

        [DefaultValue(true)]
        public bool ShowHeader
        {
            get { return showHeader; }
            set
            {
                if (showHeader != value)
                {
                    showHeader = value;

                    if (this.DataGridView != null)
                    {
                        this.DataGridView.InvalidateCell(this);
                    }
                }
            }
        }


        /// <summary>
        /// Propriedade para saber se o HeaderCell foi clicado ou não.
        /// </summary>
        public bool HeaderClicado { get; set; }

        #endregion

        #region Construtor

        public DatagridViewCheckBoxHeaderCell()
        {
        }

        #endregion

        #region Paint

        protected override void Paint(System.Drawing.Graphics graphics,
        System.Drawing.Rectangle clipBounds,
        System.Drawing.Rectangle cellBounds,
        int rowIndex,
        DataGridViewElementStates dataGridViewElementState,
        object value,
        object formattedValue,
        string errorText,
        DataGridViewCellStyle cellStyle,
        DataGridViewAdvancedBorderStyle advancedBorderStyle,
        DataGridViewPaintParts paintParts)
        {
            if (this.ShowHeader == false)
            {
                base.Paint(graphics, clipBounds, cellBounds, rowIndex, dataGridViewElementState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);

                return;
            }

            base.Paint(graphics, clipBounds, cellBounds, rowIndex,
            dataGridViewElementState, value,
            formattedValue, errorText, cellStyle,
            advancedBorderStyle, paintParts);
            Point p = new Point();
            Size s = CheckBoxRenderer.GetGlyphSize(graphics,
            System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal);

            List<DataGridViewColumn> ListaColunas = this.DataGridView.Columns.OfType<DataGridViewColumn>().Where(c => c.GetType() == typeof(DataGridViewCheckBoxLIBColumn)).ToList();

            foreach (DataGridViewColumn coluna in ListaColunas)
            {
                if (coluna.Index == 0)
                {
                    p.X = cellBounds.Location.X +
                    (cellBounds.Width / 2) - (s.Width / 2);
                }
                else
                {
                    p.X = cellBounds.Location.X +
                    (cellBounds.Width / 2) - (s.Width / 2) - 1;
                }
            }

            p.Y = cellBounds.Location.Y +
            (cellBounds.Height / 2) - (s.Height / 2);
            _cellLocation = cellBounds.Location;
            checkBoxLocation = p;
            checkBoxSize = s;
            if (_checked)
                _cbState = System.Windows.Forms.VisualStyles.
                CheckBoxState.CheckedNormal;
            else
                _cbState = System.Windows.Forms.VisualStyles.
                CheckBoxState.UncheckedNormal;
            CheckBoxRenderer.DrawCheckBox
            (graphics, checkBoxLocation, _cbState);

        }

        #endregion

        #region OnMouseClick

        protected override void OnMouseClick(DataGridViewCellMouseEventArgs e)
        {
            Point p = new Point(e.X + _cellLocation.X, e.Y + _cellLocation.Y);

            if (p.X >= checkBoxLocation.X && p.X <= checkBoxLocation.X + checkBoxSize.Width &&
                p.Y >= checkBoxLocation.Y && p.Y <= checkBoxLocation.Y + checkBoxSize.Height)
            {
                if (executarMouseClick)
                {
                    _checked = !_checked;
                    if (OnCheckBoxClicked != null)
                    {
                        HeaderClicado = true;
                        OnCheckBoxClicked(_checked);
                        this.DataGridView.InvalidateCell(this);
                    }
                }
            }
            base.OnMouseClick(e);
        }

        #endregion

        #region SetarCheckBox

        private void SetarCheckbox()
        {
            if (OnCheckBoxClicked != null)
            {
                OnCheckBoxClicked(_checked);
                this.DataGridView.InvalidateCell(this);
            }
        }

        #endregion
    }
}
