using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
    public class SlangDictionaryApp
    {
        public Dictionary<string, string> slangDictionary;
        private string filePath;

        public SlangDictionaryApp(string path)
        {
            filePath = path;
            slangDictionary = new Dictionary<string, string>();
        }

        public void LoadDictionary()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                slangDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(json) ?? new Dictionary<string, string>();
            }
        }
    }

}
