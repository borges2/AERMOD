using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AERMOD.LIB.Componentes.Splash
{
    /// <summary>
    /// Para chamada
    ///<para>        Thread splashthread = new Thread(new ThreadStart(SplashScreen.ShowSplashScreen));</para>
    ///<para>        splashthread.IsBackground = true;</para>
    ///<para>        splashthread.Start();</para>
    ///<para>        Criar a instancia e abrir o Form </para>
    ///<para>        Para fechamento</para>
    ///<para>         SplashScreen.CloseSplashScreen();</para>
    ///<para>         this.Activate();</para>
    ///               Utilizado normalmento no evento Load
    /// </summary>
    public static class SplashScreen
    {
        static SplashScreenForm sf = null;

        private static int timerSeconds = 5;

        public static int TimerSeconds
        {
            get { return timerSeconds; }
            set { timerSeconds = value; }
        }

        private static StyleProgress styleProgress = StyleProgress.Blocks;

        public static StyleProgress StyleProgress
        {
            get { return styleProgress; }
            set { styleProgress = value; }
        }

        private static string message = string.Empty;

        public static string Message
        {
            get { return message; }
            set { message = value; }
        }

        private static Point? location;

        public static Point? Location
        {
            get
            {
                return location;
            }
            set { location = value; }
        }

        /// <summary>
        /// Get or Set Handle Parent
        /// </summary>
        public static HandleRef? HandleParent { get; set; }

        [DllImport("user32.dll", EntryPoint = "SetWindowLong", CharSet = CharSet.Auto)]
        public static extern IntPtr SetWindowLongPtr32(HandleRef hWnd, int nIndex, HandleRef dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr", CharSet = CharSet.Auto)]
        public static extern IntPtr SetWindowLongPtr64(HandleRef hWnd, int nIndex, HandleRef dwNewLong);

        public static IntPtr SetWindowLong(HandleRef hWnd, int nIndex, HandleRef dwNewLong)
        {
            if (IntPtr.Size == 4)
            {
                return SetWindowLongPtr32(hWnd, nIndex, dwNewLong);
            }
            return SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
        }

        /// <summary>
        /// Displays the splashscreen
        /// </summary>
        public static void ShowSplashScreen()
        {
            if (sf == null)
            {
                sf = new SplashScreenForm();

                sf.TimerSeconds = TimerSeconds;
                sf.StyleProgress = StyleProgress;
                sf.Message = Message;

                if (HandleParent.HasValue == true)
                {
                    sf.Shown += (s, args) =>
                    {
                        if (sf != null)
                        {
                            SetWindowLong(new HandleRef(sf, sf.Handle), -8, HandleParent.Value);
                        }
                    };
                }
                if (Location.HasValue)
                {
                    sf.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
                    sf.Location = Location.Value;
                }
                
                sf.ShowSplashScreen();                
            }
        }

        /// <summary>
        /// Closes the SplashScreen
        /// </summary>
        public static void CloseSplashScreen()
        {
            if (sf != null)
            {
                sf.CloseSplashScreen();
                sf = null;
                Location = null;
                HandleParent = null;
                message = string.Empty;
            }
        }

        /// <summary>
        /// Closes the SplashScreen
        /// </summary>
        /// <param name="formActive">Form active</param>
        public static void CloseSplashScreen(Form formActive)
        {
            if (sf != null)
            {
                formActive.Activate();

                sf.CloseSplashScreen();
                sf = null;
                Location = null;
                HandleParent = null;
                message = string.Empty;

                Application.DoEvents();
                formActive.Activate();
            }
        }

        /// <summary>
        /// Calculate location in relation to parent form
        /// </summary>
        /// <param name="loc">Location Desktop</param>
        /// <param name="size">Size owner</param>
        /// <returns></returns>
        public static Point CalcLocation(Point loc, Size size)
        {
            Size sizeSplash = new Size(365, 127);

            Int32 x = 0;
            Int32 y = 0;

            if (size.Width >= sizeSplash.Width)
            {
                x = loc.X + (size.Width / 2) - (sizeSplash.Width / 2);
            }
            else
            {
                x = loc.X - ((sizeSplash.Width - size.Width) / 2);
            }

            if (size.Height >= sizeSplash.Height)
            {
                y = loc.Y + (size.Height / 2) - (sizeSplash.Height / 2);
            }
            else
            {
                y = loc.Y - ((sizeSplash.Height - size.Height) / 2);
            }

            return new Point(x, y);
        }

        /// <summary>
        /// Find Hadle parent and set in property
        /// </summary>
        public static void FindHandleParent()
        {
            if (sf == null)
            {
                try
                {
                    var mainHandle = Process.GetCurrentProcess().MainWindowHandle;
                    if (mainHandle == IntPtr.Zero)
                    {
                        var mainForm = Application.OpenForms.Cast<Form>().FirstOrDefault(f => f.Visible == true);
                        if (mainForm != null)
                        {
                            mainHandle = mainForm.Handle;
                        }
                    }

                    var formPrincipal = Form.FromHandle(mainHandle);
                    if (formPrincipal != null)
                    {
                        var handleRef = new HandleRef(formPrincipal as Form, formPrincipal.Handle);

                        HandleParent = handleRef;
                    }
                }
                catch { }
            }
        }
    }

    public enum StyleProgress
    {
        Blocks,
        Marquee
    }
}
