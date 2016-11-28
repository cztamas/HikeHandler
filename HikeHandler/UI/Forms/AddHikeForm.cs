using HikeHandler.Exceptions;
using HikeHandler.Interfaces;
using HikeHandler.ModelObjects;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HikeHandler.UI
{
    public partial class AddHikeForm : Form
    {
        private IDAOManager daoManager;

        public AddHikeForm(IDAOManager manager)
        {
            InitializeComponent();
            daoManager = manager;
        }

        private void AddHikeForm_Load(object sender, EventArgs e)
        {
            checkPointHandler.Init(daoManager, CPHandlerStyle.Add);
            regionComboBox.SelectedValueChanged += new EventHandler(checkPointHandler.Region_Refreshed);
            GetCountryList();
            GetHikeTypes();
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

        private void GetRegionList(int countryID)
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

        private void GetHikeTypes()
        {
            try
            {
                List<NameAndID> hikeTypesList = daoManager.GetHikeTypes();
                typeComboBox.DataSource = hikeTypesList;
                typeComboBox.ValueMember = "ID";
                typeComboBox.DisplayMember = "Name";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hiba");
                Close();
            }
        }

        // Collects the data on the form into a HikeForSave object.
        private HikeForSave GetDataForSave()
        {
            int countryID;
            if (countryComboBox.SelectedValue == null)
            {
                MessageBox.Show("Az ország nincs megadva.", "Hiba");
                countryComboBox.Focus();
                return null;
            }
            if (!int.TryParse(countryComboBox.SelectedValue.ToString(), out countryID))
            {
                MessageBox.Show("Nem sikerült elmenteni a túrát.", "Hiba");
                countryComboBox.Focus();
                return null;
            }
            int regionID;
            if (regionComboBox.SelectedValue == null)
            {
                MessageBox.Show("A tájegység nincs megadva.", "Hiba");
                regionComboBox.Focus();
                return null;
            }
            if (!int.TryParse(regionComboBox.SelectedValue.ToString(), out regionID))
            {
                MessageBox.Show("Nem sikerült elmenteni a túrát.", "Hiba");
                regionComboBox.Focus();
                return null;
            }
            HikeType hikeType;
            if (typeComboBox.SelectedValue == null)
            {
                MessageBox.Show("Nincs megadva a túra típusa.", "Hiba");
                typeComboBox.Focus();
                return null;
            }
            if (!Enum.TryParse(regionComboBox.SelectedItem.ToString(), out hikeType))
            {
                MessageBox.Show("Nem sikerült elmenteni a túrát.", "Hiba");
                typeComboBox.Focus();
                return null;
            }
            string description = descriptionBox.Text;
            DateTime hikeDate = dateBox.Value.Date;
            List<int> cpList = checkPointHandler.CPList;
            return new HikeForSave(countryID, regionID, hikeType, hikeDate, cpList, description);
        }

        #endregion

        #region Eventhandler Methods

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void countryComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (countryComboBox.SelectedValue.GetType() != typeof(int))
                return;
            GetRegionList((int)countryComboBox.SelectedValue);
        }

        private void addHikeButton_Click(object sender, EventArgs e)
        {
            HikeForSave hike = GetDataForSave();
            if (hike == null)
            {
                return;
            }
            try
            {
                if (daoManager.SaveHike(hike))
                {
                    MessageBox.Show("Sikeresen elmentve.");
                    Close();
                }
            }
            catch (DuplicateDateException)
            {
                MessageBox.Show("Ezzel a dátummal már van elmentve túra.", "Hiba");
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

        private void descriptionBox_TextBoxFocusEnter(object sender, EventArgs e)
        {
            AcceptButton = null;
        }

        private void descriptionBox_TextBoxFocusLeave(object sender, EventArgs e)
        {
            AcceptButton = addHikeButton;
        }

        private void descriptionBox_Leave(object sender, EventArgs e)
        {
            descriptionBox.IsTextboxActive = false;
        }

        #endregion
    }
}
