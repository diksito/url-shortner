namespace ShortURLService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReffererToStats : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UrlStats", "UrlRefferer", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UrlStats", "UrlRefferer");
        }
    }
}
