using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountManagement.Data
{
    public enum SavingsAccountType
    {
        Personal = 1,
        Student = 2,
        Business = 3
    }
    public class SavingsAccount
    {
        [Key]
        public int SavingsAccountId { get; set; }
        [Required]
        public Guid BankEmployeeId { get; set; }
        public virtual Customer Customer { get; set; }
        [Required]
        public DateTimeOffset CreatedUtc { get; set; }
        [Required]
        public decimal SavingsBalance { get; set; }
        public int SAccountNumber { get; set; }
        public SavingsAccountType TypeOfSavingsAccount { get; set; }

        public virtual List<Transaction> Transactions { get; set; }
        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }
    }
}
