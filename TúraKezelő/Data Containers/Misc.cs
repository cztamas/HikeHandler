using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TúraKezelő.Data_Containers
{
    public struct LoginData
    {
        public string username;
        public string password;

        public LoginData(string user, string pwd)
        {
            username = user;
            password = pwd;
        }
    }
}
