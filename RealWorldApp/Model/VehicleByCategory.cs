﻿using System;

namespace RealWorldApp.Droid.Model
{
    public class VehicleByCategory
    {
        public int id { get; set; }
        public string title { get; set; }
        public string price { get; set; }
        public string model { get; set; }
        public string location { get; set; }
        public string company { get; set; }

        public DateTime datePosted { get; set; }

        public bool isFeatured { get; set; }

        public string imageUrl { get; set; }
    }
}