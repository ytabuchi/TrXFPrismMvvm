using Prism;
using Prism.Ioc;
using MobileApp.ViewModels;
using MobileApp.Views;
using Xamarin.Essentials.Interfaces;
using Xamarin.Essentials.Implementation;
using Xamarin.Forms;
using MobileApp.Services;

namespace MobileApp
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
#if DEBUG
            containerRegistry.RegisterSingleton<IWeatherService, MockWeatherService>();
#else
            containerRegistry.RegisterSingleton<IWeatherService, WeatherService>();
#endif

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
        }
    }
}
