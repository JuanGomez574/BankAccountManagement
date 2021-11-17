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
    public class TransactionService
    {
        public bool CreateTransaction(TransactionCreate model)
        {
            var entity =
                new Transaction()
                {
                    AmountOfTransaction = model.AmountOfTransaction,
                    TypeOfTransaction = model.TypeOfTransaction,
                    TransactionDescription = model.TransactionDescription,
                    CheckingAccountId = model.CheckingAccountId,
                    SavingsAccountId = model.SavingsAccountId
                };
            using (var ctx = new ApplicationDbContext())
            {

                ctx.Transactions.Add(entity);
                var checkingAccount = ctx.SavingsAccounts.Find(model.SavingsAccountId);
                checkingAccount.SavingsBalance = checkingAccount.SavingsBalance - model.AmountOfTransaction;

                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<TransactionListItem> GetAllTransactions()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Transactions
                    .Where(e => e.TransactionId == e.TransactionId)
                    .Select(
                        e =>
                        new TransactionListItem
                        {
                            TransactionId = e.TransactionId,
                            AmountOfTransaction = e.AmountOfTransaction,
                            TypeOfTransaction = e.TypeOfTransaction,
                            TransactionDescription = e.TransactionDescription,
                            CheckingAccountId = e.CheckingAccountId,
                            SavingsAccountId = e.SavingsAccountId
                        });
                return query.ToArray();
            }
        }
        public TransactionDetail GetTransactionsById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Transactions
                    .Single(e => e.TransactionId == id);



                return
                    new TransactionDetail
                    {
                        TransactionId = entity.TransactionId,
                        AmountOfTransaction = entity.AmountOfTransaction,
                        TypeOfTransaction = entity.TypeOfTransaction,
                        TransactionDescription = entity.TransactionDescription,
                        CheckingAccountId = entity.CheckingAccountId,
                        SavingsAccountId = entity.SavingsAccountId
                    };


            };

        }
        ////Helper to get list of SavingsAccounts
        //public List<SelectListItem> GetSavingsAccounts()
        //{
        //    List<SelectListItem> savingsAccounts = new List<SelectListItem>();
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        savingsAccounts = ctx.SavingsAccounts.Select(
        //            c =>
        //            new SelectListItem
        //            {
        //                Value = c.SavingsAccountId.ToString(),
        //                Text = c.SavingsAccountId.ToString(),
        //            }).ToList();
        //        return savingsAccounts;
        //    }
        //}
    }
}
