using System.Collections.Generic;
using System.Linq;

namespace Skipass.Workers.Messages;

record CardHistoryResponse(
    int StatusCode,
    IEnumerable<CardHistoryEntry> History
) : IMessage;