using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    /// <summary>
    /// Dictionary generic type called CityCatalogue
    // holds cities' information returned from the DataModeler class.
    /// </summary>
    internal class CityCatalogue : IEnumerable
    {
        private Dictionary<string, CityInfo> custom_dict = new Dictionary<string, CityInfo>();

        public void Add(string key, CityInfo value)
        {
            custom_dict.Add(key, value);
        }

        public T? GetValue<T>(string key) where T : class
        {
            return custom_dict[key] as T;
        }
        public void ClearDict()
        { custom_dict.Clear(); }

        public int Count
        { get { return custom_dict.Count; } }

        // Foreach enumeration support.
        IEnumerator IEnumerable.GetEnumerator()
        { return custom_dict.GetEnumerator(); }

    }
}
