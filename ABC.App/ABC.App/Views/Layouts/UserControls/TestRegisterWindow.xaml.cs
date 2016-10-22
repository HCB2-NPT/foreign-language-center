using ABC.Database.ObjectRepositories;
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
    /// Interaction logic for TestRegisterWindow.xaml
    /// </summary>
    public partial class TestRegisterWindow : UserControl
    {
        private List<int> listthang = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
        private List<int> listquy = new List<int> { 1, 2, 3, 4 };
        public TestRegisterWindow()
        {
            
            InitializeComponent();

            dock_SL.Visibility = System.Windows.Visibility.Collapsed;
            CertificateRepository cr = new CertificateRepository();
            combo_Cert.ItemsSource = cr.All;
            combo_Cert.DisplayMemberPath = "Name";

            if (rad_Thang.IsChecked==true)
                combo_ThangQuy.ItemsSource = listthang;
            else
                combo_ThangQuy.ItemsSource = listquy;

            for (int i = 2000; i < 2050; i++)
            {
                combo_Nam.Items.Add(i);
            }


            combo_Cert.SelectedIndex = 0;
            combo_Nam.SelectedItem = DateTime.Now.Year;
            combo_ThangQuy.SelectedItem = DateTime.Now.Month;

            dg_sl.Visibility = System.Windows.Visibility.Hidden;
            dg_dt.Visibility = System.Windows.Visibility.Hidden;
            dg_kq.Visibility = System.Windows.Visibility.Hidden;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CertificateRepository cr = new CertificateRepository();
            if (rad_soluong.IsChecked == true)
            {
                if (rad_Thang.IsChecked == true)
                {
                    dg_sl.DataContext = cr.ReportNumberStudent_Certificate_Date((Certificate)combo_Cert.SelectedItem, (int)combo_ThangQuy.SelectedItem, (int)combo_Nam.SelectedItem);
                }
                else
                {
                    dg_sl.DataContext = cr.ReportNumberStudent_Certificate_Date_Quy((Certificate)combo_Cert.SelectedItem, (int)combo_ThangQuy.SelectedItem, (int)combo_Nam.SelectedItem);
                }
            }
            else if (rad_doanhthu.IsChecked == true)
            {
                if (rad_Thang.IsChecked == true)
                {
                    dg_dt.DataContext = cr.SumFee_Certificates_Date((Certificate)combo_Cert.SelectedItem, (int)combo_ThangQuy.SelectedItem, (int)combo_Nam.SelectedItem);
                }
                else
                {
                    dg_dt.DataContext = cr.SumFee_Certificates_Date_Quy((Certificate)combo_Cert.SelectedItem, (int)combo_ThangQuy.SelectedItem, (int)combo_Nam.SelectedItem);
                }
            }
            else
            {
                if (rad_Thang.IsChecked == true)
                {
                    dg_kq.DataContext = cr.ResultTest_Certificate_Date((Certificate)combo_Cert.SelectedItem, (int)combo_ThangQuy.SelectedItem, (int)combo_Nam.SelectedItem);
                }
                else
                {
                    dg_kq.DataContext = cr.ResultTest_Certificate_Date_Quy((Certificate)combo_Cert.SelectedItem, (int)combo_ThangQuy.SelectedItem, (int)combo_Nam.SelectedItem);
                }
            }
        }

        private void ThongKe_Checked(object sender, RoutedEventArgs e)
        {
            dock_SL.Visibility = System.Windows.Visibility.Visible;
            CertificateRepository cr = new CertificateRepository();
            if (dg_sl==null||dg_dt==null||dg_kq==null)
            {
                return;
            }
            if (rad_soluong.IsChecked==true)
            {
                dg_sl.DataContext = cr.ReportNumberStudent_Certificate(new Certificate { Name = "" });
                dg_sl.Visibility = System.Windows.Visibility.Visible;
                dg_dt.Visibility = System.Windows.Visibility.Hidden;
                dg_kq.Visibility = System.Windows.Visibility.Hidden;
            }
            else if (rad_doanhthu.IsChecked==true)
            {
                dg_dt.DataContext = cr.SumFee_Certificates(new Certificate { Name = "" });
                dg_dt.Visibility = System.Windows.Visibility.Visible;
                dg_sl.Visibility = System.Windows.Visibility.Hidden;
                dg_kq.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                dg_kq.DataContext = cr.ResultTest_Certificate(new Certificate { Name = "" });
                dg_kq.Visibility = System.Windows.Visibility.Visible;
                dg_sl.Visibility = System.Windows.Visibility.Hidden;
                dg_dt.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void ThangQuy_Checked(object sender, RoutedEventArgs e)
        {
            if (combo_ThangQuy==null)
            {
                return;
            }
            if (rad_Thang.IsChecked==true)
            {
                combo_ThangQuy.ItemsSource = listthang;
            }
            else
            {
                combo_ThangQuy.ItemsSource = listquy;
            }
        }
    }
}
