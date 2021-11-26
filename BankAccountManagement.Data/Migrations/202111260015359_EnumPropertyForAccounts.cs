namespace BankAccountManagement.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EnumPropertyForAccounts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CheckingAccount", "TypeOfCheckingAccount", c => c.Int(nullable: false));
            AddColumn("dbo.SavingsAccount", "TypeOfSavingsAccount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SavingsAccount", "TypeOfSavingsAccount");
            DropColumn("dbo.CheckingAccount", "TypeOfCheckingAccount");
        }
    }
}
