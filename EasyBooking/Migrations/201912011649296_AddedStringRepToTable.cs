namespace EasyBooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedStringRepToTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tables", "StringRep", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tables", "StringRep");
        }
    }
}
