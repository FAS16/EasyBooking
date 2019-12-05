namespace EasyBooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeResrvationArrivalNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Reservations", "Arrival", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reservations", "Arrival", c => c.DateTime(nullable: false));
        }
    }
}
