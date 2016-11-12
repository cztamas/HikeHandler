using System;
using System.Data;
using System.Windows.Forms;
using HikeHandler.ModelObjects;
using HikeHandler.ServiceLayer;
using System.Collections.Generic;
using System.ComponentModel;
using HikeHandler.Exceptions;

namespace HikeHandler.UI
{
    public partial class AddCPForm : Form
    {
        private DAOManager daoManager;

        public AddCPForm(DAOManager manager)
        {
            InitializeComponent();
            daoManager = manager;
        }

        private void AddCPForm_Load(object sender, EventArgs e)
        {
            GetCountries();
            GetCPTypes();
            typeComboBox.SelectedValue = -1;
            nameBox.Focus();
        }

        #region Auxiliary Methods

        private CPForSave GetDataForSave()
        {
            if (string.IsNullOrWhiteSpace(nameBox.Text))
            {
                MessageBox.Show("Nincs megadva a checkpoint neve.", "Hiba");
                nameBox.Focus();
                return null;
            }
            string name = nameBox.Text;
            string description = descriptionBox.Text;
            int countryID;
            if (!int.TryParse(countryComboBox.SelectedValue.ToString(), out countryID))
            {
                MessageBox.Show("Nem sikerült elmenteni a checkpointot.", "Hiba");
                return null;
            }
            if (countryID < 0)
            {
                MessageBox.Show("Az ország nincs megadva.", "Hiba");
                countryComboBox.Focus();
                return null;
            }
            int regionID;
            if (!int.TryParse(regionComboBox.SelectedValue.ToString(), out regionID))
            {
                MessageBox.Show("Nem sikerült elmenteni a checkpointot.", "Hiba");
                return null;
            }
            if (regionID < 0)
            {
                MessageBox.Show("A tájegység nincs megadva.", "Hiba");
                regionComboBox.Focus();
                return null;
            }
            CPType typeOfCP;
            if (!Enum.TryParse(typeComboBox.SelectedItem.ToString(), out typeOfCP))
            {
                MessageBox.Show("Nincs megadva a checkpoint típusa.", "Hiba");
                typeComboBox.Focus();
                return null;
            }
            return new CPForSave(countryID, regionID, name, typeOfCP, description);
        }

        private void GetCPTypes()
        {
            DataTable cpTypesTable = daoManager.GetCPTypes();
            if (cpTypesTable == null)
            {
                Close();
            }
            typeComboBox.DataSource = cpTypesTable;
            typeComboBox.ValueMember = "id";
            typeComboBox.DisplayMember = "name";
        }

        private void GetCountries()
        {
            try
            {
                List<NameAndID> countries = daoManager.GetAllCountryNames();
                if (countries == null)
                {
                    Close();
                }
                countryComboBox.DataSource = countries;
                countryComboBox.ValueMember = "id";
                countryComboBox.DisplayMember = "name";
                return;
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

        private void GetRegions(int countryID)
        {
            try
            {
                List<NameAndID> regions = daoManager.GetAllRegionsOfCountry(countryID);
                regionComboBox.DataSource = regions;
                regionComboBox.ValueMember = "ID";
                regionComboBox.DisplayMember = "Name";
                return;
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

        #endregion

        #region Eventhandler Methods

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void descriptionBox_Enter(object sender, EventArgs e)
        {
            AcceptButton = null;
        }

        private void descriptionBox_Leave(object sender, EventArgs e)
        {
            AcceptButton = saveCPButton;
        }

        private void saveCPButton_Click(object sender, EventArgs e)
        {
            CPForSave cp = GetDataForSave();
            if (cp == null)
            {
                return;
            }
            if (daoManager.SaveCP(cp))
            {
                MessageBox.Show("Sikeresen elmentve.");
                Close();
            }
        }

        private void countryComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (countryComboBox.SelectedValue.GetType() != typeof(int))
                return;
            GetRegions((int)countryComboBox.SelectedValue);
        }

        #endregion
    }
}
