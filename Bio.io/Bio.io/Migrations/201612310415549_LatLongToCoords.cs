namespace Bio.io.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LatLongToCoords : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DataPoints", "Coords", c => c.Double(nullable: false));
            DropColumn("dbo.DataPoints", "Latitude");
            DropColumn("dbo.DataPoints", "Longitude");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DataPoints", "Longitude", c => c.Double(nullable: false));
            AddColumn("dbo.DataPoints", "Latitude", c => c.Double(nullable: false));
            DropColumn("dbo.DataPoints", "Coords");
        }
    }
}
