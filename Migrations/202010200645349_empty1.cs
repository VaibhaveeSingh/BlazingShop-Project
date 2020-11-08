namespace Blazing_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class empty1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "ShadeColour", c => c.String(nullable: false));
            AlterColumn("dbo.Categories", "CName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Categories", "CName", c => c.String());
            AlterColumn("dbo.Products", "ShadeColour", c => c.String());
            AlterColumn("dbo.Products", "Name", c => c.String());
        }
    }
}
