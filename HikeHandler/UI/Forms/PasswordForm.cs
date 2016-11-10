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
        public LoginData loginData;

        public PasswordForm()
        {
            InitializeComponent();
        }

        private void PasswordForm_Load(object sender, EventArgs e)
        {
            pwdBox.Focus();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            loginData = new LoginData(userBox.Text, pwdBox.Text);
        }
    }
}
