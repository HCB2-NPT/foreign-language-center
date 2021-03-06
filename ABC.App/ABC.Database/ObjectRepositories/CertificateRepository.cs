﻿using ABC.Database.Helpers;
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

        public List<Certificate> All
        {
            get
            {
                var list = new List<Certificate>();
                using (var context = new MyDatabaseContext())
                {
                    list = context.Certificates.ToList();
                }
                return list;
            }
        }

        public DataTable ReportNumberStudent_Certificate(Certificate which)
        {
            //Name,Count(StudentId) as SoLuong
            var table = DataTableHelper.CreateCustomTable(
                new CustomColumn("CertificateName", typeof(string)),
                new CustomColumn("Number", typeof(int))
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
                new CustomColumn("CertificateName", typeof(string)),
                new CustomColumn("Number", typeof(int))
                );
            using (var context = new MyDatabaseContext())
            {
                using (var con = context.Database.Connection)
                {
                    con.Open();
                    using (var reader = DbMyCommand.CreateCmd(context, "dbo.ReportNumberStudent_Certificate_Date", CommandType.StoredProcedure, which.Name,month,year).ExecuteReader())
                    {
                        DataTableHelper.ReadFromDataReader(reader, ref table);
                    }
                    con.Close();
                }
            }
            NotifyPropertyChanged("All");
            return table;
        }

        public DataTable ReportNumberStudent_Certificate_Date_Quy(Certificate which, int quarter, int year)
        {
            //Name,Count(StudentId) as SoLuong
            var table = DataTableHelper.CreateCustomTable(
                new CustomColumn("CertificateName", typeof(string)),
                new CustomColumn("Number", typeof(int))
                );
            using (var context = new MyDatabaseContext())
            {
                using (var con = context.Database.Connection)
                {
                    con.Open();
                    using (var reader = DbMyCommand.CreateCmd(context, "dbo.ReportNumberStudent_Certificate_Date_Quarter", CommandType.StoredProcedure, which.Name,quarter,year).ExecuteReader())
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

        public DataTable SumFee_Certificates_Date_Quy(Certificate certificate, int quarter, int year)
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
                    using (var reader = DbMyCommand.CreateCmd(context, "dbo.SumFee_Certificates_Date_Quarter", CommandType.StoredProcedure, certificate.Name,quarter,year).ExecuteReader())
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
            //select Register.StudentId,Certificate.Name,TestSchedule.Date,Agency.Name,TestScore 


            var table = DataTableHelper.CreateCustomTable(
                new CustomColumn("StudentId", typeof(string)),
                new CustomColumn("CertificateName", typeof(string)),
                new CustomColumn("AgencyName", typeof(string)),
                new CustomColumn("Date", typeof(string)),
                new CustomColumn("TestScore", typeof(int))
                );
            using (var context = new MyDatabaseContext())
            {
                using (var con = context.Database.Connection)
                {
                    con.Open();
                    using (var reader = DbMyCommand.CreateCmd(context, "dbo.ResultTest_Certificate", CommandType.StoredProcedure).ExecuteReader())
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
                new CustomColumn("StudentId", typeof(string)),
                new CustomColumn("CertificateName", typeof(string)),
                new CustomColumn("AgencyName", typeof(string)),
                new CustomColumn("Date", typeof(string)),
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

        public DataTable ResultTest_Certificate_Date_Quy(Certificate certificate, int quarter, int year)
        {
            //Certificate.Name,Agency.Name,TestSchedule.Date,TestScore 
            var table = DataTableHelper.CreateCustomTable(
                new CustomColumn("StudentId", typeof(string)),
                new CustomColumn("CertificateName", typeof(string)),
                new CustomColumn("AgencyName", typeof(string)),
                new CustomColumn("Date", typeof(string)),
                new CustomColumn("TestScore", typeof(int))
                );
            using (var context = new MyDatabaseContext())
            {
                using (var con = context.Database.Connection)
                {
                    con.Open();
                    using (var reader = DbMyCommand.CreateCmd(context, "dbo.ResultTest_Certificate_Date_Quarter", CommandType.StoredProcedure, certificate.Name,quarter,year).ExecuteReader())
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
