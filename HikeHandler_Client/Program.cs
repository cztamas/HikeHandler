using HikeHandler.UI;
using System;
using System.Windows.Forms;

namespace HikeHandler_Client
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
            ClientDaoManager manager = new ClientDaoManager();
            Application.Run(new BaseForm(manager));
        }
    }
}
