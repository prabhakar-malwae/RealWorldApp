﻿using System;

namespace RealWorldApp.Droid.Model
{
    public class Vehicle
    {
        public string title { get; set; }
        public string description { get; set; }
        public int price { get; set; }

        public string model { get; set; }
        public string engine { get; set; }

        public string color { get; set; }
        public string company { get; set; }

        public DateTime datePosted { get; set; }

        public string condition { get; set; }
        public string location { get; set; }

        public string userid { get; set; }
        public string categoryid { get; set; }
    }
}