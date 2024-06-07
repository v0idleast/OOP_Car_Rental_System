namespace Lab2_Yampol.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DeclineReasons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CarId = c.Int(nullable: false),
                        Reason = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cars", t => t.CarId, cascadeDelete: true)
                .Index(t => t.CarId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CarId = c.Int(nullable: false),
                        UserName = c.String(maxLength: 100),
                        PassportCode = c.Int(nullable: false),
                        Days = c.Int(nullable: false),
                        IsBroken = c.Boolean(nullable: false),
                        IsDecline = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cars", t => t.CarId, cascadeDelete: true)
                .Index(t => t.CarId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "CarId", "dbo.Cars");
            DropForeignKey("dbo.DeclineReasons", "CarId", "dbo.Cars");
            DropIndex("dbo.Orders", new[] { "CarId" });
            DropIndex("dbo.DeclineReasons", new[] { "CarId" });
            DropTable("dbo.Orders");
            DropTable("dbo.DeclineReasons");
            DropTable("dbo.Cars");
        }
    }
}
