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
    public partial class AddRegionForm : Form
    {
        private DAOManager daoManager;

        public AddRegionForm(DAOManager manager)
        {
            InitializeComponent();
            daoManager = manager;
        }

        private void AddRegionForm_Load(object sender, EventArgs e)
        {
            GetCountryList();
            countryComboBox.Text = string.Empty;
            nameBox.Focus();
        }

        private void GetCountryList()
        {
            DataTable table = daoManager.GetAllCountryNames();
            countryComboBox.DataSource = table;
            countryComboBox.ValueMember = "idcountry";
            countryComboBox.DisplayMember = "name";
        }

        private HikeRegionForSave GetDataForSave()
        {
            if (string.IsNullOrWhiteSpace(nameBox.Text))
            {
                MessageBox.Show("A tájegység neve nincs megadva.", "Hiba");
                nameBox.Focus();
                return null;
            }
            if (countryComboBox.SelectedValue == null)
            {
                MessageBox.Show("Nincs ország megadva.", "Hiba");
                countryComboBox.Focus();
                return null;
            }
            int index = 0;
            if (!int.TryParse(countryComboBox.SelectedValue.ToString(), out index) || index <= 0)
            {
                MessageBox.Show("Nem sikerült elmenteni a tájegységet.", "Hiba");
                return null;
            }
            HikeRegionForSave region = new HikeRegionForSave(index, nameBox.Text, descriptionBox.Text);
            return region;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveRegionButton_Click(object sender, EventArgs e)
        {
            HikeRegionForSave region = GetDataForSave();
            if (region == null)
            {
                return;
            }
            if (daoManager.SaveRegion(region))
            {
                MessageBox.Show("Sikeresen elmentve");
                Close();
            }
        }

        private void descriptionBox_Enter(object sender, EventArgs e)
        {
            AcceptButton = null;
        }

        private void descriptionBox_Leave(object sender, EventArgs e)
        {
            AcceptButton = saveRegionButton;
        }
    }
}
