namespace EasyBooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StartAttrBooking : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservations", "Start", c => c.DateTime(nullable: false));
            AddColumn("dbo.WalkIns", "Start", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WalkIns", "Start");
            DropColumn("dbo.Reservations", "Start");
        }
    }
}
