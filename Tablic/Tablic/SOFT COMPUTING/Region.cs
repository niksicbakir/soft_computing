using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORIProjekat
{
    public class Region
    {
        private int poz1;
        private int poz2;
        private int poz3;
        private int poz4;

        public Region(string poz1, string poz2, string poz3, string poz4)
        {
            this.poz1 = Int32.Parse(poz1);
            this.poz2 = Int32.Parse(poz2);
            this.poz3 = Int32.Parse(poz3);
            this.poz4 = Int32.Parse(poz4);
        }

        public override string ToString()
        {
            return poz1 + " " + poz2 + " " + poz3 + " " + poz4;
        }

        #region Properties
        public int Poz1
        {
            get { return poz1; }
        }

        public int Poz2
        {
            get { return poz2; }
        }

        public int Poz3
        {
            get { return poz3; }
        }

        public int Poz4
        {
            get { return poz4; }
        }
        #endregion
    }
}
