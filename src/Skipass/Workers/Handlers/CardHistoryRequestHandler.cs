using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Skipass.Database;
using Skipass.Workers.Messages;

namespace Skipass.Workers.Handlers;

class CardHistoryRequestHandler
{
    private readonly SkipassDbContext database;

    public CardHistoryRequestHandler(SkipassDbContext database)
    {
        this.database = database;
    }

    public async Task<CardHistoryResponse> Handle(CardHistoryRequest request)
    {
        var card = await database.Cards
            .FindAsync(request.CardIdentifier);

        if (card == null) return new CardHistoryResponse(404, Enumerable.Empty<CardHistoryEntry>());

        var passages = await database.Passages
            .Include(passage => passage.Gate)
            .Where(passage => passage.Card.Identifier == card.Identifier)
            .Take(10)
            .ToListAsync();

        if (passages == null || !passages.Any()) return new CardHistoryResponse(200, Enumerable.Empty<CardHistoryEntry>());

        return new CardHistoryResponse(200, passages.Select(passage => new CardHistoryEntry(passage.Gate.Identifier, passage.Time)));
    }
}