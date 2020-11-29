﻿using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows;
using Lab2.Model;

namespace Lab2.View
{
    public partial class FileNotFoundMessageWindow : Window
    {
        private static readonly MainWindow MainWindow = (MainWindow)System.Windows.Application.Current.MainWindow;
        public FileNotFoundMessageWindow()
        {
            InitializeComponent();
        }

        private void NoButton_Click(Object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void YesButton_Click(Object sender, RoutedEventArgs e)
        {
            const String link = @"https://bdu.fstec.ru/files/documents/thrlist.xlsx";
            var webClient = new WebClient();
            webClient.DownloadFile(new Uri(link), "thrlist.xlsx");
            MainWindow.Data = new ObservableCollection<Data>(Parser.EnumerateFullData("thrlist.xlsx"));
            this.Close();
            MainWindow.MainWindowFrame.NavigationService.Navigate(new Uri("View/FullDataGrid.xaml", UriKind.Relative));
        }
    }
}
