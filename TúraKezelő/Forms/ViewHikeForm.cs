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
        {
            typeComboBox.Enabled = true;
            descriptionBox.Enabled = true;
            saveEditButton.Enabled = true;
            saveEditButton.Visible = true;
            cancelEditButton.Enabled = true;
            cancelEditButton.Visible = true;
            editButton.Enabled = false;
            editButton.Visible = false;
        }

        private void MakeUneditable()
        {
            typeComboBox.Enabled = false;
            descriptionBox.Enabled = false;
            saveEditButton.Enabled = false;
            saveEditButton.Visible = false;
            cancelEditButton.Enabled = false;
            cancelEditButton.Visible = false;
            editButton.Enabled = true;
            editButton.Visible = true;
        }

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
            HikeTemplate template = new HikeTemplate(IDhike);
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(template.SearchCommand(sqlConnection)))
            {
                try
                {
                    DataTable resultTable = new DataTable();
                    adapter.Fill(resultTable);
                    DataRow row = resultTable.Rows[0];
                    Hike hikeData = new Hike(IDhike);
                    hikeData.CountryName = (string)row["countryname"];
                    hikeData.RegionName = (string)row["regionname"];
                    hikeData.Position = (int)row["position"];
                    hikeData.HikeDate = Convert.ToDateTime((string)row["date"]);
                    HikeType hikeType;
                    Enum.TryParse<HikeType>((string)row["date"], out hikeType);
                    hikeData.HikeType = hikeType;
                    return hikeData;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Hiba");
                    return null;
                }
            }   
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
