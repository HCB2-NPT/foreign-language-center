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
    public class CertificateRepository
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public DataTable ReportNumberStudent_Certificate(Certificate which)
        {
            //Name,Count(StudentId) as SoLuong
            var table = DataTableHelper.CreateCustomTable(
                new CustomColumn("Name", typeof(string)),
                new CustomColumn("SoLuong", typeof(int))
                );
            using (var context = new MyDatabaseContext())
            {
                using (var con = context.Database.Connection)
                {
                    con.Open();
                    using (var reader = DbMyCommand.CreateCmd(context, "dbo.ReportNumberStudent_Certificate", CommandType.StoredProcedure, which.Name).ExecuteReader())
                    {
                        DataTableHelper.ReadFromDataReader(reader, ref table);
                    }
                    con.Close();
                }
            }
            NotifyPropertyChanged("All");
            return table;
        }

        public DataTable ReportNumberStudent_Certificate_Date(Certificate which,int month,int year)
        {
            //Name,Count(StudentId) as SoLuong
            var table = DataTableHelper.CreateCustomTable(
                new CustomColumn("Name", typeof(string)),
                new CustomColumn("SoLuong", typeof(int))
                );
            using (var context = new MyDatabaseContext())
            {
                using (var con = context.Database.Connection)
                {
                    con.Open();
                    using (var reader = DbMyCommand.CreateCmd(context, "dbo.ReportNumberStudent_Certificate_Date", CommandType.StoredProcedure, which.Name).ExecuteReader())
                    {
                        DataTableHelper.ReadFromDataReader(reader, ref table);
                    }
                    con.Close();
                }
            }
            NotifyPropertyChanged("All");
            return table;
        }

        public DataTable ReportNumberStudent_Certificate_Date_Quy(Certificate which, int quy, int year)
        {
            //Name,Count(StudentId) as SoLuong
            var table = DataTableHelper.CreateCustomTable(
                new CustomColumn("Name", typeof(string)),
                new CustomColumn("SoLuong", typeof(int))
                );
            using (var context = new MyDatabaseContext())
            {
                using (var con = context.Database.Connection)
                {
                    con.Open();
                    using (var reader = DbMyCommand.CreateCmd(context, "dbo.ReportNumberStudent_Certificate_Date_Quy", CommandType.StoredProcedure, which.Name).ExecuteReader())
                    {
                        DataTableHelper.ReadFromDataReader(reader, ref table);
                    }
                    con.Close();
                }
            }
            NotifyPropertyChanged("All");
            return table;
        }

        //
        public DataTable SumFee_Certificates(Certificate certificate)
        {
            //Certificate.Name,Sum(Fee) as TotalFee
            var table = DataTableHelper.CreateCustomTable(
                new CustomColumn("Name", typeof(string)),
                new CustomColumn("TotalFee", typeof(int))
                );
            using (var context = new MyDatabaseContext())
            {
                using (var con = context.Database.Connection)
                {
                    con.Open();
                    using (var reader = DbMyCommand.CreateCmd(context, "dbo.SumFee_Certificates", CommandType.StoredProcedure, certificate.Name).ExecuteReader())
                    {
                        DataTableHelper.ReadFromDataReader(reader, ref table);
                    }
                    con.Close();
                }
            }
            NotifyPropertyChanged("All");
            return table;
        }

        public DataTable SumFee_Certificates_Date(Certificate certificate, int month, int year)
        {
            //Certificate.Name,Sum(Fee) as TotalFee
            var table = DataTableHelper.CreateCustomTable(
                new CustomColumn("Name", typeof(string)),
                new CustomColumn("TotalFee", typeof(int))
                );
            using (var context = new MyDatabaseContext())
            {
                using (var con = context.Database.Connection)
                {
                    con.Open();
                    using (var reader = DbMyCommand.CreateCmd(context, "dbo.SumFee_Certificates_Date", CommandType.StoredProcedure, certificate.Name,month,year).ExecuteReader())
                    {
                        DataTableHelper.ReadFromDataReader(reader, ref table);
                    }
                    con.Close();
                }
            }
            NotifyPropertyChanged("All");
            return table;
        }

        public DataTable SumFee_Certificates_Date_Quy(Certificate certificate, int quy, int year)
        {
            //Certificate.Name,Sum(Fee) as TotalFee
            var table = DataTableHelper.CreateCustomTable(
                new CustomColumn("Name", typeof(string)),
                new CustomColumn("TotalFee", typeof(int))
                );
            using (var context = new MyDatabaseContext())
            {
                using (var con = context.Database.Connection)
                {
                    con.Open();
                    using (var reader = DbMyCommand.CreateCmd(context, "dbo.SumFee_Certificates_Date_Quy", CommandType.StoredProcedure, certificate.Name,quy,year).ExecuteReader())
                    {
                        DataTableHelper.ReadFromDataReader(reader, ref table);
                    }
                    con.Close();
                }
            }
            NotifyPropertyChanged("All");
            return table;
        }

        //
        public DataTable ResultTest_Certificate(Certificate certificate)
        {
            //Certificate.Name,Agency.Name,TestSchedule.Date,TestScore 
            var table = DataTableHelper.CreateCustomTable(
                new CustomColumn("Name", typeof(string)),
                new CustomColumn("Name", typeof(string)),
                new CustomColumn("Date", typeof(DateTime)),
                new CustomColumn("TestScore", typeof(int))
                );
            using (var context = new MyDatabaseContext())
            {
                using (var con = context.Database.Connection)
                {
                    con.Open();
                    using (var reader = DbMyCommand.CreateCmd(context, "dbo.ResultTest_Certificate", CommandType.StoredProcedure, certificate.Name).ExecuteReader())
                    {
                        DataTableHelper.ReadFromDataReader(reader, ref table);
                    }
                    con.Close();
                }
            }
            NotifyPropertyChanged("All");
            return table;
        }

        public DataTable ResultTest_Certificate_Date(Certificate certificate,int month,int year)
        {
            //Certificate.Name,Agency.Name,TestSchedule.Date,TestScore 
            var table = DataTableHelper.CreateCustomTable(
                new CustomColumn("Name", typeof(string)),
                new CustomColumn("Name", typeof(string)),
                new CustomColumn("Date", typeof(DateTime)),
                new CustomColumn("TestScore", typeof(int))
                );
            using (var context = new MyDatabaseContext())
            {
                using (var con = context.Database.Connection)
                {
                    con.Open();
                    using (var reader = DbMyCommand.CreateCmd(context, "dbo.ResultTest_Certificate_Date", CommandType.StoredProcedure, certificate.Name,month,year).ExecuteReader())
                    {
                        DataTableHelper.ReadFromDataReader(reader, ref table);
                    }
                    con.Close();
                }
            }
            NotifyPropertyChanged("All");
            return table;
        }

        public DataTable ResultTest_Certificate_Date_Quy(Certificate certificate, int quy, int year)
        {
            //Certificate.Name,Agency.Name,TestSchedule.Date,TestScore 
            var table = DataTableHelper.CreateCustomTable(
                new CustomColumn("Name", typeof(string)),
                new CustomColumn("Name", typeof(string)),
                new CustomColumn("Date", typeof(DateTime)),
                new CustomColumn("TestScore", typeof(int))
                );
            using (var context = new MyDatabaseContext())
            {
                using (var con = context.Database.Connection)
                {
                    con.Open();
                    using (var reader = DbMyCommand.CreateCmd(context, "dbo.ResultTest_Certificate_Date_Quy", CommandType.StoredProcedure, certificate.Name,quy,year).ExecuteReader())
                    {
                        DataTableHelper.ReadFromDataReader(reader, ref table);
                    }
                    con.Close();
                }
            }
            NotifyPropertyChanged("All");
            return table;
        }
    }
}
