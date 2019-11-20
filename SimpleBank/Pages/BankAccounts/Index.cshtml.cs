using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SimpleBank.Data;
using SimpleBank.Models;

namespace SimpleBank.Pages.BankAccounts
{
    public class IndexModel : PageModel
    {
        private readonly SimpleBank.Data.SimpleBankContext _context;

        public IndexModel(SimpleBank.Data.SimpleBankContext context)
        {
            _context = context;
        }

        public IList<BankAccount> BankAccount { get;set; }

        public async Task OnGetAsync()
        {
            BankAccount = await _context.BankAccount.ToListAsync();
        }
    }
}
