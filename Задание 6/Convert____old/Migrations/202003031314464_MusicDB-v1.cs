namespace Convert.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MusicDBv1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Songs", "Rating", c => c.Int(nullable: false, defaultValue: 0));
        }

        public override void Down()
        {
            DropColumn("dbo.Songs", "Rating");
        }
    }
}
