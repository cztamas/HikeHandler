﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using HikeHandler.ModelObjects;
using HikeHandler.ServiceLayer;

namespace HikeHandler.UI
{
    public partial class ViewHikeForm : Form
    {
        private HikeForView currentHike;
        private DAOManager daoManager;

        public ViewHikeForm(DAOManager manager, int hikeID)
        {
            InitializeComponent();
            daoManager = manager;
            currentHike = new HikeForView(hikeID);
        }

        private void ViewHikeForm_Load(object sender, EventArgs e)
        {
            GetHikeTypes();
            checkPointHandler.Init(daoManager, CPHandlerStyle.View);
            RefreshHikeData();
            RefreshForm();
            MakeUneditable();
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
            positionBox.Text = currentHike.Position.ToString();
        }

        private void RefreshHikeData()
        {
            currentHike = daoManager.SearchHike(currentHike.HikeID);
            if (currentHike == null)
            {
                Close();
            }
        }

        private void GetHikeTypes()
        {
            DataTable hikeTypesTable = daoManager.GetHikeTypes();
            typeComboBox.DataSource = hikeTypesTable;
            typeComboBox.ValueMember = "id";
            typeComboBox.DisplayMember = "name";
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
            if (typeComboBox.SelectedItem == null)
            {
                MessageBox.Show("Nincs megadva a túra típusa.", "Hiba");
                typeComboBox.Focus();
                return null;
            }
            if (!Enum.TryParse(typeComboBox.SelectedItem.ToString(), out newHikeType))
            {
                MessageBox.Show("Nem sikerült elmenteni a túrát.", "Hiba");
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
            if (daoManager.UpdateHike(hike))
            {
                RefreshHikeData();
                RefreshForm();
                MakeUneditable();
            }
        }

        private void deleteHikeButton_Click(object sender, EventArgs e)
        {
            if (daoManager.DeleteHike(currentHike))
            {
                MessageBox.Show("Törölve");
                Close();
            }
        }

        #endregion
    }
}