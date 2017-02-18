using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORIProjekat
{
    class JednacinaPrave
    {
        public static int NORMAL = 0;
        public static int ONLY_X = 1;
        public static int ONLY_Y = 2;
        public static double SEPARATOR = 0.7;
        private double k;
        private double n;
        public double x1;
        public double x2;
        public double y1;
        public double y2;

        public int slucaj;

        public JednacinaPrave(double x1, double y1, double x2, double y2)
        {
            this.x1 = x1;
            this.x2 = x2;
            this.y1 = y1;
            this.y2 = y2;
            if ((y2 - y1) != 0 && (x2 - x1) != 0)
            {
                k = (y2 - y1) / (x2 - x1);
                n = k * (-x1) + y1;
                slucaj = NORMAL;
            }
            else if (x2 - x1 == 0)
            {
                k = 1000;
                n = x1;
                slucaj = ONLY_X;
                // k = (y2 - y1) / (x2 - this.x1);
            }
            else if (y2 - y1 == 0)
            {
                k = 0;
                n = y1;
                slucaj = ONLY_Y;
            }


        }

        public double GetAngle(JednacinaPrave jednacinaPrave)
        {
            double upStatement = jednacinaPrave.K - this.K;
            double bottomStatement = 1 + this.K * jednacinaPrave.K;
            double leftStatement = Math.Abs(upStatement / bottomStatement);
            return Math.Atan(leftStatement);
        }

        #region Properties
        public double K
        {
            get { return k; }
            set
            {
                if (k != value)
                {
                    k = value;
                }
            }
        }

        public double N
        {
            get { return n; }
            set
            {
                if (n != value)
                {
                    n = value;
                }
            }
        }

        public double PresekSaXOsom
        {
            get
            {
                if (slucaj == NORMAL)
                {
                    return -(N / K);
                }
                else
                {
                    return (x1 + x2) / 2;
                }

            }
        }

        public double PresekSaYOsom
        {
            get
            {
                if (slucaj == NORMAL)
                {
                    return N;
                }
                else
                {
                    return (y1 + y2) / 2;
                }
            }
        }

        #endregion
    }
}
