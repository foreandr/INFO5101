using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1_Scrabble
{
    internal class Statistics
    {
        public string file_name;
        public int file_type;
        public CityCatalogue cityCatalogue = new CityCatalogue();

        /*Constructor (file name, file type). The user must specify the file name,
        “Canadacities”, and then determine the file type or extension to be JSON,
        XML, or CSV.
        */
        public Statistics(string _file_name, int _file_type)
        {
            file_name = _file_name;
            file_type = _file_type;

            //You may get the value of the CityCatalogue here in this constructor by calling the DataModeler.Parse method. 
            PopulateCityCatalogue(_file_type);
        }
        public void PopulateCityCatalogue(int fileType)
        {
            Console.WriteLine("USER POPULATING CATALOGUE WITH :" + fileType);
            string choice = "";
            if (fileType == 1)
            {
                choice = "JSON";
            }
            else if (fileType == 2)
            {
                choice = "JSON";
            }
            else
            {
                choice = "JSON";
            }
            DataModeler.ParseDelegate parser = DataModeler.ParseFILE;
            List<CityInfo> list = parser(choice);
            foreach (var city in list)
            {
                try
                {
                    CityInfo cityInfo = city;
                    string key = city.city;
                    cityCatalogue.Add(key, cityInfo);
                }
                catch (Exception ex)
                {
                    // DUPE
                }

            }
        }
        public void PrintAll()
        {
            foreach (KeyValuePair<string, CityInfo> entry in cityCatalogue)
            {
                // do something with entry.Value or entry.Key
                Console.WriteLine(entry.Key + ": " + entry.Value);
            }
        }
        // CITY RELATED
        public void DisplayCityInformation(string city)
        {
            CityInfo new_city = cityCatalogue.GetValue<CityInfo>(city);
            Console.WriteLine(new_city.ToString());
        }
        public void DisplayLargestPopulationCity(string province)
        {
            int maxPop = 0;
            CityInfo target = new CityInfo();
            foreach (KeyValuePair<string, CityInfo> city in cityCatalogue)
            {
                if (city.Value.city != "")
                {
                    if (city.Value.GetProvince().ToLower() == province.ToLower())
                    {
                        if (city.Value.population > maxPop)
                        {
                            maxPop = city.Value.population;
                            target = city.Value;
                        }
                    }
                }

            }
            Console.WriteLine($"Largest city in {province} is {target.ToString()}");

        }
        public void DisplaySmallestPopulationCity(string province)
        {
            int minPop = 100000; // Arbitrary
            CityInfo target = new CityInfo();
            foreach (KeyValuePair<string, CityInfo> city in cityCatalogue)
            {
                if (city.Value.city != "")
                {
                    if (city.Value.GetProvince().ToLower() == province.ToLower())
                    {
                        if (city.Value.population < minPop)
                        {
                            minPop = city.Value.population;
                            target = city.Value;
                        }
                    }
                }

            }
            Console.WriteLine($"Smallest city in {province} is {target}");
        }
        public void CompareCitiesPopluation(string city, string city2)
        {
            CityInfo target = new();
            CityInfo target2 = new();

            foreach (KeyValuePair<string, CityInfo> cty in cityCatalogue)
            {
                if (cty.Key.StartsWith(city))
                {
                    target = cty.Value;
                    break;
                }
            }
            foreach (KeyValuePair<string, CityInfo> cty in cityCatalogue)
            {
                if (cty.Key.StartsWith(city2))
                {
                    target2 = cty.Value;
                    break;
                }
            }
            if (target.population > target2.population)
            {
                Console.WriteLine($"{target.city} has a larger population.");
            }
            else
            {
                Console.WriteLine($"{target2.city} has a larger population.");
            }
            Console.WriteLine($"{target.city} - {target.population}");
            Console.WriteLine($"{target2.city} - {target2.population}");
        }
        public void ShowCityOnMap(string city)
        {
            // ???????????????????????????????
        }
        public double distanceFormula(double lat1, double lon1,  double lat2, double lon2)
        {
            return Math.Acos(Math.Sin(lat1) * Math.Sin(lat2) + Math.Cos(lat1) * Math.Cos(lat2) * Math.Cos(lon2 - lon1)) * 6371;
        }
        public void CalculateDistanceBetweenCities(string city1, string city2) {

            CityInfo new_city = cityCatalogue.GetValue<CityInfo>(city1);
            CityInfo new_city2 = cityCatalogue.GetValue<CityInfo>(city2);
            Console.WriteLine($"Distance between {city1} and {city2} is: {distanceFormula(new_city.lat, new_city.lng, new_city2.lat, new_city2.lng)}");
        }
        public void CalculateDistanceToCapital(string city1)
        {
            CityInfo new_city = cityCatalogue.GetValue<CityInfo>(city1);
            string province = new_city.GetProvince();
            string capital = GetCapitalCity(province);

            CityInfo new_city2 = cityCatalogue.GetValue<CityInfo>(capital);
            Console.WriteLine($"Distance between {city1} and {capital} is: {distanceFormula(new_city.lat, new_city.lng, new_city2.lat, new_city2.lng)}");

        }
        // PROVINCE RELATED
        public void DisplayProvincePopulation(string province)
        {
            int total_population = 0 ; 
            foreach (KeyValuePair<string, CityInfo> city in cityCatalogue)
            {
                if (city.Value.region == province)
                {
                    total_population += city.Value.population;
                }
            }
            Console.WriteLine($"{province} population is : {total_population}");
        }
        public void DisplayProvinceCities(string province)
        {
            foreach (KeyValuePair<string, CityInfo> city in cityCatalogue)
            {
                if (city.Value.region == province)
                {
                    Console.WriteLine(city.Value.city);
                }
            }
        }
        public void RankProvincesByPopulation() 
        {
            Dictionary<string, int> populationDict = new Dictionary<string, int>();
            foreach (KeyValuePair<string, CityInfo> city in cityCatalogue)
            {
                if (city.Value.city != "")
                {
                    if (!populationDict.ContainsKey(city.Key))
                    {
                        populationDict.Add(city.Key, city.Value.population);
                    }
                    else
                    {
                        populationDict[city.Key] = city.Value.population;
                    }
                    
                    
                }      
            }
            var sortedDict = from entry in populationDict orderby entry.Value ascending select entry;
            foreach (KeyValuePair<string, int> population in sortedDict)
            {
                Console.WriteLine(population.Key + ": " + population.Value);
            }
            /* 
            SortedSet<CityInfo> sortedSet = new SortedSet<CityInfo>(new OrderByPopulationDesc());// and the class OrderBySalary
            foreach (KeyValuePair<string, CityInfo> city in cityCatalogue)
            {
                sortedSet.Add(city.Value);
            }
            foreach(CityInfo city_ in sortedSet)
            {
                Console.WriteLine(city_.population);
            }*/

        }
        public void RankProvincesByCities()
        {

        }
        public void getCapital(string province)
        {
            string city = GetCapitalCity(province);
            CityInfo new_city = cityCatalogue.GetValue<CityInfo>(city);
            double lat = new_city.lat;
            double lng = new_city.lng;
            Console.WriteLine($"{city}: LAT={lat} | LNG={lng}");
        }
        public string GetCapitalCity(string province)
        {
            string city = "";
            if (province == "Newfoundland and Labrador")
            {
                city = "St. John's";
            }
            else if (province == " Nova Scotia")
            {
                city = "Halifax";
            }
            else if (province == "New Brunswick")
            {
                city = "Fredericton";
            }
            else if (province == "Prince Edward Island")
            {
                city = " Charlottetown";
            }
            else if (province == "Quebec")
            {
                city = " Québec";
            }
            else if (province == "Ontario")
            {
                city = "Toronto";
            }
            else if (province == "Manitoba")
            {
                city = "Winnipeg";
            }
            else if (province == "Saskatchewan")
            {
                city = "Regina";
            }
            else if (province == "Alberta")
            {
                city = "Edmonton";
            }
            else if (province == "British Columbia")
            {
                city = "Victoria";
            }
            else if (province == "Nunavut")
            {
                city = "Iqaluit";
            }
            else if (province == "Northwest Territories")
            {
                city = "Yellowknife";
            }
            else if (province == "Yukon")
            {
                city = "Whitehorse";
            }
            return city;
        }
    }
    public class OrderByPopulationDesc : IComparer<CityInfo> 
    {
        // sort players in ascending order according to their population properties
        public int Compare(CityInfo a, CityInfo b)
        {
            if ((a.population == b.population) && (a.population == b.population))
                return 0;
            if ((a.population > b.population) || ((a.population == b.population) && (a.population > b.population)))
                return -1;

            return 1;
        }
    }


}
