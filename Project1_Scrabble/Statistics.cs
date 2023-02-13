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
                Console.WriteLine(entry.Key + " " + entry.Value);
            }
        }
        // CITY RELATED
        public void DisplayCityInformation(string city)
        {
            CityInfo new_city = cityCatalogue.GetValue<CityInfo>(city);
            Console.WriteLine("HERE! " + new_city.ToString());
        }
        public void DisplayLargestPopulationCity(string city)
        {

        }
        public void DisplaySmallestPopulationCity(string city)
        {

        }
        public void CompareCitiesPopluation(string city, string city2)
        {

        }
        public void ShowCityOnMap(string city)
        {
            // ???????????????????????????????
        }
        public void CalculateDistanceBetweenCities(string city)
        {

        }
        public void CalculateDistanceToCapital()
        {

        }
        // PROVINCE RELATED
        public void DisplayProvincePopulation()
        {

        }
        public void DisplayProvinceCities()
        {

        }
        public void RankProvincesByPopulation()
        {

        }
        public void RankProvincesByCities()
        {

        }
        public void GetCapital()
        {

        }
    }
}
