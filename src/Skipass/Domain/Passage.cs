using System;

namespace Skipass.Domain;

public class Passage
{
    public int Identifier { get; set; }
    public DateTime Time { get; set; }

    public virtual Card Card { get; set; }
    public virtual Gate Gate { get; set; }
}