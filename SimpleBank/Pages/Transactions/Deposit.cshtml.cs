using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleBank.Data;
using SimpleBank.Models;

namespace SimpleBank.Pages.Transactions
{
    public class DepositModel : PageModel
    {
        private readonly SimpleBank.Data.SimpleBankContext _context;

        [BindProperty]
        public Transaction Transaction { get; set; }

        public BankAccount BankAccount { get; set; }

        public DepositModel(SimpleBank.Data.SimpleBankContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BankAccount = await _context.BankAccount.FindAsync(id);

            if (BankAccount == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            BankAccount = await _context.BankAccount.FindAsync(this.Transaction.BankAccountID);

            if (BankAccount == null)
            {
                return NotFound();
            }

            // Deposit
            this.Transaction.TransactionDate = DateTime.Now;
            this.Transaction.CreditType = CreditType.Deposit;
            this.Transaction.PreviousBalance = this.BankAccount.Balance;
            this.Transaction.NewBalance = this.BankAccount.Balance + this.Transaction.Amount;

            // Generate Fee transaction
            Transaction feeTrans = new Transaction();
            feeTrans.BankAccountID = this.Transaction.BankAccountID;
            feeTrans.TransactionDate = DateTime.Now;
            feeTrans.DebitType = DebitType.Fee;
            feeTrans.Amount = this.Transaction.Amount * 0.001;
            feeTrans.PreviousBalance = this.Transaction.NewBalance;
            feeTrans.NewBalance = this.Transaction.NewBalance - feeTrans.Amount;

            // Update Balance for bank account
            this.BankAccount.Balance = feeTrans.NewBalance;

            if (await TryUpdateModelAsync<BankAccount>(BankAccount))
            {
                _context.Transaction.Add(Transaction);
                await _context.SaveChangesAsync();

                _context.Transaction.Add(feeTrans);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/BankAccounts/Details", new { id = BankAccount.BankAccountID });
        }
    }
}