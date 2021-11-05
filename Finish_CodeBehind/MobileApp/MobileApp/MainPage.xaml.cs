using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MobileApp.Services;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace MobileApp
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<Weather> Weathers = new ObservableCollection<Weather>();

        bool _firstAppearing = true;

        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (_firstAppearing)
                GetWeathersAsync();

            _firstAppearing = false;
        }

        async void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count == 0)
                return;

            var current = e.CurrentSelection.FirstOrDefault() as Weather;
            collectionView.SelectedItem = null;

            await Navigation.PushModalAsync(new SecondPage($"{current?.Date:yyyy/MM/dd} は {current?.Temperature}℃ で {current?.Summary} です。" ));
        }

        void SwitchOnToggled(object sender, ToggledEventArgs e)
        {
            button.IsEnabled = e.Value;
            refreshView.IsEnabled = e.Value;
        }

        void GetWeathersButtonOnClicked(object sender, EventArgs e)
        {
            GetWeathersAsync();
        }

        void PullToRefreshing(object sender, EventArgs e)
        {
            button.IsEnabled = false;

            GetWeathersAsync();
            refreshView.IsRefreshing = false;

            button.IsEnabled = true;
        }

        void GetWeathersAsync()
        {
            Weathers.Clear();

            Weathers = new ObservableCollection<Weather>
            {
                new Weather
                {
                    Date = new DateTime(2020,11,1),
                    Summary = "Rainy",
                    Temperature = 20
                },
                new Weather
                {
                    Date = new DateTime(2020,11,2),
                    Summary = "Cloudy",
                    Temperature = 25
                },
                new Weather
                {
                    Date = new DateTime(2020,11,3),
                    Summary = "Sunny",
                    Temperature = 30
                }
            };

            BindingContext = Weathers;
        }

        // static HttpClient _httpClient = new HttpClient();
        // async Task GetWeathersAsync()
        // {
        //     Weathers.Clear();
        //
        //     try
        //     {
        //         // サイトからデータを取得
        //         var response = await _httpClient.GetAsync("https://weatherforecastsampleforprism.azurewebsites.net/weatherforecast");
        //         // レスポンスコード（200 など）を確認
        //         response.EnsureSuccessStatusCode();
        //
        //         // レスポンスからコンテンツ（JSON）を取得
        //         var json = await response.Content.ReadAsStringAsync();
        //         // Newtonsoft.Json で JSON をデシリアライズ
        //         Weathers = JsonConvert.DeserializeObject<ObservableCollection<Weather>>(json);
        //     }
        //     catch (Exception ex)
        //     {
        //         Debug.WriteLine(ex.Message);
        //     }
        //
        //     BindingContext = Weathers;
        // }
        //
    }
}
