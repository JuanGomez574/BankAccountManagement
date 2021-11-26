using BankAccountManagement.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountManagement.Models
{
    public class CheckingAccountDetail
    {
        public int CheckingAccountId { get; set; }
        public decimal CheckingBalance { get; set; }
        public int CAccountNumber { get; set; }
        public CheckingAccountType TypeOfCheckingAccount { get; set; }
        public List<Transaction> Transactions { get; set; }

    }
}
