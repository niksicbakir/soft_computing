using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace ORIProjekat
{
    public partial class Form2 : Form
    {
        public static int LAKO = 0;
        public static int TESKO = 1;
        public static int ZADNJI_IGRAC = 2;
        public static int ZADNJI_PROTIVNIK = 3;

        private Spil spil;
        private Igrac igrac;
        private Igrac komp;
        private List<Karta> talon;
        private List<Karta> selektovaniNaTabli;
        private Dictionary<int, int> baceneKarte; //ne bacene karte
        private int zadnjiPokupio; // 0 komp, 1 igrac
        private int zadnjiIgrao;
        private Karta bacenaKarta = null;

        private int redniBrojTalona;
        private int redniBrojRuke;
        private int redniBrojBacanja;
        private Dictionary<int, string> znakovi;

        private List<Panel> talonPanels;//30,29,25,26,21,22,16,18,17,19,23,24,27,28,31,32

        private List<Panel> zutePanel;//8,9,10,12,13,14

        private List<Panel> compPanel;//2,3,4,5,6,7


        public Form2()
        {
            InitializeComponent();

            zutePanel = new List<Panel>(6);
            zutePanel.Add(panel8);
            zutePanel.Add(panel9);
            zutePanel.Add(panel10);
            zutePanel.Add(panel12);
            zutePanel.Add(panel13);
            zutePanel.Add(panel14);

            redniBrojTalona = 1;
            redniBrojRuke = 1;
            redniBrojBacanja = 1;

            predlogLabel.Text = "";
            spil = new Spil();
            spil.inicijalizacijaSpila();
            igrac = new Igrac();
            komp = new Igrac();
            panel1.Tag = spil;
            selektovaniNaTabli = new List<Karta>();
            talon = new List<Karta>();
            baceneKarte = new Dictionary<int, int>();
            textBoxPathImageForTalon.Text = "jedna_ruka\\talon"+redniBrojTalona+".jpg";
            textBoxPathImagePlayer.Text = "jedna_ruka\\ruka"+redniBrojRuke+".jpg";
            textBoxIgracBaca.Text = "jedna_ruka\\bacenamoja"+redniBrojBacanja+".jpg";
            textBoxProtivnikBaca.Text = "jedna_ruka\\bacenaprotivnik"+redniBrojBacanja+".jpg";
            for (int i = 2; i < 15; i++)
            {
                baceneKarte.Add(i, 4);
            }
            znakovi = new Dictionary<int, string>();
            znakovi.Add(0, "KARO");
            znakovi.Add(1, "HERC");
            znakovi.Add(2, "TREF");
            znakovi.Add(3, "PIK");
            long usedMemory = GC.GetTotalMemory(true);
            Console.WriteLine(usedMemory);
            return;

        }
        //Start button
        private void button1_Click(object sender, EventArgs e)
        {
            //panel35.Enabled = false;
            button1.Enabled = false;

            button3.Enabled = true;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;

            //spil.inicijalizacijaSpila();
            //spil.promesajSpil();
            InicijalizacijaRukeITalona();
           // podeliKarteStart(true);
        }

        private void podeliKarteStart(Boolean podeliTalon)
        {
            Karta temp = null;
            if (podeliTalon)
            {
                temp = spil.spil.ElementAt(0);
                panel16.Tag = temp;
                panel16.BackgroundImage = imageList1.Images[(temp.broj - 1) + 13 * temp.znak];
                spil.spil.RemoveAt(0);
                panel16.Visible = true;
                talon.Add(temp);
                

                temp = spil.spil.ElementAt(0);
                panel17.Tag = temp;
                panel17.BackgroundImage = imageList1.Images[(temp.broj - 1) + 13 * temp.znak];
                spil.spil.RemoveAt(0);
                panel17.Visible = true;
                talon.Add(temp);

                temp = spil.spil.ElementAt(0);
                panel18.Tag = temp;
                panel18.BackgroundImage = imageList1.Images[(temp.broj - 1) + 13 * temp.znak];
                spil.spil.RemoveAt(0);
                panel18.Visible = true;
                talon.Add(temp);

                temp = spil.spil.ElementAt(0);
                panel19.Tag = temp;
                panel19.BackgroundImage = imageList1.Images[(temp.broj - 1) + 13 * temp.znak];
                spil.spil.RemoveAt(0);
                panel19.Visible = true;
                talon.Add(temp);

                foreach (Karta tempKarta in talon)
                {
                    if (baceneKarte.ContainsKey(tempKarta.broj))
                    {
                        baceneKarte[tempKarta.broj]--;
                        if (baceneKarte[tempKarta.broj] == 0)
                        {
                            baceneKarte.Remove(tempKarta.broj);
                        }
                    }
                }

            }
            //podela karata igracu

            temp = spil.spil.ElementAt(0);
            igrac.dodajKartu(temp);
            panel8.Tag = temp;//2
            panel8.BackgroundImage = imageList1.Images[(temp.broj - 1) + 13 * temp.znak];
            spil.spil.RemoveAt(0);
            panel8.Visible = true;

            temp = spil.spil.ElementAt(0);
            igrac.dodajKartu(temp);
            panel9.Tag = temp;//3
            panel9.BackgroundImage = imageList1.Images[(temp.broj - 1) + 13 * temp.znak];
            spil.spil.RemoveAt(0);
            panel9.Visible = true;

            temp = spil.spil.ElementAt(0);
            igrac.dodajKartu(temp);
            panel10.Tag = temp;//4
            panel10.BackgroundImage = imageList1.Images[(temp.broj - 1) + 13 * temp.znak];
            spil.spil.RemoveAt(0);
            panel10.Visible = true;

            temp = spil.spil.ElementAt(0);
            igrac.dodajKartu(temp);
            panel12.Tag = temp;//5
            panel12.BackgroundImage = imageList1.Images[(temp.broj - 1) + 13 * temp.znak];
            spil.spil.RemoveAt(0);
            panel12.Visible = true;

            temp = spil.spil.ElementAt(0);
            igrac.dodajKartu(temp);
            panel13.Tag = temp;//6
            panel13.BackgroundImage = imageList1.Images[(temp.broj - 1) + 13 * temp.znak];
            spil.spil.RemoveAt(0);
            panel13.Visible = true;

            temp = spil.spil.ElementAt(0);
            igrac.dodajKartu(temp);
            panel14.Tag = temp;//7
            panel14.BackgroundImage = imageList1.Images[(temp.broj - 1)+13*temp.znak];
            spil.spil.RemoveAt(0);
            panel14.Visible = true;

            //podela karata kompu

            komp.dodajKartu(spil.spil.ElementAt(0));
            panel2.Tag = spil.spil.ElementAt(0);
            panel2.BackgroundImage = imageList1.Images[53];
            spil.spil.RemoveAt(0);
            panel2.Visible = true;

            komp.dodajKartu(spil.spil.ElementAt(0));
            panel3.Tag = spil.spil.ElementAt(0);
            panel3.BackgroundImage = imageList1.Images[53];
            spil.spil.RemoveAt(0);
            panel3.Visible = true;

            komp.dodajKartu(spil.spil.ElementAt(0));
            panel4.Tag = spil.spil.ElementAt(0);
            panel4.BackgroundImage = imageList1.Images[53];
            spil.spil.RemoveAt(0);
            panel4.Visible = true;

            komp.dodajKartu(spil.spil.ElementAt(0));
            panel5.Tag = spil.spil.ElementAt(0);
            panel5.BackgroundImage = imageList1.Images[53];
            spil.spil.RemoveAt(0);
            panel5.Visible = true;

            komp.dodajKartu(spil.spil.ElementAt(0));
            panel6.Tag = spil.spil.ElementAt(0);
            panel6.BackgroundImage = imageList1.Images[53];
            spil.spil.RemoveAt(0);
            panel6.Visible = true;

            komp.dodajKartu(spil.spil.ElementAt(0));
            panel7.Tag = spil.spil.ElementAt(0);
            panel7.BackgroundImage = imageList1.Images[53];
            spil.spil.RemoveAt(0);
            panel7.Visible = true;

            System.Console.WriteLine("Komp ima karte");
            foreach (Karta k in komp.karte)
            {
                System.Console.WriteLine(k.broj + "_____" + k.znak);
                if (baceneKarte.ContainsKey(k.broj))
                {
                    baceneKarte[k.broj]--;
                    if (baceneKarte[k.broj] == 0)
                    {
                        baceneKarte.Remove(k.broj);
                    }
                }
            }
            System.Console.WriteLine("Komp ima karte");
            
        }

        private void panel21_Click(object sender, EventArgs e)
        {
            Panel panel = (Panel)sender;
            Console.WriteLine("Model karte je"+ ((Karta)panel.Tag).broj);
            if (panel.BorderStyle == BorderStyle.None)
            {
                panel.BorderStyle = BorderStyle.Fixed3D;
                selektovaniNaTabli.Add((Karta)panel.Tag);
            }
            else if(panel.BorderStyle == BorderStyle.Fixed3D)
            {
                panel.BorderStyle = BorderStyle.None;
                selektovaniNaTabli.Remove((Karta)panel.Tag);
            }
            
            
        }

        private void panel8_Click(object sender, EventArgs e)
        {
            Panel panel = (Panel)sender;
            int nivox=0;
            switch (komp.karte.Count)
            {
                case 6:
                    goto case 5;
                case 5:
                    goto case 4;
                case 4:
                    nivox = 8;
                    break;
                case 3:
                    nivox = 6;
                    break;
                case 2:
                    nivox = 4;
                    break;
                case 1:
                    nivox = 2;
                    break;
            }
           
            AlgoritamTest algoritam = new AlgoritamTest(komp.karte, spil.spil, talon,nivox,this.baceneKarte,TESKO);
            Console.WriteLine("Model karte je" + ((Karta)panel.Tag).broj);
            panel.Visible = false;
            igrac.baciKartu((Karta)panel.Tag);
            pokupiKartee((Karta)panel.Tag);
            selektovaniNaTabli.Clear();
            algoritam.WorkThreadFunction(komp.karte.Count);
            Karta kartaResenje = algoritam.resenje;
            pokupiKarteKomp(kartaResenje);
           // algoritam.izracunaj(komp.karte, spil.spil, talon);
            komp.baciKartu(kartaResenje);
             
          
            
            igrac.izracunajSkor();
            komp.izracunajSkor();
            label2.Text = "Poena: " + (igrac.brojPunih + igrac.brojTabli * 1 + ((igrac.brojUkupno > komp.brojUkupno) ? 3 : 0)).ToString() + " Punih: " + igrac.brojPunih.ToString() + " Ukupno: " + igrac.brojUkupno.ToString() + " Table: " + igrac.brojTabli;
            label1.Text = "Poena: " + (komp.brojPunih + komp.brojTabli * 1 + ((igrac.brojUkupno < komp.brojUkupno) ? 3 : 0)).ToString() + " Punih: " + komp.brojPunih.ToString() + " Ukupno: " + komp.brojUkupno.ToString() + " Table: " + komp.brojTabli;
            

            Console.WriteLine("Ode sa talona : ");
            if (komp.karte.Count == 0 && panel1.Visible == false)
            {
                foreach (Panel pan in panel11.Controls)
                {
                    if (pan.Visible)
                    {
                        pan.Visible = false;
                        Console.WriteLine(((Karta)pan.Tag).broj + "___" + ((Karta)pan.Tag).znak);
                        pan.Refresh();
                    }
                }
                if (zadnjiPokupio == 1)
                {
                    igrac.pokupljeneKarte.AddRange(talon);
                    Console.WriteLine("Dodate karte sa talona igracu!");
                }
                else if (zadnjiPokupio == 0)
                {
                    komp.pokupljeneKarte.AddRange(talon);
                    Console.WriteLine("Dodate karte sa talona kompu!");
                }
                
                igrac.izracunajSkor();
                komp.izracunajSkor();
                label2.Text = "Poena: " + (igrac.brojPunih + igrac.brojTabli * 1 + ((igrac.brojUkupno > komp.brojUkupno) ? 3 : 0)).ToString() + " Punih: " + igrac.brojPunih.ToString() + " Ukupno: " + igrac.brojUkupno.ToString() + " Table: " + igrac.brojTabli;
                label1.Text = "Poena: " + (komp.brojPunih + komp.brojTabli * 1 + ((igrac.brojUkupno < komp.brojUkupno) ? 3 : 0)).ToString() + " Punih: " + komp.brojPunih.ToString() + " Ukupno: " + komp.brojUkupno.ToString() + " Table: " + komp.brojTabli;
                string igracString = label2.Text;
                string kompString = label1.Text;
                string pobednik = "nereseno";
                if (igrac.brojPunih + igrac.brojTabli * 1 + ((igrac.brojUkupno > komp.brojUkupno) ? 3 : 0)  > komp.brojPunih + komp.brojTabli * 1 + ((igrac.brojUkupno < komp.brojUkupno) ? 3 : 0) )
                {
                    pobednik = "Igrac";
                }
                else if (komp.brojPunih + komp.brojTabli * 1 + ((igrac.brojUkupno < komp.brojUkupno) ? 3 : 0)  > igrac.brojPunih + igrac.brojTabli * 1 + ((igrac.brojUkupno > komp.brojUkupno) ? 3 : 0) )
                {
                    pobednik = "Racunar";
                }
                Console.WriteLine("poslednji kupio " + ((zadnjiPokupio == 1) ? "igrac" : "komp"));
                
                ProglasenjePobednika forma = new ProglasenjePobednika(igracString,kompString,pobednik);
                forma.ShowDialog();
            }
            //System.Console.WriteLine("Testiramo vidljive karte");
            foreach (KeyValuePair<int, int> x in baceneKarte)
            {
                System.Console.WriteLine(x.Value + "________" + x.Key);
            }
            //System.Console.WriteLine("Testiramo vidljive karte");
        }

        private void pokupiKartee(Karta karta)
        {
            Console.WriteLine("Na talonu pre je : " + talon.Count);
            int temp = 0;
            if (baceneKarte.ContainsKey(karta.broj))
            {
                baceneKarte[karta.broj]--;
                if (baceneKarte[karta.broj] == 0)
                {
                    baceneKarte.Remove(karta.broj);
                    
                }
            }
            foreach (Karta k in selektovaniNaTabli)
            {
                if (karta.broj < k.broj)
                {
                    temp = 0;
                    break;
                }
                temp += k.broj;
            }
            if (temp % karta.broj == 0 && temp!=0)
            {
                igrac.pokupljeneKarte.AddRange(selektovaniNaTabli);
                igrac.pokupljeneKarte.Add(karta);
                zadnjiPokupio = 1;
                foreach (Panel panel in panel11.Controls)
                {
                    if (selektovaniNaTabli.Contains((Karta)panel.Tag))
                    {
                        panel.Visible = false;
                        talon.Remove((Karta)panel.Tag);
                        panel.Refresh();
                        if (talon.Count == 0)
                        {
                            igrac.brojTabli++;
                        }
                    }
                }
            }
            else
            {
                foreach (Panel panel in panel11.Controls)
                {
                    panel.BorderStyle = BorderStyle.None;
                }
                foreach (Panel panel in panel11.Controls)
                {
                    panel.BorderStyle = BorderStyle.None;
                    if (panel.Visible == false)
                    {
                        panel.Tag = karta;
                        panel.Visible = true;
                        panel.BackgroundImage = imageList1.Images[(karta.broj - 1) + 13 * karta.znak];
                        talon.Add((Karta)panel.Tag);
                        panel.Refresh();
                        break;
                    }
                }
            }
            Console.WriteLine("Na talonu posle je : " + talon.Count);
        }
        
        private void pokupiKarteKomp(Karta karta)
        {
            foreach (Panel panel in panel33.Controls)
            {
                if (((Karta)panel.Tag).broj == karta.broj && ((Karta)panel.Tag).znak == karta.znak)
                {
            //        panel.Visible = false;
                    panel.BackgroundImage = imageList1.Images[(karta.broj - 1) + 13 * karta.znak];
                    panel.Refresh();
                 //   this.Refresh();
                 //   this.Validate();
                   
                   
                    
                }
            }
            bool pokupio = false;
            List<Karta> zaBrisanje = new List<Karta>();
            List<Karta> talonTemp = new List<Karta>();
            talonTemp.AddRange(talon);

            foreach (Karta krt in talonTemp)
            {
                if (karta.broj == krt.broj)
                {
                    foreach (Panel panel in panel11.Controls)
                    {
                        if (panel.Visible && ((Karta)panel.Tag).broj == karta.broj)
                        {
                            komp.pokupljeneKarte.Add((Karta)panel.Tag);
                            zadnjiPokupio = 0;
                            zaBrisanje.Add((Karta)panel.Tag);
                            //panel.Visible = false;
                            panel.BorderStyle = BorderStyle.Fixed3D;
                            pokupio = true;
                            panel.Refresh();
                            
                        }
                    }   
                }
                //koment
                List<Karta> tempRacunTalon = new List<Karta>();
                tempRacunTalon.AddRange(talon);
                foreach (Karta kar in talon)
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
                if (maxKey != -5)
                {
                    komp.pokupljeneKarte.AddRange(resenjaHash[maxKey]);
                    pokupio = true;
                    ukupnoB = resenjaHash[maxKey].Count;
                    zadnjiPokupio = 0;
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
                        talon.Remove(kartaZaBrisanje);
                        foreach (Panel panel in panel11.Controls)
                        {
                            if (panel.Visible && ((Karta)panel.Tag).broj == kartaZaBrisanje.broj && ((Karta)panel.Tag).znak == kartaZaBrisanje.znak)
                            {
                                zaBrisanje.Add((Karta)panel.Tag);
                                panel.BorderStyle = BorderStyle.Fixed3D;
                               // panel.Visible = false;
                                pokupio = true;
                                panel.Refresh();

                            }
                        }   

                    }



                }

                //koment
            }
           Thread.Sleep(2000);
            foreach (Panel panel in panel33.Controls)
            {
                if (((Karta)panel.Tag).broj == karta.broj && ((Karta)panel.Tag).znak == karta.znak)
                {
                           panel.Visible = false;
                           panel.Refresh();
                }
            }
            foreach (Karta kar in zaBrisanje)
            {
                
                foreach (Panel panel in panel11.Controls)
                {
                    if (panel.Visible == true && ((Karta)panel.Tag).broj == kar.broj && ((Karta)panel.Tag).znak == kar.znak)
                    {
                        panel.Visible = false;
                        System.Console.WriteLine("bakibrisetalon");
                        panel.Refresh();
                    }
                }
                talon.Remove(kar);
                if (talon.Count == 0)
                {
                    komp.brojTabli++;
                }
               

            }
            if (!pokupio)
            {
                foreach (Panel panel in panel11.Controls)
                {
                    panel.BorderStyle = BorderStyle.None;
                }
                foreach (Panel panel in panel11.Controls)
                {
                    panel.BorderStyle = BorderStyle.None;
                    if (panel.Visible == false)
                    {
                        panel.Tag = karta;
                        panel.Visible = true;
                        talon.Add((Karta)panel.Tag);
                        panel.BackgroundImage = imageList1.Images[(karta.broj - 1) + 13 * karta.znak];
                        panel.Refresh();
                        break;
                    }
                }
            }
            else
            {
                komp.pokupljeneKarte.Add(karta);
                zadnjiPokupio = 0;
            }
            Console.WriteLine("Na talonu posle je : " + talon.Count);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            DataSet.getInstance();
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            if (panel8.Visible == false && panel9.Visible == false && panel10.Visible == false && panel12.Visible == false && panel13.Visible == false && panel14.Visible == false)
            if (spil.spil.Count > 0)
            {
                podeliKarteStart(false);
                if (spil.spil.Count == 0)
                {
                    panel1.Visible = false;
                }
            }
        }
        //RESTART button
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
        //PREDLOG button
        private void button3_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = true;
            button7.Enabled = false;

            int nivox = 0;
            switch (igrac.karte.Count)
            {
                case 6:
                    goto case 5;
                case 5:
                    goto case 4;
                case 4:
                    nivox = 8;
                    break;
                case 3:
                    nivox = 6;
                    break;
                case 2:
                    nivox = 4;
                    break;
                case 1:
                    nivox = 2;
                    break;
            }

            AlgoritamTest algoritam = new AlgoritamTest(igrac.karte, spil.spil, talon, nivox, this.baceneKarte,  TESKO);
            algoritam.WorkThreadFunction(igrac.karte.Count);
            Karta kartaResenje = algoritam.resenje;
            algoritam = null;
            GC.Collect();
            //Console.Write("BACI KARTU >:" + kartaResenje.znak + " " + kartaResenje.broj);
            List<Karta> predlog = PokupiKartePredlog(kartaResenje);
            List<Karta> predlogZaKupljenje = new List<Karta>(); 
            foreach(Karta k in predlog)
            {
                bool exist = false;
                foreach(Karta kx in predlogZaKupljenje)
                {
                    if(kx.broj == k.broj && kx.znak == k.znak)
                    {
                        exist = true;
                    }
                }
                if(!exist)
                {
                    predlogZaKupljenje.Add(k);
                }
            }
            
            string predlogString = "["+predlogZaKupljenje.Count+"]PREDLOG: BACI KARTU " + znakovi[kartaResenje.znak] + " " + kartaResenje.broj;
            int x = 0;
            foreach (Karta karta in predlogZaKupljenje)
            {
               if(x++==0)
               {
                   predlogString += " I POKUPI SLEDECE KARTE : ";
               }
               predlogString += "znak : " + znakovi[karta.znak] + " broj : " + karta.broj +",";
            }
            predlogLabel.Text = predlogString;
            Console.WriteLine("NA TALONU SU SLEDECE KARTE");
            foreach(Karta karta in talon)
            {
                Console.WriteLine(karta);
            }
            
        }
        //TALON button
        private void button4_Click(object sender, EventArgs e)
        {
                if (zadnjiIgrao == ZADNJI_IGRAC)
                {
                    button5.Enabled = false;
                    button3.Enabled = false;
                    button4.Enabled = false;
                    button6.Enabled = false;
                    button7.Enabled = true;
                }else
                {
                    if (igrac.karte.Count == 0)
                    {
                        button5.Enabled = true;
                        button3.Enabled = false;
                        button4.Enabled = false;
                        button6.Enabled = false;
                        button7.Enabled = false;
                    }
                    else
                    {
                        button5.Enabled = false;
                        button3.Enabled = true;
                        button4.Enabled = false;
                        button6.Enabled = false;
                        button7.Enabled = false;
                    }
                }
            SoftAlgorithm softAlgoritham = new SoftAlgorithm();
            List<Karta> karte = softAlgoritham.Processing(textBoxPathImageForTalon.Text);
            softAlgoritham = null;
            //System.Console.WriteLine("Prepoznato je karata " + karte.Count);
            panel36.Visible = false;
            panel37.Visible = false;
            bool kupljeno = false;
            foreach (Panel panel in panel11.Controls)
            {
                if (panel.Visible)
                {
                    if (!IsExistInList((Karta)panel.Tag, karte))
                    {
                        talon.Remove((Karta)panel.Tag);
                        if (zadnjiIgrao == ZADNJI_IGRAC)
                        {
                            igrac.pokupljeneKarte.Add((Karta)panel.Tag);
                            kupljeno = true;
                        }
                        else if(zadnjiIgrao == ZADNJI_PROTIVNIK)
                        {
                            komp.pokupljeneKarte.Add((Karta)panel.Tag);
                            kupljeno = true;
                        }
                        panel.Visible = false;
                        panel.Refresh();
                    }
                }
            }
            if(kupljeno)
            {
                if (zadnjiIgrao == ZADNJI_IGRAC)
                {
                    igrac.pokupljeneKarte.Add(bacenaKarta);
                    zadnjiPokupio = 1;
                }
                else if (zadnjiIgrao == ZADNJI_PROTIVNIK)
                {
                    komp.pokupljeneKarte.Add(bacenaKarta);
                    zadnjiPokupio = 0;
                }
            }
            foreach (Karta k in karte)
            {
               // System.Console.WriteLine("Karta: oznaka karte je: " + k.broj + " znak je: " + k.znak);
                if (!IsExistInList(k, talon))
                {
                    foreach (Panel panel in panel11.Controls)
                    {
                        panel.BorderStyle = BorderStyle.None;
                        if (panel.Visible == false)
                        {
                            panel.Tag = k;
                            panel.Visible = true;
                            panel.BackgroundImage = imageList1.Images[(k.broj - 1) + 13 * k.znak];
                            talon.Add((Karta)panel.Tag);
                            panel.Refresh();
                            break;
                        }
                    }
                }
            }
            if(talon.Count == 0)
            {
                if (zadnjiIgrao == ZADNJI_IGRAC)
                {
                    igrac.brojTabli++;
                }
                else if (zadnjiIgrao == ZADNJI_PROTIVNIK)
                {
                    komp.brojTabli++;
                }
            }
            if(zadnjiIgrao == ZADNJI_PROTIVNIK && igrac.karte.Count == 0)
            {
                redniBrojRuke++;
                textBoxPathImagePlayer.Text = "jedna_ruka\\ruka" + redniBrojRuke + ".jpg";
            }
            //KRAJ
            if(redniBrojRuke == 5 && igrac.karte.Count == 0 && zadnjiIgrao == ZADNJI_PROTIVNIK)
            {
                if (zadnjiPokupio == 1)
                {
                    igrac.pokupljeneKarte.AddRange(talon);
                    Console.WriteLine("Dodate karte sa talona igracu!");
                }
                else if (zadnjiPokupio == 0)
                {
                    komp.pokupljeneKarte.AddRange(talon);
                    Console.WriteLine("Dodate karte sa talona kompu!");
                }

                igrac.izracunajSkor();
                komp.izracunajSkor();
                label2.Text = "Poena: " + (igrac.brojPunih + igrac.brojTabli * 1 + ((igrac.brojUkupno > komp.brojUkupno) ? 3 : 0)).ToString() + " Punih: " + igrac.brojPunih.ToString() + " Ukupno: " + igrac.brojUkupno.ToString() + " Table: " + igrac.brojTabli;
                label1.Text = "Poena: " + (komp.brojPunih + komp.brojTabli * 1 + ((igrac.brojUkupno < komp.brojUkupno) ? 3 : 0)).ToString() + " Punih: " + komp.brojPunih.ToString() + " Ukupno: " + komp.brojUkupno.ToString() + " Table: " + komp.brojTabli;
                string igracString = label2.Text;
                string kompString = label1.Text;
                string pobednik = "nereseno";
                if (igrac.brojPunih + igrac.brojTabli * 1 + ((igrac.brojUkupno > komp.brojUkupno) ? 3 : 0) > komp.brojPunih + komp.brojTabli * 1 + ((igrac.brojUkupno < komp.brojUkupno) ? 3 : 0))
                {
                    pobednik = "Igrac";
                }
                else if (komp.brojPunih + komp.brojTabli * 1 + ((igrac.brojUkupno < komp.brojUkupno) ? 3 : 0) > igrac.brojPunih + igrac.brojTabli * 1 + ((igrac.brojUkupno > komp.brojUkupno) ? 3 : 0))
                {
                    pobednik = "Racunar";
                }
                Console.WriteLine("poslednji kupio " + ((zadnjiPokupio == 1) ? "igrac" : "komp"));

                ProglasenjePobednika forma = new ProglasenjePobednika(igracString, kompString, pobednik);
                forma.ShowDialog();
            }
            igrac.izracunajSkor();
            komp.izracunajSkor();
            label2.Text = "Poena: " + (igrac.brojPunih + igrac.brojTabli * 1 + ((igrac.brojUkupno > komp.brojUkupno) ? 3 : 0)).ToString() + " Punih: " + igrac.brojPunih.ToString() + " Ukupno: " + igrac.brojUkupno.ToString() + " Table: " + igrac.brojTabli;
            label1.Text = "Poena: " + (komp.brojPunih + komp.brojTabli * 1 + ((igrac.brojUkupno < komp.brojUkupno) ? 3 : 0)).ToString() + " Punih: " + komp.brojPunih.ToString() + " Ukupno: " + komp.brojUkupno.ToString() + " Table: " + komp.brojTabli;
            label1.Visible = true;
            label2.Visible = true;
            IgracKartePrint();
        }
        //RUKA button
        private void button5_Click(object sender, EventArgs e)
        {
            SoftAlgorithm softAlgoritham = new SoftAlgorithm();
            List<Karta> karte = softAlgoritham.Processing(textBoxPathImagePlayer.Text);
            softAlgoritham = null;
            GC.Collect();
            foreach (Karta k in karte)
            {
               // System.Console.WriteLine("Karta: oznaka karte je: " + k.broj + " znak je: " + k.znak);
                if (!IsExistInList(k, talon))
                {
                    foreach (Panel panel in zutePanel)
                    {
                        panel.BorderStyle = BorderStyle.None;
                        if (panel.Visible == false)
                        {
                            panel.Tag = k;
                            panel.Visible = true;
                            panel.BackgroundImage = imageList1.Images[(k.broj - 1) + 13 * k.znak];
                            igrac.karte.Add((Karta)panel.Tag);
                            panel.Refresh();
                            break;
                        }
                    }
                }
            }
            foreach (Karta tempKarta in igrac.karte)
            {
                if (baceneKarte.ContainsKey(tempKarta.broj))
                {
                    baceneKarte[tempKarta.broj]--;
                    if (baceneKarte[tempKarta.broj] == 0)
                    {
                        baceneKarte.Remove(tempKarta.broj);
                    }
                }
            }

            
            button5.Enabled = false;
            button3.Enabled = true;
            button4.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
            
        }

        private bool IsExistInList(Karta k, List<Karta> lista)
        {
            foreach (Karta karta in lista)
            {
                if (karta.znak == k.znak && karta.broj == k.broj)
                {
                    return true;
                }
            }
            return false;
        }

        private List<Karta> PokupiKartePredlog(Karta karta)
        {
            List<Karta> zaBrisanje = new List<Karta>();
            List<Karta> talonTemp = new List<Karta>();
            talonTemp.AddRange(talon);
            List<Karta> retVal = new List<Karta>();
            foreach (Karta krt in talonTemp)
            {
                if (karta.broj == krt.broj)
                {
                    zaBrisanje.Add(krt);
                }
                //koment
                List<Karta> tempRacunTalon = new List<Karta>();
                tempRacunTalon.AddRange(talon);
                foreach (Karta kar in talon)
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
                if (maxKey != -5)
                {
                    foreach (Karta kartaZaBrisanje in resenjaHash[maxKey])
                    {
                        zaBrisanje.Add(kartaZaBrisanje);
                    }
                }
            }
            return zaBrisanje;
        }
        //IGRACEVA KARTA button
        private void button6_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;
            button4.Enabled = true;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;

            SoftAlgorithm softAlgoritham = new SoftAlgorithm();
            Karta karta = softAlgoritham.Processing(textBoxIgracBaca.Text)[0];
            bacenaKarta = karta;
            softAlgoritham = null;
            GC.Collect();
            panel36.Visible = true;
            panel36.BackgroundImage = imageList1.Images[(karta.broj - 1) + 13 * karta.znak];
            igrac.izbaciKartu(karta);
            foreach (Panel panel in zutePanel)
            {
                if (panel.Visible && ((Karta)panel.Tag).broj == karta.broj && ((Karta)panel.Tag).znak == karta.znak)
                {
                    panel.Visible = false;
                    panel.Refresh();
                    break;
                }
            }
            panel36.Refresh();
            zadnjiIgrao = ZADNJI_IGRAC;
            redniBrojTalona++;
            textBoxPathImageForTalon.Text = "jedna_ruka\\talon" + redniBrojTalona + ".jpg";
            //IgracKartePrint();
        }
        //PROTIVNIKOVA KARTA button
        private void button7_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;
            button4.Enabled = true;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;

            SoftAlgorithm softAlgoritham = new SoftAlgorithm();
            Karta karta = softAlgoritham.Processing(textBoxProtivnikBaca.Text)[0];
            bacenaKarta = karta;
            softAlgoritham = null;
            GC.Collect();
            panel37.Visible = true;
            panel37.BackgroundImage = imageList1.Images[(karta.broj - 1) + 13 * karta.znak];
            panel37.Refresh();
            zadnjiIgrao = ZADNJI_PROTIVNIK;
            if (baceneKarte.ContainsKey(karta.broj))
            {
                baceneKarte[karta.broj]--;
                if (baceneKarte[karta.broj] == 0)
                {
                    baceneKarte.Remove(karta.broj);
                }
            }
            
            redniBrojBacanja++;
            redniBrojTalona++;
            textBoxIgracBaca.Text = "jedna_ruka\\bacenamoja" + redniBrojBacanja + ".jpg";
            textBoxProtivnikBaca.Text = "jedna_ruka\\bacenaprotivnik" + redniBrojBacanja + ".jpg";
            textBoxPathImageForTalon.Text = "jedna_ruka\\talon" + redniBrojTalona + ".jpg";
        }

        private void InicijalizacijaRukeITalona()
        {
            SoftAlgorithm softAlgorithamM = new SoftAlgorithm();
            List<Karta> karteE = softAlgorithamM.Processing(textBoxPathImagePlayer.Text);
            softAlgorithamM = null;
            GC.Collect();
            foreach (Karta k in karteE)
            {
               // System.Console.WriteLine("Karta: oznaka karte je: " + k.broj + " znak je: " + k.znak);
                if (!IsExistInList(k, talon))
                {
                    foreach (Panel panel in zutePanel)
                    {
                        panel.BorderStyle = BorderStyle.None;
                        if (panel.Visible == false)
                        {
                            panel.Tag = k;
                            panel.Visible = true;
                            panel.BackgroundImage = imageList1.Images[(k.broj - 1) + 13 * k.znak];
                            igrac.karte.Add((Karta)panel.Tag);
                            panel.Refresh();
                            break;
                        }
                    }
                }
            }
            foreach (Karta tempKarta in igrac.karte)
            {
                if (baceneKarte.ContainsKey(tempKarta.broj))
                {
                    baceneKarte[tempKarta.broj]--;
                    if (baceneKarte[tempKarta.broj] == 0)
                    {
                        baceneKarte.Remove(tempKarta.broj);
                    }
                }
            }



            SoftAlgorithm softAlgoritham = new SoftAlgorithm();
            List<Karta> karte = softAlgoritham.Processing(textBoxPathImageForTalon.Text);
            softAlgoritham = null;
            GC.Collect();
            foreach (Panel panel in panel11.Controls)
            {
                if (panel.Visible)
                {
                    if (!IsExistInList((Karta)panel.Tag, karte))
                    {
                        talon.Remove((Karta)panel.Tag);
                        panel.Visible = false;
                        panel.Refresh();
                    }
                }
            }
            foreach (Karta k in karte)
            {
                //System.Console.WriteLine("Karta: oznaka karte je: " + k.broj + " znak je: " + k.znak);
                if (!IsExistInList(k, talon))
                {
                    foreach (Panel panel in panel11.Controls)
                    {
                        panel.BorderStyle = BorderStyle.None;
                        if (panel.Visible == false)
                        {
                            panel.Tag = k;
                            panel.Visible = true;
                            panel.BackgroundImage = imageList1.Images[(k.broj - 1) + 13 * k.znak];
                            talon.Add((Karta)panel.Tag);

                            panel.Refresh();
                            break;
                        }
                    }
                }
            }
            foreach (Karta tempKarta in talon)
            {
                if (baceneKarte.ContainsKey(tempKarta.broj))
                {
                    baceneKarte[tempKarta.broj]--;
                    if (baceneKarte[tempKarta.broj] == 0)
                    {
                        baceneKarte.Remove(tempKarta.broj);
                    }
                }
            }
            IgracKartePrint();
             
        }

        public void IgracKartePrint()
        {
            Console.WriteLine("------Igrac ima sledece karte:------");
            foreach(Karta k in igrac.karte)
            {
                Console.WriteLine(k);
            }
            Console.WriteLine("------------------------------------");

            Console.WriteLine("------Igrac je pokupio sledece karte:------");
            foreach (Karta k in igrac.pokupljeneKarte)
            {
                Console.WriteLine(k);
            }
            Console.WriteLine("Tabli: " + igrac.brojTabli);
            Console.WriteLine("------------------------------------");
            Console.WriteLine("------Protivnik je pokupio sledece karte:------");
            foreach (Karta k in komp.pokupljeneKarte)
            {
                Console.WriteLine(k);
            }
            Console.WriteLine("Tabli: " + komp.brojTabli);
            Console.WriteLine("------------------------------------");
        }

        private void panel8_Paint(object sender, EventArgs e)
        {

        }

        private void panel11_Paint(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, EventArgs e)
        {

        }

        private void panel13_Paint(object sender, EventArgs e)
        {

        }
    }
}
