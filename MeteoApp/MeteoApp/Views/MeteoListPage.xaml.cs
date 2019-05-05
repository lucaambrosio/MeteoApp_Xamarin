using MeteoApp.Views;
using System;

using System.Threading.Tasks;

using Xamarin.Forms;
using Acr;
using Acr.UserDialogs;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Collections;
using Plugin.FirebasePushNotification;

namespace MeteoApp
{
    public partial class MeteoListPage : ContentPage


    {
        public static  ArrayList locations = null;
        public MeteoListPage()
        {
            InitializeComponent();

            BindingContext = new MeteoListViewModel();
        }





        void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                Navigation.PushAsync(new MeteoItemPage()
                {
                    BindingContext = new MeteoItemViewModel(e.SelectedItem as Entry)
                });
            }
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (locations == null)
            {
                locations = new ArrayList();
                locations.AddRange(App.Database.GetItemsAsync().Result.ToArray());
                populateDatabase();


            }
        }

        async void populateDatabase()
        {
                foreach(Location location in locations)
                {
                    Entry en = await GetWeatherAsync(location.Name);
                    ((MeteoListViewModel)BindingContext).addEntry(en);
                 
                }
        }

        

        async void OnItemAdded(object sender, EventArgs e)
        {
            PromptResult pResult = await UserDialogs.Instance.PromptAsync(new PromptConfig
            {
                InputType = InputType.Default,
                OkText = "Add",
                Title = "Add location",
            });

            if (pResult.Ok && !string.IsNullOrWhiteSpace(pResult.Text))
            {
                Location newLocation = new Location();
                newLocation.Name = pResult.Text;               
                Entry en = await GetWeatherAsync(newLocation.Name);
                ((MeteoListViewModel)BindingContext).addEntry(en);
                _=App.Database.InsertItemAsync(newLocation);
            }
        }

  



        public async Task<Entry> GetWeatherAsync(string Nome)
        {
            var httpClient = new HttpClient();
            Task<string> contentsTask = httpClient.GetStringAsync("https://api.openweathermap.org/data/2.5/weather?q=" + Nome + "&units=metric&appid=c200173e4aeed3198803206f96382afe");

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
            if(Appoggio.ActualTemperature > 0)
            {
                FirebasePushNotificationManager f;
                f.
            }

            return Appoggio;
        }



       
    }
}
