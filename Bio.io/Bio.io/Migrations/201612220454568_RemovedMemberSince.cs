namespace Bio.io.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedMemberSince : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "MemberSince");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "MemberSince", c => c.DateTime(nullable: false));
        }
    }
}
