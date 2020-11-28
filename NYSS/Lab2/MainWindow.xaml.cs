using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Lab2.Model;

namespace Lab2
{
    public partial class MainWindow : Window
    {
        public static String FilePath { get; set; }
        public ObservableCollection<Data> Data { get; set; }
        private readonly Timer _timer;

        public MainWindow()
        {
            InitializeComponent();
            _timer = new Timer(UpdateByTimer, null, 0, TimeSpan.FromHours(3).Milliseconds);
        }

        private void FullDataGridView_Click(Object sender, RoutedEventArgs e)
        {
            MainWindowFrame.NavigationService.Navigate(new Uri("FullDataGrid.xaml", UriKind.Relative));
        }

        private void ShortDataGridView_Click(Object sender, RoutedEventArgs e)
        {
            MainWindowFrame.NavigationService.Navigate(new Uri("ShortDataGrid.xaml", UriKind.Relative));
        }

        private void OpenFileWindow_Click(Object sender, RoutedEventArgs e)
        {
            OpenFileWindow openFileWindow = new OpenFileWindow();
            openFileWindow.Show();
        }

        private void DownloadFile_Click(Object sender, RoutedEventArgs e)
        {
            const String link = @"https://bdu.fstec.ru/files/documents/thrlist.xlsx";
            var webClient = new WebClient();
            webClient.DownloadFile(new Uri(link), "thrlist.xlsx");
            Data = new ObservableCollection<Data>(Parser.EnumerateFullData("thrlist.xlsx"));
            MainWindowFrame.NavigationService.Navigate(new Uri("FullDataGrid.xaml", UriKind.Relative));
        }

        private void SaveFile_Click(Object sender, RoutedEventArgs e)
        {
            var saveFileWindow = new SaveFileWindow();
            saveFileWindow.Show();
        }

        private void UpdateData_Click(Object sender, RoutedEventArgs e)
        {
            const String link = @"https://bdu.fstec.ru/files/documents/thrlist.xlsx";
            var webClient = new WebClient();
            webClient.DownloadFile(new Uri(link), "thrlist.xlsx");
            FilePath = "thrlist.xlsx";
            var updateDataWindow = new UpdateDataWindow();
            updateDataWindow.Show();
        }

        private static void UpdateByTimer(Object obj)
        {
            const String link = @"https://bdu.fstec.ru/files/documents/thrlist.xlsx";
            var webClient = new WebClient();
            webClient.DownloadFile(new Uri(link), "thrlist.xlsx");
            FilePath = "thrlist.xlsx";
            var updateDataWindow = new UpdateDataWindow();
            updateDataWindow.Show();
        }
    }
}
