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
    public class DetailsModel : PageModel
    {
        private readonly SimpleBank.Data.SimpleBankContext _context;

        public DetailsModel(SimpleBank.Data.SimpleBankContext context)
        {
            _context = context;
        }

        public BankAccount BankAccount { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BankAccount = await _context.BankAccount.FirstOrDefaultAsync(m => m.ID == id);

            if (BankAccount == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
