using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HikeHandler.ModelObjects;
using HikeHandler.ServiceLayer;

namespace HikeHandler.UI
{
    public partial class AddHikeForm : Form
    {
        private DAOManager daoManager;

        public AddHikeForm(DAOManager manager)
        {
            InitializeComponent();
            daoManager = manager;
        }

        private void AddHikeForm_Load(object sender, EventArgs e)
        {
            checkPointHandler.Init(daoManager, CPHandlerStyle.Add);
            regionComboBox.SelectedValueChanged += new EventHandler(checkPointHandler.Region_Refreshed);
            GetCountryList();
            GetHikeTypes();
        }

        #region Auxiliary Methods

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

        private void GetRegionList(int countryID)
        {
            DataTable table = daoManager.GetAllRegionsOfCountry(countryID);
            regionComboBox.DataSource = table;
            regionComboBox.ValueMember = "idregion";
            regionComboBox.DisplayMember = "name";
        }

        private void GetHikeTypes()
        {
            DataTable hikeTypesTable = daoManager.GetHikeTypes();
            if (hikeTypesTable == null)
            {
                Close();
            }
            typeComboBox.DataSource = hikeTypesTable;
            typeComboBox.ValueMember = "id";
            typeComboBox.DisplayMember = "name";
        }

        // NOT IMPLEMENTED
        private HikeForSave GetDataForSave()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Eventhandler Methods

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void countryComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (countryComboBox.SelectedValue.GetType() != typeof(int))
                return;
            GetRegionList((int)countryComboBox.SelectedValue);
        }

        private void addHikeButton_Click(object sender, EventArgs e)
        {
            HikeForView hike = new HikeForView();
            hike.CountryID = (int)countryComboBox.SelectedValue;
            hike.RegionID = (int)regionComboBox.SelectedValue;
            hike.HikeType = (HikeType)typeComboBox.SelectedValue;
            hike.HikeDate = dateBox.Value.Date;
            hike.Description = descriptionBox.Text;
            hike.CPList = checkPointHandler.CPList;
            try
            {
                if (hikeDao.SaveHike(hike))
                {
                    MessageBox.Show("Sikeresen elmentve.");
                    Close();
                }
            }
            catch (DaoException ex)
            {
                if (ex.Error == ErrorType.NoDBConnection)
                {
                    MessageBox.Show("Nem lehet elérni az adatbázist.", "Hiba");
                    return;
                }
                if (ex.Error == ErrorType.DuplicateDate)
                {
                    MessageBox.Show("Ezzel a dátummal már van elmentve túra.", "Hiba");
                    return;
                }
                MessageBox.Show(ex.Message, "Hiba");
            }
        }

        private void descriptionBox_TextBoxFocusEnter(object sender, EventArgs e)
        {
            AcceptButton = null;
        }

        private void descriptionBox_TextBoxFocusLeave(object sender, EventArgs e)
        {
            AcceptButton = addHikeButton;
        }

        private void descriptionBox_Leave(object sender, EventArgs e)
        {
            descriptionBox.IsTextboxActive = false;
        }

        #endregion
    }
}
