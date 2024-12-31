namespace YummyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class social_media_add : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SocialMedias", "SocialMediaName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SocialMedias", "SocialMediaName");
        }
    }
}
