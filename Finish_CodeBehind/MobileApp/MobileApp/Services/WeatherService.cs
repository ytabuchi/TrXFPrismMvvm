using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Services
{
    class WeatherService : IWeatherService
    {
        static HttpClient _httpClient = new HttpClient();

        public async Task<ObservableCollection<Weather>> GetWeathersAsync()
        {
            try
            {
                // サイトからデータを取得
                var response = await _httpClient.GetAsync("https://weatherforecastsampleforprism.azurewebsites.net/weatherforecast");
                // レスポンスコード（200 など）を確認
                response.EnsureSuccessStatusCode();
                
                // レスポンスからコンテンツ（JSON）を取得
                var json = await response.Content.ReadAsStringAsync();
                // Newtonsoft.Json で JSON をデシリアライズ
                return JsonConvert.DeserializeObject<ObservableCollection<Weather>>(json);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
