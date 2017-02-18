using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ORIProjekat
{
    class AlgoritamTest
    {
        public List<Karta> kompKarte;
        public List<Karta> spilKarte;//treba obrisati ali prvo treba prebaciti bacene karte
        public List<Karta> talonKarte;
        public Dictionary<int, int> baceneKarte;//ne bacene karte
        public int thredIndex;
        public List<double> heuristike;
        public Karta resenje;
        public int nivo;
        public int mod;

        public AlgoritamTest(List<Karta> kompKarte, List<Karta> spilKarte, List<Karta> talonKarte, int nivo,Dictionary<int,int> baceneKarte,int mod)
        {
            this.kompKarte = kompKarte;
            this.spilKarte = spilKarte;
            this.talonKarte = talonKarte;
            thredIndex = 0;
            this.nivo = nivo;
            this.baceneKarte = new Dictionary<int, int>();
            foreach (KeyValuePair<int, int> par in baceneKarte)
            {
                this.baceneKarte.Add(par.Key, par.Value);
            }
           /* foreach (Karta karta in spilKarte)
            {
                baceneKarte[karta.broj]++;
            }
            for (int i = 2; i < 15; i++)
            {
                if (baceneKarte[i] == 0)
                {
                    baceneKarte.Remove(i);
                }
            }
            */
                heuristike = new List<double>();
            heuristike.Add(-2);
            heuristike.Add(-2);
            heuristike.Add(-2);
            heuristike.Add(-2);
            heuristike.Add(-2);
            heuristike.Add(-2);
            this.mod = mod;
        }

        public void WorkThreadFunction(int brojTredova)
        {
            Thread thread0 = null;
            Thread thread1 = null;
            Thread thread2 = null;
            Thread thread3 = null;
            Thread thread4 = null;
            Thread thread5 = null;
            switch (brojTredova)
            {
                case 6:
                    thread0 = new Thread(new ThreadStart(izracunaj));
                    thread0.Start();
                    goto case 5;
                case 5:
                    thread1 = new Thread(new ThreadStart(izracunaj));
                    thread1.Start();
                    goto case 4;
                case 4:
                    thread2 = new Thread(new ThreadStart(izracunaj));
                    thread2.Start();
                    goto case 3;
                case 3:
                    thread3 = new Thread(new ThreadStart(izracunaj));
                    thread3.Start();
                    goto case 2;
                case 2:
                    thread4 = new Thread(new ThreadStart(izracunaj));
                    thread4.Start();
                    goto case 1;
                case 1:
                    thread5 = new Thread(new ThreadStart(izracunaj));
                    thread5.Start();
                    break;
            }
            if (thread0 != null)
            {
                thread0.Join();
            }
            if (thread1 != null)
            {
                thread1.Join();
            }
            if (thread2 != null)
            {
                thread2.Join();
            }
            if (thread3!= null)
            {
                thread3.Join();
            }
            if (thread4 != null)
            {
                thread4.Join();
            }
            if (thread5 != null)
            {
                thread5.Join();
            }

            Double max = heuristike[0];
            int broj = 0;
            Console.WriteLine("br tredova : " + brojTredova);
            for (int i = 0; i < brojTredova ; i++)
            {
                if (max < heuristike[i])
                {
                    broj = i;
                    max = heuristike[i];
                }
            }
            
            resenje = kompKarte[broj];
            System.Console.WriteLine("Karta koju treba komp da odigra je : " + resenje.broj + "i znak je " + resenje.znak);
           
        }

        public void izracunaj()
        {
            int redniBojTreda = thredIndex++;
            List<Karta> kompKarteTemp = new List<Karta>();
            kompKarteTemp.AddRange(kompKarte);
         //   kompKarteTemp.RemoveAt(redniBojTreda);
            Console.WriteLine("Jedan tred dobio sledece karte a tgredindex je " + redniBojTreda);
            foreach (Karta karta in kompKarteTemp)
            {
                Console.WriteLine(karta.broj + "__" + karta.znak);
            }
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            
            State start = new State(kompKarteTemp, talonKarte, 1,baceneKarte,0,0,redniBojTreda,0,-2);
            List<State> stablo = new List<State>();
            stablo.Add(start);
            int i = 0;
            while (true)
            {
                i++;
                State temp = stablo.ElementAt(0);
                if (temp.nivo == nivo) break;
                stablo.RemoveAt(0);
                stablo.AddRange(temp.children());
            }
            Double huristika = 0;
            foreach (State state in stablo)
            {
                huristika += state.getHeuristika(mod);
            }
            System.Console.WriteLine("I heuristika je " + huristika + "za kartu" + kompKarte[redniBojTreda].broj + "_____" + kompKarte[redniBojTreda].znak+"i broj treda je"+ redniBojTreda);
            heuristike[redniBojTreda] = huristika;
        }

    }
}
