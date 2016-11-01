﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using HikeHandler.Data_Containers;
using HikeHandler.DAOs;
using HikeHandler.Exceptions;

namespace HikeHandler.Forms
{
    public partial class AddHikeForm : Form
    {
        private MySqlConnection sqlConnection;
        private HikeDao hikeDao;
        private CountryDao countryDao;
        private CPDao cpDao;
        private RegionDao regionDao;

        public AddHikeForm()
        {
            InitializeComponent();
        }

        public AddHikeForm(MySqlConnection connection)
        {
            InitializeComponent();
            hikeDao = new HikeDao(connection);
            countryDao = new CountryDao(connection);
            cpDao = new CPDao(connection);
            regionDao = new RegionDao(connection);

            sqlConnection = connection;
            checkPointHandler.Init(sqlConnection, CPHandlerStyle.Add);
            regionComboBox.SelectedValueChanged += new EventHandler(checkPointHandler.Region_Refreshed);
            GetCountryList();
            GetHikeTypes();
        }
        
        private void GetCountryList()
        {
            if (sqlConnection == null)
            {
                MessageBox.Show("Nincs kapcsolat az adatbázissal.", "Hiba");
                return;
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                MessageBox.Show("Nincs kapcsolat az adatbázissal.", "Hiba");
                return;
            }
            string commandText = "SELECT idcountry, name FROM country ORDER BY name ASC;";
            DataTable table = new DataTable();
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(commandText, sqlConnection))
            {
                try
                {
                    adapter.Fill(table);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Hiba");
                }
            }
            countryComboBox.DataSource = table;
            countryComboBox.ValueMember = "idcountry";
            countryComboBox.DisplayMember = "name";
        }
        
        private void GetRegionList(int countryID)
        {
            if (sqlConnection == null)
            {
                MessageBox.Show("Nincs kapcsolat az adatbázissal.", "Hiba");
                return;
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                MessageBox.Show("Nincs kapcsolat az adatbázissal.", "Hiba");
                return;
            }
            string commandText = "SELECT idregion, name FROM region WHERE idcountry=" + countryID + " ORDER BY name ASC;";
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(commandText, sqlConnection))
            {
                try
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    regionComboBox.DataSource = table;
                    regionComboBox.ValueMember = "idregion";
                    regionComboBox.DisplayMember = "name";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Hiba");
                }
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
        
        public void Open()
        {
            Show();
        }
        
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
            if (sqlConnection == null)
            {
                MessageBox.Show("Nincs kapcsolat az adatbázissal.", "Hiba");
                return;
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                MessageBox.Show("Nincs kapcsolat az adatbázissal.", "Hiba");
                return;
            }
            Hike hike = new Hike();
            hike.IDCountry = (int)countryComboBox.SelectedValue;
            hike.IDRegion = (int)regionComboBox.SelectedValue;
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
    }
}
