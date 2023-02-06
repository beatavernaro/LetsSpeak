using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using System.Globalization;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Lets_Speak
{
    public enum DatabaseOption { Dictionary }

    public static class Database
    {
       private static readonly string filePath = AppDomain.CurrentDomain.BaseDirectory;
       private static readonly string fileName = "dictionary.json";



        public static void Save(Dictionary<string, string> dicionario)
        {
            var path = Path.Combine(filePath, fileName);
            var content = System.Text.Json.JsonSerializer.Serialize(dicionario);
            File.WriteAllText(path, content);
        }

        public static Dictionary<string, string> Load()
        {
            var path = Path.Combine(filePath, fileName);

            if (!File.Exists(path))
                Save(new Dictionary<string, string>());

            var fileContent = File.ReadAllText(path);
            var dictionaryReturn = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(fileContent);


            return dictionaryReturn;

        }

        
       
        
    }
}
