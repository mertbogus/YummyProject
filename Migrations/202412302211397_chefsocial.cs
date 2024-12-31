namespace YummyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chefsocial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChefSocials", "SocialMediaName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ChefSocials", "SocialMediaName");
        }
    }
}
