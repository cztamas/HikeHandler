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
    public partial class ViewRegionForm : Form
    {
        private MySqlConnection sqlConnection;
        private int regionID;

        public ViewRegionForm()
        {
            InitializeComponent();
        }

        public ViewRegionForm(MySqlConnection connection, int idRegion)
        {
            InitializeComponent();
            sqlConnection = connection;
            regionID = idRegion;
            RefreshForm();
            MakeUneditable();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MakeEditable()
        {
            editButton.Enabled = false;
            editButton.Visible = false;
            saveEditButton.Enabled = true;
            saveEditButton.Visible = true;
            cancelEditButton.Enabled = true;
            cancelEditButton.Visible = true;
            nameBox.Enabled = true;
            descriptionBox.Enabled = true;
        }

        private void MakeUneditable()
        {
            editButton.Enabled = true;
            editButton.Visible = true;
            saveEditButton.Enabled = false;
            saveEditButton.Visible = false;
            cancelEditButton.Enabled = false;
            cancelEditButton.Visible = false;
            nameBox.Enabled = false;
            descriptionBox.Enabled = false;
        }

        private void RefreshForm()
        {
            HikeRegion region = GetRegionData(regionID);
            if (region == null)
                return;
            nameBox.Text = region.Name;
            countryBox.Text = region.CountryName;
            hikeCountBox.Text = region.HikeCount.ToString();
            descriptionBox.Text = region.Description;
            Text = region.Name + " adatai";
        }

        public HikeRegion GetRegionData(int regionID)
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
            HikeRegionTemplate template = new HikeRegionTemplate(regionID);
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(template.SearchCommand(sqlConnection)))
            {
                try
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    DataRow row = table.Rows[0];
                    HikeRegion region = new HikeRegion();
                    region.ID = (int)row["id"];
                    region.Name = (string)row["name"];
                    region.HikeCount = (int)row["hikecount"];
                    region.Description = (string)row["description"];
                    region.CountryName = (string)row["countryname"];
                    return region;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Hiba");
                    return null;
                }
            }
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            MakeEditable();
        }

        private void cancelEditButton_Click(object sender, EventArgs e)
        {
            RefreshForm();
            MakeUneditable();
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            RefreshForm();
            MakeUneditable();
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
            HikeRegion region = new HikeRegion();
            region.ID = regionID;
            region.Name = nameBox.Text;
            region.Description = descriptionBox.Text;
            using (MySqlCommand command = region.UpdateCommand(sqlConnection))
            {
                try
                {
                    command.ExecuteNonQuery();
                    RefreshForm();
                    MakeUneditable();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Hiba");
                }
            }
        }
    }
}
