using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GildedTros.App
{
    public static class JsonLoader
    {
        public static string[] LoadJsonArray(string path)
        {
            var json = System.IO.File.ReadAllText(path);
            return JsonConvert.DeserializeObject<string[]>(json);            
        }

        // to expand to read objects from json
    }
}
