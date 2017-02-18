﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;

namespace SlikaPrepoznavanjeKarata
{
    public class DataSetImg
    {
        private Image<Gray, Byte> image;
        private string pathName;

        public DataSetImg(string path)
        {
            pathName = path;
            image = new Image<Gray, Byte>(pathName);
        }

        public Image<Gray, Byte> Image
        {
            get
            {
                return image;
            }
        }

        public string PathName
        {
            get
            {
                return pathName;
            }
        }
    }
}
