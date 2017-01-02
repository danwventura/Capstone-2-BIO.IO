namespace Bio.io.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedChannelIdForDataPoint : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DataPoints", "ChannelId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DataPoints", "ChannelId");
        }
    }
}
