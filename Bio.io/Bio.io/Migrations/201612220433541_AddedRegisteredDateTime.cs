namespace Bio.io.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRegisteredDateTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Registered", c => c.DateTime(nullable: false));
            DropColumn("dbo.Users", "Password");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Password", c => c.String());
            DropColumn("dbo.AspNetUsers", "Registered");
        }
    }
}
