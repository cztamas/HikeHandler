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

namespace HikeHandler
{
    public partial class AddHikeForm : Form
    {
        public AddHikeForm()
        {
            InitializeComponent();
        }

        public AddHikeForm(MySqlConnection connection)
        {
            InitializeComponent();
            sqlConnection = connection;
            GetCountryList();
            GetHikeTypes();
        }

        private MySqlConnection sqlConnection;

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

        private void GetCPList(int countryID)
        { }

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
            string commandText = "SELECT idregion, name FROM region WHERE idcountry=" + countryID + ";";
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
        { }

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
            GetRegionList((int)countryComboBox.SelectedValue);
        }

        private void addHikeButton_Click(object sender, EventArgs e)
        {

        }
    }
}
