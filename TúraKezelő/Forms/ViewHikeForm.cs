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
        private List<int> cpList;
        private HikeType typeOfHike;
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
            GetHikeTypes();
            checkPointHandler.Init(sqlConnection, CPHandlerStyle.View);
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
            checkPointHandler.MakeEditable();
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
            checkPointHandler.MakeUneditable();
        }

        private void RefreshForm()
        {
            Hike hikeData = GetHikeData(IDhike);
            if (hikeData == null)
                return;
            countryBox.Text = hikeData.CountryName;
            regionBox.Text = hikeData.RegionName;
            typeComboBox.SelectedValue = (int)hikeData.HikeType;
            dateBox.Value = hikeData.HikeDate;
            descriptionBox.Text = hikeData.Description;

            checkPointHandler.RegionID = hikeData.IDRegion;
            checkPointHandler.LoadCPs(hikeData.CPString);
            checkPointHandler.RefreshControl();
            cpList = checkPointHandler.CPList;

            if (hikeData.HikeType == HikeType.túra)
                Text = hikeData.Position.ToString() + ". túra adatai";
            if (hikeData.HikeType == HikeType.séta)
                Text = "Séta adatai";
            positionBox.Text = hikeData.Position.ToString();
            typeOfHike = hikeData.HikeType;
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
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(template.SearchCommand(sqlConnection,true)))
            {
                try
                {
                    DataTable resultTable = new DataTable();
                    adapter.Fill(resultTable);
                    DataRow row = resultTable.Rows[0];
                    Hike hikeData = new Hike(IDhike);
                    hikeData.CountryName = (string)row["countryname"];
                    hikeData.RegionName = (string)row["regionname"];
                    int regID;
                    if (int.TryParse(row["idregion"].ToString(), out regID))
                        hikeData.IDRegion = regID;
                    int posInt;
                    if (int.TryParse(row["position"].ToString(), out posInt))
                        hikeData.Position = posInt;
                    hikeData.HikeDate = Convert.ToDateTime(row["date"]);
                    HikeType hikeType;
                    Enum.TryParse<HikeType>((string)row["type"], out hikeType);
                    hikeData.HikeType = hikeType;
                    hikeData.Description = (string)row["description"];
                    hikeData.CPString = (string)row["cpstring"];
                    return hikeData;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Hiba");
                    return null;
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
            Hike hikeData = new Hike(IDhike);
            hikeData.Description = descriptionBox.Text;
            hikeData.HikeType = (HikeType)typeComboBox.SelectedValue;
            hikeData.HikeDate = dateBox.Value;
            hikeData.CPList = checkPointHandler.CPList;
            using (MySqlCommand command = hikeData.UpdateCommand(sqlConnection))
            {
                try
                {
                    command.ExecuteNonQuery();
                    if (hikeData.HikeType == HikeType.túra && typeOfHike != HikeType.túra)
                        Hike.UpdatePositions(sqlConnection);

                    if (hikeData.HikeType != HikeType.túra && typeOfHike == HikeType.túra)
                        Hike.MovePositions(hikeData.HikeDate, sqlConnection, false);
                    foreach (int item in cpList)
                        CP.UpdateHikeCount(item, sqlConnection);
                    foreach (int item in checkPointHandler.CPList)
                        CP.UpdateHikeCount(item, sqlConnection);
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
