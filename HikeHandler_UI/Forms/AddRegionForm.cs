using HikeHandler.Exceptions;
using HikeHandler.Interfaces;
using HikeHandler.ModelObjects;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HikeHandler.UI
{
    public partial class AddRegionForm : Form
    {
        private IDAOManager daoManager;

        public AddRegionForm(IDAOManager manager)
        {
            InitializeComponent();
            daoManager = manager;
        }

        private void AddRegionForm_Load(object sender, EventArgs e)
        {
            GetCountryList();
            countryComboBox.Text = string.Empty;
            nameBox.Focus();
        }

        #region Auxiliary Methods

        private void GetCountryList()
        {
            try
            {
                List<NameAndID> countries = daoManager.GetAllCountryNames();
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

        private HikeRegionForSave GetDataForSave()
        {
            if (string.IsNullOrWhiteSpace(nameBox.Text))
            {
                MessageBox.Show("A tájegység neve nincs megadva.", "Hiba");
                nameBox.Focus();
                return null;
            }
            if (countryComboBox.SelectedValue == null)
            {
                MessageBox.Show("Nincs ország megadva.", "Hiba");
                countryComboBox.Focus();
                return null;
            }
            int index = 0;
            if (!int.TryParse(countryComboBox.SelectedValue.ToString(), out index) || index <= 0)
            {
                MessageBox.Show("Nem sikerült elmenteni a tájegységet.", "Hiba");
                return null;
            }
            HikeRegionForSave region = new HikeRegionForSave(index, nameBox.Text, descriptionBox.Text);
            return region;
        }

        #endregion

        #region Eventhandler Methods

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveRegionButton_Click(object sender, EventArgs e)
        {
            HikeRegionForSave region = GetDataForSave();
            if (region == null)
            {
                return;
            }
            try
            {
                if (daoManager.SaveRegion(region))
                {
                    MessageBox.Show("Sikeresen elmentve");
                    Close();
                }
            }
            catch (DuplicateItemNameException)
            {
                MessageBox.Show("Már van elmentve ilyen nevű tájegység.", "Hiba");
                nameBox.Focus();
            }
            catch (NoDBConnectionException)
            {
                MessageBox.Show("Nincs kapcsolat az adatbázissal.", "Hiba");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hiba");
            }
        }

        private void descriptionBox_Enter(object sender, EventArgs e)
        {
            AcceptButton = null;
        }

        private void descriptionBox_Leave(object sender, EventArgs e)
        {
            AcceptButton = saveRegionButton;
        }

        #endregion
    }
}
