using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountManagement.Data
{
    public class SavingsAccount
    {
        [Key]
        public int SavingsAccountId { get; set; }      
        public virtual Customer Customer { get; set; }
        [Required]
        public DateTimeOffset CreatedUtc { get; set; }
        [Required]
        public decimal SavingsBalance { get; set; }
        public virtual List<Transaction> Transactions { get; set; }
        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }
    }
}
