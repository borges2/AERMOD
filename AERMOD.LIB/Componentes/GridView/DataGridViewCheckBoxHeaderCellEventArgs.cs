using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AERMOD.LIB.Componentes.GridView
{
    public delegate void CheckBoxClickedHandler(bool state);
    public class DataGridViewCheckBoxHeaderCellEventArgs : EventArgs
    {
        #region Variáveis

        bool _bChecked;

        #endregion

        #region Construtor

        public DataGridViewCheckBoxHeaderCellEventArgs(bool bChecked)
        {
            _bChecked = bChecked;
        }

        #endregion

        #region Propriedades

        public bool Checked
        {
            get { return _bChecked; }
        }

        #endregion
    }
}
