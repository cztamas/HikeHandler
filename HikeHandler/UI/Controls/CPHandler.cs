using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private BindingList<NameAndID> cpNameList;
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
                if (cpNameList == null)
                {
                    return null;
                }
                List<int> cpList = new List<int>();
                foreach (NameAndID item in cpNameList)
                {
                    cpList.Add(item.ID);
                }
                return cpList;
            }
        }
        public string CPString
        {
            get
            {
                string cpString = string.Empty;
                foreach (NameAndID item in cpNameList)
                {
                    cpString += "." + item.ID + ".";
                }
                return cpString;
            }
        }

        public CPHandler()
        {
            InitializeComponent();
        }        

        public void Init(DAOManager manager, CPHandlerStyle style)
        {
            daoManager = manager;
            switch (style)
            {
                case CPHandlerStyle.Add:
                    anyOrderCheckBox.Visible = false;
                    anyOrderCheckBox.Enabled = false;
                    InitCPGridView();
                    break;
                case CPHandlerStyle.Search:
                    RefreshButton.Enabled = false;
                    RefreshButton.Visible = false;
                    InitCPGridView();
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

        private void InitCPGridView()
        {
            cpNameList = new BindingList<NameAndID>();
            BindingSource source = new BindingSource();
            source.DataSource = cpNameList;
            cpGridView.DataSource = source;

            cpGridView.Columns["ID"].Visible = false;
            cpGridView.Columns["Name"].HeaderText = "CheckPoint neve";
        }

        public void Clear()
        {
            cpNameList.Clear();
        }

        public void LoadCPs(List<int> cpIDList)
        {
            try
            {
                cpNameList = new BindingList<NameAndID>(daoManager.GetCPsFromList(cpIDList));
                BindingSource source = new BindingSource();
                source.DataSource = cpNameList;
                cpGridView.DataSource = source;

                cpGridView.Columns["ID"].Visible = false;
                cpGridView.Columns["Name"].HeaderText = "CheckPoint neve";
                RefreshCPList();
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
                List<NameAndID> list = daoManager.GetAllCPsOfRegion(regionID);
                cpNameComboBox.DataSource = list;
                cpNameComboBox.ValueMember = "ID";
                cpNameComboBox.DisplayMember = "Name";
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
        
        public void RefreshControl()
        {
            if (allRegionCheckBox.Checked)
            {
                GetCPList();
            }
            else
            {
                GetCPList(RegionID);
            }
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
            NameAndID item = ((List<NameAndID>)cpNameComboBox.DataSource)[cpNameComboBox.SelectedIndex];
            cpNameList.Add(item);
        }

        private void removeCPButton_Click(object sender, EventArgs e)
        {
            List<int> itemsToDelete = new List<int>();
            foreach (DataGridViewRow row in cpGridView.SelectedRows)
            {
                itemsToDelete.Add(row.Index);
            }
            foreach (int item in itemsToDelete)
            {
                cpNameList.RemoveAt(item);
            }
        }

        private void moveUpButton_Click(object sender, EventArgs e)
        {
            if (cpGridView.SelectedRows.Count != 1)
                return;
            int index = cpGridView.SelectedRows[0].Index;
            if (index == 0)
                return;
            NameAndID itemToMove = cpNameList[index];
            cpNameList.RemoveAt(index);
            cpNameList.Insert(index - 1, itemToMove);
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
            NameAndID itemToMove = cpNameList[index];
            cpNameList.RemoveAt(index);
            cpNameList.Insert(index + 1, itemToMove);
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
