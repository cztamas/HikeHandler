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
    public partial class SearchHikeForm : Form
    {
        public SearchHikeForm()
        {
            InitializeComponent();
        }

        public SearchHikeForm(MySqlConnection connection)
        {
            InitializeComponent();
            sqlConnection = connection;
            GetCountryList();
            GetHikeTypes();
            InitCPTable();
        }

        private MySqlConnection sqlConnection;
        private DataTable cpTable;

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

            row = hikeTypesTable.NewRow();
            row["id"] = -1;
            row["name"] = string.Empty;
            hikeTypesTable.Rows.Add(row);

            Array cpTypes = Enum.GetValues(typeof(CPType));
            foreach (CPType item in cpTypes)
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
            typeComboBox.SelectedValue = -1;            
            countryComboBox.Text = string.Empty;
            regionComboBox.Text = string.Empty;
            countryComboBox.Focus();
        }

        private void InitCPTable()
        {
            cpTable = new DataTable();
            cpTable.Clear();
            cpTable.Columns.Add("idcp");
            cpTable.Columns.Add("name");
            BindingSource source = new BindingSource();
            source.DataSource = cpTable;
            cpGridView.DataSource = source;
            cpGridView.Columns[0].Visible = false;
            cpGridView.Columns[1].HeaderText = "CheckPoint neve";
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            this.Clear();
        }

        private void Clear()
        {
            cpGridView.DataSource = null;
            resultView.DataSource = null;
            countryComboBox.Text = string.Empty;
            regionComboBox.Text = string.Empty;
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            if (!hikePositionBox.Text.IsIntPile())
            {
                MessageBox.Show("Hibás számformátum.");
                hikePositionBox.Focus();
                return;
            }
            if (!dateBox.Text.IsDatePile())
            {
                MessageBox.Show("Hibás dátumformátum.");
                dateBox.Focus();
                return;
            }
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
            HikeTemplate template = new HikeTemplate();
            template.CountryName = countryComboBox.Text;
            template.RegionName = regionComboBox.Text;
            if (typeComboBox.SelectedValue != null)
            {
                if ((int)typeComboBox.SelectedValue != -1)
                    template.HikeType = (HikeType)typeComboBox.SelectedValue;
            }
            template.Position = hikePositionBox.Text.ToIntPile();
            template.HikeDate = dateBox.Text.ToDatePile();
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(template.SearchCommand(sqlConnection)))
            {
                try
                {
                    DataTable resultTable = new DataTable();
                    adapter.Fill(resultTable);
                    resultView.DataSource = resultTable;
                    resultView.Columns[0].Visible = false;
                    resultView.Columns[1].HeaderText = "Sorszám";
                    resultView.Columns[2].HeaderText = "Dátum";
                    resultView.Columns[3].HeaderText = "Tájegység";
                    resultView.Columns[4].HeaderText = "Ország";
                    resultView.Columns[5].HeaderText = "Típus";
                    resultView.Columns[6].Visible = false;
                    resultView.Columns[7].Visible = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Hiba");
                }
            }
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
                int hikeID = (int)row.Cells[0].Value;
                ViewHikeForm viewHikeForm = new ViewHikeForm(sqlConnection, hikeID);
                viewHikeForm.Show();
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
            int hikeID = (int)resultView.Rows[e.RowIndex].Cells[0].Value;
            ViewHikeForm viewHikeForm = new ViewHikeForm(sqlConnection, hikeID);
            viewHikeForm.Show();
        }

        private void countryComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            int countryID;
            if (!int.TryParse(countryComboBox.SelectedValue.ToString(), out countryID))
                return;
            GetRegionList(countryID);
        }

        private void addCPButton_Click(object sender, EventArgs e)
        {
            if (cpNameComboBox.DataSource == null)
                return;
            DataRow row = ((DataTable)cpNameComboBox.DataSource).Rows[cpNameComboBox.SelectedIndex];
            cpTable.ImportRow(row);
        }

        private void removeCPButton_Click(object sender, EventArgs e)
        {
            List<DataRow> rowsToDelete = new List<DataRow>();
            foreach (DataGridViewRow row in cpGridView.SelectedRows)
            {
                int index = row.Index;
                rowsToDelete.Add(cpTable.Rows[index]);
            }
            foreach (DataRow row in rowsToDelete)
            {
                cpTable.Rows.Remove(row);
            }
        }
    }
}
