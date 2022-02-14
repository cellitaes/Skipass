using System;

namespace Skipass.Workers.Messages;

record CardValidityResponse(
    int StatusCode,
    DateTime? TimeLeft = null,
    int? PassagesLeft = null
) : IMessage;