namespace EasyBooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveStringRep : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Tables", "StringRep");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tables", "StringRep", c => c.String());
        }
    }
}
