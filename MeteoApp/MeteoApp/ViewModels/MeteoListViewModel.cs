using Newtonsoft.Json.Linq;
using Plugin.Geolocator;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace MeteoApp
{
    public class MeteoListViewModel : BaseViewModel
    {
         ObservableCollection<Entry> _entries;

        public ObservableCollection<Entry> Entries
        {
            get { return _entries; }
            set
            {
                _entries = value;
                OnPropertyChanged();
            }
        }

        public void addLocation(Location location)
        {
            Entry entry = new Entry();
            entry.ID = location.ID;
            entry.Name = location.Name;
            _entries.Add(entry);
        }

        public void addEntry(Entry entry)
        {
            _entries.Add(entry);
        }

        public MeteoListViewModel()
        {
            Entries = new ObservableCollection<Entry>();
            Entry curLocation = new Entry();
            curLocation.ID = 0;
            curLocation.Name = "Current location";

            GetLocation();
            _entries.Add(curLocation);
  
        }

        async void GetLocation()
        {
            var locator = CrossGeolocator.Current;

            var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));

            Task<Entry> task = GetWeatherAsyncLatLongAsync(position.Latitude, position.Longitude);
            var appogio = await task;

            Debug.WriteLine("ciaooo" + appogio.Name);

            Debug.WriteLine("Position Status: {0}", position.Timestamp);

            Debug.WriteLine("Position Latitude: {0}", position.Latitude);

            Debug.WriteLine("Position Longitude: {0}", position.Longitude);

            Entries.RemoveAt(0);
            Entries.Insert(0, appogio);
        }

        private async Task<Entry> GetWeatherAsyncLatLongAsync(double latitude, double longitude)
        {


            Debug.WriteLine("--------------------------------CIAO-----------------------------------------------------------");

            var httpClient = new HttpClient();
            Task<string> contentsTask = httpClient.GetStringAsync("https://api.openweathermap.org/data/2.5/weather?lat=" + latitude + "&lon=" + longitude + "&units=metric&appid=c200173e4aeed3198803206f96382afe");
            Console.WriteLine(contentsTask);
            var content = await contentsTask;
            var Appoggio = new Entry
            {
                Lat = (double)JObject.Parse(content)["coord"]["lat"],
                Lon = (double)JObject.Parse(content)["coord"]["lon"],
                Description = (string)JObject.Parse(content)["weather"][0]["description"],
                icon = "http://openweathermap.org/img/w/" + (string)JObject.Parse(content)["weather"][0]["icon"] + ".png",
                ActualTemperature = (double)JObject.Parse(content)["main"]["temp"],
                MinTemperature = (double)JObject.Parse(content)["main"]["temp_min"],
                MaxTemperature = (double)JObject.Parse(content)["main"]["temp_max"],
                Name = (string)JObject.Parse(content)["name"],
                State = (string)JObject.Parse(content)["sys"]["country"]
            };
            Debug.WriteLine(Appoggio.ToString());
            return Appoggio;
        }
    }
}
