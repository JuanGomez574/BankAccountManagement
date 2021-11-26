﻿using BankAccountManagement.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountManagement.Models
{
    public class SavingsAccountEdit
    {
        public int SavingsAccountId { get; set; }
        public decimal SavingsBalance { get; set; }
        public SavingsAccountType TypeOfSavingsAccount { get; set; }
    }
}
