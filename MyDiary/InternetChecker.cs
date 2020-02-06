using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDiary
{
    class InternetChecker
    {
        public static bool IsInternetConnection()
        {
            System.Net.WebRequest req = System.Net.WebRequest.Create("https://www.google.de/");
            System.Net.WebResponse resp;
            try
            {
                resp = req.GetResponse();
                resp.Close();
                req = null;
                return true;
            }
            catch (Exception ex)
            {
                req = null;
                return false;
            }
        }
    }
}
