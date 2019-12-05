namespace EasyBooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEndToBooking : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservations", "End", c => c.DateTime(nullable: false));
            AddColumn("dbo.WalkIns", "End", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WalkIns", "End");
            DropColumn("dbo.Reservations", "End");
        }
    }
}
