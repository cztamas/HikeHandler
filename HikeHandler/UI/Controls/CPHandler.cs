using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using HikeHandler.ServiceLayer;
using HikeHandler.ModelObjects;
using HikeHandler.Exceptions;

namespace HikeHandler.UI
{
    public partial class CPHandler : UserControl
    {
        private DAOManager daoManager;
        private List<NameAndID> cpNameList;
        public int RegionID { get; set; }
        public bool AnyCPOrder
        {
            get
            {
                return anyOrderCheckBox.Checked;
            }
        }
        public List<int> CPList
        {
            get
            {
                return GiveCPList();
            }
        }

        public CPHandler()
        {
            InitializeComponent();
        }        

        public void Init(DAOManager manager, CPHandlerStyle style)
        {
            daoManager = manager;
            //InitCPTable();
            switch (style)
            {
                case CPHandlerStyle.Add:
                    anyOrderCheckBox.Visible = false;
                    anyOrderCheckBox.Enabled = false;
                    break;
                case CPHandlerStyle.Search:
                    RefreshButton.Enabled = false;
                    RefreshButton.Visible = false;
                    break;
                case CPHandlerStyle.View:
                    anyOrderCheckBox.Visible = false;
                    anyOrderCheckBox.Enabled = false;
                    //RefreshControl();
                    MakeUneditable();
                    break;
            }            
        }

        #region Auxiliary Methods

        public void MakeEditable()
        {
            addCPButton.Enabled = true;
            removeCPButton.Enabled = true;
            moveDownButton.Enabled = true;
            moveUpButton.Enabled = true;
            cpNameComboBox.Enabled = true;
            allRegionCheckBox.Enabled = true;
            allRegionCheckBox.Visible = true;
            RefreshButton.Enabled = true;
        }

        public void MakeUneditable()
        {
            addCPButton.Enabled = false;
            removeCPButton.Enabled = false;
            moveDownButton.Enabled = false;
            moveUpButton.Enabled = false;
            cpNameComboBox.Enabled = false;
            allRegionCheckBox.Enabled = false;
            allRegionCheckBox.Visible = false;
            RefreshButton.Enabled = false;
        }

        public void Clear()
        {
            cpNameComboBox.DataSource = null;
            cpNameList.Clear();
        }

        public void LoadCPs(List<int> cpIDList)
        {
            //InitCPTable();
            cpNameList = daoManager.GetCPsFromList(cpIDList);
            BindingSource source = new BindingSource();
            source.DataSource = cpNameList;
            cpGridView.DataSource = source;
            cpGridView.Columns["idcp"].Visible = false;
            cpGridView.Columns["name"].HeaderText = "CheckPoint neve";
            if (cpNameList == null)
            {
                return;
            }
            RefreshCPList();
        }

        private List<int> GiveCPList()
        {
            //cpNameList.AcceptChanges();
            List<int> cpList = new List<int>();
            foreach (NameAndID item in cpNameList)
            {
                cpList.Add(item.ID);
            }
            return cpList;
        }

        private void GetCPList()
        {
            try
            {
                List<NameAndID> cps = daoManager.GetAllCPs();
                cpNameComboBox.DataSource = cps;
                cpNameComboBox.ValueMember = "ID";
                cpNameComboBox.DisplayMember = "Name";
                return;
            }
            catch (NoDBConnectionException)
            {
                MessageBox.Show("Nincs kapcsolat az adatbázissal.", "Hiba");
            }
            catch (DBErrorException ex)
            {
                MessageBox.Show("Hiba az adatbázisban: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hiba");
            }
        }

        private void GetCPList(int regionID)
        {
            try
            {
                List<NameAndID> table = daoManager.GetAllCPsOfRegion(regionID);
                cpNameComboBox.DataSource = table;
                cpNameComboBox.ValueMember = "idcp";
                cpNameComboBox.DisplayMember = "name";
            }
            catch (NoDBConnectionException)
            {
                MessageBox.Show("Nincs kapcsolat az adatbázissal.", "Hiba");
            }
            catch (DBErrorException ex)
            {
                MessageBox.Show("Hiba az adatbázisban: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hiba");
            }
        }

        /*private void InitCPTable()
        {
            cpNameList = new DataTable();
            cpNameList.Clear();
            cpNameList.Columns.Add("name");
            cpNameList.Columns.Add("idcp");
            BindingSource source = new BindingSource();
            source.DataSource = cpNameList;
            cpGridView.DataSource = source;
            cpGridView.Columns["idcp"].Visible = false;
            cpGridView.Columns["name"].HeaderText = "CheckPoint neve";
        }*/

        public void RefreshControl()
        {
            if (allRegionCheckBox.Checked)
            {
                GetCPList();
                return;
            }
            GetCPList(RegionID);
        }

        private string GetCPString()
        {
            string cpString = string.Empty;
            foreach (NameAndID item in cpNameList)
            {
                cpString += "." + item.ID + ".";
            }
            return cpString;
        }

        private void RefreshCPList()
        {
            if (allRegionCheckBox.Checked)
            {
                GetCPList();
                return;
            }
            if (!allRegionCheckBox.Checked)
            {
                if (RegionID == 0)
                    return;
                GetCPList(RegionID);
            }
        }


        #endregion

        #region Eventhandler Methods

        public void Region_Refreshed(object sender, EventArgs e)
        {
            if (allRegionCheckBox.Checked)
                return;
            ComboBox regionComboBox = sender as ComboBox;
            if (regionComboBox == null)
                return;
            int id;
            if (regionComboBox.SelectedValue == null)
                return;
            if (!int.TryParse(regionComboBox.SelectedValue.ToString(), out id))
                return;
            RegionID = id;
            GetCPList(RegionID);
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            RefreshControl();
        }

        private void addCPButton_Click(object sender, EventArgs e)
        {
            if (cpNameComboBox.DataSource == null)
                return;
            if (cpNameComboBox.SelectedIndex == -1)
                return;
            DataRow row = ((DataTable)cpNameComboBox.DataSource).Rows[cpNameComboBox.SelectedIndex];
            cpNameList.Rows.Add(row.ItemArray);
        }

        private void removeCPButton_Click(object sender, EventArgs e)
        {
            List<DataRow> rowsToDelete = new List<DataRow>();
            foreach (DataGridViewRow row in cpGridView.SelectedRows)
            {
                int index = row.Index;
                rowsToDelete.Add(cpNameList.Rows[index]);
            }
            foreach (DataRow row in rowsToDelete)
            {
                cpNameList.Rows.Remove(row);
            }
        }

        private void moveUpButton_Click(object sender, EventArgs e)
        {
            if (cpGridView.SelectedRows.Count != 1)
                return;
            int index = cpGridView.SelectedRows[0].Index;
            if (index == 0)
                return;
            DataRow tempRow = cpNameList.Rows[index];
            DataRow sameRow = cpNameList.NewRow();
            sameRow.ItemArray = (object[])tempRow.ItemArray.Clone();
            cpNameList.Rows.InsertAt(sameRow, index - 1);
            cpNameList.Rows.Remove(tempRow);
            foreach (DataGridViewRow row in cpGridView.Rows)
                row.Selected = false;
            cpGridView.Rows[index - 1].Selected = true;
        }

        private void moveDownButton_Click(object sender, EventArgs e)
        {
            if (cpGridView.SelectedRows.Count != 1)
                return;
            int index = cpGridView.SelectedRows[0].Index;
            if (index == cpGridView.Rows.Count - 1)
                return;
            DataRow tempRow = cpNameList.Rows[index];
            DataRow sameRow = cpNameList.NewRow();
            sameRow.ItemArray = (object[])tempRow.ItemArray.Clone();
            cpNameList.Rows.Remove(tempRow);
            cpNameList.Rows.InsertAt(sameRow, index + 1);
            foreach (DataGridViewRow row in cpGridView.Rows)
                row.Selected = false;
            cpGridView.Rows[index + 1].Selected = true;
        }
        
        private void allRegionCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            RefreshCPList();
        }

        #endregion
    }

    public enum CPHandlerStyle
    {
        Add, Search, View
    }
}
