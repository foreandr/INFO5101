// See https://aka.ms/new-console-template for more information

using System;

namespace Project1
{
    class Program
    {
        static void Main()
        {

            
            UserInterface ui = new UserInterface();
            ui.PrintTitle();
            ui.FileOptions();
            int fileType = ui.GetFileChoice();

            ui.CatalogueInfo(fileType);
            Statistics stats = new Statistics("Canadiancities", fileType);

            while (true)
            {
                int query_choice = ui.getQueryChoice();
                stats.executeWithQueryChoice(query_choice);
            }
            
            // Console.WriteLine("QUERY CHOICE " + query_choice);
            
            
            // presumabely if they click 1,2,3 then they have specified they want canadian cities?
            // stats.PrintAll();
            //stats.DisplayCityInformation("Selkirk");
            //stats.DisplayLargestPopulationCity("Manitoba");
            //stats.DisplaySmallestPopulationCity("Manitoba");
            //stats.DisplayProvincePopulation("Manitoba");
            //stats.RankProvincesByPopulation();
            //stats.DisplayProvinceCities("Manitoba");
            // stats.RankProvincesByPopulation();
            // stats.getCapital("Manitoba");

        }

        //Create a non-generic class called CityInfo that will hold information about the city. 

    }
}