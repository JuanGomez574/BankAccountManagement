using BankAccountManagement.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountManagement.Models
{
    public class TransactionListItem
    {
        public int TransactionId { get; set; }
        public decimal AmountOfTransaction { get; set; }
        public TransactionType TypeOfTransaction { get; set; }
        public string TransactionDescription { get; set; }
        public int? CheckingAccountId { get; set; }
        public int? SavingsAccountId { get; set; }
    }
}
