namespace BankAccountManagement.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedAccountNumberProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CheckingAccount", "CAccountNumber", c => c.Int(nullable: false));
            AddColumn("dbo.SavingsAccount", "SAccountNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SavingsAccount", "SAccountNumber");
            DropColumn("dbo.CheckingAccount", "CAccountNumber");
        }
    }
}
