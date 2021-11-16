namespace BankAccountManagement.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CheckingAccount",
                c => new
                    {
                        CheckingAccountId = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        CreatedUtc = c.DateTimeOffset(nullable: false, precision: 7),
                        CheckingBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.CheckingAccountId)
                .ForeignKey("dbo.Customer", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        FullName = c.String(nullable: false),
                        Email = c.String(),
                        PhoneNumber = c.Int(),
                        DateOfBirth = c.DateTime(nullable: false),
                        Address = c.String(nullable: false),
                        SocialSecurityNumber = c.Int(),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "dbo.Transaction",
                c => new
                    {
                        TransactionId = c.Int(nullable: false, identity: true),
                        AmountOfTransaction = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedUtc = c.DateTimeOffset(nullable: false, precision: 7),
                        TypeOfTransaction = c.Int(nullable: false),
                        TransactionDescription = c.String(nullable: false),
                        CheckingAccountId = c.Int(),
                        SavingsAccountId = c.Int(),
                    })
                .PrimaryKey(t => t.TransactionId)
                .ForeignKey("dbo.CheckingAccount", t => t.CheckingAccountId)
                .ForeignKey("dbo.SavingsAccount", t => t.SavingsAccountId)
                .Index(t => t.CheckingAccountId)
                .Index(t => t.SavingsAccountId);
            
            CreateTable(
                "dbo.SavingsAccount",
                c => new
                    {
                        SavingsAccountId = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        CreatedUtc = c.DateTimeOffset(nullable: false, precision: 7),
                        SavingsBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.SavingsAccountId)
                .ForeignKey("dbo.Customer", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.IdentityRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(),
                        IdentityRole_Id = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.IdentityRole", t => t.IdentityRole_Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.ApplicationUser",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogin",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRole", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserLogin", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserClaim", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserRole", "IdentityRole_Id", "dbo.IdentityRole");
            DropForeignKey("dbo.Transaction", "SavingsAccountId", "dbo.SavingsAccount");
            DropForeignKey("dbo.SavingsAccount", "CustomerId", "dbo.Customer");
            DropForeignKey("dbo.Transaction", "CheckingAccountId", "dbo.CheckingAccount");
            DropForeignKey("dbo.CheckingAccount", "CustomerId", "dbo.Customer");
            DropIndex("dbo.IdentityUserLogin", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaim", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "IdentityRole_Id" });
            DropIndex("dbo.SavingsAccount", new[] { "CustomerId" });
            DropIndex("dbo.Transaction", new[] { "SavingsAccountId" });
            DropIndex("dbo.Transaction", new[] { "CheckingAccountId" });
            DropIndex("dbo.CheckingAccount", new[] { "CustomerId" });
            DropTable("dbo.IdentityUserLogin");
            DropTable("dbo.IdentityUserClaim");
            DropTable("dbo.ApplicationUser");
            DropTable("dbo.IdentityUserRole");
            DropTable("dbo.IdentityRole");
            DropTable("dbo.SavingsAccount");
            DropTable("dbo.Transaction");
            DropTable("dbo.Customer");
            DropTable("dbo.CheckingAccount");
        }
    }
}
