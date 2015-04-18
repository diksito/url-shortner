namespace ShortURLService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CollectBrowserInStats : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UrlStats", "Browser", c => c.String());
            AddColumn("dbo.UrlStats", "MajorVersion", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UrlStats", "MajorVersion");
            DropColumn("dbo.UrlStats", "Browser");
        }
    }
}
