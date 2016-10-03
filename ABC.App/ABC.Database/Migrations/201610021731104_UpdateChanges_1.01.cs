namespace ABC.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateChanges_101 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Register");
            AddColumn("dbo.Register", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Register", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Register");
            DropColumn("dbo.Register", "Id");
            AddPrimaryKey("dbo.Register", new[] { "StudentId", "TestScheduleId" });
        }
    }
}
