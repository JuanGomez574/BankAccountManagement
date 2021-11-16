using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountManagement.Models
{
    public class CustomerListItem
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string FullName { get; set; }
        public string Email { get; set; }
        public int? PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public int? SocialSecurityNumber { get; set; }
    }
}
