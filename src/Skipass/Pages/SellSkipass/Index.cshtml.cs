using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Skipass.Domain;

namespace Skipass.Pages.SellSkipass;
[Authorize(Roles = "Admin, Seller")]
public class IndexModel : PageModel
{
    private readonly Skipass.Database.SkipassDbContext _context;

    public IndexModel(Skipass.Database.SkipassDbContext context)
    {
        _context = context;
    }

    public IList<Card> Cards { get; set; }

    public async Task OnGet()
    {
        Cards = await _context.Cards.OrderBy(e => e.Identifier).ToListAsync();
    }
}
