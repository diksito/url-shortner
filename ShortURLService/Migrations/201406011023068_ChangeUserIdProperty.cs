namespace ShortURLService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeUserIdProperty : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.URLs", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.URLs", "UserId", c => c.Int());
        }
    }
}
