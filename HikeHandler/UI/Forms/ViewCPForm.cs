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
    public partial class ViewCPForm : Form
    {
        private DAOManager daoManager;
        private CPForView currentCP;

        public ViewCPForm(DAOManager manager, int cpID)
        {
            InitializeComponent();
            daoManager = manager;
            currentCP = new CPForView(cpID);
        }

        private void ViewCPForm_Load(object sender, EventArgs e)
        {
            RefreshCPData();
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
            nameBox.Text = currentCP.Name;
            regionBox.Text = currentCP.RegionName;
            countryBox.Text = currentCP.CountryName;
            hikeCountBox.Text = currentCP.HikeCount.ToString();
            descriptionBox.Text = currentCP.Description;
            typeComboBox.Text = currentCP.TypeOfCP.ToString();
            Text = currentCP.Name + " adatai";
        }

        private void RefreshCPData()
        {
            CPForSearch template = new CPForSearch(cpID);
            try
            {
                DataTable table = cpDao.SearchCP(template);
                DataRow row = table.Rows[0];
                CPForView resultCP = new CPForView();
                resultCP.Name = (string)row["name"];
                resultCP.CountryName = (string)row["name2"];
                resultCP.RegionName = (string)row["name1"];
                resultCP.TypeOfCP = (CPType)Enum.Parse(typeof(CPType), row["type"].ToString());
                resultCP.HikeCount = (int)row["hikecount"];
                resultCP.Description = (string)row["description"];
                return resultCP;
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

        // NOT IMPLEMENTED
        private CPForUpdate GetDataForUpdate()
        {
            throw new NotImplementedException();
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
            CPForUpdate cp = GetDataForUpdate();
            if (cp == null)
                return;
            if (daoManager.UpdateCP(cp))
            {
                MakeUneditable();
                RefreshCPData();
                RefreshForm();
            }
        }

        private void showHikesButton_Click(object sender, EventArgs e)
        {
            HikeForSearch template = new HikeForSearch();
            template.CPList.Add(currentCP.CPID);
            SearchHikeForm searchHikeForm = new SearchHikeForm(daoManager, template);
            searchHikeForm.Show();
        }

        private void deleteCPbutton_Click(object sender, EventArgs e)
        {
            string message = "Biztosan törli?";
            string caption = "CheckPoint törlése";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, caption, buttons);
            if (result == DialogResult.No)
                return;
            try
            {
                if (cpDao.DeleteCP(idCP))
                {
                    MessageBox.Show("Törölve.");
                    Close();
                }
            }
            catch (DaoException ex)
            {
                if (ex.Error == ErrorType.NotDeletable)
                {
                    MessageBox.Show("Csak olyan checkpoint törölhető, amihez nincs túra hozzárendelve.", "Hiba");
                    return;
                }
                if (ex.Error == ErrorType.NoDBConnection)
                {
                    MessageBox.Show("Nincs kapcsolat az adatbázissal.", "Hiba");
                    return;
                }
                MessageBox.Show(ex.Message, "Hiba");
            }
        }

        
    }
}
