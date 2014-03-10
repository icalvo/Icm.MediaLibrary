namespace Icm.MediaLibrary.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HashAsMediaKey : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Media", "Hash", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Media", "Size", c => c.Long(nullable: false));
            DropPrimaryKey("dbo.Media");
            AddPrimaryKey("dbo.Media", "Hash");
            DropColumn("dbo.Media", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Media", "Id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Media");
            AddPrimaryKey("dbo.Media", "Id");
            DropColumn("dbo.Media", "Size");
            DropColumn("dbo.Media", "Hash");
        }
    }
}
