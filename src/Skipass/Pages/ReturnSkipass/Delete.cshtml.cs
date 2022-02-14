using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Skipass.Domain;


namespace Skipass.Pages.ReturnSkipass;
[Authorize(Roles = "Admin, Seller")]
public class DeleteModel : PageModel
{
    private readonly Skipass.Database.SkipassDbContext _context;

    public DeleteModel(Skipass.Database.SkipassDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Card Card { get; set; }

    public async Task<IActionResult> OnGetAsync(string? cardExist)
    {
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (Card.Identifier == null)
        {
            return Page(); ;
        }

        Card = await _context.Cards.FindAsync(Card.Identifier);

        if (Card == null)
        {
            ModelState.AddModelError("cardNotExist", "NIE MA KARTY O PODANYM IDENTYFIKATORZE");
            return Page();
        }

        _context.Cards.Remove(Card);
        await _context.SaveChangesAsync();


        return Page();
    }
}
