using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountManagement.Data
{
    public enum TransactionType
    {
        Credit = 1,
        Debit = 2
    }
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        [Required]
        public decimal AmountOfTransaction { get; set; }
        [Required]
        [Display(Name = "Date Created")]
        public DateTimeOffset CreatedUtc { get; set; }
        [Required]
        public TransactionType TypeOfTransaction { get; set; }
        [Required]
        public string TransactionDescription { get; set; }
        [ForeignKey(nameof(CheckingAccount))]
        public int? CheckingAccountId { get; set; }
        public virtual CheckingAccount CheckingAccount { get; set; }

        [ForeignKey(nameof(SavingsAccount))]
        public int? SavingsAccountId { get; set; }
        public virtual SavingsAccount SavingsAccount { get; set; }

    }
}
