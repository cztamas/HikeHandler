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
            GetSummary();
        }

        /*private void CreateConnection(LoginData loginData)
        {
            string connectionString = "server=localhost; database=hikehandler; uid=" + loginData.username + "; pwd=" + loginData.password + ";";
            sqlConnection = new MySqlConnection(connectionString);
            try
            {
                sqlConnection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba: " + ex.Message);
            }
        }

        //if what=true, enables all button except connectDBButton
        //if what=false, disables all button except connectDBButton
        private void EnableButtons(bool what)
        {
            foreach (Control control in searchBox.Controls)
            {
                if (control is Button)
                    control.Enabled = what;
            }
            foreach (Control control in addBox.Controls)
            {
                if (control is Button)
                    control.Enabled = what;
            }
        }

        private void ConnectToDB()
        {
            PasswordForm pwdForm = new PasswordForm();
            PasswordForm.LoginHandler handler = delegate (LoginData data)
            {
                CreateConnection(data);
                if (sqlConnection.State==ConnectionState.Open)
                {
                    uiTestMode = false;
                    connectionStateLabel.Text = "Kapcsolódva";
                    connectDBButton.Enabled = false;
                    connectDBButton.Visible = false;
                    EnableButtons(true);
                    GetSummary();
                }
            };
            PasswordForm.VoidHandler cancelHandler = delegate ()
            {
                EnableButtons(false);
            };
            PasswordForm.VoidHandler testHandler = delegate ()
            {
                uiTestMode = true;
                EnableButtons(true);
            };
            pwdForm.LoginPerformed += handler;
            pwdForm.LoginCancelled += cancelHandler;
            pwdForm.TestModeSelected += testHandler;
            pwdForm.ShowDialog();
        }*/

        private void GetSummary2()
        {
            if (sqlConnection == null)
                return;
            if (sqlConnection.State != ConnectionState.Open)
                return;
            string result;
            int count;
            string commandText = "SELECT COUNT(*) FROM country;";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                try
                {
                    result = command.ExecuteScalar().ToString();
                    if (!int.TryParse(result, out count))
                        return;
                    countryLabel.Text = count.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Hiba");
                    return;
                }
            }
            commandText = "SELECT COUNT(*) FROM region;";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                try
                {
                    result = command.ExecuteScalar().ToString();
                    if (!int.TryParse(result, out count))
                        return;
                    regionLabel.Text = count.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Hiba");
                    return;
                }
            }
            commandText = "SELECT COUNT(*) FROM cp;";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                try
                {
                    result = command.ExecuteScalar().ToString();
                    if (!int.TryParse(result, out count))
                        return;
                    cpLabel.Text = count.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Hiba");
                    return;
                }
            }
            commandText = "SELECT COUNT(*) FROM hike;";
            using (MySqlCommand command = new MySqlCommand(commandText, sqlConnection))
            {
                try
                {
                    result = command.ExecuteScalar().ToString();
                    if (!int.TryParse(result, out count))
                        return;
                    hikeLabel.Text = count.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Hiba");
                    return;
                }
            }
        }

        private void GetSummary()
        {
            BaseFormSummary summary = daoManager.GetBaseFormSummary();
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
            sCForm.Open();
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
