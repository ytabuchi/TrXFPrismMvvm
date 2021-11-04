using MobileApp.Models;
using MobileApp.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly IPageDialogService _pageDialogService;
        private readonly IDialogService _dialogService;
        private readonly IWeatherService _weatherService;

        public ObservableCollection<Weather> Weathers { get; set; } = new ObservableCollection<Weather>();

        private bool canClick = true;
        public bool CanClick
        {
            get { return canClick; }
            set { SetProperty(ref canClick, value); }
        }

        public bool IsRefreshing => !CanClick;

        private Weather selectedWeather;
        public Weather SelectedWeather
        {
            get { return selectedWeather; }
            set { SetProperty(ref selectedWeather, value); }
        }

        public DelegateCommand GetWeathersCommand { get; private set; }
        public DelegateCommand SelectWeatherCommand { get; private set; }

        public MainPageViewModel(INavigationService navigationService,
                                 IPageDialogService pageDialogService,
                                 IDialogService dialogService,
                                 IWeatherService weatherService)
            : base(navigationService)
        {
            Title = "Main Page";
            _pageDialogService = pageDialogService;
            _dialogService = dialogService;
            _weatherService = weatherService;

            GetWeathersCommand = new DelegateCommand(
                async () => await GetWeathersAsync(),
                () => CanClick)
                .ObservesCanExecute(() => CanClick);

            SelectWeatherCommand = new DelegateCommand(
                () => ShowSelectedWeather()
                //async () => await _pageDialogService.DisplayAlertAsync(
                //    "Dialog Title",
                //    $"{SelectedWeather.Date:yyyy/MM/dd} は {SelectedWeather.Temperature}℃ で {SelectedWeather.Summary} です。",
                //    "OK")
                );
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

        private void ShowSelectedWeather()
        {
            if (SelectedWeather == null)
                return;

            var parameters = new DialogParameters
            {
                { "Title", "Dialog Title" },
                { "Message", $"{SelectedWeather.Date:yyyy/MM/dd} は {SelectedWeather.Temperature}℃ で {SelectedWeather.Summary} です。" }
            };
            _dialogService.ShowDialog("DemoDialog", parameters);
        }
    }
}
