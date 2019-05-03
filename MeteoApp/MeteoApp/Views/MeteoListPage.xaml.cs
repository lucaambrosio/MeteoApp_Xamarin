using MeteoApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MeteoApp
{
    public partial class MeteoListPage : ContentPage
    {
        public MeteoListPage()
        {
            InitializeComponent();

            BindingContext = new MeteoListViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        void addNewElement()
        {
            //PromptResult pResult = await UserDialogs.Instance.PromptAsync(new PromptConfig
            //{
            //    InputType = InputType.Default,
            //    OkText = "Add",
            //    Title = "Add location",
            //});

            //if (pResult.Ok && !string.IsNullOrWhiteSpace(pResult.Text))
            //{
            //    Location newLocation = new Location();
            //    newLocation.Name = pResult.Text;
            //    await GetWeatherAsyncFromName(newLocation);
            //    if (newLocation.Weather != null)
            //    {
            //        Weather temp = newLocation.Weather;
            //        newLocation.Weather = null;
            //        ((MeteoListViewModel)BindingContext).addAndSave(newLocation);
            //        newLocation.Weather = temp;
            //    }
            //}
        }
        void OnItemAdded(object sender, EventArgs e)
        {
            addNewElement();
            Location location = new Location();
            location.Name = "Varese";
            App.Database.SaveItemAsync(location);
            //((MeteoListViewModel)BindingContext).Entries.Add(location);



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
    }
}
