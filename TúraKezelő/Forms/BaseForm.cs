using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TúraKezelő.Forms;
using TúraKezelő.Data_Containers;
using MySql.Data.MySqlClient;

namespace TúraKezelő
{
    public partial class BaseForm : Form
    {
        public BaseForm()
        {
            InitializeComponent();
            while (!isConnected && hasToRun)
            {
                GetLoginData();
                if (hasToRun)
                    ConnectToDB();
            }
        }
        
        private bool isConnected = false;
        private bool hasToRun = true;
        private LoginData loginData;
        private MySqlConnection sqlConnection;        

        private void ConnectToDB()
        {
            string connectionString = "server=localhost; database=test; uid=" + loginData.username + "; pwd=" + loginData.password + ";";
            sqlConnection = new MySqlConnection(connectionString);
            try
            {
                sqlConnection.Open();
                isConnected = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba: " + ex.Message);
            }
        }

        private void GetLoginData()
        {
            PasswordForm pwdForm = new PasswordForm();
            PasswordForm.LoginHandler handler = delegate (LoginData data)
            {
                loginData = data;
            };
            PasswordForm.VoidHandler cancelHandler = delegate ()
            {
                hasToRun = false;
            };
            pwdForm.LoginPerformed += handler;
            pwdForm.LoginCancelled += cancelHandler;
            pwdForm.ShowDialog();
        }

        private void searchHikeButton_Click(object sender, EventArgs e)
        {
            SearchHikeForm sHForm = new SearchHikeForm();
            sHForm.Show();
        }

        private void searchCPButton_Click(object sender, EventArgs e)
        {
            SearchCPForm sCPForm = new SearchCPForm();
            sCPForm.Show();
        }

        private void searchRegionButton_Click(object sender, EventArgs e)
        {
            SearchRegionForm sRForm = new SearchRegionForm();
            sRForm.Show();
        }

        private void searchCountryButton_Click(object sender, EventArgs e)
        {
            SearchCountryForm sCForm = new SearchCountryForm();
            sCForm.Show();
        }

        private void addHikeButton_Click(object sender, EventArgs e)
        {
            AddHikeForm aHForm = new AddHikeForm();
            aHForm.Show();
        }

        private void addCPButton_Click(object sender, EventArgs e)
        {
            AddCPForm aCPForm = new AddCPForm();
            aCPForm.Show();
        }

        private void addRegionButton_Click(object sender, EventArgs e)
        {
            AddRegionForm aRForm = new AddRegionForm();
            aRForm.Show();
        }

        private void addCountryButton_Click(object sender, EventArgs e)
        {
            AddCountryForm aCForm = new AddCountryForm();
            aCForm.Show();
        }
    }
}
