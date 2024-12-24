using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using Newtonsoft.Json;

namespace SlangDictionaryApp
{
    public partial class MainWindow : Window
    {
        private const string FilePath = "SlangWords.json";
        private Dictionary<string, string> slangDictionary;

        public MainWindow()
        {
            InitializeComponent();
            LoadDictionary();
        }


        private void LoadDictionary()
        {
            if (File.Exists(FilePath))
            {
                string json = File.ReadAllText(FilePath);
                slangDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(json) ?? new Dictionary<string, string>();
            }
            else
            {
                // Создание нового пустого словаря
                slangDictionary = new Dictionary<string, string>();

                // Запись пустого словаря в файл
                File.WriteAllText(FilePath, JsonConvert.SerializeObject(slangDictionary, Formatting.Indented));

                DefinitionText.Text = "Файл словаря создан.";
            }
        }

        private void SaveDictionary()
        {
            string json = JsonConvert.SerializeObject(slangDictionary, Formatting.Indented);
            File.WriteAllText(FilePath, json);
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string word = SearchBox.Text.Trim();

            if (slangDictionary.TryGetValue(word, out string definition))
            {
                DefinitionText.Text = definition;
            }
            else
            {
                DefinitionText.Text = "Слово не найдено в словаре.";
            }
        }

        private void AddWordButton_Click(object sender, RoutedEventArgs e)
        {
            var addWordWindow = new AddWordWindow(slangDictionary, SaveDictionary);
            addWordWindow.Show();
        }

        private void ViewWordListButton_Click(object sender, RoutedEventArgs e)
        {
            var wordListWindow = new WordListWindow(slangDictionary);
            wordListWindow.Show();
        }

    }
}
