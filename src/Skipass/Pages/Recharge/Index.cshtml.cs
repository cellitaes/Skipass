using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Skipass.Domain;

namespace Skipass.Pages.Recharge;
[Authorize(Roles = "Admin, Seller")]
public class CreateModel : PageModel
{
    private readonly Skipass.Database.SkipassDbContext _context;

    public CreateModel(Skipass.Database.SkipassDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> OnGetAsync(string? id)
    {
        var card = await _context.Cards.FindAsync(id);
        if (card == null) return NotFound("There is no card with given identifier");

        ViewData["StillValid"] = false;
        ViewData["CardID"] = card.Identifier;

        if (card.PassagesLeft != 0)
        {
            ViewData["PassagesLeft"] = card.PassagesLeft;
        }
        else
        {
            if (card.ValidTo > DateTime.UtcNow)
            {
                ViewData["StillValid"] = true;
            }
        }

        TakePriceList();

        return Page();
    }

    [BindProperty]
    public Card Cards { get; set; }

    [BindProperty]
    public int? ChosenPriceList { get; set; }

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync(string id)
    {
        var card = await this._context.Cards.FindAsync(id);
        if (card == null)
        {
            TakePriceList();
            return RedirectToPage("./Index");
        }

        var priceList = await this._context.PriceList.FindAsync(ChosenPriceList);
        if (priceList == null && Cards.PassagesLeft == null)
        {
            TakePriceList();
            return Page();
        }

        DateTime date = DateTime.UtcNow;

        if (date > card.ValidTo)
        {
            date = priceList == null ? date : date.AddHours(priceList.Hours);
        }
        else
        {
            date = card.ValidTo.AddHours(priceList.Hours);
        }

        card.PassagesLeft += Cards.PassagesLeft;
        card.ValidTo = date;

        var result = _context.Cards.Update(card);

        var payment = new Payment
        {
            Card = result.Entity,
            PassagesAdded = Cards.PassagesLeft,
            TimeAdded = TimeSpan.FromHours(priceList?.Hours ?? 0),
            Price = (priceList?.Price ?? 0) + Cards.PassagesLeft * 2,
            Time = DateTime.UtcNow,
        };

        await _context.Payments.AddAsync(payment);
        await _context.SaveChangesAsync();

        return RedirectToPage("/SellSkipass/Index");
    }

    public void TakePriceList()
    {
        var list = _context.PriceList.OrderBy(p => p.Identifier).ToList();

        list.Insert(0, new PriceListItem() { Identifier = -1, Name = "Brak", Price = 0 });

        ViewData["Prices"] = list;
    }
}
