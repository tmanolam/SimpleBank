using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleBank.Models;

namespace SimpleBank.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new SimpleBankContext(serviceProvider.GetRequiredService<DbContextOptions<SimpleBankContext>>()))
            {
                if (context.User.Any())
                {
                    return;
                }

                context.User.AddRange(
                    new User
                    {
                        UserID = "user01",
                        Name = "Jack Dowson",
                        PhoneNumber = "0891237797"
                    },
                    new User
                    {
                        UserID = "user02",
                        Name = "Charlie Angel",
                        PhoneNumber = "0996857214"
                    }
                );

                context.BankAccount.AddRange(
                    new BankAccount
                    {
                        BankAccountID = "NL93INGB9730769656",
                        UserID = "user01",
                        Balance = 0.0

                    },
                    new BankAccount
                    {
                        BankAccountID = "NL59ABNA9313499797",
                        UserID = "user02",
                        Balance = 0.0
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
