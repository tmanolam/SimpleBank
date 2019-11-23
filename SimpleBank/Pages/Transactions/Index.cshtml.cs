using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SimpleBank.Data;
using SimpleBank.Models;

namespace SimpleBank.Pages.Transactions
{
    public class IndexModel : PageModel
    {
        private readonly SimpleBank.Data.SimpleBankContext _context;

        public IndexModel(SimpleBank.Data.SimpleBankContext context)
        {
            _context = context;
        }

        public IList<Transaction> Transaction { get;set; }

        public async Task OnGetAsync()
        {
            Transaction = await _context.Transaction
                .Include(t => t.Account).ToListAsync();
        }
    }
}
