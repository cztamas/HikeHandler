using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using HikeHandler.ModelObjects;
using HikeHandler.ServiceLayer;

namespace HikeHandler.UI
{    
    public partial class ViewCPForm : Form
    {
        private DAOManager daoManager;
        private CPForView currentCP;

        public ViewCPForm(DAOManager manager, int cpID)
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
            regionBox.Text = currentCP.RegionName;
            countryBox.Text = currentCP.CountryName;
            hikeCountBox.Text = currentCP.HikeCount.ToString();
            descriptionBox.Text = currentCP.Description;
            typeComboBox.Text = currentCP.TypeOfCP.ToString();
            Text = currentCP.Name + " adatai";
        }

        private void RefreshCPData()
        {
            currentCP = daoManager.SearchCP(currentCP.CPID);
            if (currentCP == null)
            {
                Close();
            }
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
            if (typeComboBox.SelectedItem == null)
            {
                MessageBox.Show("A checkpoint típusa nincs megadva.");
                typeComboBox.Focus();
                return null;
            }
            if (!Enum.TryParse(typeComboBox.SelectedItem.ToString(), out cpType))
            {
                MessageBox.Show("Nem sikerült elmenteni az adatokat.", "Hiba");
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
            if (daoManager.UpdateCP(cp))
            {
                MakeUneditable();
                RefreshCPData();
                RefreshForm();
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
            if (daoManager.DeleteCP(currentCP))
            {
                MessageBox.Show("Törölve.");
                Close();
            }
        }

        #endregion
    }
}
