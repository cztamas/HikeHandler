using Equin.ApplicationFramework;
using HikeHandler.Exceptions;
using HikeHandler.Interfaces;
using HikeHandler.ModelObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace HikeHandler.UI
{
    public partial class SearchCPForm : Form
    {
        private IDAOManager daoManager;
        private CPForSearch templateToShow;

        public SearchCPForm(IDAOManager manager)
        {
            InitializeComponent();
            daoManager = manager;
        }

        public SearchCPForm(IDAOManager manager, CPForSearch template)
        {
            InitializeComponent();
            daoManager = manager;
            templateToShow = template;
        }

        private void SearchCPForm_Load(object sender, EventArgs e)
        {
            GetCountryList();
            GetCPTypes();
            if (templateToShow != null)
            {
                if (templateToShow.CountryName != string.Empty)
                    countryComboBox.Text = templateToShow.CountryName;
                if (templateToShow.RegionName != string.Empty)
                {
                    regionComboBox.SelectedIndex = -1;
                    regionComboBox.Text = templateToShow.RegionName;
                }
                MakeSearch(templateToShow);
            }
            else
            {
                countryComboBox.Text = string.Empty;
                regionComboBox.Text = string.Empty;
                nameBox.Focus();
            }
        }

        #region Auxiliary Methods

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

        private void GetRegions(int countryID)
        {
            try
            {
                List<NameAndID> list = daoManager.GetAllRegionsOfCountry(countryID);
                regionComboBox.DataSource = list;
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

        private void GetCPTypes()
        {
            DataTable cpTypesTable = new DataTable();
            DataColumn column;
            DataRow row;

            column = new DataColumn("id", typeof(int));
            cpTypesTable.Columns.Add(column);
            column = new DataColumn("name", typeof(string));
            cpTypesTable.Columns.Add(column);

            row = cpTypesTable.NewRow();
            row["id"] = -1;
            row["name"] = string.Empty;
            cpTypesTable.Rows.Add(row);

            Array cpTypes = Enum.GetValues(typeof(CPType));
            foreach (CPType item in cpTypes)
            {
                row = cpTypesTable.NewRow();
                row["id"] = (int)item;
                row["name"] = item.ToString();
                cpTypesTable.Rows.Add(row);
            }
            typeComboBox.DataSource = cpTypesTable;
            typeComboBox.ValueMember = "id";
            typeComboBox.DisplayMember = "name";
        }

        private void Clear()
        {
            resultView.DataSource = null;
            hikeNumberBox.Text = string.Empty;
            nameBox.Text = string.Empty;
            countryComboBox.Text = string.Empty;
            typeComboBox.Text = string.Empty;
            regionComboBox.DataSource = null;
            resultGroupBox.Text = "Találatok";
        }

        private void MakeSearch(CPForSearch template)
        {
            try
            {
                List<CPForView> resultList = daoManager.SearchCP(template);
                BindingListView<CPForView> bindingView = new BindingListView<CPForView>(resultList);
                resultView.DataSource = bindingView;
                resultView.Columns["CPID"].Visible = false;
                resultView.Columns["CountryID"].Visible = false;
                resultView.Columns["RegionID"].Visible = false;
                resultView.Columns["Name"].HeaderText = "Név";
                resultView.Columns["TypeOfCP"].HeaderText = "Típus";
                resultView.Columns["HikeCount"].HeaderText = "Túrák";
                resultView.Columns["HikeCount"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                resultView.Columns["RegionName"].HeaderText = "Tájegység";
                resultView.Columns["CountryName"].HeaderText = "Ország";
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
            Close();
        }

        private CPForSearch GetDataForSearch()
        {
            if (!hikeNumberBox.Text.IsIntPile())
            {
                MessageBox.Show("Nem megfelelő számformátum.", "Hiba");
                hikeNumberBox.Focus();
                return null;
            }

            CPForSearch template = new CPForSearch();
            template.HikeCount = hikeNumberBox.Text.ToIntPile();
            template.Name = nameBox.Text;
            template.CountryName = countryComboBox.Text;
            template.RegionName = regionComboBox.Text;
            if (typeComboBox.SelectedValue != null)
            {
                CPType cpType;
                if (Enum.TryParse(typeComboBox.SelectedItem.ToString(), out cpType))
                {
                    template.TypeOfCP = cpType;
                }
            }
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
            CPForSearch cp = GetDataForSearch();
            if (cp == null)
            {
                return;
            }
            MakeSearch(cp);
        }

        private void countryComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (countryComboBox.SelectedValue == null)
                return;
            int a;
            if (!int.TryParse(countryComboBox.SelectedValue.ToString(),out a))
                return;
            regionComboBox.Text = string.Empty;
            GetRegions((int)countryComboBox.SelectedValue);
            regionComboBox.Text = string.Empty;            
        }
        
        private void detailsButton_Click(object sender, EventArgs e)
        {
            if (resultView.SelectedRows == null)
                return;
            int index;
            foreach (DataGridViewRow row in resultView.SelectedRows)
            {   
                if (!int.TryParse(row.Cells["CPID"].Value.ToString(), out index))
                {
                    MessageBox.Show("Nem sikerült megjeleníteni a kiválasztott checkpointot.", "Hiba");
                    return;
                }
                ViewCPForm viewCPForm = new ViewCPForm(daoManager, index);
                viewCPForm.Show();
            }
        }

        private void resultView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            int index;
            if (!int.TryParse(resultView.Rows[e.RowIndex].Cells["CPID"].Value.ToString(), out index))
            {
                MessageBox.Show("Nem sikerült megjeleníteni a kiválasztott országot.", "Hiba");
                return;
            }
            ViewCPForm viewCPForm = new ViewCPForm(daoManager, index);
            viewCPForm.Show();
        }

        #endregion
    }
}
