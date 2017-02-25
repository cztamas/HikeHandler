using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HikeHandler.ModelObjects;

namespace HikeHandler_UI.Controls
{
    public partial class OptionalMultiSelect : UserControl
    {
        private BindingList<NameAndID> optionsList;
        private BindingList<NameAndID> selectedItems;
        public List<NameAndID> Options
        {
            set
            {
                optionsList = new BindingList<NameAndID>(value);
                optionsComboBox.DataSource = optionsList;
            }
        }
        public List<int> SelectedIDs
        {
            get
            {
                return selectedItems.Select(item => item.ID).ToList();
            }
        }
        public string LabelName
        {
            set
            {
                nameLabel.Text = value;
            }
        }

        public OptionalMultiSelect()
        {
            InitializeComponent();
            optionsList = new BindingList<NameAndID>();
            selectedItems = new BindingList<NameAndID>();
            optionsComboBox.DataSource = optionsList;
            optionsComboBox.DisplayMember = "Name";
            selectedBox.DataSource = selectedItems;
            selectedBox.DisplayMember = "Name";
        }

        private void DisableMultiple()
        {
            addButton.Enabled = false;
            removeButton.Enabled = false;
            selectedBox.Enabled = false;
        }

        private void EnableMultiple()
        {
            addButton.Enabled = true;
            removeButton.Enabled = true;
            selectedBox.Enabled = true;
        }

        private void multipleCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (multipleCheckBox.Checked)
            {
                EnableMultiple();
            }
            else
            {
                DisableMultiple();
                selectedItems.Clear();
                try
                {
                    selectedItems.Add(optionsList[optionsComboBox.SelectedIndex]);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hiba: " + ex.Message, "Hiba");
                }
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                var newItem = optionsList[optionsComboBox.SelectedIndex];
                if (!selectedItems.Contains(newItem))
                {
                    selectedItems.Add(newItem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba: " + ex.Message, "Hiba");
            }
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            try
            {
                int index = selectedBox.SelectedIndex;
                if (index >= 0)
                {
                    selectedItems.RemoveAt(index);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba: " + ex.Message, "Hiba");
            }
        }

        private void optionsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!multipleCheckBox.Checked)
            {
                selectedItems.Clear();
                try
                {
                    selectedItems.Add(optionsList[optionsComboBox.SelectedIndex]);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hiba: " + ex.Message, "Hiba");
                }
            }
        }
    }
}
