namespace BankAccountManagement.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PropertiesChanged : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Customer", "FullName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customer", "FullName", c => c.String(nullable: false));
        }
    }
}
