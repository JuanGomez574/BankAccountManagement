namespace BankAccountManagement.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AccountNumberPropertyChanged : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.CheckingAccount", "AccountNumber");
            DropColumn("dbo.SavingsAccount", "AccountNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SavingsAccount", "AccountNumber", c => c.Int(nullable: false));
            AddColumn("dbo.CheckingAccount", "AccountNumber", c => c.Int(nullable: false));
        }
    }
}
