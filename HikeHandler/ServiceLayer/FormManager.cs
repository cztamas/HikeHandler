using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using HikeHandler.UI;

namespace HikeHandler.ServiceLayer
{
    public class FormManager
    {
        public BaseForm baseForm;

        public FormManager()
        {
            baseForm = new BaseForm();
        }

        private void ConnectToDB()
        { }
    }
}
