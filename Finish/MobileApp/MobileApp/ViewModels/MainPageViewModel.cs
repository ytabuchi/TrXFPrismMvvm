using MobileApp.Models;
using MobileApp.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly IWeatherService _weatherService;

        public ObservableCollection<Weather> Weathers { get; set; } = new ObservableCollection<Weather>();

        private bool canClick = true;
        public bool CanClick
        {
            get { return canClick; }
            set { SetProperty(ref canClick, value); }
        }

        public DelegateCommand GetWeathersCommand { get; private set; }

        public MainPageViewModel(INavigationService navigationService,
                                 IWeatherService weatherService)
            : base(navigationService)
        {
            Title = "Main Page";
            _weatherService = weatherService;

            GetWeathersCommand = new DelegateCommand(
                async () => await GetWeathersAsync(),
                () => CanClick)
                .ObservesCanExecute(() => CanClick);
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            await GetWeathersAsync();
        }

        private async Task GetWeathersAsync()
        {
            CanClick = false;

            Weathers.Clear();
            // サービスの GetWeathersAsync メソッドをコールし、一時的に保存
            var tempWeathers = await _weatherService.GetWeathersAsync();
            // View から参照できるようにプロパティに流し込み
            if (tempWeathers != null)
            {
                foreach (var weather in tempWeathers)
                {
                    Weathers.Add(weather);
                }
            }

            CanClick = true;
        }
    }
}
