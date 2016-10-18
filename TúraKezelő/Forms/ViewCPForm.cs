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
    public partial class ViewCPForm : Form
    {
        private MySqlConnection sqlConnection;
        private int idCP;

        public ViewCPForm()
        {
            InitializeComponent();
        }

        public ViewCPForm(MySqlConnection connection, int cpID)
        {
            InitializeComponent();
            sqlConnection = connection;
            idCP = cpID;
            RefreshForm();
            MakeUneditable();
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
            typeComboBox.Enabled = true;
            descriptionBox.Enabled = true;
            string actualCPType = typeComboBox.Text;
            GetCPTypes();
            typeComboBox.Text = actualCPType;
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
            typeComboBox.Enabled = false;
            descriptionBox.Enabled = false;
        }

        private void RefreshForm()
        {
            CP cpData = GetCPData(idCP);
            if (cpData == null)
                return;
            nameBox.Text = cpData.Name;
            regionBox.Text = cpData.RegionName;
            countryBox.Text = cpData.CountryName;
            hikeCountBox.Text = cpData.HikeCount.ToString();
            descriptionBox.Text = cpData.Description;
            typeComboBox.Text = cpData.TypeOfCP.ToString();
            Text = cpData.Name + " adatai";
        }

        private CP GetCPData(int cpID)
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
            CPTemplate template = new CPTemplate(cpID);
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(template.SearchCommand(sqlConnection)))
            {
                try
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    DataRow row = table.Rows[0];
                    CP resultCP = new CP();
                    resultCP.Name = (string)row["name"];
                    resultCP.CountryName = (string)row["name2"];
                    resultCP.RegionName = (string)row["name1"];
                    resultCP.TypeOfCP = (CPType)Enum.Parse(typeof(CPType), row["type"].ToString());
                    resultCP.HikeCount = (int)row["hikecount"];
                    resultCP.Description = (string)row["description"];
                    return resultCP;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Hiba");
                    return null;
                }
            }
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

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
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
            CP dataCP = new CP(idCP);
            dataCP.Name = nameBox.Text;
            dataCP.Description = descriptionBox.Text;
            if (typeComboBox.Text != string.Empty)
                dataCP.TypeOfCP = (CPType)Enum.Parse(typeof(CPType), typeComboBox.Text);
            using (MySqlCommand command = dataCP.UpdateCommand(sqlConnection))
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

        private void showHikesButton_Click(object sender, EventArgs e)
        {
            HikeTemplate template = new HikeTemplate();
            template.CPList.Add(idCP);
            SearchHikeForm searchHikeForm = new SearchHikeForm(sqlConnection, template);
            searchHikeForm.Show();
        }

        private void deleteCPbutton_Click(object sender, EventArgs e)
        {
            if (!CP.IsDeletable(idCP, sqlConnection)) 
            {
                MessageBox.Show("Csak olyan checkpoint törölhető, amihez nincs túra hozzárendelve.", "Hiba");
                return;
            }

            throw new NotImplementedException();
        }
    }
}
