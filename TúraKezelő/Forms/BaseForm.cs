using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HikeHandler.Forms;
using HikeHandler.Data_Containers;
using MySql.Data.MySqlClient;

namespace HikeHandler
{
    public partial class BaseForm : Form
    {
        public BaseForm()
        {
            InitializeComponent();
            EnableButtons(false);
            ConnectToDB();            
        }        
        
        private bool uiTestMode = false;
        private MySqlConnection sqlConnection;        

        private void CreateConnection(LoginData loginData)
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
        }

        private void searchHikeButton_Click(object sender, EventArgs e)
        {
            if (uiTestMode)
            {
                SearchHikeForm sHForm = new SearchHikeForm();
                sHForm.Open();
            }
            if (!uiTestMode)
            {
                SearchHikeForm sHForm = new SearchHikeForm(sqlConnection);
                sHForm.Open();
            }
        }

        private void searchCPButton_Click(object sender, EventArgs e)
        {
            if (uiTestMode)
            {
                SearchCPForm sCPForm = new SearchCPForm();
                sCPForm.Open();
            }
            if (!uiTestMode)
            {
                SearchCPForm sCPForm = new SearchCPForm(sqlConnection);
                sCPForm.Open();
            }            
        }

        private void searchRegionButton_Click(object sender, EventArgs e)
        {
            if (uiTestMode)
            {
                SearchRegionForm sRForm = new SearchRegionForm();
                sRForm.Open();
            }
            if (!uiTestMode)
            {
                SearchRegionForm sRForm = new SearchRegionForm(sqlConnection);
                sRForm.Open();
            }            
        }

        private void searchCountryButton_Click(object sender, EventArgs e)
        {
            if (uiTestMode)
            {
                SearchCountryForm sCForm = new SearchCountryForm();
                sCForm.Open();
            }
            if (!uiTestMode)
            {
                SearchCountryForm sCForm = new SearchCountryForm(sqlConnection);
                sCForm.Open();
            }            
        }

        private void addHikeButton_Click(object sender, EventArgs e)
        {
            if (uiTestMode)
            {
                AddHikeForm aHForm = new AddHikeForm();
                aHForm.Open();
            }
            if (!uiTestMode)
            {
                AddHikeForm aHForm = new AddHikeForm(sqlConnection);
                aHForm.Open();
            }            
        }

        private void addCPButton_Click(object sender, EventArgs e)
        {
            if (uiTestMode)
            {
                AddCPForm aCPForm = new AddCPForm();
                aCPForm.Open();
            }
            if (!uiTestMode)
            {
                AddCPForm aCPForm = new AddCPForm(sqlConnection);
                aCPForm.Open();
            }            
        }

        private void addRegionButton_Click(object sender, EventArgs e)
        {
            if (uiTestMode)
            {
                AddRegionForm aRForm = new AddRegionForm();
                aRForm.Open();
            }
            if (!uiTestMode)
            {
                AddRegionForm aRForm = new AddRegionForm(sqlConnection);
                aRForm.Open();
            }            
        }

        private void addCountryButton_Click(object sender, EventArgs e)
        {
            if (uiTestMode)
            {
                AddCountryForm aCForm = new AddCountryForm();
                aCForm.Open();
            }
            if (!uiTestMode)
            {
                AddCountryForm aCForm = new AddCountryForm(sqlConnection);
                aCForm.Open();
            }            
        }

        private void connectDBButton_Click(object sender, EventArgs e)
        {
            ConnectToDB();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
