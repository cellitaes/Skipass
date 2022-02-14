using System;
using System.Threading.Tasks;
using Skipass.Database;
using Skipass.Workers.Messages;

namespace Skipass.Workers.Handlers;

class CardValidityRequestHandler
{
    private readonly SkipassDbContext database;

    public CardValidityRequestHandler(SkipassDbContext database)
    {
        this.database = database;
    }

    public async Task<CardValidityResponse> Handle(CardValidityRequest request)
    {
        var card = await database.Cards.FindAsync(request.CardIdentifier);
        if (card == null) return new CardValidityResponse(404);

        return new CardValidityResponse(200, card.ValidTo, card.PassagesLeft);
    }
}