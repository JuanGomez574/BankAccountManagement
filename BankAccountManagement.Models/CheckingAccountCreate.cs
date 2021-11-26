using BankAccountManagement.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountManagement.Models
{
    public class CheckingAccountCreate
    {        
        [Display(Name = "Starting Checking Balance")]
        public decimal CheckingBalance { get; set; }
        public int CustomerId { get; set; }
        public int CAccountNumber { get; set; }
        public CheckingAccountType TypeOfCheckingAccount { get; set; }
    }
}
