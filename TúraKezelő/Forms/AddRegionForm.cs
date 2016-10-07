using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using HikeHandler.Data_Containers;

namespace HikeHandler.Forms
{
    public partial class AddRegionForm : Form
    {
        private MySqlConnection sqlConnection;

        public AddRegionForm()
        {
            InitializeComponent();
        }

        public AddRegionForm(MySqlConnection connection)
        {
            InitializeComponent();
            sqlConnection = connection;
            GetCountryList();            
        } 
        
        public void Open()
        {
            Show();
            countryComboBox.Text = string.Empty;
            nameBox.Focus();
        }

        private void GetCountryList()
        {
            if (sqlConnection == null)
            {
                MessageBox.Show("Nincs kapcsolat az adatbázissal.", "Hiba");
                return;
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                MessageBox.Show("Nincs kapcsolat az adatbázissal.", "Hiba");
                return;
            }
            string commandText = "SELECT idcountry, name FROM country ORDER BY name ASC;";
            DataTable table = new DataTable();
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(commandText, sqlConnection))
            {                
                try
                {
                    adapter.Fill(table);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Hiba");
                }
            }
            countryComboBox.DataSource = table;
            countryComboBox.ValueMember = "idcountry";
            countryComboBox.DisplayMember = "name";
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveRegionButton_Click(object sender, EventArgs e)
        {
            if (sqlConnection == null)
            {
                MessageBox.Show("Nincs kapcsolat az adatbázissal.", "Hiba");
                return;
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                MessageBox.Show("Nincs kapcsolat az adatbázissal.", "Hiba");
                return;
            }
            HikeRegion region = new HikeRegion((int)countryComboBox.SelectedValue, nameBox.Text, descriptionBox.Text);
            using (MySqlCommand command = region.SaveCommand(sqlConnection))
            {
                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Sikeresen elmentve");
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Hiba");
                }
            }
        }

        private void descriptionBox_Enter(object sender, EventArgs e)
        {
            AcceptButton = null;
        }

        private void descriptionBox_Leave(object sender, EventArgs e)
        {
            AcceptButton = saveRegionButton;
        }
    }
}
