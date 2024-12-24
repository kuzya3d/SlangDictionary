using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace TestProject1
{
    [TestClass]
    public class SlangDictionaryTests
    {
        private string testFilePath = "SlangWords.json";
        private Dictionary<string, string> testSlangDictionary;

        [TestInitialize]
        public void Setup()
        {
            testSlangDictionary = new Dictionary<string, string>
            {
                { "Кринж", "Чувство стыда или неловкости за кого-то." },
                { "Чиллить", "Отдыхать, расслабляться." },
                { "Флексить", "Хвастаться или демонстрировать что-то." }
            };

            if (File.Exists(testFilePath))
            {
                File.Delete(testFilePath);
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (File.Exists(testFilePath))
            {
                File.Delete(testFilePath);
            }
        }

        [TestMethod]
        public void Test_LoadDictionary_FileExists()
        {
            // Arrange
            File.WriteAllText(testFilePath, JsonConvert.SerializeObject(testSlangDictionary));
            var app = new SlangDictionaryApp(testFilePath);

            // Act
            app.LoadDictionary();

            // Assert
            Assert.AreEqual(testSlangDictionary.Count, app.slangDictionary.Count);
            foreach (var kvp in testSlangDictionary)
            {
                Assert.AreEqual(kvp.Value, app.slangDictionary[kvp.Key]);
            }
        }
    }
}
