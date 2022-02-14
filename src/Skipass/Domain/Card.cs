using System;

namespace Skipass.Domain;

public class Card
{
    public string Identifier { get; set; }
    public DateTime ValidTo { get; set; }
    public int PassagesLeft { get; set; }
}