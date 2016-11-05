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
using HikeHandler.DAOs;
using HikeHandler.Exceptions;
using HikeHandler.ServiceLayer;

namespace HikeHandler.UI
{
    public partial class AddCountryForm : Form
    {
        private DAOManager daoManager;
        private CountryDao countryDao;

        public AddCountryForm()
        {
            InitializeComponent();
        }

        public AddCountryForm(CountryDao countryDaoObject)
        {
            InitializeComponent();
            countryDao = countryDaoObject;
        }

        public AddCountryForm(DAOManager manager)
        {
            InitializeComponent();
            daoManager = manager;
        }

        public void Open()
        {
            Show();
            nameBox.Focus();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nameBox.Text))
            {
                MessageBox.Show("Nincs megadva az ország neve.", "Hiba");
                nameBox.Focus();
            }
            CountryForSave country = new CountryForSave(nameBox.Text, descriptionBox.Text);
            if (daoManager.SaveCountry(country))
                Close();

            CountryForView countryData = new CountryForView(nameBox.Text, descriptionBox.Text);
            try
            {
                countryDao.SaveCountry(countryData);
                MessageBox.Show("Sikeresen elmentve.");
                Close();
            }
            catch (DaoException ex)
            {
                switch (ex.Error)
                {
                    case ErrorType.NoDBConnection:
                        MessageBox.Show("Nincs kapcsolat az adatbázissal.", "Hiba");
                        break;
                    case ErrorType.DuplicateName:
                        MessageBox.Show("Már van elmentve ilyen nevű ország.", "Hiba");
                        nameBox.Focus();
                        break;
                    case ErrorType.DBError:
                        MessageBox.Show(ex.Message, "Hiba");
                        break;
                }
            }
        }

        private void descriptionBox_Enter(object sender, EventArgs e)
        {
            AcceptButton = null;
        }

        private void descriptionBox_Leave(object sender, EventArgs e)
        {
            AcceptButton = saveButton;
        }
    }
}
