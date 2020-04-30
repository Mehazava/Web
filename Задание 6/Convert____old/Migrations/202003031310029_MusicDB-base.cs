namespace Convert.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MusicDBbase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Producers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Songs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ProducerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Producers", t => t.ProducerId, cascadeDelete: true)
                .Index(t => t.ProducerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Songs", "ProducerId", "dbo.Producers");
            DropIndex("dbo.Songs", new[] { "ProducerId" });
            DropTable("dbo.Songs");
            DropTable("dbo.Producers");
        }
    }
}
