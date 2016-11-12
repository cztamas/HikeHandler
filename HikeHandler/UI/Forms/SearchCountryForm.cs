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
    public partial class SearchCountryForm : Form
    {
        private DAOManager daoManager;
        List<CountryForView> resultList;

        public SearchCountryForm(DAOManager manager)
        {
            InitializeComponent();
            daoManager = manager;
            resultList = new List<CountryForView>();
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
            try
            {
                resultList.Clear();
                BindingList<CountryForView> bindingList = new BindingList<CountryForView>(resultList);
                BindingSource source = new BindingSource(bindingList, null);
                resultView.DataSource = source;
                resultView.Columns["CountryID"].Visible = false;
                resultView.Columns["Name"].HeaderText = "Név";
                resultView.Columns["HikeCount"].HeaderText = "Túrák";
                resultView.Columns["RegionCount"].HeaderText = "Tájegységek";
                resultView.Columns["CPCount"].HeaderText = "CheckPointok";
                resultView.Columns["Description"].Visible = false;
                resultGroupBox.Text = "Találatok száma: " + resultList.Count;
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
            resultView.DataSource = null;
        }

        private void resultView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            int index;
            if (!int.TryParse(resultView.Rows[e.RowIndex].Cells["countryID"].Value.ToString(), out index))
            {
                MessageBox.Show("Nem sikerült megjeleníteni a kiválasztott országot.", "Hiba");
                return;
            }
            try
            {
                ViewCountryForm vForm = new ViewCountryForm(daoManager, index);
                vForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hiba");
            }
        }

        private void detailsButton_Click(object sender, EventArgs e)
        {
            if (resultView.SelectedRows == null)
                return;
            foreach (DataGridViewRow row in resultView.SelectedRows)
            {
                int index;
                if (!int.TryParse(row.Cells["countryID"].Value.ToString(), out index))
                {
                    MessageBox.Show("Nem sikerült megjeleníteni a kiválasztott országot.", "Hiba");
                    return;
                }
                try
                {
                    ViewCountryForm vForm = new ViewCountryForm(daoManager, index);
                    vForm.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Hiba");
                }
            }
            
        }

        #endregion
    }
}
