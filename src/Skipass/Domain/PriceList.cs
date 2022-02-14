using System;

namespace Skipass.Domain;

public class PriceListItem
{
    public int Identifier { get; set; }
    public string Name { get; set; }
    public int Hours { get; set; }
    public double Price { get; set; }
}