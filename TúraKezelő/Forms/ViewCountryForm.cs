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
    public partial class ViewCountryForm : Form
    {
        private MySqlConnection sqlConnection;
        private Country countryData;
        private int countryID;

        public ViewCountryForm()
        {
            InitializeComponent();
            MakeUneditable();
        }

        public ViewCountryForm(int id, MySqlConnection connection)
        {
            InitializeComponent();
            sqlConnection = connection;
            countryID = id;
            RefreshForm();
        }

        private void MakeEditable()
        {
            nameBox.Enabled = true;
            descriptionBox.Enabled = true;
            saveEditButton.Enabled = true;
            saveEditButton.Visible = true;
            cancelEditButton.Enabled = true; ;
            cancelEditButton.Visible = true;
            editButton.Enabled = false;
            editButton.Visible = false;
        }

        private void MakeUneditable()
        {
            nameBox.Enabled = false;
            descriptionBox.Enabled = false;
            saveEditButton.Enabled = false;
            saveEditButton.Visible = false;
            cancelEditButton.Enabled = false;
            cancelEditButton.Visible = false;
            editButton.Enabled = true;
            editButton.Visible = true;
        }

        private void RefreshForm()
        {
            RefreshCountry(countryID);
            nameBox.Text = countryData.Name;
            hikeCountBox.Text = countryData.HikeCount.ToString();
            descriptionBox.Text = countryData.Description;
            Text = countryData.Name + " adatai";
            MakeUneditable();
        }

        private void RefreshCountry(int id)
        {
            Country country = new Country(id);
            if (sqlConnection==null)
            {
                MessageBox.Show("Nem lehet elérni az adatbázist", "Hiba");
                return;
            }
            if (sqlConnection.State!=ConnectionState.Open)
            {
                MessageBox.Show("Nem lehet elérni az adatbázist", "Hiba");
                return;
            }
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(country.RefreshCommand(sqlConnection)))
            {
                try
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    DataRow row = table.Rows[0];
                    countryData = new Country(id, (int)row["hikecount"],(string)row["name"],(string)row["description"]);
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

        private void editButton_Click(object sender, EventArgs e)
        {
            MakeEditable();
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            RefreshForm();
        }

        private void cancelEditButton_Click(object sender, EventArgs e)
        {
            RefreshForm();
        }

        private void saveEditButton_Click(object sender, EventArgs e)
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
            countryData.Name = nameBox.Text;
            countryData.Description = descriptionBox.Text;
            using (MySqlCommand command = countryData.UpdateCommand(sqlConnection))
            {
                try
                {
                    command.ExecuteNonQuery();
                    RefreshForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Hiba");
                }
            }
        }

        private void regionsOfCountryButton_Click(object sender, EventArgs e)
        {
            HikeRegionTemplate template = new HikeRegionTemplate();
            template.IDcountry = countryData.ID;
            template.CountryName = countryData.Name;
            SearchRegionForm searchRegionForm = new SearchRegionForm(sqlConnection, template);
            searchRegionForm.Show();
        }

        private void cpsOfCountryButton_Click(object sender, EventArgs e)
        {
            CPTemplate template = new CPTemplate();
            template.IDCountry = countryData.ID;
            template.CountryName = countryData.Name;
            SearchCPForm searchCPForm = new SearchCPForm(sqlConnection, template);
            searchCPForm.Show();
        }

        private void hikesOfCountryButton_Click(object sender, EventArgs e)
        {
            HikeTemplate template = new HikeTemplate();
            template.IDCountry = countryData.ID;
            template.CountryName = countryData.Name;
            SearchHikeForm searchHikeForm = new SearchHikeForm(sqlConnection, template);
            searchHikeForm.Show();
        }

        private void deleteCountryButton_Click(object sender, EventArgs e)
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
            if (!Country.IsDeletable(sqlConnection, countryID))
            {
                MessageBox.Show("Csak olyan ország törölhető, amihez nincs tájegység, checkpoint vagy túra hozzárendelve", "Hiba");
                return;
            }
            throw new NotImplementedException();
        }
    }
}
