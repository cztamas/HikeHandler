using HikeHandler.Exceptions;
using HikeHandler.Extensions;
using HikeHandler.Interfaces;
using HikeHandler.ModelObjects;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HikeHandler.UI
{
    public partial class ViewCPForm : Form
    {
        private IDAOManager daoManager;
        private CPForView currentCP;

        public ViewCPForm(IDAOManager manager, int cpID)
        {
            InitializeComponent();
            daoManager = manager;
            currentCP = new CPForView(cpID);
        }

        private void ViewCPForm_Load(object sender, EventArgs e)
        {
            RefreshCPData();
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
            typeComboBox.Enabled = true;
            descriptionBox.Enabled = true;
            string actualCPType = typeComboBox.Text;
            GetCPTypes();
            typeComboBox.Text = actualCPType;
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
            typeComboBox.Enabled = false;
            descriptionBox.Enabled = false;
        }

        private void RefreshForm()
        {
            nameBox.Text = currentCP.Name;
            regionBox.Text = currentCP.RegionNames.ToCommaSeparatedList();
            countryBox.Text = currentCP.CountryNames.ToCommaSeparatedList();
            hikeCountBox.Text = currentCP.HikeCount.ToString();
            descriptionBox.Text = currentCP.Description;
            typeComboBox.Text = currentCP.TypeOfCP.ToString();
            Text = currentCP.Name + " adatai";
        }

        private void RefreshCPData()
        {
            try
            {
                currentCP = daoManager.SearchCP(currentCP.CPID);
                return;
            }
            catch (NoItemFoundException)
            {
                MessageBox.Show("Nem található a keresett checkpoint.", "Hiba");
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

        private void GetCPTypes()
        {
            try
            {
                List<NameAndID> cpTypesList = daoManager.GetCPTypes();
                typeComboBox.DataSource = cpTypesList;
                typeComboBox.ValueMember = "id";
                typeComboBox.DisplayMember = "name";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hiba");
            }
        }

        // Collects the data from the form into a CPForUpdate object
        private CPForUpdate GetDataForUpdate()
        {
            int cpID = currentCP.CPID;
            string oldName = currentCP.Name;
            if (string.IsNullOrWhiteSpace(nameBox.Text))
            {
                MessageBox.Show("Nincs megadva a checkpoint neve.", "Hiba");
                nameBox.Focus();
                return null;
            }
            string newName = nameBox.Text;
            string description = descriptionBox.Text;
            CPType cpType;
            if (!Enum.TryParse(typeComboBox.Text, out cpType))
            {
                MessageBox.Show("A checkpoint típusa nincs megadva.", "Hiba");
                typeComboBox.Focus();
                return null;
            }
            return new CPForUpdate(cpID, oldName, newName, cpType, description);
        }

        #endregion

        #region Eventhandler Methods

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

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
            RefreshForm();
            MakeUneditable();
        }

        private void saveEditButton_Click(object sender, EventArgs e)
        {
            CPForUpdate cp = GetDataForUpdate();
            if (cp == null)
                return;
            try
            {
                if (daoManager.UpdateCP(cp))
                {
                    MakeUneditable();
                    RefreshCPData();
                    RefreshForm();
                }
            }
            catch (DuplicateItemNameException)
            {
                MessageBox.Show("Már van elmentve ilyen nevű checkpoint.");
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
            template.CPList.Add(currentCP.CPID);
            SearchHikeForm searchHikeForm = new SearchHikeForm(daoManager, template);
            searchHikeForm.Show();
        }

        private void deleteCPbutton_Click(object sender, EventArgs e)
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
                if (daoManager.DeleteCP(currentCP))
                {
                    MessageBox.Show("Törölve.");
                    Close();
                }
            }
            catch (NotDeletableException)
            {
                MessageBox.Show("Csak olyan checkpoint törölhető, amihez nem tartozik túra.");
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
