using HikeHandler.Exceptions;
using HikeHandler.Interfaces;
using HikeHandler.ModelObjects;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HikeHandler.UI
{
    public partial class ViewHikeForm : Form
    {
        private HikeForView currentHike;
        private IDAOManager daoManager;

        public ViewHikeForm(IDAOManager manager, int hikeID)
        {
            InitializeComponent();
            daoManager = manager;
            currentHike = new HikeForView(hikeID);
        }

        private void ViewHikeForm_Load(object sender, EventArgs e)
        {
            GetHikeTypes();
            checkPointHandler.Init(daoManager, CPHandlerStyle.View);
            if (RefreshHikeData())
            {
                RefreshForm();
                MakeUneditable();
            }
            else
            {
                Close();
            }
        }

        #region Auxiliary Methods

        private void MakeEditable()
        {
            dateBox.Enabled = true;
            typeComboBox.Enabled = true;
            descriptionBox.Enabled = true;
            saveEditButton.Enabled = true;
            saveEditButton.Visible = true;
            cancelEditButton.Enabled = true;
            cancelEditButton.Visible = true;
            editButton.Enabled = false;
            editButton.Visible = false;
            checkPointHandler.MakeEditable();
        }

        private void MakeUneditable()
        {
            dateBox.Enabled = false;
            typeComboBox.Enabled = false;
            descriptionBox.Enabled = false;
            saveEditButton.Enabled = false;
            saveEditButton.Visible = false;
            cancelEditButton.Enabled = false;
            cancelEditButton.Visible = false;
            editButton.Enabled = true;
            editButton.Visible = true;
            checkPointHandler.MakeUneditable();
        }

        private void RefreshForm()
        {
            try
            {
                countryBox.Text = currentHike.CountryName;
                regionBox.Text = currentHike.RegionName;
                typeComboBox.SelectedValue = (int)currentHike.HikeType;
                dateBox.Value = currentHike.HikeDate;
                descriptionBox.Text = currentHike.Description;

                checkPointHandler.RegionID = currentHike.RegionID;
                checkPointHandler.LoadCPs(currentHike.CPList);
                checkPointHandler.RefreshControl();

                if (currentHike.HikeType == HikeType.túra)
                    Text = currentHike.Position.ToString() + ". túra adatai";
                if (currentHike.HikeType == HikeType.séta)
                    Text = "Séta adatai";
                if (currentHike.Position != null)
                {
                    positionBox.Text = currentHike.Position.ToString();
                }
                else
                {
                    positionBox.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool RefreshHikeData()
        {
            try
            {
                currentHike = daoManager.SearchHike(currentHike.HikeID);
                return true;
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
            return false;
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

        private HikeForUpdate GetDataForUpdate()
        {
            int hikeID = currentHike.HikeID;
            int countryID = currentHike.CountryID;
            int regionID = currentHike.RegionID;
            string description = descriptionBox.Text;
            DateTime oldHikeDate = currentHike.HikeDate;
            DateTime newHikeDate = dateBox.Value.Date;
            HikeType oldHikeType = currentHike.HikeType;
            HikeType newHikeType;
            if (!Enum.TryParse(typeComboBox.Text, out newHikeType))
            {
                MessageBox.Show("Nincs megadva a túra típusa.", "Hiba");
                typeComboBox.Focus();
                return null;
            }
            List<int> oldCPList = currentHike.CPList;
            List<int> newCPList = checkPointHandler.CPList;
            return new HikeForUpdate(hikeID, countryID, regionID, description, oldHikeDate, newHikeDate, oldHikeType, newHikeType,
                oldCPList, newCPList);
        }

        #endregion

        #region Eventhandler Methods

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            this.MakeEditable();
        }

        private void cancelEditButton_Click(object sender, EventArgs e)
        {
            MakeUneditable();
            RefreshForm();
        }

        private void saveEditButton_Click(object sender, EventArgs e)
        {
            HikeForUpdate hike = GetDataForUpdate();
            if (hike == null)
            {
                return;
            }
            try
            {
                if (daoManager.UpdateHike(hike))
                {
                    RefreshHikeData();
                    RefreshForm();
                    MakeUneditable();
                }
            }
            catch (DuplicateDateException)
            {
                MessageBox.Show("Ezzel a dátummal már van elmentve túra", "Hiba");
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

        private void deleteHikeButton_Click(object sender, EventArgs e)
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
                if (daoManager.DeleteHike(currentHike))
                {
                    MessageBox.Show("Törölve");
                    Close();
                }
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
