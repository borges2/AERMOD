using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AERMOD.LIB.Componentes.GridView
{
    public static class DataGridViewScrollBar
    {
        /// <summary>
        /// Exibe ou esconde a scrollBar no dataGridView
        /// </summary>
        /// <param name="x">DataGridView</param>
        /// <param name="visible">True ou False</param>
        public static void ScrollBarVisible(this Control dataGrid, Boolean visible)
        {
            Boolean controleExiste = false;
            foreach (Control controle in dataGrid.Controls)
            {
                if (controle.Name == "barraVertical")
                {
                    controle.Visible = visible;
                    controleExiste = true;
                    break;
                }
            }

            if (controleExiste == false)
            {
                VScrollBar verticalScrollBar = new VScrollBar();
                verticalScrollBar.Name = "barraVertical";
                dataGrid.Controls.Add(verticalScrollBar);

                verticalScrollBar.Location = new Point(dataGrid.Width - verticalScrollBar.Width - 1, 1);
                verticalScrollBar.Size = new Size(verticalScrollBar.Size.Width, dataGrid.Height - 2);

                verticalScrollBar.Enabled = false;

                verticalScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)
                        | AnchorStyles.Bottom)));
            }
        }
    }
}
