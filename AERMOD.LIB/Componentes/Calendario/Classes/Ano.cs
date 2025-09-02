using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Netsof.LIB.Componentes.Calendario.Classes
{
    public class Ano
    {
        MonthCalendarStyleLIB identificador = null;

        public Ano(MonthCalendarStyleLIB ident)
        {
            identificador = ident;
        }
        
        List<Control> listControles = new List<Control>();

        /// <summary>
        /// Adiciona os controles do ano
        /// </summary>
        /// <param name="controles"></param>
        public void AddControles(Control[] controles)
        {
            listControles.AddRange(controles);
        }

        /// <summary>
        /// Evento mouseDown do botao Down
        /// </summary>
        public void MouseDownBaixo()
        {
            ((PictureBox)listControles.Find(f => f.Name == "pictureBoxBaixo")).Image = AERMOD.LIB.Properties.Resources.baixo_select;

            if (Convert.ToInt32(listControles.Find(f => f.Name == "lbAno").Text) > 1753)
            {
                listControles.Find(f => f.Name == "lbAno").Text = (Convert.ToInt32(listControles.Find(f => f.Name == "lbAno").Text) - 1).ToString();
                Base.RetornaClassDias(identificador).CarregaDias(listControles.Find(f => f.Name == "lbMes").Text, Convert.ToInt32(listControles.Find(f => f.Name == "lbAno").Text));
                Base.RetornaClassDias(identificador).MarcaDataSelecionada();                
            }
        }

        /// <summary>
        /// Evento mouseUp do botao Down
        /// </summary>
        public void MouseUpBaixo()
        {
            ((PictureBox)listControles.Find(f => f.Name == "pictureBoxBaixo")).Image = AERMOD.LIB.Properties.Resources.baixo;
        }

        /// <summary>
        /// Evento mouseDown do botao Up
        /// </summary>
        public void MouseDownCima()
        {
            ((PictureBox)listControles.Find(f => f.Name == "pictureBoxCima")).Image = AERMOD.LIB.Properties.Resources.cima_select;

            if (Convert.ToInt32(listControles.Find(f => f.Name == "lbAno").Text) < 2099)
            {
                listControles.Find(f => f.Name == "lbAno").Text = (Convert.ToInt32(listControles.Find(f => f.Name == "lbAno").Text) + 1).ToString();
                Base.RetornaClassDias(identificador).CarregaDias(listControles.Find(f => f.Name == "lbMes").Text, Convert.ToInt32(listControles.Find(f => f.Name == "lbAno").Text));
                Base.RetornaClassDias(identificador).MarcaDataSelecionada();
            }
        }

        /// <summary>
        /// Evento mouseUp do botao Up
        /// </summary>
        public void MouseUpCima()
        {
            ((PictureBox)listControles.Find(f => f.Name == "pictureBoxCima")).Image = AERMOD.LIB.Properties.Resources.cima;
        }
    }
}
