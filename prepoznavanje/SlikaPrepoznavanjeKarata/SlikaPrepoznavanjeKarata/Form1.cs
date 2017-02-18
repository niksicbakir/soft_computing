using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using Emgu.CV;
using Emgu.CV.Structure;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;
using SlikaPrepoznavanjeKarata.Model;
using SlikaPrepoznavanjeKarata.Soft_Computing;

namespace SlikaPrepoznavanjeKarata
{
    public partial class Form1 : Form
    {
        Image<Bgr, Byte> img1;
        List<Contour<Point>> contourList;
        List<JednacinaPrave> jednacinePrava;
        Point point1;
        Point point2;
        Point point3;
        Point point4;
        double width = 612.0 ;//+ 1224 + 612;
        double height = 816.0;// + 1632 + 816;
        
        public Form1()
        {
            InitializeComponent();
            pathToPicture.Text = "jedna_ruka\\talon1.jpg";
            jednacinePrava = new List<JednacinaPrave>();
            prviParametarHough.Text = "1";
            drugiParametarHough.Text = "360.0";
            treciParametarHough.Text = "50";
            cetvrtiParametarHough.Text = "60";
            petiParametarHough.Text = "0";
            DataSet.getInstance();

        }

        private void buttonLoadPicture_Click(object sender, EventArgs e)
        {
            //Application.Restart();
            LoadImage();
        }

        private void LoadImage()
        {
            img1 = new Image<Bgr, byte>(pathToPicture.Text);
            img1 = img1.Resize((int)width, (int)height, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);
            imageBox1.Image = img1;
            //ZA PRAVLJENJE DATASET-a
            //string[] t = pathToPicture.Text.Split('\\');
            //imeSlikeTextBox.Text = "DataSet-" + t[1];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //CvInvoke.cvCvtColor(img1,img1,Emgu.CV.CvEnum.COLOR_CONVERSION.CV_RGB2HSV);

            Double redCompenetValueMin = Double.Parse(redComponent.Text.Split(',')[0]);
            Double redCompenetValueMax = Double.Parse(redComponent.Text.Split(',')[1]);

            Double blueCompenetValueMin = Double.Parse(blueComponent.Text.Split(',')[0]);
            Double blueCompenetValueMax = Double.Parse(blueComponent.Text.Split(',')[1]);

            Double greenCompenetValueMin = Double.Parse(greenComponent.Text.Split(',')[0]);
            Double greenCompenetValueMax = Double.Parse(greenComponent.Text.Split(',')[1]);

            imageBox1.Image = FilterImageBGR(blueCompenetValueMin, greenCompenetValueMin, redCompenetValueMin, blueCompenetValueMax, greenCompenetValueMax, redCompenetValueMax,new Image<Bgr,Byte>(imageBox1.Image.Bitmap));
            //imageBox1.Image = img1.InRange(new Bgr(0, 0, 100), new Bgr(255, 255, 255));
        }

        private Image<Bgr, Byte> FilterImageBGR(double bMin, double gMin, double rMin, double bMax, double gMax, double rMax, Image<Bgr, byte> processingBGR)
        {
            //processingBGR = new Image<Bgr, byte>(imageBox1.Image.Bitmap);
            Image <Bgr,Byte> processingImage = new Image<Bgr, Byte>(processingBGR.Bitmap);
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
                        }else
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


        private Image<Bgr, Byte> FilterImageBGROnlyOneColor(Image<Bgr, Byte> proImage)
        {
            Image<Bgr, Byte> processingImage = new Image<Bgr, Byte>(proImage.Bitmap);
            for (int i = 0; i < proImage.ManagedArray.GetLength(0); i++)
            {
                for (int j = 0; j < proImage.ManagedArray.GetLength(1); j++)
                {
                    Bgr currentColor = processingImage[i, j];

                    if (currentColor.Red < currentColor.Blue + 20 && currentColor.Green < currentColor.Blue + 20)
                    {
                        processingImage[i, j] = new Bgr(currentColor.Blue, currentColor.Green, currentColor.Red);
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

        private void btnToGray_Click(object sender, EventArgs e)
        {
            Image <Gray,Byte> grayImg = ToGrayImage(Double.Parse(blueCoeficiente.Text), Double.Parse(greenCoeficiente.Text), Double.Parse(redCoeficiente.Text),new Image<Bgr,byte> (imageBox1.Image.Bitmap));
            imageBox1.Image = grayImg;
        }

        private void btnThreshold_Click(object sender, EventArgs e)
        {
            Image<Gray, Byte> grayImg = new Image<Gray, Byte>(imageBox1.Image.Bitmap);
            imageBox1.Image = grayImg.ThresholdBinary(new Gray(Double.Parse(thresholdMin.Text)), new Gray(Double.Parse(thresholdMax.Text)));
            //grayImgThr = grayDerivate.ThresholdBinary(new Gray(Double.Parse(thresholdMin.Text)), new Gray(Double.Parse(thresholdMax.Text)));
            
            //imageBox1.Image = grayImgThr;
        }

        private void btnDilate_Click(object sender, EventArgs e)
        {
            Image<Gray, Byte> grayImgThr = new Image<Gray, Byte>(imageBox1.Image.Bitmap);
            grayImgThr = grayImgThr.Dilate(1);
            imageBox1.Image = grayImgThr;
        }

        private void btnErode_Click(object sender, EventArgs e)
        {
            Image<Gray, Byte> grayImgThr = new Image<Gray, Byte>(imageBox1.Image.Bitmap);
            grayImgThr = grayImgThr.Erode(1);
            imageBox1.Image = grayImgThr;
        }
        
        private void button1_Click_1(object sender, EventArgs e)
        {
            /*
            Region[] regions = GetRegions(Post());
            for (int i = 0; i < regions.Length; i++)
            {
                Image<Bgr, Byte> croppedImage = CropImage(regions[i], new Image<Bgr, byte>(imageBox1.Image.Bitmap));
                imageBox1.Image = croppedImage;
                Image<Bgr, Byte> resizedImage = croppedImage.Resize(60, 100, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);
                resizedImage.Bitmap.Save("slika" + i + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            }
             * */
        }


        private Image<Bgr, Byte> CropImage(Region r, Image<Bgr, Byte> bgrImg)
        {
            int sirina = r.Poz4 - r.Poz2;
            int visina = r.Poz3 - r.Poz1;
            if (sirina % 4 != 0)
            {
                sirina += 4-(sirina % 4);
            }
            if (visina % 4 != 0)
            {
                visina += 4-(visina % 4);
            }


            Image<Bgr, Byte> retVal = new Image<Bgr, Byte>(new Size(sirina, visina));//sirina+1, visina

            for (int i = 0; i < visina; i++)
            {
                for (int j = 0; j < sirina; j++)
                {
                    //retVal[i, j] = grayImgThr[r.Poz1 + i, r.Poz2 + j];
                    retVal[i, j] = bgrImg[r.Poz1 + i, r.Poz2 + j];
                }
            }
            return retVal;
        }

        private Region[] GetRegions(string json)
        {
            JArray jarray = JArray.Parse(json);
            Region[] retVal = new Region[jarray.Count];
            for (int i = 0; i < jarray.Count; i++)
            {
                JToken jarrayx = jarray[i];
                Array array = jarrayx.ToArray();
                Region region = new Region(array.GetValue(0).ToString(), array.GetValue(1).ToString(), array.GetValue(2).ToString(), array.GetValue(3).ToString());
                retVal[i] = region;
            }
            return retVal;
        }


        private string Post()
        {
            /*
            long pocetak = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:8081/lang");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string json = "{\"sirina\":" + grayImgThr.ManagedArray.GetLength(0) + ",\"visina\":" + grayImgThr.ManagedArray.GetLength(1) + ",\"sadrzaj\":[";
            sb.Append(json);
            Console.WriteLine(sb);
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                for (int i = 0; i < grayImgThr.ManagedArray.GetLength(0); i++)
                {
                    for (int j = 0; j < grayImgThr.ManagedArray.GetLength(1); j++)
                    {
                        if (!(i == 0 && j == 0))
                        {
                            sb.Append(",");
                        }
                        sb.Append(grayImgThr[i, j].Intensity);
                    }
                }
                sb.Append("]}");
                streamWriter.Write(sb);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            var streamReader = new StreamReader(httpResponse.GetResponseStream());

            var result = streamReader.ReadToEnd();
            System.Console.WriteLine(result);

            long kraj = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            System.Console.WriteLine(kraj - pocetak);
            return result;
             * */
            return "zoran";
        }
        
        private string KNN(Image<Gray,Byte> image)
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
                Console.WriteLine("Karta sa imenom " + dataSetImg.PathName + " ima odnos = " + ((double)numberOfMatchedWhitePixels) / numberOfWhitePixels);
                if (((double)numberOfMatchedWhitePixels) / numberOfWhitePixels > matchRaiting)
                {
                    matchRaiting = ((double)numberOfMatchedWhitePixels) / numberOfWhitePixels;
                    maxMatchedImg = dataSetImg;
                }
            }
            Console.WriteLine("PREDPOSTAVLJA SE DA JE U PITANJU KARTA " + maxMatchedImg.PathName);
            return maxMatchedImg.PathName;
        }

        private void btnKNN_Click(object sender, EventArgs e)
        {
            Console.WriteLine("PREDPOSTAVLJA SE DA JE U PITANJU KARTA " + KNN(new Image<Gray,Byte>(imageBox1.Image.Bitmap)));
            /*
            Region[] regions = GetRegions(Post());
            double max = -1;
            int index = 0;
            for (int i = 0; i < regions.Length; i++)
            {
                Image<Gray, Byte> croppedImage = CropImage(regions[i]);
                Image<Gray, Byte> resizedImage = croppedImage.Resize(60, 100, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);
                imageBox1.Image = resizedImage;
                for (int j = 1; j < 10; j++)
                {
                    Bitmap image = new Bitmap("slika" + j + ".jpg");
                    int counter = 0;
                    int counterBlack = 0;
                    Image<Gray, Byte> imagex = new Image<Gray, Byte>(image);
                    int sirina;
                    int visina;
                    if (imagex.ManagedArray.GetLength(0) > resizedImage.ManagedArray.GetLength(0))
                    {
                        sirina = resizedImage.ManagedArray.GetLength(0);
                    }
                    else
                    {
                        sirina = imagex.ManagedArray.GetLength(0);
                    }

                    if (imagex.ManagedArray.GetLength(1) > resizedImage.ManagedArray.GetLength(1))
                    {
                        visina = resizedImage.ManagedArray.GetLength(1);
                    }
                    else
                    {
                        visina = imagex.ManagedArray.GetLength(1);
                    }
                    for (int k = 0; k < sirina; k++)
                    {
                        for (int l = 0; l < visina - 2; l++)
                        {
                            counterBlack++;
                            if (resizedImage[k, l].Intensity == imagex[k, l].Intensity)
                            {
                                counter++;
                            }


                        }
                    }
                    Console.WriteLine("Za sliku " + j + " counter je " + ((double)counter / counterBlack));
                    if (((double)counter / counterBlack) > max)
                    {
                        index = j;
                        max = ((double)counter / counterBlack);
                    }
                }
                break;
            }
            Console.WriteLine("Prepoznata karta sa indexom : " + index);
             * */
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // CvInvoke.cvCvtColor(img1, img1, Emgu.CV.CvEnum.COLOR_CONVERSION.CV_RGB2HSV);
            Bgr min = new Bgr(0, 60, 160);
            Bgr max = new Bgr(15, 255, 255);
            //     imageBox1.Image=img1.InRange(min, max);
            // imageBox1.Image = img1;
            imageBox1.Image = FilterImageBGROnlyOneColor(new Image<Bgr,Byte>(imageBox1.Image.Bitmap));

        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*
            //Image<Bgr, Byte> img = new Image<Bgr, byte>(fileNameTextBox.Text);

            //Image<Gray, Byte> gray = img1.Convert<Gray, Byte>().PyrDown().PyrUp();

           // Gray cannyThreshold = new Gray(180);
            //Gray cannyThresholdLinking = new Gray(120);


            //Image<Gray, Byte> cannyEdges = gray.Canny(cannyThreshold, cannyThresholdLinking);
           
            //LineSegment2D[] lines = grayImgThr.HoughLinesBinary(1, Math.PI / 45.0, 20, 30, 10)[0];
            //Console.WriteLine("lines " + lines.Length);
            //return;
            List<MCvBox2D> boxList = new List<MCvBox2D>();

            using (MemStorage storage = new MemStorage())
                for (Contour<Point> contours = grayConture.FindContours(); contours != null; contours = contours.HNext)
                {
                    Contour<Point> currentContour = contours.ApproxPoly(contours.Perimeter * 0.05, storage);
                   // Console.WriteLine("Area " + contours.Area);
                    if (contours.Area > 250)
                    {
                        //Console.WriteLine("tolko je " + currentContour.Total);
                        if (currentContour.Total == 4)
                        {
                            bool isRectangle = true;
                            Point[] pts = currentContour.ToArray();
                            LineSegment2D[] edges = PointCollection.PolyLine(pts, true);
                            Console.WriteLine("dal je ovde " + currentContour.Total);
                            //using edges i found coordinates.
                            for (int i = 0; i < edges.Length; i++)
                            {
                                double angle = Math.Abs(
                                   edges[(i + 1) % edges.Length].GetExteriorAngleDegree(edges[i]));
                                if (angle < 80 || angle > 100)
                                {
                                    isRectangle = false;
                                    break;
                                }
                                if (isRectangle)
                                {
                                    boxList.Add(currentContour.GetMinAreaRect());
                                    //Console.WriteLine("asdasdas" + boxList.Count);
                                }
                                else
                                {
                                    //Console.WriteLine("asfg");
                                }
                            }
                        }
                    }


                }
           */
        }

        private void button4_Click(object sender, EventArgs e)
        {
            contourList = FindContoursOfImg(new Image<Gray, Byte>(imageBox1.Image.Bitmap));
            
        }

        private List<Contour<Point>> FindContoursOfImg(Image<Gray, Byte> grayImg)
        {
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

                            imageBox1.Image = grayConture;
                            counter++;
                        }
                    }
                    Console.WriteLine("conture: " + counter);
                }
            }
            return contourList;
        }

        private void button5_Click(object sender, EventArgs e)
        {
           // Image<Bgr, Byte> imgProcessed = new Image<Bgr, byte>(@"C:\Users\Public\Pictures\Sample Pictures\1.jpg");

          //  imgProcessed1 = imgProcessed.Convert<Gray, byte>();
           // Image<Gray, Single> img_final = (grayImg.Sobel(1, 0, 5));
           // imageBox1.Image = img_final;

            Image<Gray, Byte> grayImg = new Image<Gray, byte>(imageBox1.Image.Bitmap);
           // Image<Gray, byte> gray = new Image<Gray, byte>(@"C:\Users\Public\Pictures\Sample Pictures\1.jpg");
            Image<Gray, float> grayDerivatex = grayImg.Sobel(0, 1, 3).Add(grayImg.Sobel(1, 0, 3)).AbsDiff(new Gray(0.0));

            Image<Gray, Byte>  grayDerivate = new Image<Gray, Byte>(grayDerivatex.Bitmap);
            
            imageBox1.Image = grayDerivate;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            jednacinePrava.Clear();
            Image<Bgr, Byte> grayConture = new Image<Bgr, byte>(imageBox1.Image.Bitmap);
            Image<Bgr, Byte> _linesImage = grayConture.CopyBlank();
           // LineSegment2D[] lines = grayConture.HoughLinesBinary(1, Math.PI / 360.0, 50, 60, 0)[0];
            LineSegment2D[] lines = grayConture.HoughLinesBinary(Double.Parse(prviParametarHough.Text), Math.PI / Double.Parse(drugiParametarHough.Text), Int32.Parse(treciParametarHough.Text), Double.Parse(cetvrtiParametarHough.Text), Double.Parse(petiParametarHough.Text))[0];
            //LineSegment2D[][] liness = grayConture.HoughLines(new Bgr(100, 100, 100), new Bgr(100, 100, 100), 1, Math.PI / 45.0, 50, 60, 0);
            
           // lines.
         //   grayConture.HoughLinesBinary(
            //grayConture.HoughLinesBinary(
            Console.WriteLine(lines.Length);
            foreach (LineSegment2D line in lines)
            {
               // LineSegment2D l = new LineSegment2D(new Point(line.P1.X - 100, line.P1.Y + 100),line.P2);
                //System.Console.WriteLine("Crta se linija");
                _linesImage.Draw(line, new Bgr(200,200,200), 1);
                jednacinePrava.Add(new JednacinaPrave(line.P1.X, line.P1.Y, line.P2.X, line.P2.Y));
            }
            imageBox1.Image = _linesImage;
        }

        private Image<Bgr, Byte> Decomponate(Image<Bgr, byte> image)
        {
            int i = 0;
            Image<Bgr, byte> cropedImage=null;
            foreach (Contour<Point> con in contourList)
            {
                Rectangle rec = con.BoundingRectangle;
               // Rectangle rec = new Rectangle(con.BoundingRectangle.X, con.BoundingRectangle.Y, con.BoundingRectangle.Height, con.BoundingRectangle.Width);
                Region reg = new Region((rec.Top - 5).ToString(), (rec.Left - 5 ).ToString(), (rec.Bottom + 5).ToString(), (rec.Right + 5).ToString());
                Console.WriteLine(reg);
                cropedImage = CropImage(reg,image);
                cropedImage.Bitmap.Save("Isecena" + i++ + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                imageBox1.Image = cropedImage;
            }
            return cropedImage;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            imageBox1.Image = Decomponate(img1);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Image <Gray,Byte> grayImg = new Image<Gray, Byte>(pathToPicture.Text);
            imageBox1.Image = grayImg;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Console.WriteLine("<<<<<<<<<<<<<<<<<<<<<#############>>>>>>>>>>>>>>>>>>>>>");
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

            double uporedjivanjeK;
            JednacinaPrave jedPravex = new JednacinaPrave(0, 0, 100, 0);
            List<JednacinaPrave> temp;
            if (jedPravex.GetAngle(firstSeparator[0]) < jedPravex.GetAngle(secondSeparator[0]))
            {
                uporedjivanjeK = kSrednje1;
                temp = firstSeparator;
            }
            else
            {
                uporedjivanjeK = kSrednje2;
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
            Console.WriteLine("Srednji presek sa x osom je: " + presekSaXOsom);
            Console.WriteLine("Srednji presek sa y osom je: " + presekSaYOsom);

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
            
            foreach (JednacinaPrave jed in firstSeparator)
            {
                Console.WriteLine("Presek sa y osom je: " + jed.PresekSaYOsom);
            }
            Console.WriteLine("____________________-");
            foreach (JednacinaPrave jed in secondSeparator)
            {
                Console.WriteLine("Presek sa y osom je: " + jed.PresekSaYOsom);
            }
            
            Console.WriteLine("<<<<<<<<<<<<<<<<<<<<<#############>>>>>>>>>>>>>>>>>>>>>");
            int x = 0;
            foreach(List<JednacinaPrave> listaJed in listaListiPrava)
            {
                Console.WriteLine("====" + x++ + "====");
                foreach (JednacinaPrave jed in listaJed)
                {
                    Console.WriteLine("K = " + jed.K + " , N = " + jed.N + "Presek sa y osom: " + jed.PresekSaYOsom + "Presek sa x osom: " + jed.PresekSaXOsom);
                }
                Console.WriteLine("=============");
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
            
            FindPoints(okvirKarte);
            Console.WriteLine("K=[" + okvirKarte[0].K + "," + okvirKarte[1].K + "," + okvirKarte[2].K + "," + okvirKarte[3].K + "]");
            Console.WriteLine("N=[" + okvirKarte[0].N + "," + okvirKarte[1].N + "," + okvirKarte[2].N + "," + okvirKarte[3].N + "]");
        }

        private JednacinaPrave GetAverageLinearEq(List<JednacinaPrave> list)
        {
            JednacinaPrave t = new JednacinaPrave(0, 0, 0, 0);
            t.slucaj = JednacinaPrave.NORMAL;
            bool isOnlyX = false;
            double kAverage = 0;
            foreach (JednacinaPrave jed in list)
            {
                t.x1 += jed.x1;
                t.x2 += jed.x2;
                t.y1 += jed.y1;
                t.y2 += jed.y2;
                kAverage += jed.K;
                t.N += jed.N;
                if (jed.slucaj == JednacinaPrave.ONLY_X)
                {
                    isOnlyX = true;
                    t.slucaj = JednacinaPrave.ONLY_X;
                    t.K = jed.K;
                }
            }
            t.N = t.N / list.Count;
            t.x1 = t.x1 / list.Count;
            t.x2 = t.x2 / list.Count;
            t.y1 = t.y1 / list.Count;
            t.y2 = t.y2 / list.Count;
            if (!isOnlyX)
            {
                t.K = kAverage / list.Count;
            }
            return t;
        }

        private void FindPoints(List<JednacinaPrave> okvir)
        {
            point1 = FindPoint(okvir[0],okvir[2]);
            point2 = FindPoint(okvir[0], okvir[3]);
            point3 = FindPoint(okvir[1], okvir[2]);
            point4 = FindPoint(okvir[1], okvir[3]);
            
            Console.WriteLine("--------------PRESECI---------------");
            Console.WriteLine(point1);
            Console.WriteLine(point2);
            Console.WriteLine(point3);
            Console.WriteLine(point4);
            Console.WriteLine("--------------KRAJ_PRESECI---------------");

            Image<Bgr, Byte> cpyImg = new Image<Bgr, byte>(imageBox1.Image.Bitmap);
            cpyImg.Draw(new LineSegment2D(point1, point2), new Bgr(0, 255, 0), 1);
            cpyImg.Draw(new LineSegment2D(point1, point3), new Bgr(0, 255, 0), 1);
            cpyImg.Draw(new LineSegment2D(point3, point4), new Bgr(0, 255, 0), 1);
            cpyImg.Draw(new LineSegment2D(point2, point4), new Bgr(0, 255, 0), 1);
            imageBox1.Image = cpyImg;
        }

        private Point FindPoint(JednacinaPrave first, JednacinaPrave second)
        {
            Console.WriteLine("prva prava: " + first.slucaj);
            Console.WriteLine("druga prava: " + second.slucaj);
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
            else if(first.slucaj == JednacinaPrave.ONLY_X)
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
            retVal.Y =Math.Abs((int)y);
            return retVal;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Image<Bgr,Byte> imgPersp = Perspective(img1);
            imageBox1.Image = imgPersp;
        }

        private Image<Bgr, Byte> Perspective(Image <Bgr,Byte> image)
        {
            Console.WriteLine("GRANICA WIDTH " + image.Bitmap.Width / 2);
            Console.WriteLine("GRANICA HEIGHT " + image.Bitmap.Height / 2);
            Dictionary<int, List<Point>> kvadranti = new Dictionary<int, List<Point>>();
            for (int i = 0; i < 4; i++)
            {
                kvadranti.Add(i, new List<Point>());
            }
            kvadranti[GetKvadrant(point4, image.Width,image.Height)].Add(point4);
            kvadranti[GetKvadrant(point2, image.Width, image.Height)].Add(point2);
            kvadranti[GetKvadrant(point3, image.Width, image.Height)].Add(point3);
            kvadranti[GetKvadrant(point1, image.Width, image.Height)].Add(point1);
            Console.WriteLine("PRE PREBACIVANJA");
            foreach (int index in kvadranti.Keys)
            {
                Console.WriteLine("#################################");
                Console.WriteLine(kvadranti[index].Count);
                Console.WriteLine(index);
                Console.WriteLine("#############################");
            }
            RepairKvadrants(kvadranti);
            RepairKvadrants(kvadranti);

            PointF[] srcs = new PointF[4];
            /*
            srcs[GetKvadrant(point4)] = point4;
            srcs[GetKvadrant(point2)] = point2;
            srcs[GetKvadrant(point3)] = point3;
            srcs[GetKvadrant(point1)] = point1;
             * */
            foreach (int index in kvadranti.Keys)
            {
                Console.WriteLine("****************");
                Console.WriteLine("U kvadrantu " + index + " se nalazi point " + kvadranti[index][0]);
                Console.WriteLine(kvadranti[index].Count);
                Console.WriteLine("****************");
                srcs[index] = kvadranti[index][0];
            }

            PointF[] dsts = new PointF[4];
            /*
            dsts[0] = new PointF(0, 816);
            dsts[1] = new PointF(0, 0);
            dsts[2] = new PointF(612, 0);
            dsts[3] = new PointF(612, 816);
            DATA SET */
            dsts[0] = new PointF(0, 368);
            dsts[1] = new PointF(0, 0);
            dsts[2] = new PointF(244, 0);
            dsts[3] = new PointF(244, 368);
            Console.WriteLine("Visina slike je: " + DistancePoints(srcs[0], srcs[1]));
            Console.WriteLine("Sirina slike je: " + DistancePoints(srcs[1], srcs[2]) * (height / (width - 5)));
            Console.WriteLine("Visina slike je: " + DistancePoints(srcs[1], srcs[2]));

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
            //imageBox1.Image = CropImage(new Region("0","0","816","612"));//92,61 ZA DATASET
        }

        private void RepairKvadrants(Dictionary<int,List<Point>> kvadranti)
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
                Console.WriteLine("POINT0 " + kvadranti[faultIndex][0]);
                Console.WriteLine("POINT1 " + kvadranti[faultIndex][1]);

                Console.WriteLine("RASTOJANJA : " + rastojanje0 + "," + rastojanje1);
                if (rastojanje0 < rastojanje1)
                {
                    kvadranti[zeroIndex].Add(kvadranti[faultIndex][0]);
                    kvadranti[faultIndex].RemoveAt(0);
                    Console.WriteLine("Iz kvadranta" + faultIndex + " se ubacuje u kvadrant" + zeroIndex + "point " + kvadranti[zeroIndex][0]);
                }
                else
                {
                    kvadranti[zeroIndex].Add(kvadranti[faultIndex][1]);
                    kvadranti[faultIndex].RemoveAt(1);
                    Console.WriteLine("Iz kvadranta" + faultIndex + " se ubacuje u kvadrant" + zeroIndex + "point " + kvadranti[zeroIndex][0]);
                }
            }
        }

        private double DistancePoints(PointF p1, PointF p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }

        private int GetKvadrant(Point point,int width, int height)
        {
            Console.WriteLine(point);

            if (point.X < width / 2 && point.Y < height / 2)
            {
                //GORE LEVO
                Console.WriteLine("KVADRANT1");
                return 1;
            }
            else if (point.X > width / 2 && point.Y < height / 2)
            {
                //GORE DESNO
                Console.WriteLine("KVADRANT2");
                return 2;
            }
            else if (point.X > width / 2 && point.Y > height / 2)
            {
                //DOLE DESNO
                Console.WriteLine("KVADRANT3");
                return 3;
            }
            else if (point.X < width / 2 && point.Y > height / 2)
            {
                //DOLE LEVO
                Console.WriteLine("KVADRANT0");
                return 0;
            }
            return -1;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Image<Bgr, byte> img = new Image<Bgr, byte>(imageBox1.Image.Bitmap);
            //img = img.Resize(244, 368, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);
            img.Bitmap.Save(imeSlikeTextBox.Text, System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            FinalSekvence();
        }

        private void FinalSekvence()
        {
            SoftAlgorithm soft = new SoftAlgorithm(width, height);
            soft.Processing(pathToPicture.Text);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            KNNThreads knn = new KNNThreads(new Image<Gray, Byte>(imageBox1.Image.Bitmap));
            Console.WriteLine("PREDPOSTAVLJA SE DA JE U PITANJU KARTA Thread " + knn.Start());
        }

        private void button15_Click(object sender, EventArgs e)
        {
            img1 = new Image<Bgr, Byte>(pathToPicture.Text);
            //img1 = img1.Resize((int)width, (int)height, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);
            imageBox1.Image = img1;
        }

        private void KNNTEST(string test1)
        {
            Image<Gray, Byte> testImg1 = new Image<Gray, Byte>(test1);
            Image<Gray, Byte> testImg2 = new Image<Gray, Byte>(imageBox1.Image.Bitmap);
            imageBox1.Image = testImg2;
            double matchRaiting = -1;
            
                int numberOfWhitePixels = 0;
                int numberOfMatchedWhitePixels = 0;

                for (int i = 0; i < testImg1.Width; i++)
                {
                    for (int j = 0; j < testImg1.Height; j++)
                    {
                        if (testImg1[j, i].Intensity == 255)
                        {
                            numberOfWhitePixels++;
                        }
                        if (testImg1[j, i].Intensity == 255)
                        {
                            if (testImg1[j, i].Intensity == testImg2[j, i].Intensity)
                            {
                                numberOfMatchedWhitePixels++;
                            }
                        }
                    }
                }
                Console.WriteLine("ODNOS" + ((double)numberOfMatchedWhitePixels) / numberOfWhitePixels);
                if (((double)numberOfMatchedWhitePixels) / numberOfWhitePixels > matchRaiting)
                {
                    matchRaiting = ((double)numberOfMatchedWhitePixels) / numberOfWhitePixels;
                }
                Console.WriteLine("ODNOS" + matchRaiting);

                
        }

        private void button16_Click(object sender, EventArgs e)
        {
            KNNTEST(knnTest1textBox.Text);
        }

        private void KNNTEST2(string test1)
        {
            Image<Bgr, Byte> testImg1 = new Image<Bgr, Byte>(test1);
            Image<Bgr, Byte> testImg2 = new Image<Bgr, Byte>(imageBox1.Image.Bitmap);
            //testImg1 = testImg1.Resize((int)width, (int)height, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);
            //testImg2 = testImg2.Resize((int)width, (int)height, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);
            imageBox1.Image = testImg2;
            double matchRaiting = -1;

            int numberOfWhitePixels = 0;
            int numberOfMatchedWhitePixels = 0;

            for (int i = 0; i < testImg1.Width; i++)
            {
                for (int j = 0; j < testImg1.Height; j++)
                {

                    numberOfWhitePixels++;
                    if (testImg1[j, i].Blue == testImg2[j, i].Blue && testImg1[j, i].Red == testImg2[j, i].Red && testImg1[j, i].Green == testImg2[j, i].Green)
                    {
                        numberOfMatchedWhitePixels++;
                    }

                }
            }
            Console.WriteLine("UKUPNO " + numberOfWhitePixels + " MATCH "+numberOfMatchedWhitePixels);
            Console.WriteLine("ODNOS" + ((double)numberOfMatchedWhitePixels) / numberOfWhitePixels);
            if (((double)numberOfMatchedWhitePixels) / numberOfWhitePixels > matchRaiting)
            {
                matchRaiting = ((double)numberOfMatchedWhitePixels) / numberOfWhitePixels;
            }
            Console.WriteLine("ODNOS" + matchRaiting);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            KNNTEST2(knnTest1textBox.Text);
        }
       
    }
}