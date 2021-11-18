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
        public bool CreateSavingsAccount(SavingsAccountCreate model)
        {
            var entity =
                new SavingsAccount()
                {
                    SavingsBalance = model.SavingsBalance,
                    CustomerId = model.CustomerId
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
                            CustomerName = e.Customer.FirstName
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
                        SavingsBalance = entity.SavingsBalance
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

                entity.SavingsBalance = model.SavingsBalance;;
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
            List<SelectListItem> customers = new List<SelectListItem>();
            using (var ctx = new ApplicationDbContext())
            {
                customers = ctx.Customers.Select(
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
