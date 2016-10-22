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
using System.Windows.Shapes;
using ABC.Database.ObjectRepositories;
using ABC.Database.Objects;

namespace ABC.App.Views.Layouts.UserControls
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            StudentRepository sr = new StudentRepository();
            Student st = new Student
            {
                PersonalId = txt_cmnd.Text,
                FullName = txt_name.Text,
                Birthday = dt_ngaysinh.SelectedDate.Value,
                PhoneNumber = txt_sdt.Text
            };
            sr.Add(st);
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}
