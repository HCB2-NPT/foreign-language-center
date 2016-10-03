namespace ABC.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateChanges_102 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Register", "StudentId", "dbo.Student");
            DropIndex("dbo.Register", new[] { "StudentId" });
            DropPrimaryKey("dbo.Student");
            AlterColumn("dbo.Register", "StudentId", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Student", "PersonalId", c => c.String(nullable: false, maxLength: 10));
            AddPrimaryKey("dbo.Student", "PersonalId");
            CreateIndex("dbo.Register", "StudentId");
            AddForeignKey("dbo.Register", "StudentId", "dbo.Student", "PersonalId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Register", "StudentId", "dbo.Student");
            DropIndex("dbo.Register", new[] { "StudentId" });
            DropPrimaryKey("dbo.Student");
            AlterColumn("dbo.Student", "PersonalId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Register", "StudentId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Student", "PersonalId");
            CreateIndex("dbo.Register", "StudentId");
            AddForeignKey("dbo.Register", "StudentId", "dbo.Student", "PersonalId", cascadeDelete: true);
        }
    }
}
