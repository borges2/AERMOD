using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AERMOD.LIB.Desenvolvimento
{
    public static class CrossThreadOperation
    {
        public delegate void Func();
        public delegate TResult Func<TResult>();

        /// <summary>
        /// Invokes a cross-thread operation that doesn’t return a result.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="del"></param>
        public static void Invoke(Control control, Func del)
        {
            try
            {
                if (control == null)
                    throw new ArgumentNullException(control.ToString());
                if (del == null)
                    throw new ArgumentNullException(del.ToString());

                // Check if we need to use the controls invoke method to do cross-thread operations.
                if (control.InvokeRequired)
                    control.Invoke(del);
                else
                    del();
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Invokes a cross-thread operation that returns a result.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="control"></param>
        /// <param name="del"></param>
        /// <returns></returns>
        public static TResult Invoke<TResult>(Control control, Func<TResult> del)
        {
            if (control == null)
                throw new ArgumentNullException(control.ToString());
            if (del == null)
                throw new ArgumentNullException(del.ToString());

            // Check if we need to use the controls invoke method to do cross-thread operations.
            if (control.InvokeRequired)
                return (TResult)control.Invoke(del);
            return del();
        }
    }
}
