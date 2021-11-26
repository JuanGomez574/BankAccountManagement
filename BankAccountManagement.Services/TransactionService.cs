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
        private readonly Guid _employeeId;

        public TransactionService(Guid employeeId)
        {
            _employeeId = employeeId;
        }
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
                SavingsAccount savingsAccount = ctx.SavingsAccounts.Find(model.SavingsAccountId);
                if (savingsAccount != null)
                {
                    
                    if (model.TypeOfTransaction.Equals(TransactionType.Debit) && savingsAccount.SavingsBalance > model.AmountOfTransaction)
                    {
                        savingsAccount.SavingsBalance = savingsAccount.SavingsBalance - model.AmountOfTransaction;
                    }

                    if (model.TypeOfTransaction.Equals(TransactionType.Credit))
                    {
                        savingsAccount.SavingsBalance = savingsAccount.SavingsBalance + model.AmountOfTransaction;
                    }

                }

                var checkingAccount = ctx.CheckingAccounts.Find(model.CheckingAccountId);
                if (checkingAccount != null)
                {
                    if (model.TypeOfTransaction.Equals(TransactionType.Debit) && checkingAccount.CheckingBalance > model.AmountOfTransaction)
                    {
                        checkingAccount.CheckingBalance = checkingAccount.CheckingBalance - model.AmountOfTransaction;
                    }

                    if (model.TypeOfTransaction.Equals(TransactionType.Credit))
                    {
                        checkingAccount.CheckingBalance = checkingAccount.CheckingBalance + model.AmountOfTransaction;
                    }
                }

                ctx.Transactions.Add(entity);
                return ctx.SaveChanges() > 0;
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
        public bool UpdateTransaction(TransactionEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Transactions
                        .Single(e => e.TransactionId == model.TransactionId);

                SavingsAccount savingsAccount = ctx.SavingsAccounts.Find(model.SavingsAccountId);
                if (savingsAccount != null)
                {

                    if (model.TypeOfTransaction.Equals(TransactionType.Debit) && savingsAccount.SavingsBalance > model.AmountOfTransaction && entity.AmountOfTransaction > model.AmountOfTransaction)
                    {
                        savingsAccount.SavingsBalance = savingsAccount.SavingsBalance + (entity.AmountOfTransaction - model.AmountOfTransaction);
                    }
                    if (model.TypeOfTransaction.Equals(TransactionType.Debit) && savingsAccount.SavingsBalance > model.AmountOfTransaction && entity.AmountOfTransaction < model.AmountOfTransaction)
                    {
                        savingsAccount.SavingsBalance = savingsAccount.SavingsBalance + (entity.AmountOfTransaction - model.AmountOfTransaction);
                    }

                    if (model.TypeOfTransaction.Equals(TransactionType.Credit) && entity.AmountOfTransaction > model.AmountOfTransaction)
                    {
                        savingsAccount.SavingsBalance = savingsAccount.SavingsBalance - (entity.AmountOfTransaction - model.AmountOfTransaction);
                    }
                    if (model.TypeOfTransaction.Equals(TransactionType.Credit) && entity.AmountOfTransaction < model.AmountOfTransaction)
                    {
                        savingsAccount.SavingsBalance = savingsAccount.SavingsBalance + (model.AmountOfTransaction - entity.AmountOfTransaction);
                    }

                }

                var checkingAccount = ctx.CheckingAccounts.Find(model.CheckingAccountId);
                if (checkingAccount != null)
                {
                    if (model.TypeOfTransaction.Equals(TransactionType.Debit) && checkingAccount.CheckingBalance > model.AmountOfTransaction)
                    {
                        checkingAccount.CheckingBalance = checkingAccount.CheckingBalance + (entity.AmountOfTransaction - model.AmountOfTransaction);
                    }

                    if (model.TypeOfTransaction.Equals(TransactionType.Credit))
                    {
                        checkingAccount.CheckingBalance = checkingAccount.CheckingBalance + model.AmountOfTransaction;
                    }
                }

                entity.AmountOfTransaction = model.AmountOfTransaction;
                entity.TypeOfTransaction = model.TypeOfTransaction;
                entity.TransactionDescription = model.TransactionDescription;
                entity.SavingsAccountId = model.SavingsAccountId;
                entity.TransactionId = model.TransactionId;
                entity.CheckingAccountId = model.CheckingAccountId;

                

                return ctx.SaveChanges() > 0;
            }
        }
        public bool DeleteTransaction(int transactionId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Transactions
                        .Single(e => e.TransactionId == transactionId );

                ctx.Transactions.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
