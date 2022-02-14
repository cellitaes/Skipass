using System;

namespace Skipass.Workers.Messages;

record CardHistoryEntry(
    string GateIdentifier,
    DateTime Date
);