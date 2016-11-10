using System;
using System.Data;
using System.Windows.Forms;
using HikeHandler.ModelObjects;
using HikeHandler.ServiceLayer;

namespace HikeHandler.UI
{
    public partial class SearchRegionForm : Form
    {
        private DAOManager daoManager;
        private HikeRegionForSearch templateToShow;
        
        public SearchRegionForm(DAOManager manager)
        {
            InitializeComponent();
            daoManager = manager;     
        }

        public SearchRegionForm(DAOManager manager, HikeRegionForSearch template)
        {
            InitializeComponent();
            daoManager = manager;
            templateToShow = template;
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
            DataTable table = daoManager.GetAllCountryNames();
            if (table == null)
            {
                Close();
            }
            countryComboBox.DataSource = table;
            countryComboBox.ValueMember = "idcountry";
            countryComboBox.DisplayMember = "name";
        }

        private void MakeSearch(HikeRegionForSearch template)
        {
            DataTable resultTable = daoManager.SearchRegion(template);
            resultView.DataSource = resultTable;
            resultView.Columns["id"].Visible = false;
            resultView.Columns["name"].HeaderText = "Név";
            resultView.Columns["hikecount"].HeaderText = "Túrák száma";
            resultView.Columns["countryname"].HeaderText = "Ország";
            resultGroupBox.Text = "Találatok száma: " + resultTable.Rows.Count;
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
                if (!int.TryParse(row.Cells["id"].Value.ToString(), out regionID))
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
            if (!int.TryParse(resultView.Rows[e.RowIndex].Cells[0].Value.ToString(), out regionID))
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
