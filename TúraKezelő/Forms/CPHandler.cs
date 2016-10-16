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

namespace TúraKezelő.Forms
{
    public partial class CPHandler : UserControl
    {
        private MySqlConnection sqlConnection;
        private DataTable cpTable;

        public CPHandler()
        {
            InitializeComponent();
        }

        public CPHandler(MySqlConnection connection) : this()
        {
            sqlConnection = connection;
            InitCPTable();
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
            GetCPList();
        }

        public void Region_Refreshed(object sender, EventArgs e)
        {
            if (allRegionCheckBox.Checked)
                return;
            ComboBox regionComboBox = sender as ComboBox;
            if (regionComboBox == null)
                return;
            int regionID;
            if (!int.TryParse(regionComboBox.SelectedValue.ToString(), out regionID))
                return;
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
            throw new NotImplementedException();
        }
    }
}
