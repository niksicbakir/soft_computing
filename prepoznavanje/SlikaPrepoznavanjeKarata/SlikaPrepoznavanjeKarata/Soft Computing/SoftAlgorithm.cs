using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Emgu.CV;
using Emgu.CV.Structure;
using System.IO;
using System.Net;
using System.Drawing;
using System.Threading;
using SlikaPrepoznavanjeKarata.Model;

namespace SlikaPrepoznavanjeKarata.Soft_Computing
{
    public class SoftAlgorithm
    {
        private Image<Bgr, byte> img;
        private List<Image<Bgr, byte>> decomponatedImage;
        private double width;
        private double height;

        public SoftAlgorithm(double width, double height)
        {
            this.width = width;
            this.height = height;
            decomponatedImage = new List<Image<Bgr,byte>>();
        }

        public String Processing(string mainImagePath)
        {
            img = new Image<Bgr, Byte>(mainImagePath);//Ucitavanje slike
            img = img.Resize((int)width, (int)height, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);//Resize slike
            
            Image<Bgr,byte> imgColorFilter = FilterImageBGR(100, 100, 100, 255, 255, 255, img);//Filter Color
            
            Image<Gray, byte> imgGray = ToGrayImage(0.5, 0.5, 0, imgColorFilter);//Pretvara se u sivu sliku
            
            Image<Bgr,byte> retImg = null;
            List<Contour<Point>> contourList = FindContoursOfImg(imgGray, out retImg);//Izvlace se konture
            //retImg.Bitmap.Save("konturaAlg.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            Decomponate(img, contourList); // Dekompozicija slike po konturama

            Console.WriteLine("Isekao je: " + decomponatedImage.Count + "slika");

            List<Thread> threads = new List<Thread>();
            int duzina = decomponatedImage.Count;
            for (int i = 0; i < duzina; i++)
            {
                Console.WriteLine("napravljen tred sa i: " + i);
                Thread thread = new Thread(new ThreadStart(Recognize)); 
                threads.Add(thread);
                thread.Start();
            }

            for (int i = 0; i<threads.Count; i++)
            {
                threads[i].Join();
            }

            return "Prazan talon";
        }

        private void Recognize()
        {
            int threadNumber;
            Image<Bgr, byte> processingImage = null;
            lock(decomponatedImage)
            {
                processingImage =decomponatedImage[0];
                processingImage = processingImage.Resize((int)width, (int)height, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);
                //processingImage.Bitmap.Save("TestIsecena.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                threadNumber = decomponatedImage.Count;
                decomponatedImage.RemoveAt(0);
                
            }
            Image<Bgr, byte> imgColorFilter = FilterImageBGR(100, 100, 100, 255, 255, 255, processingImage);//Filter Color

            Image<Gray, byte> imgGray = ToGrayImage(0.5, 0.5, 0, imgColorFilter);//Pretvara se u sivu sliku
            //imgGray.Bitmap.Save("Gray" + threadNumber + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            Image<Bgr, byte> contourImg = null;
            List<Contour<Point>> contourList = FindContoursOfImg(imgGray,out contourImg);//Izvlace se konture
            //contourImg.Bitmap.Save("Con" + threadNumber + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            List<JednacinaPrave> jednacinePrava = HoughLines(new Image<Bgr, byte>(contourImg.Bitmap));//HoughLine transformacija

            List<Point> points = LinearEquations(jednacinePrava);// Tacke preseka pravi

            Image<Bgr, Byte> imgPerspective = Perspective(processingImage, points);// Ispravljanje slike
            //imgPerspective.Bitmap.Save("Per" + threadNumber + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            Image<Bgr, byte> imgPerspectiveColorFilter = FilterImageBGR(100, 100, 100, 255, 255, 255, imgPerspective);//Filter Color

            Image<Gray, byte> imgPerspectiveGray = ToGrayImage(0.5, 0.5, 0, imgPerspectiveColorFilter);//Pretvara se u sivu sliku

            Image<Gray, Byte> imgPerspectiveGrayThr = null;
            imgPerspectiveGrayThr = imgPerspectiveGray.ThresholdBinary(new Gray(254.0), new Gray(255.0)); //Threshold
            //imgPerspectiveGrayThr.Bitmap.Save("Thr" + threadNumber + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            Image<Gray, Byte> imgPerspectiveGrayThrDilate = null;
            imgPerspectiveGrayThrDilate = imgPerspectiveGrayThr.Dilate(1);// Dilatcija
            //imgPerspectiveGrayThrDilate = imgPerspectiveGrayThr.Dilate(1);// Dilatcija
            //imgPerspectiveGrayThrDilate = imgPerspectiveGrayThrDilate.Erode(1);
            //imgPerspectiveGrayThrDilate = imgPerspectiveGrayThrDilate.Erode(1);
            //imgPerspectiveGrayThrDilate.Bitmap.Save("Debuggg" + threadNumber + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            //return;
            KNNThreads knnThreads = new KNNThreads(imgPerspectiveGrayThrDilate);
            Console.WriteLine("THREAD " + threadNumber + "POGADJA SE DA JE U PITANJU KARTA " + knnThreads.Start());//KNN
            //Console.WriteLine("THREAD " + threadNumber + "POGADJA SE DA JE U PITANJU KARTA " + KNN(imgPerspectiveGrayThrDilate));//KNN
            //Console.WriteLine("Zavrsen tred");
        }

        private string KNN(Image<Gray, Byte> image)
        {
            Image<Gray, Byte> decissionImg = new Image<Gray, Byte>(image.Bitmap);
            DataSetImg maxMatchedImg = null; //= DataSet.getInstance().DataSetCards[0];
            double matchRaiting = -1;
            foreach (DataSetImg dataSetImg in DataSet.getInstance().DataSetCards)
            {
                int numberOfWhitePixels = 0;
                int numberOfMatchedWhitePixels = 0;

                for (int i = 0; i < dataSetImg.Image.Width; i++)
                {
                    for (int j = 0; j < dataSetImg.Image.Height; j++)
                    {
                        if (dataSetImg.Image[j, i].Intensity == 255)
                        {
                            numberOfWhitePixels++;
                        }
                        if (decissionImg[j, i].Intensity == 255)
                        {
                            if (decissionImg[j, i].Intensity == dataSetImg.Image[j, i].Intensity)
                            {
                                numberOfMatchedWhitePixels++;
                            }
                        }
                    }
                }
               // Console.WriteLine("Karta sa imenom " + dataSetImg.PathName + " ima odnos = " + ((double)numberOfMatchedWhitePixels) / numberOfWhitePixels);
                if (((double)numberOfMatchedWhitePixels) / numberOfWhitePixels > matchRaiting)
                {
                    matchRaiting = ((double)numberOfMatchedWhitePixels) / numberOfWhitePixels;
                    maxMatchedImg = dataSetImg;
                }
            }
            return maxMatchedImg.PathName;
        }

        private Image<Bgr, Byte> Perspective(Image<Bgr, Byte> image, List<Point> points)
        {
            Dictionary<int, List<Point>> kvadranti = new Dictionary<int, List<Point>>();
            for (int i = 0; i < 4; i++)
            {
                kvadranti.Add(i, new List<Point>());
            }
            kvadranti[GetKvadrant(points[3], image.Width, image.Height)].Add(points[3]);
            kvadranti[GetKvadrant(points[1], image.Width, image.Height)].Add(points[1]);
            kvadranti[GetKvadrant(points[2], image.Width, image.Height)].Add(points[2]);
            kvadranti[GetKvadrant(points[0], image.Width, image.Height)].Add(points[0]);
            
            RepairKvadrants(kvadranti);
            RepairKvadrants(kvadranti);

            PointF[] srcs = new PointF[4];
            foreach (int index in kvadranti.Keys)
            {
                srcs[index] = kvadranti[index][0];
            }
            PointF[] dsts = new PointF[4];
            dsts[0] = new PointF(0, 368);
            dsts[1] = new PointF(0, 0);
            dsts[2] = new PointF(244, 0);
            dsts[3] = new PointF(244, 368);
            
            if (DistancePoints(srcs[0], srcs[1]) < DistancePoints(srcs[1], srcs[2]) * (height / (width - 5)))
            {
                PointF first = srcs[0];
                for (int i = 0; i < srcs.Count() - 1; i++)
                {
                    srcs[i] = srcs[i + 1];
                }
                srcs[srcs.Count() - 1] = first;
            }
            if (image.Height < 368 || image.Width < 244)
            {
                Image<Bgr, byte> imagee = new Image<Bgr, byte>((image.Width < 244) ? 244 : image.Width, (image.Height < 368) ? 368 : image.Height, new Bgr(0, 0, 0));
                for (int i = 0; i < image.Width; i++)
                {
                    for (int j = 0; j < image.Height; j++)
                    {
                        imagee[j, i] = image[j, i];
                    }
                }
                image = imagee;
            }
            HomographyMatrix mywarpmat = CameraCalibration.GetPerspectiveTransform(srcs, dsts);
            Image<Bgr, byte> retVal = new Image<Bgr, byte>(image.Bitmap).WarpPerspective(mywarpmat, Emgu.CV.CvEnum.INTER.CV_INTER_NN, Emgu.CV.CvEnum.WARP.CV_WARP_FILL_OUTLIERS, new Bgr(0, 0, 0));
            retVal = CropImage(new Region("0", "0", "368", "244"), new Image<Bgr, byte>(retVal.Bitmap));
            return retVal;
        }

        private void RepairKvadrants(Dictionary<int, List<Point>> kvadranti)
        {
            int faultIndex = -1;
            int zeroIndex = -1;
            foreach (int x in kvadranti.Keys)
            {
                if (kvadranti[x].Count > 1)
                {
                    faultIndex = x;
                }
                if (kvadranti[x].Count == 0)
                {
                    zeroIndex = x;
                }
            }

            if (faultIndex != -1)
            {
                double rastojanje0 = 0;
                double rastojanje1 = 0;
                if (zeroIndex == 1)
                {
                    rastojanje0 = Math.Sqrt(Math.Pow(kvadranti[faultIndex][0].X - 0, 2) + Math.Pow(kvadranti[faultIndex][0].Y - 0, 2));
                    rastojanje1 = Math.Sqrt(Math.Pow(kvadranti[faultIndex][1].X - 0, 2) + Math.Pow(kvadranti[faultIndex][1].Y - 0, 2));
                }
                else if (zeroIndex == 2)
                {
                    rastojanje0 = Math.Sqrt(Math.Pow(kvadranti[faultIndex][0].X - width, 2) + Math.Pow(kvadranti[faultIndex][0].Y - 0, 2));
                    rastojanje1 = Math.Sqrt(Math.Pow(kvadranti[faultIndex][1].X - width, 2) + Math.Pow(kvadranti[faultIndex][1].Y - 0, 2));
                }
                else if (zeroIndex == 3)
                {
                    rastojanje0 = Math.Sqrt(Math.Pow(kvadranti[faultIndex][0].X - width, 2) + Math.Pow(kvadranti[faultIndex][0].Y - height, 2));
                    rastojanje1 = Math.Sqrt(Math.Pow(kvadranti[faultIndex][1].X - width, 2) + Math.Pow(kvadranti[faultIndex][1].Y - height, 2));
                }
                else if (zeroIndex == 0)
                {
                    rastojanje0 = Math.Sqrt(Math.Pow(kvadranti[faultIndex][0].X - 0, 2) + Math.Pow(kvadranti[faultIndex][0].Y - height, 2));
                    rastojanje1 = Math.Sqrt(Math.Pow(kvadranti[faultIndex][1].X - 0, 2) + Math.Pow(kvadranti[faultIndex][1].Y - height, 2));
                }
                
                if (rastojanje0 < rastojanje1)
                {
                    kvadranti[zeroIndex].Add(kvadranti[faultIndex][0]);
                    kvadranti[faultIndex].RemoveAt(0);
                }
                else
                {
                    kvadranti[zeroIndex].Add(kvadranti[faultIndex][1]);
                    kvadranti[faultIndex].RemoveAt(1);
                }
            }
        }

        private double DistancePoints(PointF p1, PointF p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }

        private int GetKvadrant(Point point, int width, int height)
        {
            Console.WriteLine(point);

            if (point.X < width / 2 && point.Y < height / 2)
            {
                //GORE LEVO
                return 1;
            }
            else if (point.X > width / 2 && point.Y < height / 2)
            {
                //GORE DESNO
                return 2;
            }
            else if (point.X > width / 2 && point.Y > height / 2)
            {
                //DOLE DESNO
                return 3;
            }
            else if (point.X < width / 2 && point.Y > height / 2)
            {
                //DOLE LEVO
                return 0;
            }
            return -1;
        }

        #region Methods

        private List<Point> LinearEquations(List<JednacinaPrave> jednacinePrava)
        {
            List<List<JednacinaPrave>> listaListiPrava = new List<List<JednacinaPrave>>();
            for (int i = 0; i < 4; i++)
            {
                listaListiPrava.Add(new List<JednacinaPrave>());
            }

            double kSrednje1 = 0;
            double kSrednje2 = 0;
            List<JednacinaPrave> firstSeparator = new List<JednacinaPrave>();
            List<JednacinaPrave> secondSeparator = new List<JednacinaPrave>();
            foreach (JednacinaPrave jed in jednacinePrava)
            {
                if (firstSeparator.Count == 0)
                {
                    firstSeparator.Add(jed);
                    kSrednje1 += jed.K;
                }
                else
                {
                    if (firstSeparator[0].GetAngle(jed) < JednacinaPrave.SEPARATOR)
                    {
                        firstSeparator.Add(jed);
                        kSrednje1 += jed.K;
                    }
                    else
                    {
                        secondSeparator.Add(jed);
                        kSrednje2 += jed.K;
                    }
                }
            }

            kSrednje1 = kSrednje1 / firstSeparator.Count;
            kSrednje2 = kSrednje2 / secondSeparator.Count;

            JednacinaPrave jedPravex = new JednacinaPrave(0, 0, 100, 0);
            List<JednacinaPrave> temp;
            if (jedPravex.GetAngle(firstSeparator[0]) < jedPravex.GetAngle(secondSeparator[0]))
            {
                temp = firstSeparator;
            }
            else
            {
                temp = secondSeparator;
            }

            double presekSaXOsom = 0;
            double presekSaYOsom = 0;

            foreach (JednacinaPrave jed in temp)
            {
                presekSaYOsom += jed.PresekSaYOsom;
            }

            if (temp == firstSeparator)
            {
                temp = secondSeparator;
            }
            else
            {
                temp = firstSeparator;
            }

            foreach (JednacinaPrave jed in temp)
            {
                presekSaXOsom += jed.PresekSaXOsom;
            }

            presekSaXOsom = presekSaXOsom / temp.Count;
            List<JednacinaPrave> tempp = temp;
            if (temp == firstSeparator)
            {
                temp = secondSeparator;
            }
            else
            {
                temp = firstSeparator;
            }
            presekSaYOsom = presekSaYOsom / temp.Count;
            temp = tempp;
            
            foreach (JednacinaPrave jed in temp)
            {
                if (jed.PresekSaXOsom < presekSaXOsom)
                {
                    listaListiPrava[0].Add(jed);
                }
                else
                {
                    listaListiPrava[1].Add(jed);
                }
            }

            if (temp == firstSeparator)
            {
                temp = secondSeparator;
            }
            else
            {
                temp = firstSeparator;
            }

            foreach (JednacinaPrave jed in temp)
            {
                if (jed.PresekSaYOsom < presekSaYOsom)
                {
                    listaListiPrava[2].Add(jed);
                }
                else
                {
                    listaListiPrava[3].Add(jed);
                }
            }

            // PRESEK PRAVA
            List<JednacinaPrave> okvirKarte = new List<JednacinaPrave>(4);
            //// okvirKarte.Add(GetAverageLinearEq(listaListiPrava[0]));
            okvirKarte.Add(listaListiPrava[0][0]);
            // okvirKarte.Add(GetAverageLinearEq(listaListiPrava[1]));
            okvirKarte.Add(listaListiPrava[1][0]);
            // okvirKarte.Add(GetAverageLinearEq(listaListiPrava[2]));
            okvirKarte.Add(listaListiPrava[2][0]);
            // okvirKarte.Add(GetAverageLinearEq(listaListiPrava[3]));
            okvirKarte.Add(listaListiPrava[3][0]);

            Point point1 = FindPoint(okvirKarte[0], okvirKarte[2]);
            Point point2 = FindPoint(okvirKarte[0], okvirKarte[3]);
            Point point3 = FindPoint(okvirKarte[1], okvirKarte[2]);
            Point point4 = FindPoint(okvirKarte[1], okvirKarte[3]);


            List<Point> retVal = new List<Point>();
            retVal.Add(point1);
            retVal.Add(point2);
            retVal.Add(point3);
            retVal.Add(point4);
            return retVal;
            
        }

        private Point FindPoint(JednacinaPrave first, JednacinaPrave second)
        {
            Point retVal = new Point();
            double x = -1;
            double y = -1;
            if ((first.slucaj == JednacinaPrave.NORMAL || first.slucaj == JednacinaPrave.ONLY_Y) && (second.slucaj == JednacinaPrave.NORMAL || second.slucaj == JednacinaPrave.ONLY_Y))
            {
                if (first.slucaj == JednacinaPrave.ONLY_Y)
                {
                    y = first.N;
                    x = (y - second.N) / second.K;
                }
                else if (second.slucaj == JednacinaPrave.ONLY_Y)
                {
                    y = second.N;
                    x = (y - first.N) / first.K;
                }
                else
                {
                    x = (first.N - second.N) / (second.K - first.K);
                    y = first.K * x + first.N;
                }
            }
            else if (first.slucaj == JednacinaPrave.ONLY_X)
            {
                x = Math.Abs(first.N);
                y = second.K * x + second.N;
            }
            else if (second.slucaj == JednacinaPrave.ONLY_X)
            {
                x = Math.Abs((second.N));
                y = first.K * x + first.N;
            }
            retVal.X = Math.Abs((int)x);
            retVal.Y = Math.Abs((int)y);
            return retVal;
        }

        private List<JednacinaPrave> HoughLines(Image<Bgr, byte> contureImage)
        {
            LineSegment2D[] lines = contureImage.HoughLinesBinary(1.0, Math.PI / 360.0, 50, 60.0, 0.0)[0];
            List<JednacinaPrave> jednacinePrava = new List<JednacinaPrave>();

            foreach (LineSegment2D line in lines)
            {
                jednacinePrava.Add(new JednacinaPrave(line.P1.X, line.P1.Y, line.P2.X, line.P2.Y));
            }
            return jednacinePrava;
        }

        private Image<Bgr, Byte> FilterImageBGR(double bMin, double gMin, double rMin, double bMax, double gMax, double rMax, Image<Bgr, byte> processingBGR)
        {
            //processingBGR = new Image<Bgr, byte>(imageBox1.Image.Bitmap);
            Image<Bgr, Byte> processingImage = new Image<Bgr, Byte>(processingBGR.Bitmap);
            for (int i = 0; i < processingBGR.ManagedArray.GetLength(0); i++)
            {
                for (int j = 0; j < processingBGR.ManagedArray.GetLength(1); j++)
                {
                    Bgr currentColor = processingImage[i, j];

                    if ((currentColor.Red >= rMin && currentColor.Red <= rMax) && (currentColor.Green >= gMin && currentColor.Green <= gMax) && (currentColor.Blue >= bMin && currentColor.Blue <= bMax))
                    {
                        if (Math.Abs(currentColor.Red - currentColor.Blue) < 30 && Math.Abs(currentColor.Red - currentColor.Green) < 30 && Math.Abs(currentColor.Green - currentColor.Blue) < 30)
                        {
                            processingImage[i, j] = new Bgr(currentColor.Blue, currentColor.Green, currentColor.Red);
                        }
                        else
                        {
                            processingImage[i, j] = new Bgr(255, 255, 255);
                        }

                    }
                    else
                    {
                        processingImage[i, j] = new Bgr(255, 255, 255);
                    }
                }
            }
            return processingImage;
        }

        private Image<Gray, Byte> ToGrayImage(double blue, double green, double red, Image<Bgr, byte> proImage)
        {
            // Image<Bgr, byte> processingBGR = new Image<Bgr, byte>(proImage.Image.Bitmap);
            Image<Gray, Byte> processingImag = new Image<Gray, Byte>(proImage.ManagedArray.GetLength(1), proImage.ManagedArray.GetLength(0));
            for (int i = 0; i < proImage.ManagedArray.GetLength(0); i++)
            {
                for (int j = 0; j < proImage.ManagedArray.GetLength(1); j++)
                {
                    Bgr currentColor = proImage[i, j];
                    processingImag[i, j] = new Gray(currentColor.Blue * blue + currentColor.Green * green + currentColor.Red * red);
                }
            }
            return processingImag;
        }

        private List<Contour<Point>> FindContoursOfImg(Image<Gray, Byte> grayImg,out Image<Bgr, Byte> retImg)
        {
            retImg = null;
            grayImg._Not();
            List<Contour<Point>> contourList = new List<Contour<Point>>();
            using (MemStorage storage = new MemStorage())
            {
                //add points to listbox
                using (var p2 = new Pen(Color.Yellow, 2))
                {
                    Image<Bgr, Byte> grayConture = new Image<Bgr, Byte>(grayImg.Width, grayImg.Height);
                    int counter = 0;

                    for (Contour<Point> contours = grayImg.FindContours(Emgu.CV.CvEnum.CHAIN_APPROX_METHOD.CV_CHAIN_APPROX_SIMPLE, Emgu.CV.CvEnum.RETR_TYPE.CV_RETR_EXTERNAL, storage); contours != null; contours = contours.HNext)
                    {

                        if (contours.Perimeter >= 300)
                        {
                            Contour<Point> currentContour = contours.ApproxPoly(contours.Perimeter * 0.015, storage);
                            contourList.Add(contours);
                            CvInvoke.cvDrawContours(grayConture, contours, new MCvScalar(255), new MCvScalar(255), -1, 2, Emgu.CV.CvEnum.LINE_TYPE.EIGHT_CONNECTED, new Point(0, 0));

                            retImg = grayConture;
                            counter++;
                        }
                    }
                }
            }
            return contourList;
        }

        private void Decomponate(Image<Bgr, byte> image, List<Contour<Point>> contourList)
        {
            int i = 0;
            foreach (Contour<Point> con in contourList)
            {
                Image<Bgr, byte> cropedImage = null;
                Rectangle rec = con.BoundingRectangle;
                Region reg = new Region((rec.Top - 5).ToString(), (rec.Left - 5).ToString(), (rec.Bottom + 5).ToString(), (rec.Right + 5).ToString());
                cropedImage = CropImage(reg, image);
                cropedImage.Bitmap.Save("TestIsecena" + i + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                //Image<Bgr, byte> xxx = new Image<Bgr, byte>("TestIsecena" + i++ + ".jpg");
                //xxx = xxx.Resize((int)width, (int)height, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);
                decomponatedImage.Add(cropedImage);
                //cropedImage = cropedImage.Resize((int)width, (int)height, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);
                //cropedImage.Bitmap.Save("TestIsecena.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }

        private Image<Bgr, Byte> CropImage(Region r, Image<Bgr, Byte> bgrImg)
        {
            int sirina = r.Poz4 - r.Poz2;
            int visina = r.Poz3 - r.Poz1;
            if (sirina % 4 != 0)
            {
                sirina += 4 - (sirina % 4);
            }
            if (visina % 4 != 0)
            {
                visina += 4 - (visina % 4);
            }


            Image<Bgr, Byte> retVal = new Image<Bgr, Byte>(new Size(sirina, visina));//sirina+1, visina

            for (int i = 0; i < visina; i++)
            {
                for (int j = 0; j < sirina; j++)
                {
                    retVal[i, j] = bgrImg[r.Poz1 + i, r.Poz2 + j];
                }
            }
            return retVal;
        }
        
        #endregion
    }
}
