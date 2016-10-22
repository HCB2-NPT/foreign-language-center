using ABC.App.Views.Layouts.UserControls;
using ABC.Database.ObjectContexts;
using ABC.Database.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace ABC.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += (s, e) => MyDatabaseContext.Initialize();
            this.Closed += (s, e) => Application.Current.Shutdown();

            // Defaut Function
            var def = new TestScheduleWindow();
            def.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            def.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
            MainContent.Children.Add(def);

            
        }

        private void clickTestSchedules(object sender, RoutedEventArgs e)
        {
            MainContent.Children.Clear();
            MainContent.Children.Add(new TestScheduleWindow());
        }

        private void clickTestResults(object sender, RoutedEventArgs e)
        {
            MainContent.Children.Clear();
            MainContent.Children.Add(new TestResultsWindow());
        }

        private void clickTestRegister(object sender, RoutedEventArgs e)
        {
            MainContent.Children.Clear();
            MainContent.Children.Add(new TestRegisterWindow());
        }

        private void ThemHocVien_Click(object sender, RoutedEventArgs e)
        {
            Window1 w1 = new Window1();
            w1.ShowDialog();
        }

    }
}
