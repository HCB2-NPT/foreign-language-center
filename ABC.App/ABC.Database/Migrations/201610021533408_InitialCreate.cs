namespace ABC.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Agency",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Certificate",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                        Fee = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Name);
            
            CreateTable(
                "dbo.Register",
                c => new
                    {
                        StudentId = c.String(nullable: false, maxLength: 128),
                        TestScheduleId = c.Int(nullable: false),
                        DateReg = c.DateTime(nullable: false),
                        TestScore = c.Double(nullable: false),
                    })
                .PrimaryKey(t => new { t.StudentId, t.TestScheduleId })
                .ForeignKey("dbo.TestSchedule", t => t.TestScheduleId, cascadeDelete: true)
                .ForeignKey("dbo.Student", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.TestScheduleId);
            
            CreateTable(
                "dbo.TestSchedule",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        AgencyId = c.Int(nullable: false),
                        CertificateId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Agency", t => t.AgencyId, cascadeDelete: true)
                .ForeignKey("dbo.Certificate", t => t.CertificateId, cascadeDelete: true)
                .Index(t => t.AgencyId)
                .Index(t => t.CertificateId);
            
            CreateTable(
                "dbo.Student",
                c => new
                    {
                        PersonalId = c.String(nullable: false, maxLength: 128),
                        FullName = c.String(nullable: false, maxLength: 64),
                        Birthday = c.DateTime(nullable: false),
                        PhoneNumber = c.String(nullable: false, maxLength: 16),
                    })
                .PrimaryKey(t => t.PersonalId);
            CreateStores();
        }
        
        public override void Down()
        {
            DropStores();
            DropForeignKey("dbo.Register", "StudentId", "dbo.Student");
            DropForeignKey("dbo.Register", "TestScheduleId", "dbo.TestSchedule");
            DropForeignKey("dbo.TestSchedule", "CertificateId", "dbo.Certificate");
            DropForeignKey("dbo.TestSchedule", "AgencyId", "dbo.Agency");
            DropIndex("dbo.TestSchedule", new[] { "CertificateId" });
            DropIndex("dbo.TestSchedule", new[] { "AgencyId" });
            DropIndex("dbo.Register", new[] { "TestScheduleId" });
            DropIndex("dbo.Register", new[] { "StudentId" });
            DropTable("dbo.Student");
            DropTable("dbo.TestSchedule");
            DropTable("dbo.Register");
            DropTable("dbo.Certificate");
            DropTable("dbo.Agency");
        }
    }
}
