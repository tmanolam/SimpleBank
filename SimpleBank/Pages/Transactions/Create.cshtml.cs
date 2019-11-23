using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SimpleBank.Data;
using SimpleBank.Models;

namespace SimpleBank.Pages.Transactions
{
    public class CreateModel : PageModel
    {
        private readonly SimpleBank.Data.SimpleBankContext _context;

        public CreateModel(SimpleBank.Data.SimpleBankContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["BankAccountID"] = new SelectList(_context.BankAccount, "BankAccountID", "BankAccountID");
            return Page();
        }

        [BindProperty]
        public Transaction Transaction { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Transaction.Add(Transaction);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
