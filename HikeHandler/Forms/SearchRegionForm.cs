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
using HikeHandler.ModelObjects;
using HikeHandler.DAOs;
using HikeHandler.Exceptions;

namespace HikeHandler.Forms
{
    public partial class SearchRegionForm : Form
    {
        private MySqlConnection sqlConnection;
        private RegionDao regionDao;

        public SearchRegionForm()
        {
            InitializeComponent();
        }
        
        public SearchRegionForm(MySqlConnection connection)
        {
            InitializeComponent();
            sqlConnection = connection;
            regionDao = new RegionDao(connection);
            GetCountryList();            
        }

        public SearchRegionForm(MySqlConnection connection, HikeRegionForSearch template)
        {
            InitializeComponent();
            sqlConnection = connection;
            regionDao = new RegionDao(connection);
            GetCountryList();
            countryComboBox.Text = template.CountryName;
            MakeSearch(template);
        }

        public void Open()
        {
            Show();
            countryComboBox.Text = string.Empty;
            nameBox.Focus();
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
            resultView.DataSource = null;
            nameBox.Text = string.Empty;
            hikeNumberBox.Text = string.Empty;
            countryComboBox.Text = string.Empty;
            resultGroupBox.Text = "Találatok";
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
           
        private void MakeSearch(HikeRegionForSearch template)
        {
            try
            {
                DataTable resultTable = regionDao.SearchRegion(template);
                resultView.DataSource = resultTable;
                resultView.Columns[0].Visible = false;
                resultView.Columns[1].HeaderText = "Név";
                resultView.Columns[2].HeaderText = "Túrák száma";
                resultView.Columns[3].Visible = false;
                resultView.Columns[4].HeaderText = "Ország";
                resultGroupBox.Text = "Találatok száma: " + resultTable.Rows.Count;
            }
            catch (DaoException ex)
            {
                if (ex.Error == ErrorType.NoDBConnection)
                {
                    MessageBox.Show("Nincs kapcsolat az adatbázissal.", "Hiba");
                    return;
                }
                MessageBox.Show(ex.Message, "Hiba");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hiba");
            }
        }
             
        private void searchButton_Click(object sender, EventArgs e)
        {
            if (!hikeNumberBox.Text.IsIntPile())
            {
                MessageBox.Show("Nem megfelelő számformátum.", "Hiba");
                hikeNumberBox.Focus();
                return;
            }
            
            HikeRegionForSearch template = new HikeRegionForSearch(countryComboBox.Text, nameBox.Text, hikeNumberBox.Text.ToIntPile());
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
                int regionID = (int)row.Cells[0].Value;
                ViewRegionForm viewRegionForm = new ViewRegionForm(sqlConnection, regionID);
                viewRegionForm.Show();
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
            int regionID = (int)resultView.Rows[e.RowIndex].Cells[0].Value;
            ViewRegionForm viewRegionForm = new ViewRegionForm(sqlConnection, regionID);
            viewRegionForm.Show();
        }
                
    }
}
