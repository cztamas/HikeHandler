using System;
using System.Windows.Forms;
using HikeHandler.ModelObjects;
using HikeHandler.ServiceLayer;

namespace HikeHandler.UI
{
    public partial class ViewCountryForm : Form
    {
        private DAOManager daoManager;
        private CountryForView currentCountry;
        
        public ViewCountryForm(DAOManager manager, int countryID)
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
            descriptionBox.Text = currentCountry.Description;
            Text = currentCountry.Name + " adatai";
        }

        private void RefreshCountryData()
        {
            currentCountry = daoManager.SearchCountry(currentCountry.CountryID);
            if (currentCountry == null)
            {
                Close();
            }
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
            if (daoManager.DeleteCountry(currentCountry.CountryID))
            {
                MessageBox.Show("Törölve.");
                Close();
            }
        }

        #endregion
    }
}
