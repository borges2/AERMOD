using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;
using Netsof.LIB.Componentes;
using System.Runtime.InteropServices;
using AERMOD.LIB.Componentes.Botao;
using AERMOD.LIB.Componentes;

namespace AERMOD.LIB.Desenvolvimento
{
    public static class GetControls
    {
        [DllImport("user32.dll")]
        static extern IntPtr WindowFromPoint(Point point);
        
        public static List<Control> GetAllControls(IList ctrls)
        {
            List<Control> RetCtrls = new List<Control>();
            foreach (Control ctl in ctrls)
            {
                RetCtrls.Add(ctl);
                List<Control> SubCtrls = GetAllControls(ctl.Controls);
                RetCtrls.AddRange(SubCtrls);
            }
            return RetCtrls;
        }

        /// <summary>
        /// Verifica se o mouse esta sobre determinado controle
        /// </summary>
        /// <param name="controle">Controle a ser verificador</param>
        /// <returns></returns>
        public static bool CheckMouseOverControl(Control controle)
        {
            bool over = false;
            if (Form.MousePosition.X >= controle.PointToScreen(new Point()).X && Form.MousePosition.Y >= controle.PointToScreen(new Point()).Y)
            {
                if (Form.MousePosition.X <= controle.PointToScreen(new Point()).X + controle.Width && Form.MousePosition.Y <= controle.PointToScreen(new Point()).Y + controle.Height)
                {
                    over = true;
                }
            }

            return over;
        }

        /// <summary>
        /// Verifica se o mouse esta sobre alguns dos controles
        /// </summary>
        /// <param name="listControle">Lista de controle a ser verificado</param>
        /// <returns></returns>
        public static ControlMouseHover CheckMouseOverControl(List<Control> listControle)
        {
            ControlMouseHover hover = new ControlMouseHover();
            foreach (Control controle in listControle)
            {
                if (controle.CanSelect == true)
                {
                    if (Form.MousePosition.X >= controle.PointToScreen(new Point()).X && Form.MousePosition.Y >= controle.PointToScreen(new Point()).Y)
                    {
                        if (Form.MousePosition.X <= controle.PointToScreen(new Point()).X + controle.Width && Form.MousePosition.Y <= controle.PointToScreen(new Point()).Y + controle.Height)
                        {
                            hover.IsHover = true;
                            hover.Controle = controle;

                            break;
                        }
                    }
                }
            }

            return hover;
        }

        /// <summary>
        /// Get o controle a onde esta o mouse
        /// </summary>
        /// <returns></returns>
        public static Control GetControlMouseOver()
        {
            IntPtr hwnd = WindowFromPoint(Control.MousePosition);
            Control controle = Control.FromHandle(hwnd);

            if (controle != null && controle is ButtonLIB)
            {
                if (controle.Parent != null && controle.Parent.Parent != null && controle.Parent.Parent is DataLIB)
                {
                    controle = controle.Parent.Parent;
                }
            }

            return controle;
        }


        /// <summary>
        /// Obtem o proximo controle
        /// </summary>
        /// <param name="ctl">ActiveControl</param>
        /// <param name="enabled">Somente controles habilitados</param>
        /// <param name="controlException">Controles que não serão considerados</param>
        /// <returns></returns>
        public static Control GetNextControl(Control ctl, bool enabled, Control[] controlException)
        {
            Form form = ctl.FindForm();

            bool forward = true;
            bool tabStopOnly = true;
            bool nested = true;
            bool wrap = true;

            if (!form.Contains(ctl) || (!nested && (ctl.Parent != form)))
            {
                ctl = null;
            }
            bool flag = false;
            Control control = ctl;
            do
            {
                ctl = form.GetNextControl(ctl, forward);
                if (ctl == null)
                {
                    if (!wrap)
                    {
                        break;
                    }
                    if (flag)
                    {
                        return null;
                    }
                    flag = true;
                }
                else if (((ctl.CanSelect || ctl.Enabled == enabled) && (!tabStopOnly || ctl.TabStop)) && (nested || (ctl.Parent == form)))
                {
                    if (enabled == true)
                    {
                        if ((ctl.CanSelect == true && (!tabStopOnly || ctl.TabStop)) && (nested || (ctl.Parent == form)))
                        {
                            if (controlException == null || controlException.Contains(ctl) == false)
                            {
                                return ctl;
                            }
                        }
                    }
                    else
                    {
                        if ((ctl.CanSelect == false && (!tabStopOnly || ctl.TabStop)) && (nested || (ctl.Parent == form)))
                        {
                            if (controlException == null || controlException.Contains(ctl) == false)
                            {
                                return ctl;
                            }
                        }
                    }
                }
            }
            while (ctl != control);
            return null;
        }

        /// <summary>
        /// Obtem o próximo controle habilitado disponível para ser ativado e ganhar foco.
        /// </summary>
        /// <param name="container">Container onde possui os controles.</param>
        /// <returns></returns>
        public static Control ProximoControle(Control container)
        {
            Control controle = null;
            List<Control> ListaControles = GetControls.GetAllControls(container.Controls).Where(I => I.Enabled == true && I.GetType().GetProperties().Any(X => X.Name == "HotKey")).ToList();
            //List<Control> ListaControles = GetControls.GetAllControls(container.Controls).Where(I => I.Enabled == true && I.GetType().GetProperties().Any(X => X.Name == "HotKey") && I.GetType().GetProperties().Any(X => X.Name == "CampoHabilitado") && I.GetType().GetProperty("CampoHabilitado").GetValue(I, null).Equals(true)).ToList();
            if (ListaControles.Count > 0) controle = ListaControles.FirstOrDefault(I => I.TabIndex == ListaControles.Min(X => X.TabIndex));
            return controle;
        }
    }

    public class ControlMouseHover
    {
        public bool IsHover { get; set; }
        public Control Controle { get; set; }
    }
}
