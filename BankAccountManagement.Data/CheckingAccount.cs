using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountManagement.Data
{
    public enum CheckingAccountType
    {
        Personal = 1,
        Student =2,
        Business = 3
    }
    public class CheckingAccount
    {
        [Key]
        public int CheckingAccountId { get; set; }      
        [Required]
        public Guid BankEmployeeId { get; set; }
        [Required]
        [Display(Name = "Date Created")]
        public DateTimeOffset CreatedUtc { get; set; }
        [Required]
        public decimal CheckingBalance { get; set; }
        public int CAccountNumber { get; set; }
        public CheckingAccountType TypeOfCheckingAccount { get; set; }

        public virtual List<Transaction> Transactions { get; set; }
        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }

}
