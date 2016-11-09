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
    public partial class BaseForm : Form
    {
        private DAOManager daoManager;
        
        public BaseForm(DAOManager manager)
        {
            InitializeComponent();
            daoManager = manager;
        }

        private void BaseForm_Load(object sender, EventArgs e)
        {
            GetSummary();
        }
        
        private void GetSummary()
        {
            BaseFormSummary summary = daoManager.GetBaseFormSummary();
            if (summary == null)
            {
                Close();
            }
            hikeLabel.Text = summary.HikeCount.ToString();
            regionLabel.Text = summary.RegionCount.ToString();
            cpLabel.Text = summary.CPCount.ToString();
            countryLabel.Text = summary.CountryCount.ToString();
        }

        private void searchHikeButton_Click(object sender, EventArgs e)
        {
            SearchHikeForm sHForm = new SearchHikeForm(daoManager);
            sHForm.Open();
        }

        private void searchCPButton_Click(object sender, EventArgs e)
        {
            SearchCPForm sCPForm = new SearchCPForm(daoManager);
            sCPForm.Open();
        }

        private void searchRegionButton_Click(object sender, EventArgs e)
        {
            SearchRegionForm sRForm = new SearchRegionForm(daoManager);
            sRForm.Open();
        }

        private void searchCountryButton_Click(object sender, EventArgs e)
        {
            SearchCountryForm sCForm = new SearchCountryForm(daoManager);
            sCForm.Show();
        }

        private void addHikeButton_Click(object sender, EventArgs e)
        {
            AddHikeForm aHForm = new AddHikeForm(daoManager);
            aHForm.Open();
        }

        private void addCPButton_Click(object sender, EventArgs e)
        {
            AddCPForm aCPForm = new AddCPForm(daoManager);
            aCPForm.Open();
        }

        private void addRegionButton_Click(object sender, EventArgs e)
        {
            AddRegionForm aRForm = new AddRegionForm(daoManager);
            aRForm.Open();
        }

        private void addCountryButton_Click(object sender, EventArgs e)
        {
            AddCountryForm addCountryForm = new AddCountryForm(daoManager);
            addCountryForm.Open();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            GetSummary();
        }
        
    }
}
