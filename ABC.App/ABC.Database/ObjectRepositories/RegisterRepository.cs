using ABC.Database.ObjectContexts;
using ABC.Database.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Database.ObjectRepositories
{
    public class RegisterRepository
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        public void Add(Student st,TestSchedule ts)
        {
            using (var context = new MyDatabaseContext())
            {
                context.Database.ExecuteSqlCommand("exec dbo.AddRegister @studentid,@testscheduleid", st.PersonalId,ts.TestScheduleId);
            }
            NotifyPropertyChanged("All");
        }

        public void AddOnline(Student st, TestSchedule ts)
        {
            using (var context = new MyDatabaseContext())
            {
                context.Database.ExecuteSqlCommand("exec dbo.RegisterTestOnline @studentid,@testscheduleid", st.PersonalId,ts.TestScheduleId);
            }
            NotifyPropertyChanged("All");
        }
    }
}
