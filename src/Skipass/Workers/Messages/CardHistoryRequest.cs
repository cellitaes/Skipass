namespace Skipass.Workers.Messages;

record CardHistoryRequest(
    string CardIdentifier,
    string ScannerIdentifier
) : IMessage;