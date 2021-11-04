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
        [JsonProperty("temperature")]
        public int Temperature { get; set; }
        [JsonProperty("summary")]
        public string Summary { get; set; }
    }
}
