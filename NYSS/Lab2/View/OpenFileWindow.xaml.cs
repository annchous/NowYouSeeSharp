using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Xml.Serialization;
using Lab2.Model;
using String = System.String;

namespace Lab2.View
{
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
                this.Close();
                var fileNotFoundWindow = new FileNotFoundMessageWindow();
                fileNotFoundWindow.Show();
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
            MainWindow.MainWindowFrame.NavigationService.Navigate(new Uri("View/FullDataGrid.xaml", UriKind.Relative));
        }
    }
}
