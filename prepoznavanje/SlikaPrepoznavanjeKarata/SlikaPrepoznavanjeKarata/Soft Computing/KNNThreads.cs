using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Threading;

namespace SlikaPrepoznavanjeKarata.Soft_Computing
{
    public class KNNThreads
    {
        private Image<Gray, byte> image;
        private Dictionary<Double, String> result;

        public KNNThreads(Image<Gray, byte> image)
        {
            this.image = image;
            result = new Dictionary<double, string>();
        }

        public String Start()
        {
            Thread thread1 = new Thread(() => KNN(0,26));
            thread1.Start();
            Thread thread2 = new Thread(() => KNN(26, 52));
            thread2.Start();
            Thread thread3 = new Thread(() => KNN(52, 78));
            thread3.Start();
            Thread thread4 = new Thread(() => KNN(78, 104));
            thread4.Start();

            thread1.Join();
            thread2.Join();
            thread3.Join();
            thread4.Join();

            string retValMax = result.First().Value;
            double raitingMax = result.First().Key;
            foreach(double key in result.Keys)
            {
                if (raitingMax < key)
                {
                    raitingMax = key;
                    retValMax = result[key];
                }
            }

            System.Console.WriteLine("KNN Thread predpostavlja da je karta " + retValMax);

            return retValMax;
        }

        public void KNN(int minImage, int maxImage)
        {
            List<int> over = new List<int>();
            Image<Gray, Byte> decissionImg = new Image<Gray, Byte>(image.Bitmap);
            DataSetImg maxMatchedImg = null; //= DataSet.getInstance().DataSetCards[0];
            double matchRaiting = -1;
            DataSetImg dataSetImg=null;
            for(int k=minImage; k<maxImage; k++)
            {
                dataSetImg = DataSet.getInstance().DataSetCards[k];
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
                //Console.WriteLine("Karta sa imenom " + dataSetImg.PathName + " ima odnos = " + ((double)numberOfMatchedWhitePixels) / numberOfWhitePixels);
                if (((double)numberOfMatchedWhitePixels) / numberOfWhitePixels > matchRaiting)
                {
                    matchRaiting = ((double)numberOfMatchedWhitePixels) / numberOfWhitePixels;
                    maxMatchedImg = dataSetImg;
                    
                }
                
                if ((double)numberOfMatchedWhitePixels / numberOfWhitePixels > 0.9)
                {
                    over.Add(k);
                }
                 
            }

            
            if (maxMatchedImg.PathName.Contains("11") && over.Count>0)
            {
                matchRaiting = -1;
                dataSetImg = null;
                foreach (int i in over)
                {
                    dataSetImg = DataSet.getInstance().DataSetCards[i];
                    int numberOfPixels = 0;
                    int numberOfMatchedPixels = 0;
                    for (int j = 0; j < dataSetImg.Image.Width; j++)
                    {
                        for (int l = 0; l < dataSetImg.Image.Height; l++)
                        {
                            numberOfPixels++;
                            if (decissionImg[l, j].Intensity == dataSetImg.Image[l, j].Intensity)
                            {
                                numberOfMatchedPixels++;
                            }
                        }
                    }

                    //Console.WriteLine("2.Karta sa imenom " + dataSetImg.PathName + " ima odnos = " + ((double)numberOfMatchedPixels) / numberOfPixels);
                    if (((double)numberOfMatchedPixels) / numberOfPixels > matchRaiting)
                    {
                        matchRaiting = ((double)numberOfMatchedPixels) / numberOfPixels;
                        maxMatchedImg = dataSetImg;
                        
                    }

                }
            }
            
            lock (result)
            {
                
                    result.Add(matchRaiting, maxMatchedImg.PathName);

                
            }
        }
    }
}
