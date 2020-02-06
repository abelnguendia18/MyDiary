using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDiary
{
    class Veranstaltung
    {
        string titel;
        int h; // für Stunden
        int m; // für Minuten

        public Veranstaltung(string titel, int h, int m)
        {
            this.titel = titel;
            this.h = h;
            this.m = m;
        }
    }
}
