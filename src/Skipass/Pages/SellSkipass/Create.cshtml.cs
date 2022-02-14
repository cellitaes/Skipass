using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Skipass.Domain;


namespace Skipass.Pages.SellSkipass;

[Authorize(Roles = "Admin, Seller")]
public class CreateModel : PageModel
{
    private readonly Skipass.Database.SkipassDbContext _context;

    public CreateModel(Skipass.Database.SkipassDbContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        TakePriceList();
        return Page();
    }

    [BindProperty]
    public Card Card { get; set; }

    [BindProperty]
    public int? ChosenPriceList { get; set; }

    [BindProperty]
    public int? PassagesLeft { get; set; }

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
        var card = await this._context.Cards.FindAsync(Card.Identifier);
        if (card != null)
        {
            ModelState.AddModelError("cardAlreadyCharged", "JUŻ ZAREJESTROWNAO KARTĘ O PODANYM IDENTYFIKATORZE");
            TakePriceList();
            return Page();
        }

        var priceList = await this._context.PriceList.FindAsync(ChosenPriceList);
        if (priceList == null && Card.PassagesLeft == null)
        {
            TakePriceList();
            return Page();
        }

        var newCard = new Card
        {
            Identifier = Card.Identifier,
            PassagesLeft = Card.PassagesLeft,
            ValidTo = priceList == null ? DateTime.UtcNow : DateTime.UtcNow.AddHours(priceList.Hours),
        };

        var result = await _context.Cards.AddAsync(newCard);

        var payment = new Payment
        {
            Card = result.Entity,
            PassagesAdded = Card.PassagesLeft,
            TimeAdded = TimeSpan.FromHours(priceList?.Hours ?? 0),
            Price = (priceList?.Price ?? 0) + Card.PassagesLeft * 2,
            Time = DateTime.UtcNow,
        };

        await _context.Payments.AddAsync(payment);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }


    public void TakePriceList()
    {
        var list = _context.PriceList.OrderBy(p => p.Identifier).ToList();

        list.Insert(0, new PriceListItem() { Identifier = -1, Name = "Brak", Price = 0 });

        ViewData["Prices"] = list;
    }
}
