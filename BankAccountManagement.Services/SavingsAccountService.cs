using BankAccountManagement.Data;
using BankAccountManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BankAccountManagement.Services
{
    public class SavingsAccountService
    {
        private readonly Guid _employeeId;

        public SavingsAccountService(Guid employeeId)
        {
            _employeeId = employeeId;
        }
        public bool CreateSavingsAccount(SavingsAccountCreate model)
        {
            var random = new Random();
            model.SAccountNumber = random.Next(100000000, 999999999);
            var entity =
                new SavingsAccount()
                {
                    SavingsBalance = model.SavingsBalance,
                    CustomerId = model.CustomerId,
                    SAccountNumber = model.SAccountNumber,
                    TypeOfSavingsAccount =model.TypeOfSavingsAccount
                    
                    
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.SavingsAccounts.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<SavingsAccountListItem> GetAllSavingsAccounts()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .SavingsAccounts
                    .Where(e => e.SavingsAccountId == e.SavingsAccountId)
                    .Select(
                        e =>
                        new SavingsAccountListItem
                        {
                            SavingsAccountId = e.SavingsAccountId,
                            SavingsBalance = e.SavingsBalance,
                            CustomerName = e.Customer.FirstName,
                            SAccountNumber = e.SAccountNumber,
                            TypeOfSavingsAccount = e.TypeOfSavingsAccount
                        });
                return query.ToArray();
            }
        }
        public SavingsAccountDetail GetSavingsAccountById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .SavingsAccounts
                    .Single(e => e.SavingsAccountId == id);

                return
                    new SavingsAccountDetail
                    {
                        SavingsAccountId = entity.SavingsAccountId,
                        SavingsBalance = entity.SavingsBalance,
                        SAccountNumber = entity.SAccountNumber,
                        Transactions = entity.Transactions,
                        TypeOfSavingsAccount =entity.TypeOfSavingsAccount
                    };
            };

        }
        public bool UpdateSavingsAccount(SavingsAccountEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .SavingsAccounts
                        .Single(e => e.SavingsAccountId == model.SavingsAccountId);

                entity.SavingsBalance = model.SavingsBalance;
                entity.TypeOfSavingsAccount = model.TypeOfSavingsAccount;
                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteSavingsAccount(int savingsAccountId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .SavingsAccounts
                        .Single(e => e.SavingsAccountId == savingsAccountId);

                ctx.SavingsAccounts.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
        public List<SelectListItem> GetCustomers()
        {
            
            using (var ctx = new ApplicationDbContext())
            {
                var customers = ctx.Customers.Select(
                    c =>
                    new SelectListItem
                    {
                        Value = c.CustomerId.ToString(),
                        Text = c.FirstName
                    }).ToList();
                return customers;
            }
        }
    }
}
