namespace Skipass.Workers.Messages;

record EntryRequest(
    string CardIdentifier,
    string GateIdentifier
) : IMessage;