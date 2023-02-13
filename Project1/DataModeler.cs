using System.Reflection;
using Newtonsoft.Json;
using CsvHelper;
using System.Globalization;
using System.Xml.Serialization;

namespace Project1
{
    internal class DataModeler
    {
        // 2b)
        internal delegate List<CityInfo> ParseDelegate(string file_type);
        // 2a)
        internal List<CityInfo> ParseJSON()
        {
            Assembly program = Assembly.GetExecutingAssembly();
            string resourceName = program.GetManifestResourceNames().Single(str => str.EndsWith("Canadacities-JSON.json"));
            using (StreamReader r = new StreamReader(program.GetManifestResourceStream(resourceName)!))
            {
                string json = r.ReadToEnd();
                List<CityInfo> items = JsonConvert.DeserializeObject<List<CityInfo>>(json)!;
                return items;
            }
        }
        internal List<CityInfo> ParseCSV()
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
            Assembly program = Assembly.GetExecutingAssembly();
            string resourceName = program.GetManifestResourceNames().Single(str => str.EndsWith("Canadacities-XML.xml"));
            using (TextReader r = new StreamReader(program.GetManifestResourceStream(resourceName)!))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<CityInfo>), new XmlRootAttribute("CanadaCities"));
                List<CityInfo> items = (List<CityInfo>)serializer.Deserialize(r)!;
                return items;
            }
        }

        //2c)
        internal List<CityInfo> ParseFILE(string file_type)
        {
            
            if (file_type == "JSON")
            {
                Console.WriteLine("PARSING JSON");
                return ParseJSON();
            }
            else if (file_type == "XML")
            {
                Console.WriteLine("PARSING XML");
                return ParseXML();
            }
            else
            {
                Console.WriteLine("PARSING CSV");
                return ParseCSV();
            }

        }
    }
}
