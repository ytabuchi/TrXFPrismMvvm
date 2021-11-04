using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Services
{
    public interface IWeatherService
    {
        Task<ObservableCollection<Weather>> GetWeathersAsync();
    }
}
