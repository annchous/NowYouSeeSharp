using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Lab2.Model;

namespace Lab2.View
{
    public partial class ShortDataGrid : Page
    {
        private static readonly MainWindow MainWindow = (MainWindow)System.Windows.Application.Current.MainWindow;
        private ObservableCollection<ShortData> _collection;
        private List<ShortData> _pageContent;
        private const Int32 RowsPerPage = 30;
        private Int32 _currentPage = 1;
        private Int32 PagesCount
            => _collection.Count % RowsPerPage == 0
            ? _collection.Count / RowsPerPage
            : _collection.Count / RowsPerPage + 1;


        public ShortDataGrid()
        {
            InitializeComponent();
        }

        private void GeneratePageContent(Int32 pageNum)
        {
            _pageContent = _collection.Skip((pageNum - 1) * RowsPerPage)
                .Take(RowsPerPage).ToList();
            _currentPage = pageNum;
            currentPage.Text = _currentPage.ToString();
            ShortDataTable.ItemsSource = _pageContent;
        }

        private void NextPageButton_Click(Object sender, RoutedEventArgs e)
        {
            _currentPage = Int32.Parse(currentPage.Text);
            if (_currentPage < PagesCount)
                GeneratePageContent(_currentPage + 1);
        }

        private void PreviousPageButton_Click(Object sender, RoutedEventArgs e)
        {
            _currentPage = Int32.Parse(currentPage.Text);
            if (_currentPage > 1)
                GeneratePageContent(_currentPage - 1);
        }

        private void GoToPageButton_Click(Object sender, RoutedEventArgs e)
        {
            if (!Int32.TryParse(pageNumber.Text, out Int32 pageNumberResult))
            {
                MessageBox.Show("Введённое значение не является числом!", "Ошибка", MessageBoxButton.OK);
                pageNumber.Text = String.Empty;
                return;
            }

            if (pageNumberResult > PagesCount || pageNumberResult < 1)
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

        private void ShortDataGrid_Initialized(Object sender, EventArgs e)
        {
            _collection = new ObservableCollection<ShortData>(Parser.EnumerateShortFromFullData(MainWindow.Data));
            totalPages.Text = PagesCount.ToString();
            currentPage.Text = _currentPage.ToString();
            GeneratePageContent(_currentPage);
        }
    }
}
