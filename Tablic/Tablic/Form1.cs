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
    public partial class Form1 : Form
    {
        public static int LAKO = 0;
        public static int TESKO = 1;

        private Spil spil;
        private Igrac igrac;
        private Igrac komp;
        private List<Karta> talon;
        private List<Karta> selektovaniNaTabli;
        private Dictionary<int, int> baceneKarte; //ne bacene karte
        private int zadnjiPokupio; // 0 komp, 1 igrac
        public Form1()
        {
            InitializeComponent();
            spil = new Spil();
            spil.inicijalizacijaSpila();
            igrac = new Igrac();
            komp = new Igrac();
            panel1.Tag = spil;
            selektovaniNaTabli = new List<Karta>();
            talon = new List<Karta>();
            baceneKarte = new Dictionary<int, int>();
            for (int i = 2; i < 15; i++)
            {
                baceneKarte.Add(i, 4);
            }

        }



        private void panel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel35.Enabled = false;
            button1.Enabled = false;
            spil.inicijalizacijaSpila();
            spil.promesajSpil();
            podeliKarteStart(true);
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
           
            AlgoritamTest algoritam = new AlgoritamTest(komp.karte, spil.spil, talon,nivox,this.baceneKarte,((radioButton1.Checked)?LAKO:TESKO));
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
            System.Console.WriteLine("Testiramo vidljive karte");
            foreach (KeyValuePair<int, int> x in baceneKarte)
            {
                System.Console.WriteLine(x.Value + "________" + x.Key);
            }
            System.Console.WriteLine("Testiramo vidljive karte");
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
       
        private void panel19_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataSet.getInstance();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
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

        private void panel13_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel30_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel25_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel21_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel16_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel17_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel23_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SoftAlgorithm softAlgorithm = new SoftAlgorithm();
            softAlgorithm.Processing("probaUgao.jpg");
        }
    }
}
