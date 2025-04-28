using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Application.Helpers
{
    public interface IWeatherServices
    {
        Task<WeatherResponseDto> GetWeatherByPincodeAsync(string pincode, string countryCode );
    }
    public class WeatherServices:IWeatherServices
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public WeatherServices(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _apiKey = config["OpenWeatherMap:ApiKey"];
        }

        public async Task<WeatherResponseDto> GetWeatherByPincodeAsync(string pincode, string countryCode)
        {
            var response = await _httpClient.GetAsync($"https://api.openweathermap.org/data/2.5/weather?zip={pincode},{countryCode}&units=metric&appid={_apiKey}");

            if (!response.IsSuccessStatusCode)
                throw new Exception("Weather data not found");

            var content = await response.Content.ReadAsStringAsync();
            dynamic weatherData = JsonConvert.DeserializeObject(content);

            return new WeatherResponseDto
            {
                City = weatherData.name,
                Country = weatherData.sys.country,
                Temperature = weatherData.main.temp,
                Weather = weatherData.weather[0].description,
                WindSpeed = weatherData.wind.speed,
                Humidity = weatherData.main.humidity
            };
        }
    }
}

