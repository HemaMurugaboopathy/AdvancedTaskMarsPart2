using Newtonsoft.Json;
using NUnit.Framework;

namespace AdvancedTaskSpecFlow.Utilities
{
    public class JsonReader
    {
        public static List<T> loadData<T>(String jsonFilename)
        {
            string currentDirectory = TestContext.CurrentContext.TestDirectory;
            string filePath = Path.Combine(currentDirectory, "JSON_Data", jsonFilename);
            using (StreamReader reader = new StreamReader(filePath))
            {
                var jsonContent = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<List<T>>(jsonContent);
            }
        }
    }
}
