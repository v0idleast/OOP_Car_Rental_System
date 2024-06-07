namespace Lab2_Yampol.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "PassportCode", c => c.String());
            AlterColumn("dbo.Orders", "Days", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "Days", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "PassportCode", c => c.Int(nullable: false));
        }
    }
}
