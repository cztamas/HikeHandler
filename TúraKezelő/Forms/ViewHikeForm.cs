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
using HikeHandler.Data_Containers;

namespace HikeHandler.Forms
{
    public partial class ViewHikeForm : Form
    {
        private int IDhike;
        private MySqlConnection sqlConnection;

        public ViewHikeForm()
        {
            InitializeComponent();
        }

        public ViewHikeForm(MySqlConnection connection, int hikeID)
        {
            InitializeComponent();
            sqlConnection = connection;
            IDhike = hikeID;
            RefreshForm();
            MakeUneditable();
        }        

        private void MakeEditable()
        { }

        private void MakeUneditable()
        { }

        private void RefreshForm()
        {
            Hike hikeData = GetHikeData(IDhike);
            if (hikeData == null)
                return;
            countryBox.Text = hikeData.CountryName;
            regionBox.Text = hikeData.RegionName;
            typeComboBox.Text = hikeData.HikeType.ToString();
            dateBox.Value = hikeData.HikeDate;
            descriptionBox.Text = hikeData.Description;
            Text = hikeData.Position.ToString() + ". túra adatai";
            positionBox.Text = hikeData.Position.ToString();
        }

        private Hike GetHikeData(int hikeID)
        {
            if (sqlConnection == null)
            {
                MessageBox.Show("Nem lehet elérni az adatbázist", "Hiba");
                return null;
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                MessageBox.Show("Nem lehet elérni az adatbázist", "Hiba");
                return null;
            }



            return null;
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

        

    }
}
