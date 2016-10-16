using ABC.Database.ObjectRepositories;
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

namespace ABC.App.Views.Layouts.UserControls
{
    /// <summary>
    /// Interaction logic for TestScheduleWindow.xaml
    /// </summary>
    public partial class TestScheduleWindow : UserControl
    {
        public TestScheduleWindow()
        {
            InitializeComponent();
            this.DataContext = new TestScheduleRepository().getTestSchedules();
        }
    }
}
