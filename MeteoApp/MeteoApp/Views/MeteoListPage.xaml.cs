using Android.Webkit;
using MeteoApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;
using Acr;
using Acr.UserDialogs;
using Android.Support.V7.App;

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

        public async void addCity()
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
                App.Database.SaveItemAsync(newLocation);
                ((MeteoListViewModel)BindingContext).addLocation(newLocation);
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

                 //App.Database.SaveItemAsync(newLocation);

                ((MeteoListViewModel)BindingContext).addLocation(newLocation);
            }
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
