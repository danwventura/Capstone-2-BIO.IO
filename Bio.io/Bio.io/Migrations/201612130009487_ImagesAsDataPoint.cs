namespace Bio.io.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImagesAsDataPoint : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Images", "Route_RouteID", "dbo.Routes");
            DropIndex("dbo.Images", new[] { "Route_RouteID" });
            AddColumn("dbo.DataPoints", "Snapshot_ImageID", c => c.Int());
            CreateIndex("dbo.DataPoints", "Snapshot_ImageID");
            AddForeignKey("dbo.DataPoints", "Snapshot_ImageID", "dbo.Images", "ImageID");
            DropColumn("dbo.Images", "Route_RouteID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Images", "Route_RouteID", c => c.Int());
            DropForeignKey("dbo.DataPoints", "Snapshot_ImageID", "dbo.Images");
            DropIndex("dbo.DataPoints", new[] { "Snapshot_ImageID" });
            DropColumn("dbo.DataPoints", "Snapshot_ImageID");
            CreateIndex("dbo.Images", "Route_RouteID");
            AddForeignKey("dbo.Images", "Route_RouteID", "dbo.Routes", "RouteID");
        }
    }
}
