using MeteoApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MeteoApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            

            var nav = new NavigationPage(new MeteoListPage())
            {
                BarBackgroundColor = Color.FromHex("404040"),
                BarTextColor = Color.White
            };

            MainPage = nav;
            
        }
        static Database database;

        public static Database Database
        {
            get
            {
                if (database == null) // se l'istanza è nulla, la creo
                    database = new Database();
                return database; // ritorno l'istanza
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
