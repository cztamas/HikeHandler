﻿using System;
using System.Data;
using System.Windows.Forms;
using HikeHandler.ModelObjects;
using HikeHandler.ServiceLayer;

namespace HikeHandler.UI
{
    public partial class SearchCountryForm : Form
    {
        private DAOManager daoManager;

        public SearchCountryForm(DAOManager manager)
        {
            InitializeComponent();
            daoManager = manager;
        }

        private void SearchCountryForm_Load(object sender, EventArgs e)
        {
            //GetCountryList();
            countryNameBox.Text = string.Empty;
            countryNameBox.Focus();
        }

        #region Auxiliary Methods

        private void Clear()
        {
            resultView.DataSource = null;
            countryNameBox.Text = string.Empty;
            hikeCountBox.Text = string.Empty;
            cpCountBox.Text = string.Empty;
            regionCountBox.Text = string.Empty;
            resultGroupBox.Text = "Találatok";
            countryNameBox.Focus();
        }

        /*private void GetCountryList()
        {
            DataTable table = daoManager.GetAllCountryNames();
            if (table == null)
            {
                Close();
            }
            countryComboBox.DataSource = table;
            countryComboBox.ValueMember = "idcountry";
            countryComboBox.DisplayMember = "name";
        }*/

        private CountryForSearch GetDataForSearch()
        {
            if (!cpCountBox.Text.IsIntPile())
            {
                MessageBox.Show("Nem megfelelő számformátum.", "Hiba");
                cpCountBox.Focus();
                return null;
            }
            if (!hikeCountBox.Text.IsIntPile())
            {
                MessageBox.Show("Nem megfelelő számformátum.", "Hiba");
                hikeCountBox.Focus();
                return null;
            }
            if (!regionCountBox.Text.IsIntPile())
            {
                MessageBox.Show("Nem megfelelő számformátum.", "Hiba");
                regionCountBox.Focus();
                return null;
            }
            return new CountryForSearch(countryNameBox.Text, hikeCountBox.Text.ToIntPile(), 
                cpCountBox.Text.ToIntPile(), regionCountBox.Text.ToIntPile());
        }

        #endregion

        #region Eventhandler Methods

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            this.Clear();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            CountryForSearch template = GetDataForSearch();
            if (template == null)
            {
                return;
            }
            DataTable table = daoManager.SearchCountry(template);
            if (table == null)
            {
                return;
            }

            resultView.DataSource = table;
            resultView.Columns["idcountry"].Visible = false;
            resultView.Columns["name"].HeaderText = "Név";
            resultView.Columns["hikecount"].HeaderText = "Túrák száma";
            resultView.Columns["regioncount"].HeaderText = "Tájegységek";
            resultView.Columns["cpcount"].HeaderText = "CheckPointok";
            resultGroupBox.Text = "Találatok száma: " + table.Rows.Count;
        }

        private void resultView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            int index;
            if (!int.TryParse(resultView.Rows[e.RowIndex].Cells["idcountry"].Value.ToString(), out index))
            {
                MessageBox.Show("Nem sikerült megjeleníteni a kiválasztott országot.", "Hiba");
                return;
            }
            ViewCountryForm vForm = new ViewCountryForm(daoManager, index);
            vForm.Show();
        }

        private void detailsButton_Click(object sender, EventArgs e)
        {
            if (resultView.SelectedRows == null)
                return;
            foreach (DataGridViewRow row in resultView.SelectedRows)
            {
                int index;
                if (!int.TryParse(row.Cells["idcountry"].Value.ToString(), out index))
                {
                    MessageBox.Show("Nem sikerült megjeleníteni a kiválasztott országot.", "Hiba");
                    return;
                }
                ViewCountryForm vForm = new ViewCountryForm(daoManager, index);
                vForm.Show();
            }
            
        }

        #endregion
    }
}
