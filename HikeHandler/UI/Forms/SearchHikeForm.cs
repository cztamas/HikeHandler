using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using HikeHandler.ModelObjects;
using HikeHandler.ServiceLayer;
using HikeHandler.Exceptions;

namespace HikeHandler.UI
{
    public partial class SearchHikeForm : Form
    {
        private DAOManager daoManager;
        private HikeForSearch templateToShow;

        public SearchHikeForm(DAOManager manager)
        {
            InitializeComponent();
            daoManager = manager;
        }

        public SearchHikeForm(DAOManager manager, HikeForSearch template)
        {
            InitializeComponent();
            daoManager = manager;
            templateToShow = template;
        }

        private void SearchHikeForm_Load(object sender, EventArgs e)
        {
            checkPointHandler.Init(daoManager, CPHandlerStyle.Search);
            regionComboBox.SelectedValueChanged += new EventHandler(checkPointHandler.Region_Refreshed);
            GetCountryList();
            GetHikeTypes();
            if (templateToShow != null)
            { 
                if (templateToShow.CountryName != string.Empty)
                    countryComboBox.Text = templateToShow.CountryName;
                if (templateToShow.RegionName != string.Empty)
                    regionComboBox.Text = templateToShow.RegionName;
                if (templateToShow.CPList.Count != 0)
                    checkPointHandler.LoadCPs(templateToShow.CPList);
                MakeSearch(templateToShow);
            }
            else
            {
                typeComboBox.SelectedValue = -1;
                countryComboBox.Text = string.Empty;
                regionComboBox.Text = string.Empty;
                countryComboBox.Focus();
            }
        }

        #region Auxiliary Methods

        private void GetCountryList()
        {
            try
            {
                List<NameAndID> countries = daoManager.GetAllCountryNames();
                countryComboBox.DataSource = countries;
                countryComboBox.ValueMember = "ID";
                countryComboBox.DisplayMember = "Name";
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

        private void GetRegionList(int countryID)
        {
            try
            {
                List<NameAndID> table = daoManager.GetAllRegionsOfCountry(countryID);
                if (table == null)
                {
                    Close();
                }
                regionComboBox.DataSource = table;
                regionComboBox.ValueMember = "ID";
                regionComboBox.DisplayMember = "Name";
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
        
        private void Clear()
        {
            checkPointHandler.Clear();
            resultView.DataSource = null;
            countryComboBox.Text = string.Empty;
            regionComboBox.Text = string.Empty;
            resultGroupBox.Text = "Találatok";
        }

        private void MakeSearch(HikeForSearch template)
        {
            try
            {
                List<HikeForView> resultList = daoManager.SearchHike(template, checkPointHandler.AnyCPOrder);
                resultView.DataSource = resultList;
                resultView.Columns["HikeID"].Visible = false;
                resultView.Columns["Position"].HeaderText = "Sorszám";
                resultView.Columns["HikeDate"].HeaderText = "Dátum";
                resultView.Columns["RegionID"].Visible = false;
                resultView.Columns["CountryID"].Visible = false;
                resultView.Columns["RegionName"].HeaderText = "Tájegység";
                resultView.Columns["RegionName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                resultView.Columns["CountryName"].HeaderText = "Ország";
                resultView.Columns["HikeType"].HeaderText = "Típus";
                resultView.Columns["Description"].Visible = false;
                resultView.Columns["CPString"].Visible = false;
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
            Close();
        }

        private HikeForSearch GetDataForSearch()
        {
            if (!hikePositionBox.Text.IsIntPile())
            {
                MessageBox.Show("Hibás számformátum.");
                hikePositionBox.Focus();
                return null;
            }
            if (!dateBox.Text.IsDatePile())
            {
                MessageBox.Show("Hibás dátumformátum.");
                dateBox.Focus();
                return null;
            }
            HikeForSearch template = new HikeForSearch();
            template.CountryName = countryComboBox.Text;
            template.RegionName = regionComboBox.Text;
            if (typeComboBox.SelectedItem != null)
            {
                HikeType hikeType;
                if (Enum.TryParse(typeComboBox.SelectedItem.ToString(), out hikeType))
                    template.HikeType = hikeType;
            }
            template.Position = hikePositionBox.Text.ToIntPile();
            template.HikeDate = dateBox.Text.ToDatePile();
            template.CPList = checkPointHandler.CPList;
            return template;
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
            HikeForSearch template = GetDataForSearch();
            if (template == null)
            {
                return;
            }
            MakeSearch(template);
        }

        private void detailsButton_Click(object sender, EventArgs e)
        {
            if (resultView.SelectedRows == null)
            {
                return;
            }
            int hikeID;
            foreach (DataGridViewRow row in resultView.SelectedRows)
            {
                if (!int.TryParse(row.Cells["HikeID"].Value.ToString(), out hikeID))
                {
                    MessageBox.Show("Nem sikerült megjeleníteni a kért országot.", "Hiba");
                    return;
                }
                ViewHikeForm viewHikeForm = new ViewHikeForm(daoManager, hikeID);
                viewHikeForm.Show();
            }
        }

        private void resultView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            int hikeID;
            if (!int.TryParse(resultView.Rows[e.RowIndex].Cells["HikeID"].Value.ToString(), out hikeID))
            {
                MessageBox.Show("Nem sikerült megjeleníteni a kért országot.", "Hiba");
                return;
            }
            ViewHikeForm viewHikeForm = new ViewHikeForm(daoManager, hikeID);
            viewHikeForm.Show();
        }

        private void countryComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (countryComboBox.SelectedValue == null)
                return;
            int countryID;
            if (!int.TryParse(countryComboBox.SelectedValue.ToString(), out countryID))
                return;
            GetRegionList(countryID);
        }

        #endregion
    }
}
