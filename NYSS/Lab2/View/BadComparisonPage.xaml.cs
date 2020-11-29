using System;
using System.Windows.Controls;

namespace Lab2.View
{
    public partial class BadComparisonPage : Page
    {
        private static readonly MainWindow MainWindow = (MainWindow)System.Windows.Application.Current.MainWindow;
        public BadComparisonPage(String message)
        {
            InitializeComponent();
            MessageTextBox.Text = message;
        }
    }
}
