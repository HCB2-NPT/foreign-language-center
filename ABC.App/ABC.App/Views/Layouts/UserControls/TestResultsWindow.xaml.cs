﻿using ABC.Database.ObjectRepositories;
using ABC.Database.Objects;
using System;
using System.Collections.Generic;
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

namespace ABC.App.Views.Layouts.UserControls
{
    /// <summary>
    /// Interaction logic for TestResultsWindow.xaml
    /// </summary>
    public partial class TestResultsWindow : UserControl
    {
        public TestResultsWindow()
        {
            InitializeComponent();
        }

        private void getResults(object sender, System.Windows.RoutedEventArgs e)
        {
            dg.DataContext = new TestScheduleRepository().CheckTestScore(new Student{PersonalId=PersonalID.Text});
        }

        private void clearResults(object sender, System.Windows.RoutedEventArgs e)
        {
            dg.DataContext = null;
            PersonalID.Clear();
        }
    }
}
