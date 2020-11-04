using MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Services
{
    class MockWeatherService : IWeatherService
    {
        public async Task<ObservableCollection<Weather>> GetWeathersAsync()
        {
            var weathers = new ObservableCollection<Weather>
            {
                new Weather
                {
                    Date = new DateTime(2020,11,1),
                    Summary = "Rainy",
                    TemperatureCelsius = 20
                },
                new Weather
                {
                    Date = new DateTime(2020,11,2),
                    Summary = "Cloudy",
                    TemperatureCelsius = 25
                },
                new Weather
                {
                    Date = new DateTime(2020,11,3),
                    Summary = "Sunny",
                    TemperatureCelsius = 30
                }
            };

            return weathers;
        }
    }
}
