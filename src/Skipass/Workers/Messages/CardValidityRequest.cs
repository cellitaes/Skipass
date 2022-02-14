using System;

namespace Skipass.Workers.Messages;

record CardValidityRequest(
    string CardIdentifier
) : IMessage;