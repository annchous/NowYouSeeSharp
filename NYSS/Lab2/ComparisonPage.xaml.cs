using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Lab2.Model;

namespace Lab2
{
    public partial class ComparisonPage : Page
    {
        private readonly List<ComparedData> _comparedData;
        private Int32 _currentPage;
        public ComparisonPage(List<ComparedData> comparedData)
        {
            InitializeComponent();
            _comparedData = comparedData;
            totalPages.Text = _comparedData.Count.ToString();
            _currentPage = _comparedData.Count > 0 ? 1 : 0;
            currentPage.Text = _currentPage.ToString();
        }

        private void GoToPageButton_Click(Object sender, RoutedEventArgs e)
        {
            if (!Int32.TryParse(pageNumber.Text, out Int32 pageNumberResult))
            {
                MessageBox.Show("Введённое значение не является числом!", "Ошибка", MessageBoxButton.OK);
                pageNumber.Text = String.Empty;
                return;
            }

            if (pageNumberResult > _comparedData.Count || pageNumberResult < 1)
            {
                MessageBox.Show($"Страницы под номером {pageNumberResult} не существует!", "Ошибка",
                    MessageBoxButton.OK);
                pageNumber.Text = String.Empty;
                return;
            }
            _currentPage = pageNumberResult;
            pageNumber.Text = String.Empty;
            GeneratePageContent(_currentPage);
        }

        private void PreviousPageButton_Click(Object sender, RoutedEventArgs e)
        {
            _currentPage = Int32.Parse(currentPage.Text);
            if (_currentPage > 1)
                GeneratePageContent(_currentPage - 1);
        }

        private void NextPageButton_Click(Object sender, RoutedEventArgs e)
        {
            _currentPage = Int32.Parse(currentPage.Text);
            if (_currentPage < _comparedData.Count)
                GeneratePageContent(_currentPage + 1);
        }

        private void GeneratePageContent(Int32 pageNum)
        {
            var pageContent = _comparedData.Skip(pageNum - 1).Take(1).First();
            var items = new List<ComparisonItem>
            {
                new ComparisonItem()
                {
                    Parameter = "Id", OldParameterValue = pageContent.OldData.Id.ToString(), NewParameterValue = "нет изменений"
                },
                new ComparisonItem() {OldParameterValue = "БЫЛО", NewParameterValue = "СТАЛО"},
                new ComparisonItem()
                {
                    Parameter = "Название угрозы",
                    OldParameterValue = ProcessChange(pageContent.OldData.Name, pageContent.NewData.Name).Key,
                    NewParameterValue = ProcessChange(pageContent.OldData.Name, pageContent.NewData.Name).Value
                },
                new ComparisonItem()
                {
                    Parameter = "Описание угрозы",
                    OldParameterValue = ProcessChange(pageContent.OldData.Description, pageContent.NewData.Description).Key,
                    NewParameterValue = ProcessChange(pageContent.OldData.Description, pageContent.NewData.Description).Value
                },
                new ComparisonItem()
                {
                    Parameter = "Источник угрозы",
                    OldParameterValue = ProcessChange(pageContent.OldData.Source, pageContent.NewData.Source).Key,
                    NewParameterValue = ProcessChange(pageContent.OldData.Source, pageContent.NewData.Source).Value
                },
                new ComparisonItem()
                {
                    Parameter = "Объект воздействия угрозы",
                    OldParameterValue = ProcessChange(pageContent.OldData.Target, pageContent.NewData.Target).Key,
                    NewParameterValue = ProcessChange(pageContent.OldData.Target, pageContent.NewData.Target).Value
                },
                new ComparisonItem()
                {
                    Parameter = "Название угрозы",
                    OldParameterValue = ProcessChange(pageContent.OldData.ConfidentialityOffense, pageContent.NewData.ConfidentialityOffense).Key,
                    NewParameterValue = ProcessChange(pageContent.OldData.ConfidentialityOffense, pageContent.NewData.ConfidentialityOffense).Value
                },
                new ComparisonItem()
                {
                    Parameter = "Название угрозы",
                    OldParameterValue = ProcessChange(pageContent.OldData.ContinuityOffense, pageContent.NewData.ContinuityOffense).Key,
                    NewParameterValue = ProcessChange(pageContent.OldData.ContinuityOffense, pageContent.NewData.ContinuityOffense).Value
                },
                new ComparisonItem()
                {
                    Parameter = "Название угрозы",
                    OldParameterValue = ProcessChange(pageContent.OldData.AvailabilityOffense, pageContent.NewData.AvailabilityOffense).Key,
                    NewParameterValue = ProcessChange(pageContent.OldData.AvailabilityOffense, pageContent.NewData.AvailabilityOffense).Value
                }
            };

            _currentPage = pageNum;
            currentPage.Text = _currentPage.ToString();
            ListBoxData.ItemsSource = items;
        }

        private KeyValuePair<String, String> ProcessChange(String oldValue, String newValue)
        {
            return oldValue == newValue
                ? new KeyValuePair<String, String>(oldValue, "нет изменений")
                : new KeyValuePair<String, String>(oldValue, newValue);
        }
    }
}
