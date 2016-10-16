using ABC.Database.Helpers;
using ABC.Database.ObjectContexts;
using ABC.Database.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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

        public DataTable ListTested(Student which)
        {
            //TestSchedule.TestScheduleId,TestSchedule.Date,Agency.Name,Certificate.Name,Fee
            var table = DataTableHelper.CreateCustomTable(
                new CustomColumn("TestScheduleId", typeof(string)),
                new CustomColumn("Date", typeof(string)),
                new CustomColumn("Name", typeof(string)),
                new CustomColumn("Name", typeof(string)),
                new CustomColumn("Fee", typeof(double))
                );
            using (var context = new MyDatabaseContext())
            {
                using (var con = context.Database.Connection)
                {
                    con.Open();
                    using (var reader = DbMyCommand.CreateCmd(context, "dbo.ListTested", CommandType.StoredProcedure,which.PersonalId).ExecuteReader())
                    {
                        DataTableHelper.ReadFromDataReader(reader, ref table);
                    }
                    con.Close();
                }
            }
            NotifyPropertyChanged("All");
            return table;
        }

        public DataTable CheckListTestSchedule(Student which)
        {
            //TestSchedule.CertificateId,TestSchedule.Date,Agency.Name,Certificate.Name,Certificate.Fee
            var table = DataTableHelper.CreateCustomTable(
                new CustomColumn("CertificateId", typeof(string)),
                new CustomColumn("Date", typeof(DateTime)),
                new CustomColumn("NameAgency", typeof(string)),
                new CustomColumn("NameCertificate", typeof(string)),
                new CustomColumn("Fee", typeof(double))
                );
            using (var context = new MyDatabaseContext())
            {
                using (var con = context.Database.Connection)
                {
                    con.Open();
                    using (var reader = DbMyCommand.CreateCmd(context, "dbo.CheckListTestSchedule", CommandType.StoredProcedure,
                        which.PersonalId).ExecuteReader())
                    {
                        DataTableHelper.ReadFromDataReader(reader, ref table);
                    }
                    con.Close();
                }
            }
            NotifyPropertyChanged("All");
            return table;
        }

        public DataTable CheckTestScore(Student which)
        {
            //TestSchedule.TestScheduleId,TestSchedule.Date,Agency.Name,Register.TestScore
            var table = DataTableHelper.CreateCustomTable(
                new CustomColumn("TestScheduleId", typeof(string)),
                new CustomColumn("Date", typeof(DateTime)),
                new CustomColumn("Name", typeof(string)),
                new CustomColumn("TestScore", typeof(double))
                );
            using (var context = new MyDatabaseContext())
            {
                using (var con = context.Database.Connection)
                {
                    con.Open();
                    using (var reader = DbMyCommand.CreateCmd(context, "dbo.CheckTestScore", CommandType.StoredProcedure, which.PersonalId).ExecuteReader())
                    {
                        DataTableHelper.ReadFromDataReader(reader, ref table);
                    }
                    con.Close();
                }
            }
            NotifyPropertyChanged("All");
            return table;
        }

        public DataTable getTestSchedules()
        {
            var table = DataTableHelper.CreateCustomTable(
                new CustomColumn("TestScheduleId", typeof(string)),
                new CustomColumn("CertificateId", typeof(string)),
                new CustomColumn("Date", typeof(string)),
                new CustomColumn("AgencyName", typeof(string)),
                new CustomColumn("CertificateName", typeof(string)),
                new CustomColumn("Fee", typeof(double))
                );
            using (var context = new MyDatabaseContext())
            {
                using (var connection = context.Database.Connection)
                {
                    connection.Open();
                    using (var reader = DbMyCommand.CreateCmd(
                        context, "dbo.usp_getTestSchedules",
                        CommandType.StoredProcedure
                    ).ExecuteReader())
                    {
                        DataTableHelper.ReadFromDataReader(reader, ref table);
                    }
                    connection.Close();
                }
            }
            NotifyPropertyChanged("All");
            return table;
        }
    }
}
