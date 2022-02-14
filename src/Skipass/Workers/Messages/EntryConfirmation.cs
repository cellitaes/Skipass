namespace Skipass.Workers.Messages;

record EntryConfirmation(
    int StatusCode,
    string CardIdentifier,
    string GateIdentifier
) : IMessage;