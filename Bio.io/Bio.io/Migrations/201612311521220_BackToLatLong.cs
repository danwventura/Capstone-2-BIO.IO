namespace Bio.io.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BackToLatLong : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DataPoints", "Lat", c => c.Double(nullable: false));
            AddColumn("dbo.DataPoints", "Long", c => c.Double(nullable: false));
            DropColumn("dbo.DataPoints", "Coords");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DataPoints", "Coords", c => c.Double(nullable: false));
            DropColumn("dbo.DataPoints", "Long");
            DropColumn("dbo.DataPoints", "Lat");
        }
    }
}
