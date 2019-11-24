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
    public class TransferModel : PageModel
    {
        private readonly SimpleBank.Data.SimpleBankContext _context;

        [BindProperty]
        public Transaction Transaction { get; set; }

        [BindProperty]
        public BankAccount TransferToAccount { get; set; }

        public BankAccount BankAccount { get; set; }

        public TransferModel(SimpleBank.Data.SimpleBankContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transferToQuery = from b in _context.BankAccount
                                  where b.BankAccountID != id
                                  select b;

            // List of Bank account to transfer money to
            ViewData["BankAccountID"] = new SelectList(transferToQuery, "BankAccountID", "BankAccountID");

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

            var transTo = await _context.BankAccount.FindAsync(this.TransferToAccount.BankAccountID);
            if (transTo == null)
            {
                return NotFound();
            }

            // validation
            if (this.BankAccount.Balance < this.Transaction.Amount)
                return Page();

            // source account
            this.Transaction.TransactionDate = DateTime.Now;
            this.Transaction.DebitType = DebitType.TransferOut;
            this.Transaction.PreviousBalance = this.BankAccount.Balance;
            this.Transaction.NewBalance = this.BankAccount.Balance - this.Transaction.Amount;

            // destination account
            Transaction destTrans = new Transaction();
            destTrans.BankAccountID = transTo.BankAccountID;
            destTrans.TransactionDate = DateTime.Now;
            destTrans.CreditType = CreditType.TransferIn;
            destTrans.Amount = this.Transaction.Amount;
            destTrans.PreviousBalance = transTo.Balance;
            destTrans.NewBalance = transTo.Balance + this.Transaction.Amount;

            // Update Balance for bank account
            this.BankAccount.Balance = this.Transaction.NewBalance;
            transTo.Balance = destTrans.NewBalance;

            _context.Transaction.Add(Transaction);
            _context.Transaction.Add(destTrans);

            if (await TryUpdateModelAsync<BankAccount>(BankAccount))
            {
                if (await TryUpdateModelAsync<BankAccount>(transTo))
                    await _context.SaveChangesAsync();
            }

            return RedirectToPage("/BankAccounts/Details", new { id = BankAccount.BankAccountID });
        }

    }
}