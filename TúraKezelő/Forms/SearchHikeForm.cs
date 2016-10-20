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
            checkPointHandler.Init(sqlConnection, CPHandlerStyle.Search);
            regionComboBox.SelectedValueChanged += new EventHandler(checkPointHandler.Region_Refreshed);
            GetCountryList();
            GetHikeTypes();            
        }

        public SearchHikeForm(MySqlConnection connection, HikeTemplate template)
        {
            InitializeComponent();
            sqlConnection = connection;
            checkPointHandler.Init(sqlConnection, CPHandlerStyle.Search);
            regionComboBox.SelectedValueChanged += new EventHandler(checkPointHandler.Region_Refreshed);
            GetCountryList();
            GetHikeTypes();
            if (template.CountryName != string.Empty)
                countryComboBox.Text = template.CountryName;
            if (template.RegionName != string.Empty)
                regionComboBox.Text = template.RegionName;
            if (template.GetCPString() != string.Empty)
                checkPointHandler.LoadCPs(template.GetCPString());
            MakeSearch(template);
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
            string commandText = "SELECT idregion, name FROM region WHERE idcountry=" + countryID + " ORDER BY name ASC;";
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

            Array cpTypes = Enum.GetValues(typeof(HikeType));
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
            checkPointHandler.Clear();
            resultView.DataSource = null;
            countryComboBox.Text = string.Empty;
            regionComboBox.Text = string.Empty;
            resultGroupBox.Text = "Találatok";
        }

        private void MakeSearch(HikeTemplate template)
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
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(template.SearchCommand(sqlConnection, checkPointHandler.AnyCPOrder)))
            {
                try
                {
                    DataTable resultTable = new DataTable();
                    adapter.Fill(resultTable);
                    resultView.DataSource = resultTable;
                    resultView.Columns["idhike"].Visible = false;
                    resultView.Columns["position"].HeaderText = "Sorszám";
                    resultView.Columns["date"].HeaderText = "Dátum";
                    resultView.Columns["idregion"].Visible = false;
                    resultView.Columns["regionname"].HeaderText = "Tájegység";
                    resultView.Columns["countryname"].HeaderText = "Ország";
                    resultView.Columns["type"].HeaderText = "Típus";
                    resultView.Columns["description"].Visible = false;
                    resultView.Columns["cpstring"].Visible = false;
                    resultView.Columns["idcountry"].Visible = false;
                    resultGroupBox.Text = "Találatok száma: " + resultTable.Rows.Count;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Hiba");
                }
            }
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
            template.CPList = checkPointHandler.CPList;
            MakeSearch(template);
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
            if (countryComboBox.SelectedValue == null)
                return;
            int countryID;
            if (!int.TryParse(countryComboBox.SelectedValue.ToString(), out countryID))
                return;
            GetRegionList(countryID);
        }
    }
}
