// See https://aka.ms/new-console-template for more information

using System;

namespace Project1_Scrabble
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
            int query_choice = ui.getQueryChoice();
            Console.WriteLine("QUERY CHOICE " + query_choice);
                   
            // presumabely if they click 1,2,3 then they have specified they want canadian cities?
            Statistics stats = new Statistics("Canadiancities", fileType);
            // stats.PrintAll();
            stats.DisplayCityInformation("Selkirk");

        }




        //Create a non-generic class called CityInfo that will hold information about the city. 



    }
}