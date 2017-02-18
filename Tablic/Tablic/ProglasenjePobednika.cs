using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ORIProjekat
{
    public partial class ProglasenjePobednika : Form
    {
        public ProglasenjePobednika(string igrac,string komp,string pobednik)
        {
            InitializeComponent();
            label2.Text = pobednik;
            label5.Text = igrac;
            label6.Text = komp;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
           // Application.Restart();
        }
    }
}
