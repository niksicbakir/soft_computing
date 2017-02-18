using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORIProjekat
{
    public class Karta
    {
        public static int KARO = 0;
        public static int HERC = 1;
        public static int TREF = 2;
        public static int PIK = 3;

        public int broj;
        public int znak;

        public Karta(int broj,int znak)
        {
            this.broj = broj;
            this.znak = znak;
        }

        public Karta(string karta)//DataSet-img14_2.jpg
        {
            string[] s = karta.Split(new string[] { "img" }, StringSplitOptions.None);
            string[] st = s[1].Split(new string[] { ".jpg" }, StringSplitOptions.None);
            string[] str = st[0].Split(new string[] { "_" }, StringSplitOptions.None);
            this.broj = int.Parse(str[0]);
            int znakKarte = int.Parse(str[1]);
            switch (znakKarte)
            {
                case 1:
                {
                    this.znak = PIK;
                    break;
                }
                case 2:
                {
                    this.znak = TREF;
                    break;
                }
                case 3:
                {
                    this.znak = HERC;
                    break;
                }
                case 4:
                {
                    this.znak = KARO;
                    break;
                }
            }
            
        }
        public override string ToString()
        {
            return "BROJ "+broj + " ZNAK "+znak;
        }
        
    }
}
