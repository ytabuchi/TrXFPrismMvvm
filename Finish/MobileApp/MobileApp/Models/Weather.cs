using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Models
{
    public class Weather
    {
        [JsonProperty("date")]
        public DateTime Date { get; set; }
        [JsonProperty("temperatureC")]
        public int TemperatureCelsius { get; set; }
        [JsonProperty("temperatureF")]
        public int TemperatureFahrenheit { get; set; }
        [JsonProperty("summary")]
        public string Summary { get; set; }
        public string ImageUrl
        {
            get => $"{Summary}.png";
        }
    }
}
