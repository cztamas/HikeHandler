using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace HikeHandler
{
    public partial class AddHikeForm : Form
    {
        public AddHikeForm()
        {
            InitializeComponent();
        }

        public AddHikeForm(MySqlConnection connection)
        {
            InitializeComponent();
            sqlConnection = connection;
        }

        private MySqlConnection sqlConnection;

        public void Open()
        {
            Show();
        }
        
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
