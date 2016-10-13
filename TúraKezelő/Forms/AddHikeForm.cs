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
            GetCountryList();
            GetHikeTypes();
        }

        private MySqlConnection sqlConnection;

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

        private void GetCPList()
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
            string commandText = "SELECT idcp, name FROM cp;";
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(commandText, sqlConnection))
            {
                try
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    cpNameComboBox.DataSource = table;
                    cpNameComboBox.ValueMember = "idcp";
                    cpNameComboBox.DisplayMember = "name";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Hiba");
                }
            }
        }

        private void GetCPList(int regionID)
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
            string commandText = "SELECT idcp, name FROM cp WHERE idregion=" + regionID + ";";
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(commandText, sqlConnection))
            {
                try
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    cpNameComboBox.DataSource = table;
                    cpNameComboBox.ValueMember = "idcp";
                    cpNameComboBox.DisplayMember = "name";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Hiba");
                }
            }
        }

        private void GetRegionList(int countryID)
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
            string commandText = "SELECT idregion, name FROM region WHERE idcountry=" + countryID + ";";
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

        private void GetHikeTypes()
        {
            DataTable hikeTypesTable = new DataTable();
            DataColumn column;
            DataRow row;

            column = new DataColumn("id", typeof(int));
            hikeTypesTable.Columns.Add(column);
            column = new DataColumn("name", typeof(string));
            hikeTypesTable.Columns.Add(column);            

            Array hikeTypes = Enum.GetValues(typeof(HikeType));
            foreach (HikeType item in hikeTypes)
            {
                row = hikeTypesTable.NewRow();
                row["id"] = (int)item;
                row["name"] = item.ToString();
                hikeTypesTable.Rows.Add(row);
            }
            typeComboBox.DataSource = hikeTypesTable;
            typeComboBox.ValueMember = "id";
            typeComboBox.DisplayMember = "name";
        }

        public void Open()
        {
            Show();
        }
        
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void countryComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (countryComboBox.SelectedValue.GetType() != typeof(int))
                return;
            GetRegionList((int)countryComboBox.SelectedValue);
        }

        private void addHikeButton_Click(object sender, EventArgs e)
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
            Hike hike = new Hike();
            hike.IDCountry = (int)countryComboBox.SelectedValue;
            hike.IDRegion = (int)regionComboBox.SelectedValue;
            hike.HikeType = (HikeType)typeComboBox.SelectedValue;
            hike.HikeDate = dateBox.Value.Date;
            hike.Description = descriptionBox.Text;
            using (MySqlCommand command = hike.SaveCommand(sqlConnection))
            {
                try
                {
                    command.ExecuteNonQuery();
                    if (hike.HikeType == HikeType.túra)
                        Hike.UpdatePositions(sqlConnection);
                    MessageBox.Show("Sikeresen elmentve.");
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
            AcceptButton = addHikeButton;
        }

        private void regionComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (regionComboBox.SelectedValue.GetType() != typeof(int))
                return;
            if (allRegionCheckBox.Checked == true)
                return;
            GetCPList((int)regionComboBox.SelectedValue);
        }

        private void allRegionCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (allRegionCheckBox.Checked)
                GetCPList();
            if (!allRegionCheckBox.Checked)
            {
                GetCPList((int)regionComboBox.SelectedValue);
                cpNameComboBox.Text = string.Empty;
            }
        }
    }
}
