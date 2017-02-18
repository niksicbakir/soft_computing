using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORIProjekat
{
    class Spil
    {
        public List<Karta> spil;

        public Spil()
        {
            spil = new List<Karta>();
        }

        public Spil(Spil sp)
        {
            spil = new List<Karta>();
            spil.AddRange(sp.spil);
        }

        public void inicijalizacijaSpila()
        {
            spil.Clear();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 2; j < 15; j++)
                {
                    Karta karta = new Karta(j, i);
                    spil.Add(karta);
                }
            }
        }

        public void promesajSpil()
        {
            int n = spil.Count;
            Random rng = new Random();
            while (n > 1)
            {
                System.Console.WriteLine(rng.Next(n+1));
                n--;
                int k = rng.Next(n + 1);
                Karta value = spil[k];
                spil[k] = spil[n];
                spil[n] = value;
            }
        }
    }
}
