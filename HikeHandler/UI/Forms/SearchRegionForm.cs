using HikeHandler.Exceptions;
using HikeHandler.Interfaces;
using HikeHandler.ModelObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace HikeHandler.UI
{
    public partial class SearchRegionForm : Form
    {
        private IDAOManager daoManager;
        private HikeRegionForSearch templateToShow;
        private List<HikeRegionForView> resultList;
        
        public SearchRegionForm(IDAOManager manager)
        {
            InitializeComponent();
            daoManager = manager;
            resultList = new List<HikeRegionForView>();
        }

        public SearchRegionForm(IDAOManager manager, HikeRegionForSearch template)
        {
            InitializeComponent();
            daoManager = manager;
            templateToShow = template;
            resultList = new List<HikeRegionForView>();
        }

        private void SearchRegionForm_Load(object sender, EventArgs e)
        {
            GetCountryList();
            if (templateToShow != null)
            {
                countryComboBox.Text = templateToShow.CountryName;
                MakeSearch(templateToShow);
                return;
            }
            else
            {
                countryComboBox.Text = string.Empty;
                nameBox.Focus();
            }
        }

        #region Auxiliary Methods

        private void Clear()
        {
            resultView.DataSource = null;
            nameBox.Text = string.Empty;
            hikeNumberBox.Text = string.Empty;
            countryComboBox.Text = string.Empty;
            resultGroupBox.Text = "Találatok";
        }

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

        private void MakeSearch(HikeRegionForSearch template)
        {
            try
            {
                resultList = daoManager.SearchRegion(template);
                BindingList<HikeRegionForView> bindingList = new BindingList<HikeRegionForView>();
                BindingSource source = new BindingSource(bindingList, null);
                resultView.DataSource = resultList;
                resultView.Columns["RegionID"].Visible = false;
                resultView.Columns["CountryID"].Visible = false;
                resultView.Columns["Name"].HeaderText = "Név";
                resultView.Columns["HikeCount"].HeaderText = "Túrák";
                resultView.Columns["CPCount"].HeaderText = "CheckPointok";
                resultView.Columns["CountryName"].HeaderText = "Ország";
                resultView.Columns["Description"].Visible = false;
                resultGroupBox.Text = "Találatok száma: " + resultList.Count;
                foreach (DataGridViewColumn column in resultView.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.Automatic;
                }
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

        private HikeRegionForSearch GetDataForSearch()
        { 
            if (!hikeNumberBox.Text.IsIntPile())
            {
                MessageBox.Show("Nem megfelelő számformátum.", "Hiba");
                hikeNumberBox.Focus();
                return null;
            }
            IntPile hikeCount = hikeNumberBox.Text.ToIntPile();
            return new HikeRegionForSearch(countryComboBox.Text, nameBox.Text, hikeCount);
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
            HikeRegionForSearch template = GetDataForSearch();
            if (template == null)
            {
                return;
            }
            MakeSearch(template);
        }

        private void detailsButton_Click(object sender, EventArgs e)
        {
            if (resultView.SelectedRows == null)
                return;
            int regionID;
            foreach (DataGridViewRow row in resultView.SelectedRows)
            {
                if (!int.TryParse(row.Cells["RegionID"].Value.ToString(), out regionID))
                {
                    MessageBox.Show("Nem sikerült megjeleníteni a kért tájegységet.", "Hiba");
                    return;
                }
                ViewRegionForm viewRegionForm = new ViewRegionForm(daoManager, regionID);
                viewRegionForm.Show();
            }
        }
        
        private void resultView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            int regionID;
            if (!int.TryParse(resultView.Rows[e.RowIndex].Cells["RegionID"].Value.ToString(), out regionID))
            {
                MessageBox.Show("Nem sikerült megjeleníteni a kért tájegységet.", "Hiba");
                return;
            }
            ViewRegionForm viewRegionForm = new ViewRegionForm(daoManager, regionID);
            viewRegionForm.Show();
        }

        #endregion
    }
}
