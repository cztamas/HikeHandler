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
using HikeHandler.ServiceLayer;

namespace HikeHandler.UI
{
    public partial class ViewCountryForm : Form
    {
        private DAOManager daoManager;
        private CountryForView currentCountry;
        
        public ViewCountryForm(DAOManager manager, CountryForView country)
        {
            InitializeComponent();
            daoManager = manager;
            currentCountry = country;
            RefreshForm();
            MakeUneditable();
        }

        private void MakeEditable()
        {
            nameBox.Enabled = true;
            descriptionBox.Enabled = true;
            saveEditButton.Enabled = true;
            saveEditButton.Visible = true;
            cancelEditButton.Enabled = true; 
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
            nameBox.Text = currentCountry.Name;
            hikeCountLabel.Text = currentCountry.HikeCount.ToString();
            regionCountLabel.Text = currentCountry.RegionCount.ToString();
            descriptionBox.Text = currentCountry.Description;
            Text = currentCountry.Name + " adatai";
        }

        private void RefreshCountryData()
        {
            currentCountry = daoManager.SearchCountry(currentCountry.CountryID);
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
            RefreshCountryData();
            RefreshForm();
        }

        private void cancelEditButton_Click(object sender, EventArgs e)
        {
            MakeUneditable();
            RefreshForm();
        }

        private void saveEditButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nameBox.Text))
            {
                MessageBox.Show("Nincs megadva az ország neve.", "Hiba");
                nameBox.Focus();
            }
            CountryForUpdate country =
                new CountryForUpdate(currentCountry.CountryID, currentCountry.Name, nameBox.Text, descriptionBox.Text);
            if (daoManager.UpdateCountry(country))
            {
                MakeUneditable();
                RefreshCountryData();
                RefreshForm();
            }
        }
        
        private void regionsOfCountryButton_Click(object sender, EventArgs e)
        {
            HikeRegionForSearch template = new HikeRegionForSearch();
            template.IDcountry = currentCountry.CountryID;
            template.CountryName = currentCountry.Name;

            throw new NotImplementedException();
        }

        private void cpsOfCountryButton_Click(object sender, EventArgs e)
        {
            CPForSearch template = new CPForSearch();
            template.IDCountry = currentCountry.CountryID;
            template.CountryName = currentCountry.Name;

            throw new NotImplementedException();
        }

        private void hikesOfCountryButton_Click(object sender, EventArgs e)
        {
            HikeForSearch template = new HikeForSearch();
            template.IDCountry = currentCountry.CountryID;
            template.CountryName = currentCountry.Name;

            throw new NotImplementedException();
        }

        private void deleteCountryButton_Click(object sender, EventArgs e)
        {
            if (daoManager.DeleteCountry(currentCountry.CountryID))
            {
                MessageBox.Show("Törölve.");
                Close();
            }
        }



            /*// Asks for confirmation of deletion   
            string message = "Biztosan törli?";
            string caption = "Ország törlése";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, caption, buttons);
            if (result == DialogResult.No)
                return;

            try
            {
                if (countryDao.DeleteCountry(currentCountry.ID))
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
            }*/
        
    }
}
