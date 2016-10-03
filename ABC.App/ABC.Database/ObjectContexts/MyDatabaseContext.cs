using ABC.Database.Migrations;
using ABC.Database.Objects;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Database.ObjectContexts
{
    public class MyDatabaseContext : DbContext
    {
        public static void Initialize()
        {
            //System.Data.Entity.Database.SetInitializer(new DropCreateDatabaseIfModelChanges<MyDatabaseContext>());
            //System.Data.Entity.Database.SetInitializer(new MigrateDatabaseToLatestVersion<MyDatabaseContext, Configuration>());
            //System.Data.Entity.Database.SetInitializer(new DropCreateDatabaseAlways<ApplicationDbContext>());
            using (var init = new MyDatabaseContext()) { }
        }

        public DbSet<Agency> Agencies { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<Register> Registers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<TestSchedule> TestSchedules { get; set; }
    }
}
