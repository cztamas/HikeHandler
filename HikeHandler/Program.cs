using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using HikeHandler.ServiceLayer;

namespace HikeHandler
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            FormManager formManager = new FormManager();
            BaseForm baseForm = formManager.ConnectToDB();
            if (baseForm == null)
                return;
            Application.Run(baseForm);
        }
    }
}
