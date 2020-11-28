using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Serialization;
using Lab2.Model;
using String = System.String;

namespace Lab2
{
    /// <summary>
    /// Логика взаимодействия для OpenFileWindow.xaml
    /// </summary>
    public partial class OpenFileWindow : Window
    {
        private static readonly MainWindow MainWindow = (MainWindow)System.Windows.Application.Current.MainWindow;
        public OpenFileWindow()
        {
            InitializeComponent();
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(filePath.Text))
            {
                MessageBox.Show("Путь к файлу не может быть пустым!", "Ошибка", MessageBoxButton.OK);
                filePath.Text = String.Empty;
                return;
            }

            if (!File.Exists(filePath.Text))
            {
                MessageBox.Show($"Файл по пути {filePath.Text} не найден!", "Ошибка", MessageBoxButton.OK);
                filePath.Text = String.Empty;
                return;
            }

            MainWindow.FilePath = filePath.Text;

            var formatter = new XmlSerializer(typeof(ObservableCollection<Data>));
            using (var fs = new FileStream(MainWindow.FilePath, FileMode.OpenOrCreate))
            {
                var data = (ObservableCollection<Data>)formatter.Deserialize(fs);
                MainWindow.Data = data;
            }

            this.Close();
            MainWindow.MainWindowFrame.NavigationService.Navigate(new Uri("FullDataGrid.xaml", UriKind.Relative));
        }
    }
}
