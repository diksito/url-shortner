namespace ShortURLService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class URLStats : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UrlStats",
                c => new
                    {
                        UrlStatId = c.Int(nullable: false, identity: true),
                        UserAgent = c.String(),
                        UserHostAddress = c.String(),
                        UserLanguage = c.String(),
                        IsMobile = c.Boolean(nullable: false),
                        URL_UrlId = c.Int(),
                    })
                .PrimaryKey(t => t.UrlStatId)
                .ForeignKey("dbo.URLs", t => t.URL_UrlId)
                .Index(t => t.URL_UrlId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UrlStats", "URL_UrlId", "dbo.URLs");
            DropIndex("dbo.UrlStats", new[] { "URL_UrlId" });
            DropTable("dbo.UrlStats");
        }
    }
}
