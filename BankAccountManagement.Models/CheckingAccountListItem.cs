using BankAccountManagement.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountManagement.Models
{
    public class CheckingAccountListItem
    {
        public int CheckingAccountId { get; set; }
        public decimal CheckingAccountBalance { get; set; }
        public string CustomerName { get; set; }
        public int CAccountNumber { get; set; }
        public CheckingAccountType TypeOfCheckingAccount { get; set; }
    }
}
