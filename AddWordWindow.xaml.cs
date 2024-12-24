using System;
using System.Collections.Generic;
using System.Windows;

namespace SlangDictionaryApp
{
    public partial class AddWordWindow : Window
    {
        private readonly Dictionary<string, string> _slangDictionary;
        private readonly Action _saveDictionaryCallback;

        public AddWordWindow(Dictionary<string, string> slangDictionary, Action saveDictionaryCallback)
        {
            InitializeComponent();
            _slangDictionary = slangDictionary;
            _saveDictionaryCallback = saveDictionaryCallback;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string newWord = NewWordBox.Text.Trim();
            string newDefinition = NewDefinitionBox.Text.Trim();

            if (!string.IsNullOrEmpty(newWord) && !string.IsNullOrEmpty(newDefinition))
            {
                if (_slangDictionary.ContainsKey(newWord))
                {
                    MessageBox.Show("Такое слово уже существует в словаре!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    _slangDictionary[newWord] = newDefinition;
                    _saveDictionaryCallback?.Invoke(); // сохранение изменения в файл
                    MessageBox.Show("Слово успешно добавлено!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Заполните оба поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            string wordToDelete = NewWordBox.Text.Trim();

            if (_slangDictionary.ContainsKey(wordToDelete))
            {
                _slangDictionary.Remove(wordToDelete);
                _saveDictionaryCallback?.Invoke(); // сохранение изменения в файл
                MessageBox.Show($"Слово '{wordToDelete}' успешно удалено.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Слово не найдено в словаре.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void NewWordBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            NewWordPlaceholder.Visibility = string.IsNullOrEmpty(NewWordBox.Text) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void NewDefinitionBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            NewDefinitionPlaceholder.Visibility = string.IsNullOrEmpty(NewDefinitionBox.Text) ? Visibility.Visible : Visibility.Collapsed;
        }

    }
}
