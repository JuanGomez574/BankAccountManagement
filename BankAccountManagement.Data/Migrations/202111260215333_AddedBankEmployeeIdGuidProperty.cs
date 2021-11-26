namespace BankAccountManagement.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBankEmployeeIdGuidProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CheckingAccount", "BankEmployeeId", c => c.Guid(nullable: false));
            AddColumn("dbo.Customer", "BankEmployeeId", c => c.Guid(nullable: false));
            AddColumn("dbo.SavingsAccount", "BankEmployeeId", c => c.Guid(nullable: false));
            AddColumn("dbo.Transaction", "BankEmployeeId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Transaction", "BankEmployeeId");
            DropColumn("dbo.SavingsAccount", "BankEmployeeId");
            DropColumn("dbo.Customer", "BankEmployeeId");
            DropColumn("dbo.CheckingAccount", "BankEmployeeId");
        }
    }
}
