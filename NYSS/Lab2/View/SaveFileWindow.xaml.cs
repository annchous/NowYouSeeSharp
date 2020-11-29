using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Xml.Serialization;
using Lab2.Model;

namespace Lab2.View
{
    public partial class SaveFileWindow : Window
    {
        private static readonly MainWindow MainWindow = (MainWindow)System.Windows.Application.Current.MainWindow;
        public SaveFileWindow()
        {
            InitializeComponent();
        }

        private void SaveButton_OnClick(Object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(filePath.Text))
            {
                MessageBox.Show("Путь к файлу не может быть пустым!", "Ошибка", MessageBoxButton.OK);
                filePath.Text = String.Empty;
                return;
            }

            if (File.Exists(filePath.Text))
            {
                MessageBox.Show($"Файл по пути {filePath.Text} уже существует!", "Ошибка", MessageBoxButton.OK);
                filePath.Text = String.Empty;
                return;
            }

            MainWindow.FilePath = filePath.Text;

            var formatter = new XmlSerializer(typeof(ObservableCollection<Data>));
            using (var fs = new FileStream(MainWindow.FilePath, FileMode.OpenOrCreate))
                formatter.Serialize(fs, MainWindow.Data);

            this.Close();
            MessageBox.Show("Сохранено!", "Информация", MessageBoxButton.OK);
        }
    }
}
