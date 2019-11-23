using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SimpleBank.Data;
using SimpleBank.Models;

namespace SimpleBank.Pages.Users
{
    public class DetailsModel : PageModel
    {
        private readonly SimpleBank.Data.SimpleBankContext _context;

        public DetailsModel(SimpleBank.Data.SimpleBankContext context)
        {
            _context = context;
        }

        public User User { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User = await _context.User
                .Include(u => u.BankAccounts)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.UserID == id);

            if (User == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
