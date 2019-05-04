using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeteoApp
{
    public class Location
    {

        public int ID { get; set; }
        public string Name { get; set; }
        public double Long { get; set; }
        public double Lat { get; set; }
    }
}
