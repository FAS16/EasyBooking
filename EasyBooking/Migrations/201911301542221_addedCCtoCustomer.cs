namespace EasyBooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedCCtoCustomer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "CountryCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "CountryCode");
        }
    }
}
