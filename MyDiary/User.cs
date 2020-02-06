using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDiary
{
    public class User
    {
        string mBenutzername { get; set; }
        string mPasswort { get; set; }
        string mEmail { get; set; }
        public User(string benutzername, string passwort, string email)
        {
            mBenutzername = benutzername;
            mPasswort = passwort;
            mEmail = email;
        }
    }
}
