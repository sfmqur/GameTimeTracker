using System.IO;
using Newtonsoft.Json;

namespace GameTimeTracker
{
    public class JsonHandler
    {
        private string myDocsPath;
        private const string dataFolder = "GameTimeTracker";
        private const string dataFileName = "GameTimeData.json";
        private string dataFilePath;

        public JsonHandler() {
            myDocsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            dataFilePath = $"{myDocsPath}\\{dataFolder}\\{dataFileName}";
            LoadData();
        }

        public JsonDTO JsonData { get; set; }

        public void LoadData()
        {
            if (!Directory.Exists(myDocsPath+"\\"+dataFolder) || !File.Exists(dataFilePath)) {
                JsonData = new JsonDTO(new List<string>(), new List<Game>());
                return;
            }
            JsonDTO? jsonIn; 
            using (var fs = File.OpenText($"{myDocsPath}\\{dataFolder}\\{dataFileName}"))
            {
                jsonIn = JsonConvert.DeserializeObject<JsonDTO>(fs.ReadToEnd());
            }
            if (jsonIn == null) {
                JsonData = new JsonDTO(new List<string>(), new List<Game>());
                return;
            }
            JsonData = jsonIn; 
        }

        public void SaveData()
        {
            if (!Directory.Exists(myDocsPath + "\\" + dataFolder))
            {
                Directory.CreateDirectory(myDocsPath+"\\"+dataFolder);
            }
            using (var fs = File.CreateText(dataFilePath))
            {
                var jsonOut = JsonConvert.SerializeObject(JsonData, Formatting.Indented);
                fs.Write(jsonOut);
            }
        }
    }
}
