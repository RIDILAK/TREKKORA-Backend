using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Application.Dto
{
    public class PlaceDto
    {
        public Guid Id { get; set; }
        public string PlaceName { get; set; }
        public string StateId { get; set; }
        public string CountryName { get; set; }

        public string CountryCode { get; set; }
        public string StateName { get; set; }
        public byte[] ImageUrl { get;set; }
        public int MinimumDays { get; set; }
        public string Pincode { get; set; }

    
        public string BestTimeToTravel { get; set; }
      
        public string Description { get; set; }
        public decimal Price { get; set; }
    }

    public class AddPlaceDto
    {
       
        public string PlaceName { get; set; }
      
        public string StateId { get; set; }
        public int MinimumDays { get; set; }

        public string Pincode { get; set; }
      

        public string BestTimeToTravel { get; set; }
        
        public string Description { get; set; }
        public decimal Price { get; set; }
    }

    public class WeatherResponseDto
    {
        public string City { get; set; }
        public string Country { get; set; }
        public float Temperature { get; set; }
        public string Weather { get; set; }
        public float WindSpeed { get; set; }
        public int Humidity { get; set; }
    }

   
}
