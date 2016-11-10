using System;
using System.Windows.Forms;
using HikeHandler.ModelObjects;
using HikeHandler.ServiceLayer;

namespace HikeHandler.UI
{
    public partial class AddCountryForm : Form
    {
        private DAOManager daoManager;
        
        public AddCountryForm(DAOManager manager)
        {
            InitializeComponent();
            daoManager = manager;
        }

        private void AddCountryForm_Load(object sender, EventArgs e)
        {
            nameBox.Focus();
        }

        #region Auxiliary Methods

        private CountryForSave GetDataForSave()
        {
            if (string.IsNullOrWhiteSpace(nameBox.Text))
            {
                MessageBox.Show("Nincs megadva az ország neve.", "Hiba");
                nameBox.Focus();
                return null;
            }
            return new CountryForSave(nameBox.Text, descriptionBox.Text);
        }

        #endregion

        #region Eventhandler Methods

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            CountryForSave country = GetDataForSave();
            if (country == null)
                return;
            if (daoManager.SaveCountry(country))
            {
                MessageBox.Show("Sikeresen elmentve.");
                Close();
            }
        }
        
        private void descriptionBox_Enter(object sender, EventArgs e)
        {
            AcceptButton = null;
        }

        private void descriptionBox_Leave(object sender, EventArgs e)
        {
            AcceptButton = saveButton;
        }

        #endregion
    }
}
