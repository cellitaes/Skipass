using System;

namespace Skipass.Domain;

public class Payment
{
    public long Identifier { get; set; }
    public TimeSpan? TimeAdded { get; set; }
    public int PassagesAdded { get; set; }
    public double Price { get; set; }
    public DateTime Time { get; set; }

    public virtual Card Card { get; set; }
}