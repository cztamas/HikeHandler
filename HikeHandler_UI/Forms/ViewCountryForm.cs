﻿using HikeHandler.Exceptions;
using HikeHandler.Interfaces;
using HikeHandler.ModelObjects;
using System;
using System.Windows.Forms;

namespace HikeHandler.UI
{
    public partial class ViewCountryForm : Form
    {
        private IDAOManager daoManager;
        private CountryForView currentCountry;
        
        public ViewCountryForm(IDAOManager manager, int countryID)
        {
            InitializeComponent();
            daoManager = manager;
            currentCountry = new CountryForView(countryID);
        }

        private void ViewCountryForm_Load(object sender, EventArgs e)
        {
            RefreshCountryData();
            RefreshForm();
            MakeUneditable();
        }

        #region Auxiliary Methods

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
            cpCountLabel.Text = currentCountry.CPCount.ToString();
            descriptionBox.Text = currentCountry.Description;
            Text = currentCountry.Name + " adatai";
        }

        private void RefreshCountryData()
        {
            try
            {
                currentCountry = daoManager.SearchCountry(currentCountry.CountryID);
                if (currentCountry == null)
                {
                    Close();
                }
                return;
            }
            catch (NoItemFoundException)
            {
                MessageBox.Show("Nem található a keresett ország.", "Hiba");
            }
            catch (NoDBConnectionException)
            {
                MessageBox.Show("Nincs kapcsolat az adatbázissal.", "Hiba");
            }
            catch (DBErrorException ex)
            {
                MessageBox.Show("Hiba az adatbázisban: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hiba");
            }
            Close();
        }

        private CountryForUpdate GetDataForUpdate()
        {
            if (string.IsNullOrWhiteSpace(nameBox.Text))
            {
                MessageBox.Show("Nincs megadva az ország neve.", "Hiba");
                nameBox.Focus();
                return null;
            }
            return new CountryForUpdate(currentCountry.CountryID, currentCountry.Name, nameBox.Text, descriptionBox.Text);
        }

        #endregion

        #region Eventhandler methods

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
            CountryForUpdate country = GetDataForUpdate();
            if (country == null)
            {
                return;
            }
            try
            {
                daoManager.UpdateCountry(country);
                MakeUneditable();
                RefreshCountryData();
                RefreshForm();
            }
            catch (DuplicateItemNameException)
            {
                MessageBox.Show("Már van elmentve ilyen nevű ország.", "Hiba");
                nameBox.Focus();
            }
            catch (NoDBConnectionException)
            {
                MessageBox.Show("Nincs kapcsolat az adatbázissal.", "Hiba");
            }
            catch (DBErrorException ex)
            {
                MessageBox.Show("Hiba az adatbázisban: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hiba");
            }
        }
        
        private void regionsOfCountryButton_Click(object sender, EventArgs e)
        {
            HikeRegionForSearch template = new HikeRegionForSearch();
            template.IDcountry = currentCountry.CountryID;
            template.CountryName = currentCountry.Name;
            SearchRegionForm sRForm = new SearchRegionForm(daoManager, template);
            sRForm.Show();
        }

        private void cpsOfCountryButton_Click(object sender, EventArgs e)
        {
            CPForSearch template = new CPForSearch();
            template.IDCountry = currentCountry.CountryID;
            template.CountryName = currentCountry.Name;
            SearchCPForm sCPForm = new SearchCPForm(daoManager, template);
            sCPForm.Show();
        }

        private void hikesOfCountryButton_Click(object sender, EventArgs e)
        {
            HikeForSearch template = new HikeForSearch();
            template.IDCountry = currentCountry.CountryID;
            template.CountryName = currentCountry.Name;
            SearchHikeForm sHForm = new SearchHikeForm(daoManager, template);
            sHForm.Show();
        }

        private void deleteCountryButton_Click(object sender, EventArgs e)
        {
            // asking for confirmation
            string message = "Biztosan törli?";
            string caption = "Ország törlése";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, caption, buttons);
            if (result == DialogResult.No)
            {
                return;
            }
            try
            {
                daoManager.DeleteCountry(currentCountry.CountryID);
                MessageBox.Show("Törölve.");
                Close();
            }
            catch (NotDeletableException)
            {
                MessageBox.Show("Csak olyan ország törölhető, amihez nem tartozik tájegység, checkpoint vagy túra.");
            }
            catch (NoDBConnectionException)
            {
                MessageBox.Show("Nincs kapcsolat az adatbázissal.", "Hiba");
            }
            catch (DBErrorException ex)
            {
                MessageBox.Show("Hiba az adatbázisban: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hiba");
            }
        }

        #endregion
    }
}
