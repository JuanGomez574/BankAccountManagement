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
        private readonly Guid _employeeId;

        public CheckingAccountService(Guid employeeId)
        {
            _employeeId = employeeId;
        }
        public bool CreateCheckingAccount(CheckingAccountCreate model)
        {
            var random = new Random();
            model.CAccountNumber = random.Next(100000000, 999999999);
            var entity =
                new CheckingAccount()
                {
                    CheckingBalance = model.CheckingBalance,
                    CustomerId = model.CustomerId,
                    CAccountNumber = model.CAccountNumber,
                    TypeOfCheckingAccount = model.TypeOfCheckingAccount
                    
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
                            CustomerName = e.Customer.FirstName,
                            CAccountNumber = e.CAccountNumber,
                            TypeOfCheckingAccount = e.TypeOfCheckingAccount
                            

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
                        CheckingBalance = entity.CheckingBalance,
                        CAccountNumber = entity.CAccountNumber,
                        TypeOfCheckingAccount = entity.TypeOfCheckingAccount,
                        Transactions = entity.Transactions
                        
                    };


            };

        }
        public bool UpdateCheckingAccount(CheckingAccountEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .CheckingAccounts
                        .Single(e => e.CheckingAccountId == model.CheckingAccountId);

                entity.CheckingBalance = model.CheckingBalance;
                entity.TypeOfCheckingAccount = model.TypeOfCheckingAccount;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteCheckingAccount(int checkingAccountId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .CheckingAccounts
                        .Single(e => e.CheckingAccountId == checkingAccountId);

                ctx.CheckingAccounts.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

    }
}

