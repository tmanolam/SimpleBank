using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleBank.Models;

namespace SimpleBank.Data
{
    public class SimpleBankContext : DbContext
    {
        public SimpleBankContext (DbContextOptions<SimpleBankContext> options)
            : base(options)
        {
        }

        public DbSet<SimpleBank.Models.BankAccount> BankAccount { get; set; }

        public DbSet<SimpleBank.Models.User> User { get; set; }

        public DbSet<SimpleBank.Models.Transaction> Transaction { get; set; }
    }
}
