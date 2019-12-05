namespace EasyBooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCreatedToBooking : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservations", "Created", c => c.DateTime(nullable: false));
            AddColumn("dbo.WalkIns", "Created", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WalkIns", "Created");
            DropColumn("dbo.Reservations", "Created");
        }
    }
}
