using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace HikeHandler.Forms
{
    public partial class CPHandler : UserControl
    {
        private MySqlConnection sqlConnection;
        private DataTable cpTable;
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

        public void Init(MySqlConnection connection, CPHandlerStyle style)
        {
            sqlConnection = connection;
            InitCPTable();
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

        public void MakeEditable()
        {
            addCPButton.Enabled = true;
            removeCPButton.Enabled = true;
            moveDownButton.Enabled = true;
            moveUpButton.Enabled = true;
            cpNameComboBox.Enabled = true;
            allRegionCheckBox.Enabled = true;
            allRegionCheckBox.Visible = true;
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
        }

        public void Clear()
        {
            cpNameComboBox.DataSource = null;
            cpTable.Clear();
        }

        public void LoadCPs(string cpString)
        {
            InitCPTable();
            if (!cpString.IsCPString())
                return;
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
            char[] separator = new char[] { '.' };
            int id;
            string[] cpInts = cpString.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            foreach (string item in cpInts)
            {
                if (!int.TryParse(item, out id))
                    continue;
                string commandText = "SELECT name, idcp FROM cp WHERE idcp=" + id + ";";
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(commandText, sqlConnection))
                {
                    try
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        DataRow row = table.Rows[0];
                        cpTable.Rows.Add(row.ItemArray);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            RefreshCPList();
        }

        private List<int> GiveCPList()
        {
            cpTable.AcceptChanges();
            List<int> cpList = new List<int>();
            int item;
            foreach (DataRow row in cpTable.Rows)
            {
                if (!int.TryParse(row["idcp"].ToString(), out item))
                    continue;
                cpList.Add(item);
            }
            return cpList;
        }

        private void GetCPList()
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
            string commandText = "SELECT name, idcp FROM cp;";
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(commandText, sqlConnection))
            {
                try
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    cpNameComboBox.DataSource = table;
                    cpNameComboBox.ValueMember = "idcp";
                    cpNameComboBox.DisplayMember = "name";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Hiba");
                }
            }
        }

        private void GetCPList(int regionID)
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
            string commandText = "SELECT name, idcp FROM cp WHERE idregion=" + regionID + ";";
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(commandText, sqlConnection))
            {
                try
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    cpNameComboBox.DataSource = table;
                    cpNameComboBox.ValueMember = "idcp";
                    cpNameComboBox.DisplayMember = "name";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Hiba");
                }
            }
        }

        private void InitCPTable()
        {
            cpTable = new DataTable();
            cpTable.Clear();
            cpTable.Columns.Add("name");
            cpTable.Columns.Add("idcp");
            BindingSource source = new BindingSource();
            source.DataSource = cpTable;
            cpGridView.DataSource = source;
            cpGridView.Columns["idcp"].Visible = false;
            cpGridView.Columns["name"].HeaderText = "CheckPoint neve";
        }

        public void RefreshControl()
        {
            if (allRegionCheckBox.Checked)
            {
                GetCPList();
                return;
            }
            GetCPList(RegionID);
        }

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
            cpTable.Rows.Add(row.ItemArray);
        }

        private void removeCPButton_Click(object sender, EventArgs e)
        {
            List<DataRow> rowsToDelete = new List<DataRow>();
            foreach (DataGridViewRow row in cpGridView.SelectedRows)
            {
                int index = row.Index;
                rowsToDelete.Add(cpTable.Rows[index]);
            }
            foreach (DataRow row in rowsToDelete)
            {
                cpTable.Rows.Remove(row);
            }
        }

        private void moveUpButton_Click(object sender, EventArgs e)
        {
            if (cpGridView.SelectedRows.Count != 1)
                return;
            int index = cpGridView.SelectedRows[0].Index;
            if (index == 0)
                return;
            DataRow tempRow = cpTable.Rows[index];
            DataRow sameRow = cpTable.NewRow();
            sameRow.ItemArray = (object[])tempRow.ItemArray.Clone();
            cpTable.Rows.InsertAt(sameRow, index - 1);
            cpTable.Rows.Remove(tempRow);
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
            DataRow tempRow = cpTable.Rows[index];
            DataRow sameRow = cpTable.NewRow();
            sameRow.ItemArray = (object[])tempRow.ItemArray.Clone();
            cpTable.Rows.Remove(tempRow);
            cpTable.Rows.InsertAt(sameRow, index + 1);
            foreach (DataGridViewRow row in cpGridView.Rows)
                row.Selected = false;
            cpGridView.Rows[index + 1].Selected = true;
        }

        public string GetCPString()
        {
            string cpString = string.Empty;
            int cpID;
            foreach (DataRow row in cpTable.Rows)
            {
                if (!int.TryParse(row["idcp"].ToString(), out cpID))
                    continue;
                cpString += "." + cpID + ".";
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

        private void allRegionCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            RefreshCPList();
        }        
    }

    public enum CPHandlerStyle
    {
        Add, Search, View
    }
}
