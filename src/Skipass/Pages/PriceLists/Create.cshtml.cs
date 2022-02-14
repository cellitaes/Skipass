using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Skipass.Domain;


namespace Skipass.Pages.PriceLists;
[Authorize(Roles = "Admin")]
public class CreateModel : PageModel
{
    private readonly Skipass.Database.SkipassDbContext _context;

    public CreateModel(Skipass.Database.SkipassDbContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public PriceListItem PriceList { get; set; }


    public async Task<IActionResult> OnPostAsync()
    {
        var card = await this._context.PriceList.FindAsync(PriceList.Identifier);
        if (card != null) return Page();

        var newPriceListItem = new PriceListItem
        {
            Identifier = 0,
            Name = PriceList.Name,
            Price = PriceList.Price,
            Hours = PriceList.Hours
        };

        await _context.PriceList.AddAsync(newPriceListItem);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
