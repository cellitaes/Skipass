using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Skipass.Domain;

namespace Skipass.Pages.PriceLists;
[Authorize(Roles = "Admin")]
public class EditModel : PageModel
{
    private readonly Skipass.Database.SkipassDbContext _context;

    public EditModel(Skipass.Database.SkipassDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public PriceListItem PriceList { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        PriceList = await _context.PriceList.FindAsync(id);

        if (PriceList == null)
        {
            return NotFound();
        }
        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (PriceList == null)
        {
            return NotFound();
        }

        _context.Attach(PriceList).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PriceListExists(PriceList.Identifier))
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

    private bool PriceListExists(int id)
    {
        return _context.PriceList.Any(e => e.Identifier == id);
    }
}
