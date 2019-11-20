using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBank.Models
{
    public class BankAccount
    {
        public int ID { get; set; }
        public string IBAN { get; set; }
        public string OwnerID { get; set; }
        public double Balance { get; set; }
    }
}
