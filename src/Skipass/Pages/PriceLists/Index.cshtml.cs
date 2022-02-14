using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Skipass.Domain;

namespace Skipass.Pages.PriceLists;
[Authorize(Roles = "Admin, Seller")]
public class IndexModel : PageModel
{
    private readonly Skipass.Database.SkipassDbContext _context;

    public IndexModel(Skipass.Database.SkipassDbContext context)
    {
        _context = context;
    }

    public IList<PriceListItem> PriceList { get; set; }

    public async Task OnGet()
    {
        PriceList = await _context.PriceList.OrderBy(p => p.Identifier).ToListAsync();
    }
}