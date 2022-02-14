using System;
using System.Threading.Tasks;
using Serilog;
using Skipass.Database;
using Skipass.Domain;
using Skipass.Workers.Messages;

namespace Skipass.Workers.Handlers;

class EntryConfirmationHandler
{
    private readonly ILogger logger;
    private readonly SkipassDbContext database;

    public EntryConfirmationHandler(ILogger logger, SkipassDbContext database)
    {
        this.logger = logger.ForContext<EntryConfirmationHandler>();
        this.database = database;
    }

    public async Task<NullMessage> Handle(EntryConfirmation request)
    {
        if (request.StatusCode != 200)
        {
            return NullMessage.Instance;
        }

        var card = await database.Cards.FindAsync(request.CardIdentifier);
        var gate = await database.Gates.FindAsync(request.GateIdentifier);

        if (card == null || gate == null)
        {
            this.logger.Error("Cannot get required data! (card: {}, gate: {})", request.CardIdentifier, request.GateIdentifier);
            return NullMessage.Instance;
        }

        await database.Passages
            .AddAsync(new Passage { Identifier = 0, Time = DateTime.UtcNow, Card = card, Gate = gate });

        if (card.ValidTo < DateTime.UtcNow)
        {
            card.PassagesLeft--;
            database.Cards.Update(card);
        }

        await database.SaveChangesAsync();
        return NullMessage.Instance;
    }
}