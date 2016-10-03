namespace ABC.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateChanges_103 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TestSchedule", "CertificateId", "dbo.Certificate");
            DropIndex("dbo.TestSchedule", new[] { "CertificateId" });
            DropPrimaryKey("dbo.Certificate");
            AlterColumn("dbo.Certificate", "Name", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.TestSchedule", "CertificateId", c => c.String(nullable: false, maxLength: 10));
            AddPrimaryKey("dbo.Certificate", "Name");
            CreateIndex("dbo.TestSchedule", "CertificateId");
            AddForeignKey("dbo.TestSchedule", "CertificateId", "dbo.Certificate", "Name", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TestSchedule", "CertificateId", "dbo.Certificate");
            DropIndex("dbo.TestSchedule", new[] { "CertificateId" });
            DropPrimaryKey("dbo.Certificate");
            AlterColumn("dbo.TestSchedule", "CertificateId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Certificate", "Name", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Certificate", "Name");
            CreateIndex("dbo.TestSchedule", "CertificateId");
            AddForeignKey("dbo.TestSchedule", "CertificateId", "dbo.Certificate", "Name", cascadeDelete: true);
        }
    }
}
