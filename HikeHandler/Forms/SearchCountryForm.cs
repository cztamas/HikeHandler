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

namespace HikeHandler.UI
{
    public partial class SearchCountryForm : Form
    {
        private CountryDao countryDao;
        private MySqlConnection sqlConnection;

        public SearchCountryForm()
        {
            InitializeComponent();
        }

        public SearchCountryForm(MySqlConnection connection)
        {
            InitializeComponent();
            countryDao = new CountryDao(connection);
            sqlConnection = connection;
            GetCountryList();
        }        

        public void Open()
        {
            Show();
            countryComboBox.Text = string.Empty;
            countryComboBox.Focus();
        }

        private void Clear()
        {
            resultView.DataSource = null;
            countryComboBox.Text = string.Empty;
            hikeNumberBox.Text = string.Empty;
            resultGroupBox.Text = "Találatok";
            countryComboBox.Focus();
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
            CountryForSearch template = new CountryForSearch(countryComboBox.Text, 
                hikeNumberBox.Text.ToIntPile());
            try
            {
                DataTable table = countryDao.SearchCountry(template);
                resultView.DataSource = table;
                resultView.Columns[0].Visible = false;
                resultView.Columns[1].HeaderText = "Név";
                resultView.Columns[2].HeaderText = "Túrák száma";
                resultGroupBox.Text = "Találatok száma: " + table.Rows.Count;
            }
            catch (DaoException ex)
            {
                if (ex.Error==ErrorType.NoDBConnection)
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
            int index = (int)resultView.Rows[e.RowIndex].Cells[0].Value;
            ViewCountryForm vForm = new ViewCountryForm(index, sqlConnection);
            vForm.Show();
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
                int index = (int)row.Cells[0].Value;
                ViewCountryForm vForm = new ViewCountryForm(index, sqlConnection);
                vForm.Show();
            }
            
        }
    }
}
