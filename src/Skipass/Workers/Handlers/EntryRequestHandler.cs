using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Skipass.Database;
using Skipass.Workers.Messages;

namespace Skipass.Workers.Handlers;

class EntryRequestHandler
{
    public const int MIN_ENTRY_MINUTES = 1;
    private readonly SkipassDbContext database;

    public EntryRequestHandler(SkipassDbContext database)
    {
        this.database = database;
    }

    public async Task<EntryResponse> Handle(EntryRequest request)
    {
        var card = await database.Cards.FindAsync(request.CardIdentifier);
        if (card == null) return new EntryResponse(404);

        var lastPassage = await database.Passages
            .Where(e => e.Card.Identifier == card.Identifier)
            .OrderByDescending(e => e.Time)
            .FirstOrDefaultAsync();

        if ((card.ValidTo < DateTime.UtcNow && card.PassagesLeft <= 0) || (lastPassage != null && DateTime.UtcNow - lastPassage.Time < TimeSpan.FromMinutes(MIN_ENTRY_MINUTES)))
        {
            return new EntryResponse(403, card.ValidTo, 0);
        }

        return new EntryResponse(200, card.ValidTo, card.PassagesLeft - 1);
    }
}