using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBank.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string UserID { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<BankAccount> BankAccounts { get; set; }
    }
}
