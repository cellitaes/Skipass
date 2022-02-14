namespace Skipass.Workers.Messages;

interface IMessage { }

record NullMessage() : IMessage
{
    public static readonly NullMessage Instance = new();
};