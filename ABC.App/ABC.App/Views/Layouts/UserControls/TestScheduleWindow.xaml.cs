using ABC.Database.ObjectContexts;
using ABC.Database.ObjectRepositories;
using ABC.Database.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            TestScheduleRepository sche = new TestScheduleRepository();
            dg.DataContext = sche.getTestSchedules();
        }

        private void TraCuu_Click(object sender, RoutedEventArgs e)
        {
            TestScheduleRepository tr = new TestScheduleRepository();
            if (rd_ChuaThi.IsChecked==true)
            {
                
                if (txt_CMND.Text == "" || txt_CMND.Text.Length < 10)
                {
                    MessageBox.Show("Sai CMND");
                    return;
                }
                else
                {
                    dg_creg.Visibility = System.Windows.Visibility.Hidden;
                    dg.DataContext = tr.CheckListTestSchedule(new Student { PersonalId = txt_CMND.Text });
                }
            }
            else
            {
                if (txt_CMND.Text == "" || txt_CMND.Text.Length < 10)
                {
                    MessageBox.Show("Sai CMND");
                }
                else
                {
                    dg_creg.Visibility = System.Windows.Visibility.Hidden;
                    dg.DataContext = tr.ListTested(new Student { PersonalId = txt_CMND.Text });
                }
            }

            
        }

        private void Reg_Click(object sender, RoutedEventArgs e)
        {
            if (txt_CMND.Text=="")
            {
                MessageBox.Show("CMND không được để trống");
                return;
            }
            //MessageBox.Show(((DataRowView)dg.CurrentCell.Item)[0].ToString());
            Student s = new Student();
            s.PersonalId = txt_CMND.Text;
            StudentRepository sr = new StudentRepository();
            Student check = sr.All.Where(x => x.PersonalId == s.PersonalId).FirstOrDefault();
            if (check != null)
            {
                //MessageBox.Show("Đã tồn tại sinh viên");
                TestSchedule ts = new TestScheduleRepository().All.Where(x=>x.TestScheduleId == ((DataRowView)dg.CurrentCell.Item)[0].ToString()).FirstOrDefault();
                RegisterRepository rr = new RegisterRepository();
                rr.AddOnline(check, ts);
            }
            else
            {
                MessageBox.Show("Sinh viên chưa tồn tại");
            }
        }
    }
}
