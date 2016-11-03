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
using HikeHandler.DAOs;
using HikeHandler.Exceptions;

namespace HikeHandler.Forms
{
    public partial class ViewCountryForm : Form
    {
        private CountryDao countryDao;

        private MySqlConnection sqlConnection;
        private CountryForView countryData;

        public ViewCountryForm()
        {
            InitializeComponent();
            MakeUneditable();
        }

        public ViewCountryForm(int idCountry, MySqlConnection connection)
        {
            InitializeComponent();
            countryDao = new CountryDao(connection);
            sqlConnection = connection;
            countryData = new CountryForView(idCountry);
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
            RefreshCountryData(countryData.ID);
            nameBox.Text = countryData.Name;
            hikeCountBox.Text = countryData.HikeCount.ToString();
            descriptionBox.Text = countryData.Description;
            Text = countryData.Name + " adatai";
            MakeUneditable();
        }

        private void RefreshCountryData(int idCountry)
        {
            try
            {
                countryData = countryDao.GetCountryData(idCountry);
            }
            catch (DaoException ex)
            {
                switch (ex.Error)
                {
                    case ErrorType.NoDBConnection:
                        MessageBox.Show("Nincs kapcslat az adatbázissal", "Hiba");
                        break;
                    default:
                        MessageBox.Show(ex.Message, "Hiba");
                        break;
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
            countryData.Name = nameBox.Text;
            countryData.Description = descriptionBox.Text;
            try
            {
                countryDao.UpdateCountry(countryData);
                RefreshForm();
            }
            catch (DaoException ex)
            {
                switch (ex.Error)
                {
                    case ErrorType.NoDBConnection:
                        MessageBox.Show("Nincs kapcsolat az adatbázissal.", "Hiba");
                        break;
                    default:
                        MessageBox.Show(ex.Message, "Hiba");
                        break;
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
            // Asks for confirmation of deletion   
            string message = "Biztosan törli?";
            string caption = "Ország törlése";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, caption, buttons);
            if (result == DialogResult.No)
                return;

            try
            {
                if (countryDao.DeleteCountry(countryData.ID))
                {
                    MessageBox.Show("Törölve");
                    Close();
                }
            }
            catch (DaoException ex)
            {
                switch (ex.Error)
                {
                    case ErrorType.NoDBConnection:
                        MessageBox.Show("Nincs kapcsolat az adatbázissal.", "Hiba");
                        break;
                    case ErrorType.NotDeletable:
                        MessageBox.Show("Csak olyan ország törölhető, amihez nincs tájegység, checkpoint vagy túra hozzárendelve.", "Hiba");
                        break;
                    default:
                        MessageBox.Show(ex.Message, "Hiba");
                        break;
                }
            }
        }
    }
}
