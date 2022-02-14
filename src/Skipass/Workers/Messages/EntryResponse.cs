using System;

namespace Skipass.Workers.Messages;

record EntryResponse(
    int StatusCode,
    DateTime? TimeLeft = null,
    int? PassagesLeft = null
) : IMessage;