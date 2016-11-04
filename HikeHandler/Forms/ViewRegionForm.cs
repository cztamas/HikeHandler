using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using HikeHandler.ModelObjects;
using HikeHandler.DAOs;
using HikeHandler.Exceptions;

namespace HikeHandler.Forms
{
    public partial class ViewRegionForm : Form
    {
        private MySqlConnection sqlConnection;
        private RegionDao regionDao;
        private HikeRegionForView currentRegion;

        public ViewRegionForm()
        {
            InitializeComponent();
            currentRegion = new HikeRegionForView();
        }

        public ViewRegionForm(MySqlConnection connection, int idRegion)
        {
            InitializeComponent();
            sqlConnection = connection;
            regionDao = new RegionDao(connection);
            currentRegion = new HikeRegionForView(idRegion);
            RefreshForm();
            MakeUneditable();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

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
            HikeRegionForView region = GetRegionData(currentRegion.ID);
            if (region == null)
                return;
            currentRegion = region;
            nameBox.Text = region.Name;
            countryBox.Text = region.CountryName;
            hikeCountBox.Text = region.HikeCount.ToString();
            descriptionBox.Text = region.Description;
            Text = region.Name + " adatai";
        }

        public HikeRegionForView GetRegionData(int regionID)
        {   
            HikeRegionForSearch template = new HikeRegionForSearch(regionID);
            try
            {
                DataTable table = regionDao.SearchRegion(template);
                DataRow row = table.Rows[0];
                HikeRegionForView region = new HikeRegionForView();
                region.ID = (int)row["id"];
                region.Name = (string)row["name"];
                region.HikeCount = (int)row["hikecount"];
                region.Description = (string)row["description"];
                region.CountryName = (string)row["countryname"];
                return region;
            }
            catch (DaoException ex)
            {
                if (ex.Error == ErrorType.NoDBConnection)
                {
                    MessageBox.Show("Nincs kapcsolat az adatbázissal.", "Hiba");
                    return null;
                }
                MessageBox.Show(ex.Message, "Hiba");
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hiba");
                return null;
            }
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
            HikeRegionForView region = new HikeRegionForView(currentRegion.ID);
            region.Name = nameBox.Text;
            region.Description = descriptionBox.Text;
            try
            {
                if (regionDao.UpdateRegion(region))
                {
                    RefreshForm();
                    MakeUneditable();
                }
            }
            catch (DaoException ex)
            {
                if (ex.Error == ErrorType.NoDBConnection)
                {
                    MessageBox.Show("Nem lehet elérni az adatbázist.", "Hiba");
                    return;
                }
                if (ex.Error == ErrorType.DuplicateName)
                {
                    MessageBox.Show("Már van elmentve ilyen nevű tájegység.", "Hiba");
                    return;
                }
                MessageBox.Show(ex.Message, "Hiba");
            }
            
        }

        private void showHikesButton_Click(object sender, EventArgs e)
        {
            HikeForSearch template = new HikeForSearch();
            template.IDRegion = currentRegion.ID;
            template.RegionName = currentRegion.Name;
            SearchHikeForm searchHikeForm = new SearchHikeForm(sqlConnection, template);
            searchHikeForm.Show();
        }

        private void showCPsButton_Click(object sender, EventArgs e)
        {
            CPForSearch template = new CPForSearch();
            template.IDRegion = currentRegion.ID;
            template.RegionName = currentRegion.Name;
            SearchCPForm searchCPForm = new SearchCPForm(sqlConnection, template);
            searchCPForm.Show();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            string message = "Biztosan törli?";
            string caption = "Tájegység törlése";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, caption, buttons);
            if (result == DialogResult.No)
                return;
            try
            {
                if (regionDao.DeleteRegion(currentRegion.ID))
                {
                    MessageBox.Show("Törölve");
                    Close();
                }
            }
            catch (DaoException ex)
            {
                if (ex.Error == ErrorType.NotDeletable)
                {
                    MessageBox.Show("Csak olyan tájegység törölhető, amihez nincs checkpoint vagy túra hozzárendelve");
                    return;
                }
                if (ex.Error == ErrorType.NoDBConnection)
                {
                    MessageBox.Show("Nem lehet elérni az adatbázist.", "Hiba");
                    return;
                }
                MessageBox.Show(ex.Message, "Hiba");
            }
        }
    }
}
