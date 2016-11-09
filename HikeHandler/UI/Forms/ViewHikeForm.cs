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

namespace HikeHandler.UI
{
    public partial class ViewHikeForm : Form
    {
        //private int IDhike;
        //private List<int> cpList;
        private HikeForView hikeData;
        private MySqlConnection sqlConnection;

        private CountryDao countryDao;
        private CPDao cpDao;
        private RegionDao regionDao;
        private HikeDao hikeDao;
        
        public ViewHikeForm()
        {
            InitializeComponent();
        }

        public ViewHikeForm(MySqlConnection connection, int hikeID)
        {
            InitializeComponent();
            hikeDao = new HikeDao(connection);
            countryDao = new CountryDao(connection);
            cpDao = new CPDao(connection);
            regionDao = new RegionDao(connection);
            hikeData = new HikeForView(hikeID);
            sqlConnection = connection;
            GetHikeTypes();
            checkPointHandler.Init(sqlConnection, CPHandlerStyle.View);
            RefreshForm();
            MakeUneditable();
        }        

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
            hikeData = GetHikeData(hikeData.HikeID);
            if (hikeData == null)
                return;
            countryBox.Text = hikeData.CountryName;
            regionBox.Text = hikeData.RegionName;
            typeComboBox.SelectedValue = (int)hikeData.HikeType;
            dateBox.Value = hikeData.HikeDate;
            descriptionBox.Text = hikeData.Description;

            checkPointHandler.RegionID = hikeData.RegionID;
            checkPointHandler.LoadCPs(hikeData.CPString);
            checkPointHandler.RefreshControl();
            //cpList = checkPointHandler.CPList;

            if (hikeData.HikeType == HikeType.túra)
                Text = hikeData.Position.ToString() + ". túra adatai";
            if (hikeData.HikeType == HikeType.séta)
                Text = "Séta adatai";
            positionBox.Text = hikeData.Position.ToString();
        }

        private HikeForView GetHikeData(int hikeID)
        {
            HikeForSearch template = new HikeForSearch(hikeData.HikeID);
            try
            {
                DataTable resultTable = hikeDao.SearchHike(template, true);
                DataRow row = resultTable.Rows[0];
                HikeForView tempHike = new HikeForView(hikeData.HikeID);
                tempHike.CountryName = (string)row["countryname"];
                tempHike.RegionName = (string)row["regionname"];
                int regID;
                if (int.TryParse(row["idregion"].ToString(), out regID))
                    tempHike.RegionID = regID;
                int countryID;
                if (int.TryParse(row["idcountry"].ToString(), out countryID))
                    tempHike.CountryID = countryID;
                int posInt;
                if (int.TryParse(row["position"].ToString(), out posInt))
                    tempHike.Position = posInt;
                tempHike.HikeDate = Convert.ToDateTime(row["date"]);
                HikeType hikeType;
                Enum.TryParse<HikeType>((string)row["type"], out hikeType);
                tempHike.HikeType = hikeType;
                tempHike.Description = (string)row["description"];
                tempHike.CPString = (string)row["cpstring"];
                return tempHike;
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

        private void GetHikeTypes()
        {
            DataTable hikeTypesTable = new DataTable();
            DataColumn column;
            DataRow row;

            column = new DataColumn("id", typeof(int));
            hikeTypesTable.Columns.Add(column);
            column = new DataColumn("name", typeof(string));
            hikeTypesTable.Columns.Add(column);

            Array hikeTypes = Enum.GetValues(typeof(HikeType));
            foreach (HikeType item in hikeTypes)
            {
                row = hikeTypesTable.NewRow();
                row["id"] = (int)item;
                row["name"] = item.ToString();
                hikeTypesTable.Rows.Add(row);
            }
            typeComboBox.DataSource = hikeTypesTable;
            typeComboBox.ValueMember = "id";
            typeComboBox.DisplayMember = "name";
        }

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
            this.MakeUneditable();
            RefreshForm();
        }

        private void saveEditButton_Click(object sender, EventArgs e)
        {
            if (sqlConnection == null)
            {
                MessageBox.Show("Nem lehet elérni az adatbázist", "Hiba");
                return;
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                MessageBox.Show("Nem lehet elérni az adatbázist", "Hiba");
                return;
            }
            HikeForView newHikeData = new HikeForView(hikeData.HikeID);
            newHikeData.Description = descriptionBox.Text;
            newHikeData.HikeType = (HikeType)typeComboBox.SelectedValue;
            newHikeData.HikeDate = dateBox.Value;
            newHikeData.CPList = checkPointHandler.CPList;
            newHikeData.HikeDate = dateBox.Value;
            try
            {
                hikeDao.UpdateHike(newHikeData, hikeData);
                /*command.ExecuteNonQuery();
                if (dateChanged)
                {
                    hikeDao.MovePositions(hikeData.HikeDate, false);
                    hikeDao.UpdatePositions();
                }
                if (typeChanged)
                {
                    countryDao.UpdateHikeCount(hikeData.IDCountry);
                    countryDao.UpdateHikeCount(tempHike.IDCountry);
                    regionDao.UpdateHikeCount(hikeData.IDRegion);
                    regionDao.UpdateHikeCount(tempHike.IDRegion);
                    if (tempHike.HikeType == HikeType.túra && hikeData.HikeType != HikeType.túra)
                        hikeDao.UpdatePositions();
                    if (tempHike.HikeType != HikeType.túra && hikeData.HikeType == HikeType.túra)
                        hikeDao.MovePositions(hikeData.HikeDate, false);
                }
                foreach (int item in cpList)
                    cpDao.UpdateHikeCount(item);
                foreach (int item in checkPointHandler.CPList)
                    cpDao.UpdateHikeCount(item);*/
                RefreshForm();
                MakeUneditable();
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
                    MessageBox.Show("Ezzel a dátummal már van elmentve túra.", "Hiba");
                    return;
                }
                MessageBox.Show(ex.Message, "Hiba");
            }
        }

        private void deleteHikeButton_Click(object sender, EventArgs e)
        {
            string message = "Biztosan törli?";
            string caption = "Túra törlése";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, caption, buttons);
            if (result == DialogResult.No)
                return;

            try
            {
                if (hikeDao.DeleteHike(hikeData))
                {
                    MessageBox.Show("Törölve");
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
                MessageBox.Show(ex.Message, "Hiba");
            }
        }
    }
}
