﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBank.Models
{
    public class BankAccount
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string BankAccountID { get; set; }
        public string UserID { get; set; }
        public double Balance { get; set; }
        public User User { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
