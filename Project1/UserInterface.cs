using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    public class UserInterface
    {
        public void RESTART()
        {
            PrintTitle();
            FileOptions();
            GetFileChoice();
        }
        public void PrintTitle()
        {
            Console.WriteLine("======================");
            Console.WriteLine("Program Functionality");
            Console.WriteLine("======================");
        }
        public void FileOptions()
        {
            Console.WriteLine("- To exit the program, enter `exit` at any point");
            Console.WriteLine("- To restart the program, enter `restart` at any point");
            Console.WriteLine("- You will be presented with a numbered list of options. Please enter a value, when prompted,\n to a coresponding file name, file type, or data querying routine.\n Fetching a list of available file names to be processed and queried...");
        }
        public void PrintFileChoices()
        {
            Console.WriteLine("");
            Console.WriteLine("1) canadiancities-CSV");
            Console.WriteLine("2) canadiancities-JSON");
            Console.WriteLine("3) canadiancities-XML");
            Console.WriteLine("Select an option from the list above(e.g. 1, 2, 3) ");
        }
        public List<string> CreateOptionsList()
        {
            List<string> options = new List<string>();
            options.Add("1");
            options.Add("2");
            options.Add("3");
            options.Add("restart");
            options.Add("exit");
            return options;
        }
        public int GetFileChoice()
        {
            PrintFileChoices();
            string choice = CustomInput<string>();
            List<string> options = CreateOptionsList();


            while (!(options.Contains(choice)))
            {
                PrintFileChoices();
                choice = CustomInput<string>();

            }
            

            return TryStringtoIntCast(choice);
        }

        public string GetCityChoice()
        {
            Console.Write("Type in a Canadian City:");
            return CustomInput<string>();
        }
        public string GetProvinceChoice()
        {
            Console.Write("Type in a Canadian Province:");
            return CustomInput<string>();
        }
        public int TryStringtoIntCast(string myString)
        {
            try
            {
                int result = Int32.Parse(myString);
                // Console.WriteLine("teeeeeest",result);
                return result;
            }
            catch (FormatException)
            {
                Console.WriteLine($"Unable to parse '{myString}'");
            }
            return -1;
        }
        public string CustomInput<T>() where T : System.IComparable<T>
        {
            string value = Console.ReadLine();
            if (value == "exit")
            {
                Environment.Exit(0);
                return "";
            }
            else if (value == "restart")
            {
                Console.WriteLine("\n");
                RESTART();
                Environment.Exit(0);
                return ""; // should never get here

            }
            else
            {
                return value;
            }
        }
        public void CatalogueInfo(int choice)
        {
            string choiceString = "";
            if (choice == 1)
            {
                choiceString = "-CSV.csv";
                
            }
            else if (choice == 2)
            {
                choiceString = "-JSON.json";
            }
            else
            {
                choiceString = "-XML.xml";
            }
            Console.WriteLine($"\nA city catalogue has now been poopulated from the candiancities{choiceString} file");
            Console.WriteLine($"Fetching list of available data querying routines that can be run on candiancities{choiceString} file");
        }
        public void PrintQueryChoices()
        {
            Console.WriteLine("");
            Console.WriteLine("1) Display City Information");
            Console.WriteLine("2) Display Largest Population City");
            Console.WriteLine("3) Display Smallest Population City");
            Console.WriteLine("4) Compare the Populations of Two Cities");
            Console.WriteLine("5) Calculate the Distance Between Two Cities");
            Console.WriteLine("6) Calculate the Distance To the Provincial Capital");
            Console.WriteLine("7) Display Province Population");
            Console.WriteLine("8) List Cities of Province");
            Console.WriteLine("9) Rank Provinces by Population");
            Console.WriteLine("10) Rank Provinces by Number of Cities");
            Console.WriteLine("11) Get Capital City for Province");
            Console.WriteLine("-/- Restart Program and Choose Another File Type to Query");
            Console.WriteLine("Select a data query routine from the list above");
        }
        public List<string> CreateQueryOptionsList()
        {
            List<string> options = new List<string>();
            options.Add("1");
            options.Add("2");
            options.Add("3");
            options.Add("4");
            options.Add("5");
            options.Add("6");
            options.Add("7");
            options.Add("8");
            options.Add("9");
            options.Add("10");
            options.Add("11");
            options.Add("restart");
            options.Add("exit");
            return options;
        }
        public int getQueryChoice()
        {
            PrintQueryChoices();
            string choice = CustomInput<string>();
            List<string> options = CreateQueryOptionsList();


            while (!(options.Contains(choice)))
            {
                PrintQueryChoices();
                choice = CustomInput<string>();

            }

            return TryStringtoIntCast(choice);
        }
        
    }
}
