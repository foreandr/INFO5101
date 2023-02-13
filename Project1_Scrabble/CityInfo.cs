using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1_Scrabble
{   
    // 1a)
    public class CityInfo
    {
        public long id;
        public string city;
        public string city_ascii;
        public int population;
        public string region;
        public double lat;
        public double lng;

        //1 c)
        public CityInfo() { } // not sure what is suppposed to be in the construcor?

        // 1b)
        public string GetProvince()
        {
            return region;
        }
        public int GetPopulation()
        {
            return population;
        }
        public List<double> GetLocation()
        {
            List<double> location = new List<double>();
            location.Add(lat);
            location.Add(lng);
            return location;
        }
        public Dictionary<string, CityInfo> asObject()
        {
            Dictionary<string, CityInfo> returnDict = new Dictionary<string, CityInfo>();
            returnDict.Add(this.city, this);
            return returnDict;
        }
        public override string ToString()
        {
            return $"|{id}|{city}|{city_ascii}|{population}|{region}|{lat}|{lng}";
        }
    }
}
