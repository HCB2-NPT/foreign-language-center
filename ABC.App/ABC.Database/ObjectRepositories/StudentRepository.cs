﻿using ABC.Database.ObjectContexts;
using ABC.Database.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ABC.Database.Helpers;

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
                context.Database.ExecuteSqlCommand("exec dbo.AddStudent @id, @fn, @bd, @pn", which.PersonalId,which.FullName,which.Birthday,which.PhoneNumber);
            }
            NotifyPropertyChanged("All");
        }
    }
}
