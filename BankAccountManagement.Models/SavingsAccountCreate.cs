using BankAccountManagement.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountManagement.Models
{
    public class SavingsAccountCreate
    {
        
        public decimal SavingsBalance { get; set; }
        [Display(Name = "Name")]
        public int CustomerId { get; set; }
        public int SAccountNumber { get; set; }
        public SavingsAccountType TypeOfSavingsAccount { get; set; }
    }
}
