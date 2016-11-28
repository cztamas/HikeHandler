using System;
using System.Windows.Forms;
using HikeHandler.ModelObjects;
using HikeHandler.ServiceLayer;
using HikeHandler.Exceptions;

namespace HikeHandler.UI
{
    public partial class ViewRegionForm : Form
    {
        private DAOManager daoManager;
        private HikeRegionForView currentRegion;

        public ViewRegionForm(DAOManager manager, int idRegion)
        {
            InitializeComponent();
            daoManager = manager;
            currentRegion = new HikeRegionForView(idRegion);
        }

        private void ViewRegionForm_Load(object sender, EventArgs e)
        {
            RefreshRegionData();
            RefreshForm();
            MakeUneditable();
        }

        #region Auxiliary Methods

        private void MakeEditable()
        {
            editButton.Enabled = false;
            editButton.Visible = false;
            saveEditButton.Enabled = true;
            saveEditButton.Visible = true;
            cancelEditButton.Enabled = true;
            cancelEditButton.Visible = true;
            nameBox.Enabled = true;
            descriptionBox.Enabled = true;
        }

        private void MakeUneditable()
        {
            editButton.Enabled = true;
            editButton.Visible = true;
            saveEditButton.Enabled = false;
            saveEditButton.Visible = false;
            cancelEditButton.Enabled = false;
            cancelEditButton.Visible = false;
            nameBox.Enabled = false;
            descriptionBox.Enabled = false;
        }

        private void RefreshForm()
        {
            nameBox.Text = currentRegion.Name;
            countryBox.Text = currentRegion.CountryName;
            hikeCountBox.Text = currentRegion.HikeCount.ToString();
            descriptionBox.Text = currentRegion.Description;
            Text = currentRegion.Name + " adatai";
        }

        private void RefreshRegionData()
        {
            try
            {
                currentRegion = daoManager.SearchRegion(currentRegion.RegionID);
                return;
            }
            catch (NoItemFoundException)
            {
                MessageBox.Show("Nem található a keresett tájegység.", "Hiba");
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

        private HikeRegionForUpdate GetDataForUpdate()
        {
            if (string.IsNullOrWhiteSpace(nameBox.Text))
            {
                MessageBox.Show("Nincs megadva a tájegység neve;", "Hiba");
                nameBox.Focus();
                return null;
            }
            string newName = nameBox.Text;
            string oldName = currentRegion.Name;
            string description = descriptionBox.Text;
            int regionID = currentRegion.RegionID;
            return new HikeRegionForUpdate(regionID, oldName, newName, description);
        }

        #endregion

        #region Eventhandler Methods

        private void editButton_Click(object sender, EventArgs e)
        {
            MakeEditable();
        }

        private void cancelEditButton_Click(object sender, EventArgs e)
        {
            RefreshForm();
            MakeUneditable();
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            RefreshRegionData();
            RefreshForm();
            MakeUneditable();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveEditButton_Click(object sender, EventArgs e)
        {
            HikeRegionForUpdate region = GetDataForUpdate();
            if (region == null)
            {
                return;
            }
            try
            {
                if (daoManager.UpdateRegion(region))
                {
                    RefreshRegionData();
                    RefreshForm();
                    MakeUneditable();
                }
            }
            catch (DuplicateItemNameException)
            {
                MessageBox.Show("Már van elmentve ilyen nevű tájegység.");
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

        private void showHikesButton_Click(object sender, EventArgs e)
        {
            HikeForSearch template = new HikeForSearch();
            template.IDRegion = currentRegion.RegionID;
            template.RegionName = currentRegion.Name;
            SearchHikeForm searchHikeForm = new SearchHikeForm(daoManager, template);
            searchHikeForm.Show();
        }

        private void showCPsButton_Click(object sender, EventArgs e)
        {
            CPForSearch template = new CPForSearch();
            template.IDRegion = currentRegion.RegionID;
            template.RegionName = currentRegion.Name;
            SearchCPForm searchCPForm = new SearchCPForm(daoManager, template);
            searchCPForm.Show();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            // asking for confirmation
            string message = "Biztosan törli?";
            string caption = "Tájegység törlése";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, caption, buttons);
            if (result == DialogResult.No)
            {
                return;
            }
            try
            {
                if (daoManager.DeleteRegion(currentRegion))
                {
                    MessageBox.Show("Törölve");
                    Close();
                }
            }
            catch (NotDeletableException)
            {
                MessageBox.Show("Csak olyan tájegység törölhető, amihez nem tartozik checkpoint vagy túra.");
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
