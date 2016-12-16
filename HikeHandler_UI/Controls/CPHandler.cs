using HikeHandler.Exceptions;
using HikeHandler.Interfaces;
using HikeHandler.ModelObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace HikeHandler.UI
{
    public partial class CPHandler : UserControl
    {
        private IDAOManager daoManager;
        private BindingList<NameAndID> cpNameList;
        public int RegionID { get; set; }
        private int tempRegionID;
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
                    return new List<int>();
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
                if (cpNameList == null)
                {
                    return string.Empty;
                }
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

        public void Init(IDAOManager manager, CPHandlerStyle style)
        {
            daoManager = manager;
            GetRegions();
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
                    newCPButton.Enabled = false;
                    newCPButton.Visible = false;
                    InitCPGridView();
                    break;
                case CPHandlerStyle.View:
                    anyOrderCheckBox.Visible = false;
                    anyOrderCheckBox.Enabled = false;
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
            otherRegionCheckBox.Enabled = true;
            otherRegionCheckBox.Visible = true;
            RefreshButton.Enabled = true;
            newCPButton.Enabled = true;
        }

        public void MakeUneditable()
        {
            addCPButton.Enabled = false;
            removeCPButton.Enabled = false;
            moveDownButton.Enabled = false;
            moveUpButton.Enabled = false;
            cpNameComboBox.Enabled = false;
            otherRegionCheckBox.Enabled = false;
            otherRegionCheckBox.Visible = false;
            RefreshButton.Enabled = false;
            newCPButton.Enabled = false;
            regionComboBox.Enabled = false;
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
                RefreshControl();
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
        
        private void GetRegions()
        {
            try
            {
                List<NameAndID> list = daoManager.GetAllRegions();
                regionComboBox.DataSource = list;
                regionComboBox.ValueMember = "ID";
                regionComboBox.DisplayMember = "Name";
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

        public void RefreshControl()
        {
            if (!otherRegionCheckBox.Checked)
            {
                GetCPList(RegionID);
            }
            else
            {
                GetCPList(tempRegionID);
            }
        }
                
        #endregion

        #region Eventhandler Methods

        public void Region_Refreshed(object sender, EventArgs e)
        {
            ComboBox senderRegionBox = sender as ComboBox;
            if (senderRegionBox == null)
                return;
            int id;
            if (senderRegionBox.SelectedValue == null)
                return;
            if (!int.TryParse(senderRegionBox.SelectedValue.ToString(), out id))
                return;
            RegionID = id;
            if (otherRegionCheckBox.Checked)
            {
                return;
            }
            try
            {
                regionComboBox.SelectedValue = RegionID;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hiba");
            }
            cpNameComboBox.Text = string.Empty;
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
        
        private void otherRegionCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!otherRegionCheckBox.Checked)
            {
                try
                {
                    regionComboBox.SelectedValue = RegionID;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Hiba");
                }
                regionComboBox.Enabled = false;
                RefreshControl();
            }
            else
            {
                regionComboBox.Enabled = true;
            }
        }
        
        private void newCPButton_Click(object sender, EventArgs e)
        {
            AddCPForm addCPForm = new AddCPForm(daoManager);
            addCPForm.Show();
        }

        private void regionComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (regionComboBox.SelectedValue == null || !int.TryParse(regionComboBox.SelectedValue.ToString(), out tempRegionID))
            {
                return;
            }
            RefreshControl();
        }

        #endregion
    }

    public enum CPHandlerStyle
    {
        Add, Search, View
    }
}
