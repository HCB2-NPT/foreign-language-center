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
    public class StudentRepository : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public List<Student> All
        {
            get
            {
                var list = new List<Student>();
                using (var context = new MyDatabaseContext())
                {
                    list = context.Students.ToList();
                }
                return list;
            }
        }

        public void Add(Student which)
        {
            using (var context = new MyDatabaseContext())
            {
                object[] p = {
                                 new SqlParameter("@id", which.PersonalId),
                                 new SqlParameter("@fn", which.FullName),
                                 new SqlParameter("@bd", which.Birthday),
                                 new SqlParameter("@pn", which.PhoneNumber)
                             };
                context.Database.ExecuteSqlCommand("exec dbo.AddStudent @id, @fn, @bd, @pn", p);
            }
            NotifyPropertyChanged("All");
        }
    }
}
