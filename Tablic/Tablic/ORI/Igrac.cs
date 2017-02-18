using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORIProjekat
{
    class Igrac
    {
        public List<Karta> karte;//ruka
        public List<Karta> pokupljeneKarte;
        public int brojPunih = 0;
        public int brojUkupno = 0;
        public int brojTabli = 0;
        public Igrac()
        {
            karte = new List<Karta>();
            pokupljeneKarte= new List<Karta>();
        }

        public void dodajKartu(Karta karta)
        {
            karte.Add(karta);
            
        }

        public void izracunajSkor()
        {
            brojPunih = 0;
            brojUkupno = 0;
            foreach (Karta karta in pokupljeneKarte)
            {
                brojUkupno++;
                if (karta.broj > 9)
                {
                    brojPunih++;
                }
                if ((karta.broj == 2 && karta.znak == Karta.TREF) || (karta.broj == 10 && karta.znak == Karta.KARO))
                {
                    brojPunih++;
                }
            }
            
        }

        public void baciKartu(Karta karta)
        {
            karte.Remove(karta);
        }

        public void izbaciKartu(Karta karta)
        {
            for(int i=karte.Count-1;i>=0;i--)
            {
                if(karte[i].broj == karta.broj && karte[i].znak == karta.znak)
                {
                    karte.RemoveAt(i);
                    break;
                }
            }
        }
    }
}
