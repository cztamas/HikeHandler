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
    public partial class AddCPForm : Form
    {
        private MySqlConnection sqlConnection;

        public AddCPForm()
        {
            InitializeComponent();
        }

        public AddCPForm(MySqlConnection connection)
        {
            InitializeComponent();
            sqlConnection = connection;
            GetCPTypes();
            GetCountries();            
        } 

        public void Open()
        {
            Show();
            typeComboBox.SelectedValue = -1;
            nameBox.Focus();
        }
        
        private void GetCPTypes()
        {
            DataTable cpTypesTable = new DataTable();
            DataColumn column;
            DataRow row;

            column = new DataColumn("id", typeof(int));
            cpTypesTable.Columns.Add(column);
            column = new DataColumn("name", typeof(string));
            cpTypesTable.Columns.Add(column);
            
            Array cpTypes = Enum.GetValues(typeof(CPType));
            foreach( CPType item in cpTypes)
            {
                row = cpTypesTable.NewRow();
                row["id"] = (int)item;
                row["name"] = item.ToString();
                cpTypesTable.Rows.Add(row);
            }
            typeComboBox.DataSource = cpTypesTable;
            typeComboBox.ValueMember = "id";
            typeComboBox.DisplayMember = "name";
        }

        private void GetCountries()
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
            string commandText = "SELECT idcountry, name FROM country;";
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(commandText, sqlConnection))
            {
                try
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    countryComboBox.DataSource = table;
                    countryComboBox.ValueMember = "idcountry";
                    countryComboBox.DisplayMember = "name";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Hiba");
                }
            }
        }

        private void GetRegions(int countryID)
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
            string commandText = "SELECT idregion, name FROM region WHERE idcountry="+countryID+"ORDER BY name ASC;";
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(commandText, sqlConnection))
            {
                try
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    regionComboBox.DataSource = table;
                    regionComboBox.ValueMember = "idregion";
                    regionComboBox.DisplayMember = "name";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Hiba");
                }
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void descriptionBox_Enter(object sender, EventArgs e)
        {
            AcceptButton = null;
        }

        private void descriptionBox_Leave(object sender, EventArgs e)
        {
            AcceptButton = saveCPButton;
        }

        private void saveCPButton_Click(object sender, EventArgs e)
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
            CP checkPoint = new CP();
            checkPoint.Name = nameBox.Text;
            checkPoint.Description = descriptionBox.Text;
            checkPoint.IDCountry = (int)countryComboBox.SelectedValue;
            checkPoint.IDRegion = (int)regionComboBox.SelectedValue;
            if ((int)typeComboBox.SelectedValue != -1)
                checkPoint.TypeOfCP = (CPType)typeComboBox.SelectedValue;
            if (checkPoint.IsDuplicateName(sqlConnection))
            {
                MessageBox.Show("Már létezik ilyen néven checkpoint.", "Hiba");
                return;
            }
            using (MySqlCommand command = checkPoint.SaveCommand(sqlConnection))
            {
                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Sikeresen elmentve.");
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Hiba");
                }
            }
        }

        private void countryComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (countryComboBox.SelectedValue.GetType() != typeof(int))
                return;
            GetRegions((int)countryComboBox.SelectedValue);
        }
    }
}
