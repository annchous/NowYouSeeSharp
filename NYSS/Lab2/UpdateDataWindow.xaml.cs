using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
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
using Lab2.Model;

namespace Lab2
{
    public partial class UpdateDataWindow : Window
    {
        private static readonly MainWindow MainWindow = (MainWindow)System.Windows.Application.Current.MainWindow;
        public UpdateDataWindow()
        {
            InitializeComponent();
        }

        private void UpdateDataWindow_Initialized(object sender, EventArgs e)
        {
            const String link = @"https://bdu.fstec.ru/files/documents/thrlist.xlsx";
            var webClient = new WebClient();
            webClient.DownloadFile(new Uri(link), "thrlist.xlsx");
            MainWindow.FilePath = "thrlist.xlsx";

            try
            {
                var result = DataComparator
                    .Compare(MainWindow.Data, new ObservableCollection<Data>(Parser.EnumerateFullData(MainWindow.FilePath)));
                var comparisonPage = new ComparisonPage(result);
                UpdateDataWindowFrame.Navigate(comparisonPage);
            }
            catch (Exception exception)
            {
                var badComparisonPage = new BadComparisonPage(exception.Message);
                UpdateDataWindowFrame.Navigate(badComparisonPage);
            }
        }
    }
}
