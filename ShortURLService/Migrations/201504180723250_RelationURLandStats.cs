namespace ShortURLService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RelationURLandStats : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UrlStats", "URL_UrlId", "dbo.URLs");
            DropIndex("dbo.UrlStats", new[] { "URL_UrlId" });
            AddColumn("dbo.UrlStats", "UrlId", c => c.Int(nullable: false));
            CreateIndex("dbo.UrlStats", "UrlId");
            AddForeignKey("dbo.UrlStats", "UrlId", "dbo.URLs", "UrlId", cascadeDelete: true);
            DropColumn("dbo.UrlStats", "URL_UrlId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UrlStats", "URL_UrlId", c => c.Int());
            DropForeignKey("dbo.UrlStats", "UrlId", "dbo.URLs");
            DropIndex("dbo.UrlStats", new[] { "UrlId" });
            DropColumn("dbo.UrlStats", "UrlId");
            CreateIndex("dbo.UrlStats", "URL_UrlId");
            AddForeignKey("dbo.UrlStats", "URL_UrlId", "dbo.URLs", "UrlId");
        }
    }
}
