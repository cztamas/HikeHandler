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
using HikeHandler.ServiceLayer;

namespace HikeHandler.UI
{
    public partial class SearchCPForm : Form
    {
        private DAOManager daoManager;

        public SearchCPForm(DAOManager manager)
        {
            InitializeComponent();
            daoManager = manager;
            GetCountryList();
            GetCPTypes();            
        }

        public SearchCPForm(DAOManager manager, CPForSearch template)
        {
            InitializeComponent();
            daoManager = manager;
            GetCountryList();
            GetCPTypes();
            if (template.CountryName != string.Empty)
                countryComboBox.Text = template.CountryName;
            if (template.RegionName != string.Empty)
            {
                regionComboBox.SelectedIndex = -1;
                regionComboBox.Text = template.RegionName;
            }
            MakeSearch(template);
        }

        public void Open()
        {
            Show();
            countryComboBox.Text = string.Empty;
            regionComboBox.Text = string.Empty;
            nameBox.Focus();
        }

        private void GetCountryList()
        {
            DataTable table = daoManager.GetAllCountryNames();
            countryComboBox.DataSource = table;
            countryComboBox.ValueMember = "idcountry";
            countryComboBox.DisplayMember = "name";
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
            resultView.DataSource = null;
            hikeNumberBox.Text = string.Empty;
            nameBox.Text = string.Empty;
            countryComboBox.Text = string.Empty;
            typeComboBox.Text = string.Empty;
            regionComboBox.DataSource = null;
            resultGroupBox.Text = "Találatok";
        }

        private void MakeSearch(CPForSearch template)
        {
            DataTable resultTable = daoManager.SearchCP(template);
            if (resultTable == null)
                return;
            resultView.DataSource = resultTable;
            resultView.Columns["idcp"].Visible = false;
            resultView.Columns["name"].HeaderText = "Név";
            resultView.Columns["type"].HeaderText = "Típus";
            resultView.Columns["hikecount"].HeaderText = "Túrák száma";
            resultView.Columns["regionname"].HeaderText = "Tájegység";
            resultView.Columns["countryname"].HeaderText = "Ország";
            resultGroupBox.Text = "Találatok száma: " + resultTable.Rows.Count;
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
            
            CPForSearch template = new CPForSearch();
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
            if (countryComboBox.SelectedValue == null)
                return;
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
            int index;
            foreach (DataGridViewRow row in resultView.SelectedRows)
            {   
                if (!int.TryParse(row.Cells["idcp"].Value.ToString(), out index))
                {
                    MessageBox.Show("Nem sikerült megjeleníteni a kiválasztott checkpointot.", "Hiba");
                    return;
                }
                ViewCPForm viewCPForm = new ViewCPForm(daoManager, index);
                viewCPForm.Show();
            }
        }

        private void resultView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            int index;
            if (!int.TryParse(resultView.Rows[e.RowIndex].Cells["idcp"].Value.ToString(), out index))
            {
                MessageBox.Show("Nem sikerült megjeleníteni a kiválasztott országot.", "Hiba");
                return;
            }
            ViewCPForm viewCPForm = new ViewCPForm(daoManager, index);
            viewCPForm.Show();
        }
    }
}
