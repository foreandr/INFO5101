using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    internal class Statistics
    {
        public string file_name;
        public int file_type;
        UserInterface ui = new UserInterface();
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
            switch (fileType)
            {
                case 1: choice = "CSV"; break;
                case 2: choice = "JSON"; break;
                case 3: choice = "XML"; break;
            }
            DataModeler modeler = new();
            DataModeler.ParseDelegate parser = modeler.ParseFILE;
            List<CityInfo> list = parser(choice);
            foreach (var city in list)
            {
                try
                {
                    CityInfo cityInfo = city;
                    string key = city.CityName + ", " + city.Province;
                    cityCatalogue.Add(key, cityInfo);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
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
        public void DisplayCityInformation(string input)
        {
            foreach (KeyValuePair<string, CityInfo> city in cityCatalogue)
            {
                if (city.Key.StartsWith(input))
                {
                    Console.WriteLine(city.Value);
                }
            }
        }
        public void DisplayLargestPopulationCity(string province)
        {
            int maxPop = 0;
            CityInfo target = new();
            foreach (KeyValuePair<string, CityInfo> city in cityCatalogue)
            {
                if (city.Value.Province.ToLower() == province.ToLower())
                {
                    if (city.Value.Population > maxPop)
                    {
                        maxPop = city.Value.Population;
                        target = city.Value;
                    }
                }
            }
            Console.WriteLine($"Largest city in {province} is {target}");
        }
        public void DisplaySmallestPopulationCity(string province)
        {
            int minPop = 100000; // Arbitrary
            CityInfo target = new();
            foreach (KeyValuePair<string, CityInfo> city in cityCatalogue)
            {
                if (city.Value.Province.ToLower() == province.ToLower())
                {
                    if (city.Value.Population < minPop)
                    {
                        minPop = city.Value.Population;
                        target = city.Value;
                    }
                }
            }
            Console.WriteLine($"Largest city in {province} is {target}");
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
            if (target.Population > target2.Population)
            {
                Console.WriteLine($"{target.CityName} has a larger population.");
            }
            else
            {
                Console.WriteLine($"{target2.CityName} has a larger population.");
            }
            Console.WriteLine($"{target.CityName} - {target.Population}");
            Console.WriteLine($"{target2.CityName} - {target2.Population}");
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

            CityInfo new_city = cityCatalogue.GetValue<CityInfo>(city1)!;
            CityInfo new_city2 = cityCatalogue.GetValue<CityInfo>(city2)!;
            Console.WriteLine($"Distance between {city1} and {city2} is: {distanceFormula(new_city.Latitude, new_city.Longitude, new_city2.Latitude, new_city2.Longitude)}");
        }
        public void CalculateDistanceToCapital(string city1)
        {
            //Console.WriteLine("CITY TEST:" + city1);
            CityInfo new_city = cityCatalogue.GetValue<CityInfo>(city1)!; // seems to be an issue here
            //Console.WriteLine("CITY TEST2:" + new_city.ToString());

            string province = new_city.GetProvince();
            string capital = GetCapitalCity(province);

            CityInfo new_city2 = cityCatalogue.GetValue<CityInfo>(capital)!;
            Console.WriteLine($"Distance between {city1} and {capital} is: {distanceFormula(new_city.Latitude, new_city.Longitude, new_city2.Latitude, new_city2.Longitude)}");

        }
        // PROVINCE RELATED
        public void DisplayProvincePopulation(string province)
        {
            int total_population = 0 ; 
            foreach (KeyValuePair<string, CityInfo> city in cityCatalogue)
            {
                if (city.Value.Province == province)
                {
                    total_population += city.Value.Population;
                }
            }
            Console.WriteLine($"{province} population is : {total_population}");
        }
        public void DisplayProvinceCities(string province)
        {
            foreach (KeyValuePair<string, CityInfo> city in cityCatalogue)
            {
                if (city.Value.Province == province)
                {
                    Console.WriteLine(city.Value.CityName);
                }
            }
        }
        public void RankProvincesByPopulation() 
        {
            Dictionary<string, int> provinces = new();
            foreach (KeyValuePair<string, CityInfo> city in cityCatalogue)
            {
                if (provinces.TryAdd(city.Value.Province, 0) && city.Value.Province != "")
                {
                    provinces[city.Value.Province] = city.Value.Population;
                }
                else
                {
                    provinces[city.Value.Province] += city.Value.Population;
                }
            }

            List<KeyValuePair<string, int>> sortedProvinces = new();
            foreach (KeyValuePair<string, int> province in provinces)
            {
                sortedProvinces.Add(province);
            }
            sortedProvinces = sortedProvinces.OrderByDescending(provinceCountPair => provinceCountPair.Value).ToList();
            Console.WriteLine("Ranking Provinces by Population of Cities");
            foreach (KeyValuePair<string, int> pair in sortedProvinces)
            {
                Console.WriteLine($"{pair.Key} with {pair.Value} people");
            }

        }
        public void RankProvincesByCities()
        {
            Dictionary<string, int> provinces = new();
            foreach (KeyValuePair<string, CityInfo> city in cityCatalogue)
            {
                if(provinces.TryAdd(city.Value.Province, 0))
                {
                    provinces[city.Value.Province] = 1;
                } else
                {
                    provinces[city.Value.Province] = provinces[city.Value.Province] + 1;
                }
            }

            List<KeyValuePair<string, int>> sortedProvinces = new();
            foreach (KeyValuePair<string, int> province in provinces)
            {
                sortedProvinces.Add(province);
            }
            sortedProvinces = sortedProvinces.OrderByDescending(provinceCountPair => provinceCountPair.Value).ToList();
            Console.WriteLine("Ranking Provinces by Number of Cities");
            foreach (KeyValuePair<string, int> pair in sortedProvinces)
            {
                Console.WriteLine($"{pair.Key} with {pair.Value} cities");
            }
        }
        public string GetCapitalCity(string province)
        {
            CityInfo target = new();
            foreach (KeyValuePair<string, CityInfo> city in cityCatalogue)
            {
                if(city.Value.Capital != "" && city.Value.Province == province)
                {
                    target = city.Value;
                }
            }
            return $"The capital of {province} is {target.CityName}, at {target.Longitude} and {target.Latitude}";
        }
        public void executeWithQueryChoice(int choice)
        {
            switch (choice)
            {
                case 1: DisplayCityInformation(ui.GetCityChoice()); break;
                case 2: DisplayLargestPopulationCity(ui.GetProvinceChoice()); break;
                case 3: DisplaySmallestPopulationCity(ui.GetProvinceChoice()); break;
                case 4: CompareCitiesPopluation(ui.GetCityChoice(), ui.GetCityChoice()); break;
                case 5: CalculateDistanceBetweenCities(ui.GetCityChoice(), ui.GetCityChoice()); break;
                case 6: CalculateDistanceToCapital(ui.GetCityChoice()); break;
                case 7: DisplayProvincePopulation(ui.GetProvinceChoice()); break;
                case 8: DisplayProvinceCities(ui.GetProvinceChoice()); break;
                case 9: RankProvincesByPopulation(); break;
                case 10: RankProvincesByCities(); break;
                case 11: GetCapitalCity(ui.GetProvinceChoice()); break;
            }
        }
    }
    public class OrderByPopulationDesc : IComparer<CityInfo> 
    {
        // sort players in ascending order according to their population properties
        public int Compare(CityInfo a, CityInfo b)
        {
            if ((a.Population == b.Population) && (a.Population == b.Population))
                return 0;
            if ((a.Population > b.Population) || ((a.Population == b.Population) && (a.Population > b.Population)))
                return -1;

            return 1;
        }
    }


}
