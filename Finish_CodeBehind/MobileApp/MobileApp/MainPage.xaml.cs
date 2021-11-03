using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileApp.Services;
using Xamarin.Forms;

namespace MobileApp
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<Weather> Weathers = new ObservableCollection<Weather>();

        IWeatherService _weatherService;

        public MainPage()
        {
            InitializeComponent();

#if DEBUG
            _weatherService = new MockWeatherService();
#else
            _weatherService = new WeatherService();
#endif
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await GetWeathersAsync();
        }

        async void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count == 0)
                return;

            var current = (e.CurrentSelection.FirstOrDefault() as Weather)?.Date;
            await DisplayAlert("Selected", $"{current} is selected.", "OK");
            collectionView.SelectedItem = null;
        }

        async Task GetWeathersAsync()
        {
            Weathers.Clear();
            Weathers = new ObservableCollection<Weather>(await _weatherService.GetWeathersAsync());
            BindingContext = Weathers;
        }

        async void GetWeathersButtonOnClicked(object sender, EventArgs e)
        {
            await GetWeathersAsync();
        }
    }
}
