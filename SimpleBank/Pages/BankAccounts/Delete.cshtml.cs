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
    public class DeleteModel : PageModel
    {
        private readonly SimpleBank.Data.SimpleBankContext _context;

        public DeleteModel(SimpleBank.Data.SimpleBankContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BankAccount BankAccount { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BankAccount = await _context.BankAccount
                .Include(b => b.User).FirstOrDefaultAsync(m => m.BankAccountID == id);

            if (BankAccount == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BankAccount = await _context.BankAccount.FindAsync(id);

            if (BankAccount != null)
            {
                _context.BankAccount.Remove(BankAccount);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Users/Details", new { id = BankAccount.UserID });
        }
    }
}
