using Newtonsoft.Json;
using System.Xml.Serialization;
using System.Reflection;
using CsvHelper;
using System.Globalization;


namespace Project1_Scrabble
{
    internal class DataModeler
    {
        const string JSON_FILE_LOCATION = "./Data/Canadacities-JSON.json";
        const string CSV_FILE_LOCATION = "./Data/Canadacities.csv";
        const string XML_FILE_LOCATION = "./Data/Canadacities-XML.xml";
        
        // 2b)
        internal delegate List<CityInfo> ParseDelegate(string file_type);
        // 2a)
        internal static List<CityInfo> ParseJSON()
        {
            using (StreamReader r = new StreamReader(JSON_FILE_LOCATION))
            {
                string json = r.ReadToEnd();
                List<CityInfo> items = JsonConvert.DeserializeObject<List<CityInfo>>(json);
                return items;
            }

        }
        internal static List<CityInfo> ParseCSV()
        {
            Assembly program = Assembly.GetExecutingAssembly();
            string resourceName = program.GetManifestResourceNames().Single(str => str.EndsWith("Canadacities.csv"));
            using (StreamReader r = new StreamReader(program.GetManifestResourceStream(resourceName)!))
            using (CsvReader csvReader = new CsvReader(r, CultureInfo.InvariantCulture))
            {
                var items = csvReader.GetRecords<CityInfo>();
                List<CityInfo> output = new();
                foreach (CityInfo item in items)
                {
                    output.Add(item);
                }
                return output;
            }
        }
        internal static List<CityInfo> ParseXML()
        {
            using (StreamReader r = new StreamReader(XML_FILE_LOCATION))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<CityInfo>), new XmlRootAttribute("CanadaCities"));
                List<CityInfo> items = (List<CityInfo>)serializer.Deserialize(r);
                return items;
            }
        }
        //2c)
        internal static List<CityInfo> ParseFILE(string file_type)
        {
            
            if (file_type == "JSON")
            {
                //Console.WriteLine("PARSING JSON");
                return ParseJSON();
            }
            else if (file_type == "XML")
            {
                //Console.WriteLine("PARSING XML");
                return ParseXML();
            }
            else
            {
                //Console.WriteLine("PARSING CSV");
                return ParseCSV();
            }

        }
    }
}
