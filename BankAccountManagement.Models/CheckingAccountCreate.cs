using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountManagement.Models
{
    public class CheckingAccountCreate
    {        
        public decimal CheckingBalance { get; set; }
        public int CustomerId { get; set; }
    }
}
