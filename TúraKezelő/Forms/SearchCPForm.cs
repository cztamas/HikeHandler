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
    public partial class SearchCPForm : Form
    {
        public SearchCPForm()
        {
            InitializeComponent();
        }

        public SearchCPForm(MySqlConnection connection)
        {
            InitializeComponent();            
            sqlConnection = connection;
            GetCountryList();
            GetCPTypes();            
        }

        public SearchCPForm(MySqlConnection connection, CPTemplate template)
        {
            InitializeComponent();
            sqlConnection = connection;
            GetCountryList();
            GetCPTypes();
            if (template.CountryName != string.Empty)
                countryComboBox.Text = template.CountryName;
            if (template.RegionName != string.Empty)
                regionComboBox.Text = template.RegionName;
            MakeSearch(template);
        }

        private MySqlConnection sqlConnection;

        public void Open()
        {
            Show();
            countryComboBox.Text = string.Empty;
            regionComboBox.Text = string.Empty;
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

        private void GetCPTypes()
        {
            DataTable cpTypesTable = new DataTable();
            DataColumn column;
            DataRow row;

            column = new DataColumn("id", typeof(int));
            cpTypesTable.Columns.Add(column);
            column = new DataColumn("name", typeof(string));
            cpTypesTable.Columns.Add(column);

            row = cpTypesTable.NewRow();
            row["id"] = -1;
            row["name"] = string.Empty;
            cpTypesTable.Rows.Add(row);

            Array cpTypes = Enum.GetValues(typeof(CPType));
            foreach (CPType item in cpTypes)
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

        private void Clear()
        {
            throw new NotImplementedException();
        }

        private void MakeSearch(CPTemplate template)
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
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(template.SearchCommand(sqlConnection)))
            {
                try
                {
                    DataTable resultTable = new DataTable();
                    adapter.Fill(resultTable);
                    resultView.DataSource = resultTable;
                    resultView.Columns[0].Visible = false;
                    resultView.Columns[1].HeaderText = "Név";
                    resultView.Columns[2].HeaderText = "Típus";
                    resultView.Columns[3].HeaderText = "Túrák száma";
                    resultView.Columns[4].HeaderText = "Tájegység";
                    resultView.Columns[5].HeaderText = "Ország";
                    resultView.Columns[6].Visible = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Hiba");
                }
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            this.Clear();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            if (!hikeNumberBox.Text.IsIntPile())
            {
                MessageBox.Show("Nem megfelelő számformátum.", "Hiba");
                hikeNumberBox.Focus();
                return;
            }
            
            CPTemplate template = new CPTemplate();
            template.HikeCount = hikeNumberBox.Text.ToIntPile();
            template.Name = nameBox.Text;
            template.CountryName = countryComboBox.Text;
            template.RegionName = regionComboBox.Text;
            if ((int)typeComboBox.SelectedValue != -1)
                template.TypeOfCP = (CPType)typeComboBox.SelectedValue;
            MakeSearch(template);
        }

        private void countryComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            int a;
            if (!int.TryParse(countryComboBox.SelectedValue.ToString(),out a))
                return;
            regionComboBox.Text = string.Empty;
            GetRegions((int)countryComboBox.SelectedValue);
            regionComboBox.Text = string.Empty;            
        }
        

        private void detailsButton_Click(object sender, EventArgs e)
        {
            if (resultView.SelectedRows == null)
                return;
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
            foreach (DataGridViewRow row in resultView.SelectedRows)
            {
                int cpID = (int)row.Cells[0].Value;
                ViewCPForm viewCPForm = new ViewCPForm(sqlConnection, cpID);
                viewCPForm.Show();
            }
        }

        private void resultView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
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
            int cpID = (int)resultView.Rows[e.RowIndex].Cells[0].Value;
            ViewCPForm viewCPForm = new ViewCPForm(sqlConnection, cpID);
            viewCPForm.Show();
        }
    }
}
