using BankAccountManagement.Data;
using BankAccountManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountManagement.Services
{
    public class CheckingAccountService
    {
        public bool CreateCheckingAccount(CheckingAccountCreate model)
        {
            var entity =
                new CheckingAccount()
                {
                    CheckingBalance = model.CheckingBalance,
                    CustomerId = model.CustomerId
                    
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.CheckingAccounts.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<CheckingAccountListItem> GetAllCheckingAccounts()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .CheckingAccounts
                    .Where(e => e.CheckingAccountId == e.CheckingAccountId)
                    .Select(
                        e =>
                        new CheckingAccountListItem
                        {
                            CheckingAccountId = e.CheckingAccountId,
                            CheckingAccountBalance = e.CheckingBalance,
                            CustomerName = e.Customer.FirstName

                        });
                return query.ToArray();
            }
        }
        public CheckingAccountDetail GetCheckingAccountById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .CheckingAccounts
                    .Single(e => e.CheckingAccountId == id);



                return
                    new CheckingAccountDetail
                    {
                        CheckingAccountId = entity.CheckingAccountId,
                        CheckingBalance = entity.CheckingBalance
                        
                    };


            };

        }



        public bool UpdateCheckingAccount(CheckingAccountEdit model, int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx.CheckingAccounts
                    .Single(e => e.CheckingAccountId == id);

                entity.CheckingBalance = model.CheckingBalance;

                return ctx.SaveChanges() == 1;
            }
        }


    }
}

