using HikeHandler.Exceptions;
using HikeHandler.Interfaces;
using HikeHandler.ModelObjects;
using System;
using System.Windows.Forms;

namespace HikeHandler.UI
{
    public partial class BaseForm : Form
    {
        private IDAOManager daoManager;
        
        public BaseForm(IDAOManager manager)
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
            try
            {
                BaseFormSummary summary = daoManager.GetBaseFormSummary();
                hikeLabel.Text = summary.HikeCount.ToString();
                regionLabel.Text = summary.RegionCount.ToString();
                cpLabel.Text = summary.CPCount.ToString();
                countryLabel.Text = summary.CountryCount.ToString();
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
            Close();
        }

        private void searchHikeButton_Click(object sender, EventArgs e)
        {
            SearchHikeForm sHForm = new SearchHikeForm(daoManager);
            sHForm.Show();
        }

        private void searchCPButton_Click(object sender, EventArgs e)
        {
            SearchCPForm sCPForm = new SearchCPForm(daoManager);
            sCPForm.Show();
        }

        private void searchRegionButton_Click(object sender, EventArgs e)
        {
            SearchRegionForm sRForm = new SearchRegionForm(daoManager);
            sRForm.Show();
        }

        private void searchCountryButton_Click(object sender, EventArgs e)
        {
            SearchCountryForm sCForm = new SearchCountryForm(daoManager);
            sCForm.Show();
        }

        private void addHikeButton_Click(object sender, EventArgs e)
        {
            AddHikeForm aHForm = new AddHikeForm(daoManager);
            aHForm.Show();
        }

        private void addCPButton_Click(object sender, EventArgs e)
        {
            AddCPForm aCPForm = new AddCPForm(daoManager);
            aCPForm.Show();
        }

        private void addRegionButton_Click(object sender, EventArgs e)
        {
            AddRegionForm aRForm = new AddRegionForm(daoManager);
            aRForm.Show();
        }

        private void addCountryButton_Click(object sender, EventArgs e)
        {
            AddCountryForm addCountryForm = new AddCountryForm(daoManager);
            addCountryForm.Show();
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
