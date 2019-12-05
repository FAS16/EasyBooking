namespace EasyBooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DontKnow1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reservations", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Reservations", "Table_Id", "dbo.Tables");
            DropForeignKey("dbo.WalkIns", "Table_Id", "dbo.Tables");
            DropIndex("dbo.Reservations", new[] { "Customer_Id" });
            DropIndex("dbo.Reservations", new[] { "Table_Id" });
            DropIndex("dbo.WalkIns", new[] { "Table_Id" });
            AlterColumn("dbo.Reservations", "Customer_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Reservations", "Table_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.WalkIns", "Table_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Reservations", "Customer_Id");
            CreateIndex("dbo.Reservations", "Table_Id");
            CreateIndex("dbo.WalkIns", "Table_Id");
            AddForeignKey("dbo.Reservations", "Customer_Id", "dbo.Customers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Reservations", "Table_Id", "dbo.Tables", "Id", cascadeDelete: true);
            AddForeignKey("dbo.WalkIns", "Table_Id", "dbo.Tables", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WalkIns", "Table_Id", "dbo.Tables");
            DropForeignKey("dbo.Reservations", "Table_Id", "dbo.Tables");
            DropForeignKey("dbo.Reservations", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.WalkIns", new[] { "Table_Id" });
            DropIndex("dbo.Reservations", new[] { "Table_Id" });
            DropIndex("dbo.Reservations", new[] { "Customer_Id" });
            AlterColumn("dbo.WalkIns", "Table_Id", c => c.Int());
            AlterColumn("dbo.Reservations", "Table_Id", c => c.Int());
            AlterColumn("dbo.Reservations", "Customer_Id", c => c.Int());
            CreateIndex("dbo.WalkIns", "Table_Id");
            CreateIndex("dbo.Reservations", "Table_Id");
            CreateIndex("dbo.Reservations", "Customer_Id");
            AddForeignKey("dbo.WalkIns", "Table_Id", "dbo.Tables", "Id");
            AddForeignKey("dbo.Reservations", "Table_Id", "dbo.Tables", "Id");
            AddForeignKey("dbo.Reservations", "Customer_Id", "dbo.Customers", "Id");
        }
    }
}
