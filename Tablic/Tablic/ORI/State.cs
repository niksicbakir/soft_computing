using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORIProjekat
{
    class State
    {
        public static int KOMP = 0;
        public static int IGRAC = 0;
        public static int MAX_NIVO = 9;

        public State parent;
        public List<Karta> karteURuci;
        public List<Karta> karteNaTalonu;
        public Dictionary<int,int> baceneKarte;//ne bacene karte
        public int brojPokupljenihPunih;
        public int brojPokupljenihUkupno;
        public int brojPokupljenihTabli;
        public int redniBrojThread;
        public int nivo;
        public int poslednjiIgrao;

        public State(List<Karta> karteURuci, List<Karta> karteNaTalonu, int nivo, Dictionary<int, int> baceneKarte, int brojPokupljenihPunih, int brojPokupljenihUkupno,int redniBrojThreda,int brojPokupljenihTabli, int poslednjiIgrao)
        {
            this.karteNaTalonu = new List<Karta>();
            this.karteURuci = new List<Karta>();
            this.karteNaTalonu.AddRange(karteNaTalonu);
            this.karteURuci.AddRange(karteURuci);
            this.baceneKarte = new Dictionary<int, int>();
            foreach (KeyValuePair<int, int> x in baceneKarte)
            {
                this.baceneKarte.Add(x.Key, x.Value);
            }
            this.nivo = nivo;
            this.brojPokupljenihPunih = brojPokupljenihPunih;
            this.brojPokupljenihUkupno = brojPokupljenihUkupno;
            this.redniBrojThread = redniBrojThreda;
            this.brojPokupljenihTabli = brojPokupljenihTabli;
            this.poslednjiIgrao = poslednjiIgrao;
        }

        public State(Karta karteURuci, List<Karta> karteNaTalonu, int nivo, Dictionary<int, int> baceneKarte, int brojPokupljenihPunih, int brojPokupljenihUkupno)
        {
            this.karteNaTalonu = new List<Karta>();
            this.karteURuci = new List<Karta>();
            this.karteNaTalonu.AddRange(karteNaTalonu);
            this.karteURuci.Add(karteURuci);
            this.baceneKarte = new Dictionary<int, int>();
            foreach (KeyValuePair<int, int> x in baceneKarte)
            {
                this.baceneKarte.Add(x.Key, x.Value);
            }
            this.nivo = nivo;
            this.brojPokupljenihPunih = brojPokupljenihPunih;
            this.brojPokupljenihUkupno = brojPokupljenihUkupno;
        }

        public List<State> children()
        {
            List<State> children = new List<State>();
            

            if (this.nivo%2 != 0)
            {
                int brojac = 0;
                foreach(Karta karta in karteURuci)
                {
                    
                    if (nivo == 1)
                    {
                        if (brojac++ != this.redniBrojThread)
                        {
                            continue;
                        }

                    }
                    
                    List<Karta> karteURuciTemp = new List<Karta>();
                    karteURuciTemp.AddRange(karteURuci);
                    karteURuciTemp.Remove(karta);
                    List<Karta> tempKarteNaTalonu = new List<Karta>();
                    tempKarteNaTalonu.AddRange(karteNaTalonu);
                    int pune = 0;
                    int ukupno = 0;
                    foreach (Karta kar in karteNaTalonu)
                    {
                        if (kar.broj == karta.broj)
                        {
                            tempKarteNaTalonu.Remove(kar);
                            if (kar.broj > 9 || (kar.broj == 2 && kar.znak == Karta.TREF))
                            {
                                pune++;
                                if (kar.broj == 10 && kar.znak == Karta.KARO)
                                {
                                    pune++;
                                }
                            }
                            ukupno++;
                            this.poslednjiIgrao = KOMP;
                        }
                    }
                    //koment
                    List<Karta> tempRacunTalon = new List<Karta>();
                    tempRacunTalon.AddRange(karteNaTalonu);
                    foreach (Karta kar in karteNaTalonu)
                    {
                        if (kar.broj >= karta.broj)
                        {
                            tempRacunTalon.Remove(kar);
                        }
                    }
                    Dictionary<int, List<Karta>> resenjaHash = new Dictionary<int, List<Karta>>();
                    if (tempRacunTalon.Count > 1)
                        for (int i = 0; i < tempRacunTalon.Count; i++)
                        {
                            if (tempRacunTalon.Count > 1)
                                for (int j = 0; j < tempRacunTalon.Count; j++)
                                {
                                    int sumaParova = 0;
                                    if (j != i)
                                    {
                                        sumaParova = tempRacunTalon[i].broj + tempRacunTalon[j].broj;
                                        if (sumaParova == karta.broj)
                                        {
                                            List<Karta> listaZaDodavanje = new List<Karta>();
                                            listaZaDodavanje.Add(tempRacunTalon[i]);
                                            listaZaDodavanje.Add(tempRacunTalon[j]);
                                            int punih = 0;
                                            if (tempRacunTalon[i].broj > 9 || (tempRacunTalon[i].broj == 2 && tempRacunTalon[i].znak == Karta.TREF))
                                            {
                                                punih++;
                                                if (tempRacunTalon[i].broj == 10 && tempRacunTalon[i].znak == Karta.KARO)
                                                {
                                                    punih++;
                                                }
                                            }
                                            if (tempRacunTalon[j].broj > 9 || (tempRacunTalon[j].broj == 2 && tempRacunTalon[j].znak == Karta.TREF))
                                            {
                                                punih++;
                                                if (tempRacunTalon[j].broj == 10 && tempRacunTalon[j].znak == Karta.KARO)
                                                {
                                                    punih++;
                                                }
                                            }
                                            if (!resenjaHash.ContainsKey(5 * punih + 2))
                                            {
                                                resenjaHash.Add(5 * punih + 2, listaZaDodavanje);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                    if (tempRacunTalon.Count > 2)
                                        for (int k = 0; k < tempRacunTalon.Count; k++)
                                        {
                                            int sumaTrojki = 0;
                                            if (k != i && k != j && i != j)
                                            {
                                                sumaTrojki = tempRacunTalon[i].broj + tempRacunTalon[j].broj + tempRacunTalon[k].broj;
                                                if (sumaTrojki == karta.broj)
                                                {
                                                    List<Karta> listaZaDodavanje = new List<Karta>();
                                                    listaZaDodavanje.Add(tempRacunTalon[i]);
                                                    listaZaDodavanje.Add(tempRacunTalon[j]);
                                                    listaZaDodavanje.Add(tempRacunTalon[k]);
                                                    int punih = 0;
                                                    if (tempRacunTalon[i].broj > 9 || (tempRacunTalon[i].broj == 2 && tempRacunTalon[i].znak == Karta.TREF))
                                                    {
                                                        punih++;
                                                        if (tempRacunTalon[i].broj == 10 && tempRacunTalon[i].znak == Karta.KARO)
                                                        {
                                                            punih++;
                                                        }
                                                    }
                                                    if (tempRacunTalon[j].broj > 9 || (tempRacunTalon[j].broj == 2 && tempRacunTalon[j].znak == Karta.TREF))
                                                    {
                                                        punih++;
                                                        if (tempRacunTalon[j].broj == 10 && tempRacunTalon[j].znak == Karta.KARO)
                                                        {
                                                            punih++;
                                                        }
                                                    }
                                                    if (tempRacunTalon[k].broj > 9 || (tempRacunTalon[k].broj == 2 && tempRacunTalon[k].znak == Karta.TREF))
                                                    {
                                                        punih++;
                                                        if (tempRacunTalon[k].broj == 10 && tempRacunTalon[k].znak == Karta.KARO)
                                                        {
                                                            punih++;
                                                        }
                                                    }
                                                    if (!resenjaHash.ContainsKey(5 * punih + 3))
                                                    {
                                                        resenjaHash.Add(5 * punih + 3, listaZaDodavanje);
                                                    }

                                                }
                                            }
                                            else
                                            {
                                                continue;
                                            }
                                            if (tempRacunTalon.Count > 3)
                                                for (int l = 0; l < tempRacunTalon.Count; l++)
                                                {
                                                    int sumaCetvorki = 0;
                                                    if (l != i && l != j && l != k && i != j && i != k && j != k)
                                                    {
                                                        sumaCetvorki = tempRacunTalon[i].broj + tempRacunTalon[j].broj + tempRacunTalon[k].broj + tempRacunTalon[l].broj;
                                                        int suma1 = tempRacunTalon[i].broj + tempRacunTalon[j].broj;
                                                        int suma2 = tempRacunTalon[i].broj + tempRacunTalon[k].broj;
                                                        int suma3 = tempRacunTalon[j].broj + tempRacunTalon[k].broj;
                                                        if (suma1 == karta.broj || suma2 == karta.broj || suma3 == karta.broj)
                                                        {
                                                            sumaCetvorki -= karta.broj;
                                                        }
                                                        if (sumaCetvorki == karta.broj)
                                                        {
                                                            List<Karta> listaZaDodavanje = new List<Karta>();
                                                            listaZaDodavanje.Add(tempRacunTalon[i]);
                                                            listaZaDodavanje.Add(tempRacunTalon[j]);
                                                            listaZaDodavanje.Add(tempRacunTalon[k]);
                                                            listaZaDodavanje.Add(tempRacunTalon[l]);
                                                            int punih = 0;
                                                            if (tempRacunTalon[i].broj > 9 || (tempRacunTalon[i].broj == 2 && tempRacunTalon[i].znak == Karta.TREF))
                                                            {
                                                                punih++;
                                                                if (tempRacunTalon[i].broj == 10 && tempRacunTalon[i].znak == Karta.KARO)
                                                                {
                                                                    punih++;
                                                                }
                                                            }
                                                            if (tempRacunTalon[j].broj > 9 || (tempRacunTalon[j].broj == 2 && tempRacunTalon[j].znak == Karta.TREF))
                                                            {
                                                                punih++;
                                                                if (tempRacunTalon[j].broj == 10 && tempRacunTalon[j].znak == Karta.KARO)
                                                                {
                                                                    punih++;
                                                                }
                                                            }
                                                            if (tempRacunTalon[k].broj > 9 || (tempRacunTalon[k].broj == 2 && tempRacunTalon[k].znak == Karta.TREF))
                                                            {
                                                                punih++;
                                                                if (tempRacunTalon[k].broj == 10 && tempRacunTalon[k].znak == Karta.KARO)
                                                                {
                                                                    punih++;
                                                                }
                                                            }
                                                            if (tempRacunTalon[l].broj > 9 || (tempRacunTalon[l].broj == 2 && tempRacunTalon[l].znak == Karta.TREF))
                                                            {
                                                                punih++;
                                                                if (tempRacunTalon[l].broj == 10 && tempRacunTalon[l].znak == Karta.KARO)
                                                                {
                                                                    punih++;
                                                                }
                                                            }
                                                            if (!resenjaHash.ContainsKey(5 * punih + 4))
                                                            {
                                                                resenjaHash.Add(5 * punih + 4, listaZaDodavanje);
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        continue;
                                                    }
                                                }
                                        }
                                }
                        }
                    int maxKey = -5;
                    foreach (KeyValuePair<int, List<Karta>> kartax in resenjaHash)
                    {
                        if (kartax.Key > maxKey)
                        {
                            maxKey = kartax.Key;
                        }
                    }
                    int ukupnoB = 0;
                    int punihB = 0;
                    int tempBrojPokupljenihTabli = 0;
                    if (maxKey != -5)
                    {
                        foreach (Karta kartaZaBrisanje in resenjaHash[maxKey])
                        {
                            if (kartaZaBrisanje.broj > 9 || (kartaZaBrisanje.broj == 2 && kartaZaBrisanje.znak == Karta.TREF))
                            {
                                punihB++;
                                if (kartaZaBrisanje.broj == 10 && kartaZaBrisanje.znak == Karta.KARO)
                                {
                                    punihB++;
                                }
                            }
                            ukupnoB++;
                            this.poslednjiIgrao = KOMP;
                            tempKarteNaTalonu.Remove(kartaZaBrisanje);
                            
                            if (tempKarteNaTalonu.Count == 0)
                            {
                                brojPokupljenihTabli++;
                            }
                        }
                        
                    }
                        
                    //koment
                    if (tempKarteNaTalonu.Count == karteNaTalonu.Count)
                    {
                        tempKarteNaTalonu.Add(karta);
                    }
                    if (pune > 0) pune++;
                    int igracPune = 0;
                    int igracUkupno = 0;
                    if (poslednjiIgrao == KOMP && baceneKarte.Count == 0)
                    {
                        foreach (Karta k in tempKarteNaTalonu)
                        {
                            if (k.broj > 9 || (k.broj == 2 && k.znak == Karta.TREF))
                            {
                                punihB++;
                                if (k.broj == 10 && k.znak == Karta.KARO)
                                {
                                    punihB++;
                                }
                            }
                            ukupnoB++;
                        }
                        tempKarteNaTalonu.Clear();
                    }
                    else if (poslednjiIgrao == IGRAC && baceneKarte.Count == 0)
                    {
                        foreach (Karta k in tempKarteNaTalonu)
                        {
                            if (k.broj > 9 || (k.broj == 2 && k.znak == Karta.TREF))
                            {
                                igracPune++;
                                if (k.broj == 10 && k.znak == Karta.KARO)
                                {
                                    igracPune++;
                                }
                            }
                            igracUkupno++;
                        }
                        tempKarteNaTalonu.Clear();
                    }

                    State state = new State(karteURuciTemp, tempKarteNaTalonu, this.nivo + 1, this.baceneKarte, (this.brojPokupljenihPunih + (pune + punihB - igracPune) * Math.Abs(MAX_NIVO - nivo)), this.brojPokupljenihUkupno + (ukupno + ukupnoB - igracUkupno) * Math.Abs(MAX_NIVO - nivo), this.redniBrojThread, this.brojPokupljenihTabli + tempBrojPokupljenihTabli * Math.Abs(MAX_NIVO - nivo), this.poslednjiIgrao);
                    state.parent = this;
                   
                    children.Add(state);
                    
                }
            }
            else
            {
                foreach (KeyValuePair<int, int> x in this.baceneKarte)
                {
                    Dictionary<int, int> tempBaceneKarte = new Dictionary<int, int>();
                    foreach (KeyValuePair<int, int> xx in this.baceneKarte)
                    {
                        tempBaceneKarte.Add(xx.Key, xx.Value);
                    }
                        
                    if (tempBaceneKarte[x.Key] > 0)
                        {
                            tempBaceneKarte[x.Key]--;
                            if (tempBaceneKarte[x.Key] == 0)
                            {
                                tempBaceneKarte.Remove(x.Key);
                            }
                        }

                        List<Karta> tempKarteNaTalonu = new List<Karta>();
                        tempKarteNaTalonu.AddRange(karteNaTalonu);
                    //    tempKarteNaTalonu.Add(new Karta(x.Key,x.Value));
                        int pune = 0;
                        int ukupno = 0;
                    
                        foreach (Karta kar in karteNaTalonu)
                        {
                            if (kar.broj == x.Key)
                            {
                                tempKarteNaTalonu.Remove(kar);
                                if (kar.broj > 9 || (kar.broj == 2 && kar.znak == Karta.TREF))
                                {
                                    pune++;
                                    if (kar.broj == 10 && kar.znak == Karta.KARO)
                                    {
                                        pune++;
                                    }
                                }
                                ukupno++;
                                poslednjiIgrao = IGRAC;
                            }
                            
                        }
                     
                        List<Karta> tempRacunTalon = new List<Karta>();
                        tempRacunTalon.AddRange(karteNaTalonu);
                        foreach (Karta kar in karteNaTalonu)
                        {
                            if (kar.broj >= x.Key)
                            {
                                tempRacunTalon.Remove(kar);
                            }
                        }
                        Dictionary<int, List<Karta>> resenjaHash= new Dictionary<int,List<Karta>>();
                        if (tempRacunTalon.Count > 1)
                            for (int i = 0; i < tempRacunTalon.Count; i++)
                            {
                                if (tempRacunTalon.Count > 1)
                                    for (int j = 0; j < tempRacunTalon.Count; j++)
                                    {
                                        int sumaParova = 0;
                                        if (j != i)
                                        {
                                            sumaParova = tempRacunTalon[i].broj + tempRacunTalon[j].broj;
                                            if (sumaParova == x.Key)
                                            {
                                                List<Karta> listaZaDodavanje = new List<Karta>();
                                                listaZaDodavanje.Add(tempRacunTalon[i]);
                                                listaZaDodavanje.Add(tempRacunTalon[j]);
                                                int punih = 0;
                                                if (tempRacunTalon[i].broj > 9 || (tempRacunTalon[i].broj == 2 && tempRacunTalon[i].znak == Karta.TREF))
                                                {
                                                    punih++;
                                                    if (tempRacunTalon[i].broj == 10 && tempRacunTalon[i].znak == Karta.KARO)
                                                    {
                                                        punih++;
                                                    }
                                                }
                                                if (tempRacunTalon[j].broj > 9 || (tempRacunTalon[j].broj == 2 && tempRacunTalon[j].znak == Karta.TREF))
                                                {
                                                    punih++;
                                                    if (tempRacunTalon[j].broj == 10 && tempRacunTalon[j].znak == Karta.KARO)
                                                    {
                                                        punih++;
                                                    }
                                                }
                                                if (!resenjaHash.ContainsKey(5 * punih + 2))
                                                {
                                                    resenjaHash.Add(5 * punih + 2, listaZaDodavanje);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            continue;
                                        }
                                        if (tempRacunTalon.Count > 2)
                                            for (int k = 0; k < tempRacunTalon.Count; k++)
                                            {
                                                int sumaTrojki = 0;
                                                if (k != i && k != j && i != j)
                                                {
                                                    sumaTrojki = tempRacunTalon[i].broj + tempRacunTalon[j].broj + tempRacunTalon[k].broj;
                                                    if (sumaTrojki == x.Key)
                                                    {
                                                        List<Karta> listaZaDodavanje = new List<Karta>();
                                                        listaZaDodavanje.Add(tempRacunTalon[i]);
                                                        listaZaDodavanje.Add(tempRacunTalon[j]);
                                                        listaZaDodavanje.Add(tempRacunTalon[k]);
                                                        int punih = 0;
                                                        if (tempRacunTalon[i].broj > 9 || (tempRacunTalon[i].broj == 2 && tempRacunTalon[i].znak == Karta.TREF))
                                                        {
                                                            punih++;
                                                            if (tempRacunTalon[i].broj == 10 && tempRacunTalon[i].znak == Karta.KARO)
                                                            {
                                                                punih++;
                                                            }
                                                        }
                                                        if (tempRacunTalon[j].broj > 9 || (tempRacunTalon[j].broj == 2 && tempRacunTalon[j].znak == Karta.TREF))
                                                        {
                                                            punih++;
                                                            if (tempRacunTalon[j].broj == 10 && tempRacunTalon[j].znak == Karta.KARO)
                                                            {
                                                                punih++;
                                                            }
                                                        }
                                                        if (tempRacunTalon[k].broj > 9 || (tempRacunTalon[k].broj == 2 && tempRacunTalon[k].znak == Karta.TREF))
                                                        {
                                                            punih++;
                                                            if (tempRacunTalon[k].broj == 10 && tempRacunTalon[k].znak == Karta.KARO)
                                                            {
                                                                punih++;
                                                            }
                                                        }
                                                        if (!resenjaHash.ContainsKey(5 * punih + 3))
                                                        {
                                                            resenjaHash.Add(5 * punih + 3, listaZaDodavanje);
                                                        }

                                                    }
                                                }
                                                else
                                                {
                                                    continue;
                                                }
                                                if (tempRacunTalon.Count > 3)
                                                    for (int l = 0; l < tempRacunTalon.Count; l++)
                                                    {
                                                        int sumaCetvorki = 0;
                                                        if (l != i && l != j && l != k && i != j && i != k && j != k)
                                                        {
                                                            sumaCetvorki = tempRacunTalon[i].broj + tempRacunTalon[j].broj + tempRacunTalon[k].broj + tempRacunTalon[l].broj;
                                                            sumaCetvorki = tempRacunTalon[i].broj + tempRacunTalon[j].broj + tempRacunTalon[k].broj + tempRacunTalon[l].broj;
                                                            int suma1 = tempRacunTalon[i].broj + tempRacunTalon[j].broj;
                                                            int suma2 = tempRacunTalon[i].broj + tempRacunTalon[k].broj;
                                                            int suma3 = tempRacunTalon[j].broj + tempRacunTalon[k].broj;
                                                            if (suma1 == x.Key || suma2 == x.Key || suma3 == x.Key)
                                                            {
                                                                sumaCetvorki -= x.Key;
                                                            }
                                                            if (sumaCetvorki == x.Key)
                                                            {
                                                                List<Karta> listaZaDodavanje = new List<Karta>();
                                                                listaZaDodavanje.Add(tempRacunTalon[i]);
                                                                listaZaDodavanje.Add(tempRacunTalon[j]);
                                                                listaZaDodavanje.Add(tempRacunTalon[k]);
                                                                listaZaDodavanje.Add(tempRacunTalon[l]);
                                                                int punih = 0;
                                                                if (tempRacunTalon[i].broj > 9 || (tempRacunTalon[i].broj == 2 && tempRacunTalon[i].znak == Karta.TREF))
                                                                {
                                                                    punih++;
                                                                    if (tempRacunTalon[i].broj == 10 && tempRacunTalon[i].znak == Karta.KARO)
                                                                    {
                                                                        punih++;
                                                                    }
                                                                }
                                                                if (tempRacunTalon[j].broj > 9 || (tempRacunTalon[j].broj == 2 && tempRacunTalon[j].znak == Karta.TREF))
                                                                {
                                                                    punih++;
                                                                    if (tempRacunTalon[j].broj == 10 && tempRacunTalon[j].znak == Karta.KARO)
                                                                    {
                                                                        punih++;
                                                                    }
                                                                }
                                                                if (tempRacunTalon[k].broj > 9 || (tempRacunTalon[k].broj == 2 && tempRacunTalon[k].znak == Karta.TREF))
                                                                {
                                                                    punih++;
                                                                    if (tempRacunTalon[k].broj == 10 && tempRacunTalon[k].znak == Karta.KARO)
                                                                    {
                                                                        punih++;
                                                                    }
                                                                }
                                                                if (tempRacunTalon[l].broj > 9 || (tempRacunTalon[l].broj == 2 && tempRacunTalon[l].znak == Karta.TREF))
                                                                {
                                                                    punih++;
                                                                    if (tempRacunTalon[l].broj == 10 && tempRacunTalon[l].znak == Karta.KARO)
                                                                    {
                                                                        punih++;
                                                                    }
                                                                }
                                                                if (!resenjaHash.ContainsKey(5 * punih + 4))
                                                                {
                                                                    resenjaHash.Add(5 * punih + 4, listaZaDodavanje);
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            continue;
                                                        }
                                                    }
                                            }
                                    }
                            }
                    int maxKey = -5;
                    foreach (KeyValuePair<int, List<Karta>> kartax in resenjaHash)
                    {
                        if (kartax.Key > maxKey)
                        {
                            maxKey = kartax.Key;
                        }
                    }
                    int temoBrojPokupljenihTabli = 0;
                    if (maxKey != -5)
                    {
                        tempKarteNaTalonu.AddRange(resenjaHash[maxKey]);
                        
                        foreach (Karta kartaZaBrisanje in resenjaHash[maxKey])
                        {

                            //tempKarteNaTalonu.Remove(kartaZaBrisanje);
                                if (kartaZaBrisanje.broj > 9 || (kartaZaBrisanje.broj == 2 && kartaZaBrisanje.znak == Karta.TREF))
                                {
                                    pune++;
                                    if (kartaZaBrisanje.broj == 10 && kartaZaBrisanje.znak == Karta.KARO)
                                    {
                                        pune++;
                                    }
                                }
                                ukupno++;
                                poslednjiIgrao = IGRAC;
                            
                            tempKarteNaTalonu.Remove(kartaZaBrisanje);
                            if (tempKarteNaTalonu.Count == 0)
                            {
                                temoBrojPokupljenihTabli++;
                            }
                        }
                        
                    }
                        
                        if (pune > 0) pune++;
                        if (tempKarteNaTalonu.Count == karteNaTalonu.Count)
                        {
                            tempKarteNaTalonu.Add(new Karta(x.Key,x.Value));
                        }

                        State state = new State(karteURuci, tempKarteNaTalonu, this.nivo + 1, tempBaceneKarte, this.brojPokupljenihPunih - pune, this.brojPokupljenihUkupno - ukupno, this.redniBrojThread, this.brojPokupljenihTabli - temoBrojPokupljenihTabli * (Math.Abs(MAX_NIVO - nivo)), this.poslednjiIgrao);
                    state.parent = this;
                    children.Add(state);
                }
            }
        
            return children;
        }

        public Double getHeuristika(int mod)
        {
            if (mod == Form1.TESKO)
            {
                return brojPokupljenihUkupno + brojPokupljenihPunih * 5 + brojPokupljenihTabli * 5;
            }
            else
            {
                return brojPokupljenihUkupno;
            }
        }

        public List<State> path()
        {
            List<State> path = new List<State>();
            State tt = this;
            while (tt != null)
            {
                path.Insert(0, tt);
                tt = tt.parent;
            }
            return path;
        }
    }
}
