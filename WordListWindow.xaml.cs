using System.Collections.Generic;
using System.Windows;

namespace SlangDictionaryApp
{
    public partial class WordListWindow : Window
    {
        private readonly Dictionary<string, string> _slangDictionary;

        public WordListWindow(Dictionary<string, string> slangDictionary)
        {
            InitializeComponent();
            _slangDictionary = slangDictionary;
            WordListBox.ItemsSource = _slangDictionary.Keys;
        }

        private void WordListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // получ выбранное слово из списка
            if (WordListBox.SelectedItem is string selectedWord)
            {
                // поиск опр этого слова в словаре
                if (_slangDictionary.TryGetValue(selectedWord, out string definition))
                {
                    // вывод 
                    DefinitionText.Text = $"Определение: {definition}";
                }
                else
                {
                    DefinitionText.Text = "Определение не найдено.";
                }
            }
        }
    }
}
