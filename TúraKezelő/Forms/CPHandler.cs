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
        private int regionID;

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
                    RefreshButton.Enabled = false;
                    RefreshButton.Visible = false;
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

        public void LoadCPs(string cpString)
        {
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
            DataTable table = new DataTable();
            string[] cpInts = cpString.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            foreach (string item in cpInts)
            {
                if (!int.TryParse(item, out id))
                    continue;
                string commandText = "SELECT idcp, name FROM cp WHERE idcp=" + id + ";";
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(commandText, sqlConnection))
                {
                    try
                    {                        
                        adapter.Fill(table);
                        DataRow row = table.Rows[0];
                        cpTable.Rows.Add(row);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
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
            string commandText = "SELECT idcp, name FROM cp;";
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
            string commandText = "SELECT idcp, name FROM cp WHERE idregion=" + regionID + ";";
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
            cpTable.Columns.Add("idcp");
            cpTable.Columns.Add("name");
            BindingSource source = new BindingSource();
            source.DataSource = cpTable;
            cpGridView.DataSource = source;
            cpGridView.Columns[0].Visible = false;
            cpGridView.Columns[1].HeaderText = "CheckPoint neve";
        }

        public void RefreshControl()
        {
            if (allRegionCheckBox.Checked)
            {
                GetCPList();
                return;
            }
            GetCPList(regionID);
        }

        public void Region_Refreshed(object sender, EventArgs e)
        {
            if (allRegionCheckBox.Checked)
                return;
            ComboBox regionComboBox = sender as ComboBox;
            if (regionComboBox == null)
                return;
            int id;
            if (!int.TryParse(regionComboBox.SelectedValue.ToString(), out id))
                return;
            regionID = id;
            GetCPList(regionID);
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            RefreshControl();
        }

        private void addCPButton_Click(object sender, EventArgs e)
        {
            if (cpNameComboBox.DataSource == null)
                return;
            DataRow row = ((DataTable)cpNameComboBox.DataSource).Rows[cpNameComboBox.SelectedIndex];
            cpTable.ImportRow(row);
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

        public string GetSearchCondition(string variable)
        {
            string condition = string.Empty;
            int idCP;
            if (anyOrderCheckBox.Checked)
            {
                foreach (DataRow row in cpTable.Rows)
                {
                    if (!int.TryParse(row["cpid"].ToString(), out idCP))
                        continue;
                    condition += " AND " + variable + " LIKE '%." + idCP + ".%'";
                }
                if (condition != string.Empty)
                    return "(" + condition + ")";
                return string.Empty;
            }
            foreach (DataRow row in cpTable.Rows)
            {
                if (!int.TryParse(row["cpid"].ToString(), out idCP))
                    continue;
                condition += "%." + idCP + ".%";
            }
            if (condition != string.Empty)
                return " AND " + variable + " LIKE '" + condition + "';";
            return string.Empty;            
        }

        private void allRegionCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (allRegionCheckBox.Checked)
            {
                GetCPList();
                return;
            }                
            if (!allRegionCheckBox.Checked)
            {
                if (regionID == 0)
                    return;
                GetCPList(regionID);
            }
        }
    }

    public enum CPHandlerStyle
    {
        Add, Search, View
    }
}
