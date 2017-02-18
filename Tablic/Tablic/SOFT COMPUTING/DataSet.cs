using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;

namespace ORIProjekat
{
    public class DataSet
    {
        private static DataSet instance;
        private List<DataSetImg> dataSetCards;
        
        private DataSet()
        {
            dataSetCards = new List<DataSetImg>();

            for (int i = 1; i < 5; i++)
            {
                for (int j = 2; j < 15; j++)
                {
                    DataSetImg currentImg = new DataSetImg("DATASETNORMAL\\DataSet-img" + j + "_" + i + ".jpg");
                    dataSetCards.Add(currentImg);
                    DataSetImg currentImg2 = new DataSetImg("DATASETOKRENUTE\\DataSet-img" + j + "_" + i + ".jpg");
                    dataSetCards.Add(currentImg2);
                }
            }
        }
        public static DataSet getInstance()
        {
            if (instance == null)
            {
                instance = new DataSet();
            }
            return instance;
        }

        public List<DataSetImg> DataSetCards
        {
            get
            {
                return dataSetCards;
            }
        }
    }
}
