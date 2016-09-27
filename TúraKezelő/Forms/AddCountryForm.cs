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
using HikeHandler.Data_Containers;

namespace HikeHandler.Forms
{
    public partial class AddCountryForm : Form
    {
        public AddCountryForm()
        {
            InitializeComponent();
        }

        public AddCountryForm(MySqlConnection connection)
        {
            InitializeComponent();
            sqlConnection = connection;
        }

        private MySqlConnection sqlConnection;

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            Country country = new Country(nameBox.Text, descriptionBox.Text);
            MySqlCommand command = country.SaveCommand(sqlConnection);
            try
            {
                command.ExecuteNonQuery();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba: " + ex.Message);
            }
        }
    }
}
