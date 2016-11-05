using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HikeHandler.ModelObjects;

namespace HikeHandler.UI
{
    public partial class PasswordForm : Form
    {
        public PasswordForm()
        {
            InitializeComponent();
            pwdBox.Select();
        }

        public delegate void LoginHandler(LoginData data);
        public delegate void VoidHandler();

        public event LoginHandler LoginPerformed;
        public event VoidHandler LoginCancelled;
        public event VoidHandler TestModeSelected;

        private void okButton_Click(object sender, EventArgs e)
        {
            LoginData data = new LoginData(userBox.Text, pwdBox.Text);
            LoginPerformed(data);
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            LoginCancelled();
            Close();
        }

        private void testButton_Click(object sender, EventArgs e)
        {
            TestModeSelected();
            Close();
        }
    }
}
