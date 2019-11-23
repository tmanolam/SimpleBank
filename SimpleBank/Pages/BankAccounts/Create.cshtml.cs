using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SimpleBank.Data;
using SimpleBank.Models;

namespace SimpleBank.Pages.BankAccounts
{
    public class CreateModel : PageModel
    {
        private readonly SimpleBank.Data.SimpleBankContext _context;

        public CreateModel(SimpleBank.Data.SimpleBankContext context)
        {
            _context = context;
        }

        /*
        public IActionResult OnGet(string userId)
        {
            UserID = userId;

            if (userId != null)
                ViewData["UserID"] = new SelectList(_context.User, "UserID", "UserID", userId);
            else
                ViewData["UserID"] = new SelectList(_context.User, "UserID", "UserID");

            return Page();
        }
        */

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User = await _context.User.FindAsync(id);

            if (User == null)
            {
                return NotFound();
            }
            return Page();
        }

        [BindProperty]
        public BankAccount BankAccount { get; set; }
        public User User { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.BankAccount.Add(BankAccount);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Users/Details", new { id = BankAccount.UserID });
        }
    }
}
