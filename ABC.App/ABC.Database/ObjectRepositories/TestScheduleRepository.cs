using ABC.Database.ObjectContexts;
using ABC.Database.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Database.ObjectRepositories
{
    public class TestScheduleRepository : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public List<TestSchedule> All
        {
            get
            {
                var list = new List<TestSchedule>();
                using (var context = new MyDatabaseContext())
                {
                    list = context.TestSchedules.Include("Certificate").Include("Agency").ToList();
                }
                return list;
            }
        }

        public void Add(TestSchedule which)
        {
            using (var context = new MyDatabaseContext())
            {
                context.TestSchedules.Add(which);
                context.SaveChanges();
            }
            NotifyPropertyChanged("All");
        }
    }
}
