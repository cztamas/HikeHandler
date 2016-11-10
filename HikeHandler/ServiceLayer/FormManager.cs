using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using HikeHandler.UI;
using HikeHandler.ModelObjects;

namespace HikeHandler.ServiceLayer
{
    public class FormManager
    {
        private bool isConnected;
        private MySqlConnection sqlConnection;

        public FormManager()
        {
            isConnected = false;
        }

        public BaseForm ConnectToDB()
        {
            PasswordForm pwdForm = new PasswordForm();
            while (!isConnected)
            {
                if (pwdForm.ShowDialog() == DialogResult.Cancel)
                {
                    pwdForm.Dispose();
                    return null;
                }
                else
                {
                    isConnected = CreateConnection(pwdForm.loginData);
                }
            }
            pwdForm.Dispose();
            DAOManager manager = new DAOManager(sqlConnection);
            BaseForm baseForm = new BaseForm(manager);
            return baseForm;
        }

        private bool CreateConnection(LoginData loginData)
        {
            string connectionString = "server=localhost; database=hikehandler; uid=" + loginData.username + "; pwd=" + loginData.password + ";";
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                sqlConnection = connection;
                return true;
            }
            catch (Exception)
            {
                //MessageBox.Show(ex.Message, "Hiba");
                MessageBox.Show("A bejelentkezés sikertelen.");
                return false;
            }
        }
    }
}
