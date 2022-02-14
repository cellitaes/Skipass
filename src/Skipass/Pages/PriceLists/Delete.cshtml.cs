using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Skipass.Domain;


namespace Skipass.Pages.PriceLists
{
    public class DeleteModel : PageModel
    {
        private readonly Skipass.Database.SkipassDbContext _context;

        public DeleteModel(Skipass.Database.SkipassDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PriceListItem PriceItem { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PriceItem = await _context.PriceList.FindAsync(id);

            if (PriceItem == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PriceItem = await _context.PriceList.FindAsync(id);

            if (PriceItem != null)
            {
                _context.PriceList.Remove(PriceItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
