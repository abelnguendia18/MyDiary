using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDiary
{
    public class Kontakt
    {
       public string mVorname { get; set; }
       public string mNachname { get; set; }
       public string mTelefonnummer { get; set; }
       public string mEmailAdresse { get; set; }

        public Kontakt() { }

        public Kontakt(string vorname, string nachname, string telefonnummer, string emailAdresse)
        {
            mVorname = vorname;
            mNachname = nachname;
            mTelefonnummer = telefonnummer;
            mEmailAdresse = emailAdresse;
        }
    }
}
