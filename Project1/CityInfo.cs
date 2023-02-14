using CsvHelper.Configuration.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Project1
{
    [Serializable, XmlRoot("CanadaCities"), XmlType("CanadaCity")]
    public class CityInfo
    {
        [XmlElement("city")]
        [Name("city")]
        [JsonProperty(PropertyName = "city")]
        public string CityName { get; set; }
        
        [XmlElement("city_ascii")]
        [Name("city_ascii")]
        [JsonProperty(PropertyName = "city_ascii")]
        public string CityAscii { get; set; }
        
        [XmlElement("population")]
        [Name("population")]
        [JsonProperty(PropertyName = "population")]
        public int Population { get; set; }
        
        [XmlElement("region")]
        [Name("region")]
        [JsonProperty(PropertyName = "region")]
        public string Province { get; set; }
        
        [XmlElement("lat")]
        [Name("lat")]
        [JsonProperty(PropertyName = "lat")]
        public double Latitude { get; set; }

        [Name("lng")]
        [JsonProperty(PropertyName = "lng")]
        public double Longitude { get; set; }
        
        [XmlElement("id")]        
        [Name("id")]
        [JsonProperty(PropertyName = "id")]
        public int CityID { get; set; }
        [XmlElement("capital")]        
        [Name("capital")]
        [JsonProperty(PropertyName = "capital")]
        public string Capital { get; set; }

        public CityInfo()
        {
            CityID = 0;
            CityName = "";
            CityAscii = "";
            Population = 0;
            Province = "";
            Latitude = 0;
            Longitude = 0;
            Capital = "";
        }
        public CityInfo
            (
                [Name("id")] int cityID,
                [Name("city")] string cityName,
                [Name("city_ascii")] string cityAscii,
                [Name("population")] int population,
                [Name("region")] string province,
                [Name("lat")] double latitude,
                [Name("lng")] double longitude,
                [Name("lng")] string capital
            )
        {
            CityID = cityID;
            CityName = cityName;
            CityAscii = cityAscii;
            Population = population;
            Province = province;
            Latitude = latitude;
            Longitude = longitude;
            Capital = capital;
        }

        public string GetProvince()
        {
            return Province;
        }
        public int GetPopulation()
        {
            return Population;
        }
        public List<double> GetLocation()
        {
            List<double> location = new List<double>();
            location.Add(Latitude);
            location.Add(Longitude);
            return location;
        }
        public override string ToString()
        {
            return $"{CityName}|{CityAscii}|{Population}|{Province}|{Latitude}|{Longitude}|{CityID}";
        }
    }
}