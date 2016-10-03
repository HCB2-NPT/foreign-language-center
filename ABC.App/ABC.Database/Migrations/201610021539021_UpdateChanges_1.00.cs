namespace ABC.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateChanges_100 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Agency", "Name", c => c.String(nullable: false, maxLength: 256));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Agency", "Name", c => c.String());
        }
    }
}
