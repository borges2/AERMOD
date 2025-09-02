using AERMOD.LIB.Desenvolvimento;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AERMOD.LIB.Componentes.GridView
{
    public class DataGridViewCheckBoxLIBColumn : DataGridViewCheckBoxColumn
    {
        #region Variáveis

        int linhasDGV = 0; //Armaneza quantidade total de linhas que o DataGridView possui       
        DatagridViewCheckBoxHeaderCell chkHeader = new DatagridViewCheckBoxHeaderCell();

        bool executarValueChanged = true;

        #endregion

        #region Propriedades

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
                    chkHeader.ShowHeader = value;
                }
            }
        }

        #endregion

        #region Clone

        public override object Clone()
        {
            DataGridViewCheckBoxLIBColumn novoObj = (DataGridViewCheckBoxLIBColumn)base.Clone();
            novoObj.ShowHeader = this.ShowHeader;

            return novoObj;
        }

        #endregion

        #region OnDataGridViewChanged

        protected override void OnDataGridViewChanged()
        {
            if (this.DataGridView != null)
            {
                CrossThreadOperation.Invoke(this.DataGridView, delegate
                {
                    this.HeaderCell = chkHeader;
                    this.HeaderText = string.Empty;
                });
            }

            chkHeader.OnCheckBoxClicked += new CheckBoxClickedHandler(chkHeader_OnCheckBoxClicked);

            if (this.DataGridView != null)
            {
                this.DataGridView.MultiSelect = false;
                this.DataGridView.CellValueChanged += new DataGridViewCellEventHandler(DataGridView_CellValueChanged);
                this.DataGridView.CurrentCellDirtyStateChanged += new EventHandler(DataGridView_CurrentCellDirtyStateChanged);
                this.DataGridView.CellEnter += new DataGridViewCellEventHandler(DataGridView_CellEnter);
                this.DataGridView.RowsRemoved += new DataGridViewRowsRemovedEventHandler(DataGridView_RowsRemoved);
                this.DataGridView.KeyUp += new KeyEventHandler(DataGridView_KeyUp);
                this.DataGridView.RowsAdded += new DataGridViewRowsAddedEventHandler(DataGridView_RowsAdded);
            }

            base.OnDataGridViewChanged();
        }

        #endregion

        #region Eventos DataGridView

        private void DataGridView_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control == false && e.Alt == false && e.KeyCode == Keys.Space)
            {
                if (this.DataGridView != null && this.DataGridView.RowCount > 0)
                {
                    if (this.DataGridView.CurrentRow.Index < this.DataGridView.RowCount - 1)
                    {
                        if (this.DataGridView.CurrentCell.ColumnIndex == this.Index)
                        {
                            if (this.DataGridView.CurrentRow.Cells[this.Index].ReadOnly == true)
                            {
                                return;
                            }

                            if (Convert.ToBoolean(this.DataGridView.CurrentRow.Cells[this.Index].Value) == false)
                            {
                                this.DataGridView.CurrentRow.Cells[this.Index].Value = true;
                            }
                            else
                            {
                                this.DataGridView.CurrentRow.Cells[this.Index].Value = false;
                            }
                            int linha = this.DataGridView.CurrentRow.Index;
                            this.DataGridView.Rows[linha + 1].Cells[this.Index].Selected = true;
                            e.Handled = true;
                        }
                    }
                }
            }
        }

        void DataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (this.DataGridView != null)
            {
                if (this.DataGridView.CurrentCell.ColumnIndex == this.Index)
                {
                    CrossThreadOperation.Invoke(this.DataGridView, delegate { this.DataGridView.ReadOnly = false; });
                }
            }
        }

        private void DataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (this.DataGridView != null)
            {
                if (this.DataGridView.CurrentCell is DataGridViewCheckBoxCell)
                {
                    this.DataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
            }
        }

        private void DataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (this.DataGridView != null && e.ColumnIndex >= 0 && e.RowIndex >= 0 && e.ColumnIndex == this.Index)
            {
                if (executarValueChanged == false)
                {
                    return;
                }

                Boolean existeFalse = this.DataGridView.Rows.Cast<DataGridViewRow>().Any(f => Convert.ToBoolean(f.Cells[e.ColumnIndex].Value) == false);

                if (existeFalse == true)
                {
                    if (chkHeader.Checked == true)
                    {
                        chkHeader.Checked = false;
                        this.DataGridView.RefreshEdit();
                    }
                }
                else
                {
                    chkHeader.Checked = true;
                    this.DataGridView.RefreshEdit();
                }
            }

            //if (this.DataGridView != null && e.ColumnIndex >= 0 && e.RowIndex >= 0) //&& (this.DataGridView.CurrentRow != null && this.DataGridView.CurrentRow.Cells[e.ColumnIndex].Visible == true))
            //{
            //    if (this.DataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewCheckBoxCell)
            //    {
            //        if (clickedHeader == false)
            //        {
            //            RowCheckBoxClick((DataGridViewCheckBoxCell)this.DataGridView[e.ColumnIndex, e.RowIndex]);
            //        }
            //    }
            //}
        }

        private void DataGridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            linhasDGV--;

            if (this.DataGridView != null)
            {
                if (linhasDGV == 0)
                {
                    chkHeader.Checked = false;
                    this.DataGridView.RefreshEdit();
                }
                else
                {
                    Int32 numeroLinhas = this.DataGridView.Rows.Cast<DataGridViewRow>().Where(f => Convert.ToBoolean(f.Cells[this.Index].Value) == true).Count();
                    if (numeroLinhas == linhasDGV)
                    {
                        chkHeader.Checked = true;
                        this.DataGridView.RefreshEdit();
                    }
                }
            }

            //if (this.DataGridView != null && linhasDGV == 0) chkHeader.Checked = false;
        }

        private void DataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (this.DataGridView != null)
            {
                linhasDGV = this.DataGridView.RowCount;

                #region Marcar HeaderCheckBox

                if (chkHeader.Checked == true)
                {
                    executarValueChanged = false;

                    this.DataGridView.Rows[e.RowIndex].Cells[this.Index].Value = true;
                    this.DataGridView.RefreshEdit();

                    executarValueChanged = true;
                }

                //Boolean existeFalse = this.DataGridView.Rows.OfType<DataGridViewRow>().Any(f => Convert.ToBoolean(f.Cells[this.Index].Value) == false);

                //if (existeFalse == true)
                //{
                //    if (chkHeader.Checked == true)
                //    {
                //        chkHeader.Checked = false;
                //        this.DataGridView.RefreshEdit();
                //    }
                //}
                //else
                //{
                //    chkHeader.Checked = true;
                //    this.DataGridView.RefreshEdit();
                //}

                #endregion
            }
        }

        #endregion

        #region Eventos HeaderCell

        void chkHeader_OnCheckBoxClicked(bool state)
        {
            if (chkHeader.HeaderClicado == true)
            {
                executarValueChanged = false;

                for (Int32 i = 0; i < this.DataGridView.Rows.Count; i++)
                {
                    this.DataGridView.Rows[i].Cells[this.Index].Value = state;
                    this.DataGridView.RefreshEdit();
                }
                executarValueChanged = true;
            }
        }

        #endregion

        #region RowCheckBoxClick

        private void RowCheckBoxClick(DataGridViewCheckBoxCell RCheckBox)
        {
            if (RCheckBox != null)
            {
                Boolean existeFalse = this.DataGridView.Rows.Cast<DataGridViewRow>().Any(f => Convert.ToBoolean(f.Cells[this.Index].Value) == false);

                if (existeFalse == true)
                {
                    if (chkHeader.Checked == true)
                    {
                        chkHeader.Checked = false;
                        this.DataGridView.RefreshEdit();
                    }
                }
                else
                {
                    chkHeader.Checked = true;
                    this.DataGridView.RefreshEdit();
                }
            }
        }

        #endregion
    }
}
