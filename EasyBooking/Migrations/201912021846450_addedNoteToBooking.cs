namespace EasyBooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedNoteToBooking : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservations", "Note", c => c.String());
            AddColumn("dbo.WalkIns", "Note", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.WalkIns", "Note");
            DropColumn("dbo.Reservations", "Note");
        }
    }
}
