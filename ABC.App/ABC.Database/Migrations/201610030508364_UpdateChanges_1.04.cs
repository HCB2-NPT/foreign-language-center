namespace ABC.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateChanges_104 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TestSchedule", "AgencyId", "dbo.Agency");
            DropForeignKey("dbo.Register", "TestScheduleId", "dbo.TestSchedule");
            DropIndex("dbo.Register", new[] { "TestScheduleId" });
            DropIndex("dbo.TestSchedule", new[] { "AgencyId" });
            DropPrimaryKey("dbo.Agency");
            DropPrimaryKey("dbo.TestSchedule");
            AddColumn("dbo.Agency", "AgencyId", c => c.String(nullable: false, maxLength: 10));
            AddColumn("dbo.TestSchedule", "TestScheduleId", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Register", "TestScheduleId", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.TestSchedule", "AgencyId", c => c.String(nullable: false, maxLength: 10));
            AddPrimaryKey("dbo.Agency", "AgencyId");
            AddPrimaryKey("dbo.TestSchedule", "TestScheduleId");
            CreateIndex("dbo.Register", "TestScheduleId");
            CreateIndex("dbo.TestSchedule", "AgencyId");
            AddForeignKey("dbo.TestSchedule", "AgencyId", "dbo.Agency", "AgencyId", cascadeDelete: true);
            AddForeignKey("dbo.Register", "TestScheduleId", "dbo.TestSchedule", "TestScheduleId", cascadeDelete: true);
            DropColumn("dbo.Agency", "Id");
            DropColumn("dbo.TestSchedule", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TestSchedule", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Agency", "Id", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Register", "TestScheduleId", "dbo.TestSchedule");
            DropForeignKey("dbo.TestSchedule", "AgencyId", "dbo.Agency");
            DropIndex("dbo.TestSchedule", new[] { "AgencyId" });
            DropIndex("dbo.Register", new[] { "TestScheduleId" });
            DropPrimaryKey("dbo.TestSchedule");
            DropPrimaryKey("dbo.Agency");
            AlterColumn("dbo.TestSchedule", "AgencyId", c => c.Int(nullable: false));
            AlterColumn("dbo.Register", "TestScheduleId", c => c.Int(nullable: false));
            DropColumn("dbo.TestSchedule", "TestScheduleId");
            DropColumn("dbo.Agency", "AgencyId");
            AddPrimaryKey("dbo.TestSchedule", "Id");
            AddPrimaryKey("dbo.Agency", "Id");
            CreateIndex("dbo.TestSchedule", "AgencyId");
            CreateIndex("dbo.Register", "TestScheduleId");
            AddForeignKey("dbo.Register", "TestScheduleId", "dbo.TestSchedule", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TestSchedule", "AgencyId", "dbo.Agency", "Id", cascadeDelete: true);
        }
    }
}
