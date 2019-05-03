using System;

namespace MeteoApp
{
    public class Entry
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public double ActualTemperature { get; set; }
        public double MaxTemperature { get; set; }
        public double MinTemperature { get; set; }
        public string icon { get; set; }
        public string State { get; set; }  
        public string Description { get; set; } 


        public override string ToString()
        {
            return string.Format("ID: {0}, Name: {1},Lat: {2},Lon: {3},ActualTemperature: {4},MaxTemperature: {5},MinTemperature: {6},icon: {7},State: {8}, Description{9}", ID, Name, Lat, Lon, ActualTemperature, MaxTemperature, MinTemperature, icon, State, Description);
        }
    }
}