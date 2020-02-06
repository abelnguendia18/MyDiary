using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDiary
{
    class Termin
    {
        int terminId;
        string titel;
        string uhrzeit;
        int stunden;
        int minuten;
        public Termin(string titel, int stunden, int minuten)
        {
            this.titel = titel;
            this.stunden = stunden;
            this.minuten = minuten;
        }

        public bool isActive()
        {
            DateTime d = DateTime.Now;

            return d.Hour == stunden && d.Minute == minuten;
        }

        public virtual void klingeln()
        {

            System.Media.SoundPlayer player = new System.Media.SoundPlayer();
            player.SoundLocation = "C:/sound1.wav";
            player.Play();
            //System.Media.SystemSounds.Asterisk.Play();
        }


    }


}
