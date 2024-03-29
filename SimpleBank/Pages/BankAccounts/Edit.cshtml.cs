﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SimpleBank.Data;
using SimpleBank.Models;

namespace SimpleBank.Pages.BankAccounts
{
    public class EditModel : PageModel
    {
        private readonly SimpleBank.Data.SimpleBankContext _context;

        public EditModel(SimpleBank.Data.SimpleBankContext context)
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
           ViewData["UserID"] = new SelectList(_context.User, "UserID", "UserID");
            return Page();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(BankAccount).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BankAccountExists(BankAccount.BankAccountID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BankAccountExists(string id)
        {
            return _context.BankAccount.Any(e => e.BankAccountID == id);
        }
    }
}
