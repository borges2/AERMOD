using AERMOD.LIB.Desenvolvimento;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AERMOD.LIB.Forms
{
    [DebuggerStepThrough]
    public static class FormShowDialogFade
    {
        #region Classes e Propriedades

        [DllImport("user32")]
        internal static extern bool EnableWindow(IntPtr hwnd, bool bEnable);

        #endregion

        #region Métodos

        /// <summary>
        /// Abre o form com efeito de escurecimento(Fade)
        /// </summary>        
        /// <param name="formAbrir">Form que será aberto</param>
        /// <param name="formPai">Form utilizado(this)</param>
        /// <param name="activeOwner">Se ao fechar o fade o form Owner será ativo</param>
        [Description("Abre o form com efeito de escurecimento(Fade)")]
        public static void ShowDialogFade(this Form formAbrir, Form formPai, bool activeOwner = true)
        {
            formAbrir.StartPosition = FormStartPosition.Manual;

            FrmFade frmFade = new FrmFade();
            if (activeOwner == true)
            {
                frmFade.FormClosed += new FormClosedEventHandler(frmFade_FormClosed);
            }

            //if (formFilho.InvokeRequired == true || (formPai != null && formPai.InvokeRequired == true))
            if (formPai != null)
            {
                formPai.Invoke((Action)(() =>
                {
                    frmFade.Show(formPai);

                    frmFade.Size = new Size(formPai.Size.Width - 15, formPai.Size.Height - 6); //formPai.Size;
                    frmFade.Location = new Point(formPai.Location.X + 8, formPai.Location.Y);
                }));

                //CrossThreadOperation.Invoke(formPai, delegate
                //{
                //    frmFade.Show(formPai);
                //
                //    frmFade.Size = formPai.Size;
                //    frmFade.Location = formPai.Location;
                //});
            }
            else
            {
                frmFade.Show(formPai);

                frmFade.Size = formPai.Size;
                frmFade.Location = formPai.Location;
            }

            Int32 x = 0;
            Int32 y = 0;

            if (formPai.Size.Width >= formAbrir.Size.Width)
            {
                x = formPai.DesktopLocation.X + ((formPai.Size.Width - formAbrir.Size.Width) / 2);
            }
            else
            {
                x = formPai.DesktopLocation.X - ((formAbrir.Size.Width - formPai.Size.Width) / 2);
            }

            if (formPai.Height >= formAbrir.Size.Height)
            {
                y = formPai.DesktopLocation.Y + ((formPai.Size.Height - formAbrir.Size.Height) / 2);
            }
            else
            {
                y = formPai.DesktopLocation.Y - ((formAbrir.Size.Height - formPai.Size.Height) / 2);
            }

            formAbrir.Location = new Point(x, y);

            Point lastLocation = formAbrir.Location;

            formAbrir.Move += delegate
            {
                if (formAbrir.Visible == true && formAbrir == Form.ActiveForm && Form.MouseButtons == MouseButtons.Left)
                {
                    Int32 xDif = formAbrir.Location.X - lastLocation.X;
                    Int32 yDif = formAbrir.Location.Y - lastLocation.Y;

                    formPai.Location = new Point(formPai.Location.X + xDif, formPai.Location.Y + yDif);
                    frmFade.Location = formPai.Location;
                }
                lastLocation = formAbrir.Location;
            };

            formAbrir.Shown += delegate
            {
                Form formDesktop = Application.OpenForms.Cast<Form>().FirstOrDefault(f => f.Name == "FrmDesktop");
                if (formDesktop != null)
                {
                    EnableWindow(formDesktop.Handle, true);
                }
            };

            //if (formFilho.InvokeRequired == true || (formPai != null && formPai.InvokeRequired == true))
            if (formPai != null)
            {
                formPai.Invoke((Action)(() =>
                {
                    formAbrir.ShowDialog(frmFade);
                    frmFade.Close();
                }));
                //CrossThreadOperation.Invoke(formPai, delegate
                //{
                //    formFilho.ShowDialog(frmFade);
                //    frmFade.Close();
                //});
            }
            else
            {
                formAbrir.ShowDialog(frmFade);
                frmFade.Close();
            }
        }

        public static void ShowDialogFade(this Form formAbrir, Form formPai, LocationManual localizacaoManual)
        {
            //Crio umas instancia do Fade
            FrmFade frmFade = new FrmFade();
            frmFade.FormClosed += new FormClosedEventHandler(frmFade_FormClosed);
            frmFade.Show(formPai);

            frmFade.Size = formPai.Size;
            frmFade.Location = formPai.Location;

            formAbrir.ShowDialog();
            frmFade.Close();
        }

        /// <summary>
        /// Abre o form com efeito de escurecimento(Fade)
        /// </summary>        
        /// <param name="formAbrir">Form que será aberto</param>
        /// <param name="formPai">Form utilizado(this)</param>
        /// <param name="activeOwner">Se ao fechar o fade o form Owner será ativo</param>
        [Description("Abre o form com efeito de escurecimento(Fade)")]
        public static FrmFade ShowFade(this Form formAbrir, Form formPai, bool activeOwner = true)
        {
            formAbrir.StartPosition = FormStartPosition.Manual;

            //Crio umas instancia do Fade
            FrmFade frmFade = new FrmFade();
            if (activeOwner == true)
            {
                frmFade.FormClosed += new FormClosedEventHandler(frmFade_FormClosed);

            }

            if (formAbrir.InvokeRequired == true || (formPai != null && formPai.InvokeRequired == true))
            {
                CrossThreadOperation.Invoke(formPai, delegate
                {
                    frmFade.Show(formPai);

                    frmFade.Size = formPai.Size;
                    frmFade.Location = formPai.Location;
                });
            }
            else
            {
                frmFade.Show(formPai);

                frmFade.Size = formPai.Size;
                frmFade.Location = formPai.Location;
            }

            Int32 x = 0;
            Int32 y = 0;

            if (formPai.Size.Width >= formAbrir.Size.Width)
            {
                x = formPai.DesktopLocation.X + ((formPai.Size.Width - formAbrir.Size.Width) / 2);
            }
            else
            {
                x = formPai.DesktopLocation.X - ((formAbrir.Size.Width - formPai.Size.Width) / 2);
            }

            if (formPai.Height >= formAbrir.Size.Height)
            {
                y = formPai.DesktopLocation.Y + ((formPai.Size.Height - formAbrir.Size.Height) / 2);
            }
            else
            {
                y = formPai.DesktopLocation.Y - ((formAbrir.Size.Height - formPai.Size.Height) / 2);
            }

            formAbrir.Location = new Point(x, y);

            formAbrir.Tag = frmFade;
            EventHandler delegateAtivarFilho = delegate
            {
                if (formAbrir.InvokeRequired == true)
                {
                    CrossThreadOperation.Invoke(formAbrir, delegate { formAbrir.Activate(); });
                }
                else
                {
                    formAbrir.Activate();
                }
            };

            formPai.Activated += delegateAtivarFilho;
            frmFade.Activated += delegateAtivarFilho;
            formAbrir.FormClosing += delegate
            {
                formPai.Activated -= delegateAtivarFilho;
                frmFade.Close();
            };

            formAbrir.Show();
            formAbrir.VisibleChanged += new EventHandler(formAbrir_VisibleChanged);

            return frmFade;
        }

        #endregion

        #region Eventos formAbrir

        private static void formAbrir_VisibleChanged(object sender, EventArgs e)
        {
            ((FrmFade)((Form)sender).Tag).Visible = ((Form)sender).Visible;
        }

        #endregion

        #region Eventos frmFade

        private static void frmFade_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((Form)sender).Owner.Activate();
        }

        #endregion
    }

    public enum LocationManual
    {
        SetLocation
    }
}
