namespace HikeHandler.ModelObjects
{
    public class LoginData
    {
        public string Username;
        public string Password;

        public LoginData(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
