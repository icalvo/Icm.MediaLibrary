namespace Icm.MediaLibrary.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveHash : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Media", "FileName", c => c.String(nullable: false, maxLength: 128));
            DropPrimaryKey("dbo.Media");
            AddPrimaryKey("dbo.Media", "FileName");
            DropColumn("dbo.Media", "Hash");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Media", "Hash", c => c.String(nullable: false, maxLength: 128));
            DropPrimaryKey("dbo.Media");
            AddPrimaryKey("dbo.Media", "Hash");
            AlterColumn("dbo.Media", "FileName", c => c.String());
        }
    }
}
