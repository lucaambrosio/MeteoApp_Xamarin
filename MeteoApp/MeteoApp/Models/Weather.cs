using System;
using System.Collections.Generic;
using System.Text;

namespace MeteoApp
{
    public class Weather
    {
        public Location Location { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public double Temp { get; set; }
        public double MaxTemp { get; set; }
        public double MinTemp { get; set; }
    }
}

