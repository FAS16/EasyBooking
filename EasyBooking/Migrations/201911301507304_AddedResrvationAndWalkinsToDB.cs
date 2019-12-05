namespace EasyBooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedResrvationAndWalkinsToDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Arrival = c.DateTime(nullable: false),
                        Customer_Id = c.Int(),
                        Table_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .ForeignKey("dbo.Tables", t => t.Table_Id)
                .Index(t => t.Customer_Id)
                .Index(t => t.Table_Id);
            
            CreateTable(
                "dbo.WalkIns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Table_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tables", t => t.Table_Id)
                .Index(t => t.Table_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WalkIns", "Table_Id", "dbo.Tables");
            DropForeignKey("dbo.Reservations", "Table_Id", "dbo.Tables");
            DropForeignKey("dbo.Reservations", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.WalkIns", new[] { "Table_Id" });
            DropIndex("dbo.Reservations", new[] { "Table_Id" });
            DropIndex("dbo.Reservations", new[] { "Customer_Id" });
            DropTable("dbo.WalkIns");
            DropTable("dbo.Reservations");
            DropTable("dbo.Customers");
        }
    }
}
