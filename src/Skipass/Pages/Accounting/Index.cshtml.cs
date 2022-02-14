using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Skipass.Domain;

namespace Skipass.Pages.Accounting;
[Authorize(Roles = "Admin, Seller")]
public class IndexModel : PageModel
{
    private readonly Skipass.Database.SkipassDbContext _context;

    public IndexModel(Skipass.Database.SkipassDbContext context)
    {
        _context = context;
    }

    public int PageNumber { get; set; }
    public double MoneySum { get; set; }
    public IList<AccountingItem> Items { get; set; }

    public async Task OnGet([FromQuery] int? pageNumber)
    {
        PageNumber = pageNumber ?? 0;
        var now = DateTime.UtcNow.AddMonths(PageNumber);
        var firstDay = new DateTime(now.Year, now.Month, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        var lastDay = firstDay.AddMonths(1);

        MoneySum = await _context.Payments
            .Where(e => e.Time >= firstDay && e.Time < lastDay)
            .SumAsync(e => e.Price);

        var passages = await _context.Passages
            .Include(e => e.Gate).ThenInclude(e => e.Owner)
            .Where(e => e.Time >= firstDay && e.Time < lastDay)
            .ToListAsync();

        var groupedPassages = passages.GroupBy(e => e.Gate.Owner)
            .Select(e => new { Key = e.Key, Passages = e.Count(), })
            .AsEnumerable();

        double passagesSum = (double)groupedPassages.Sum(e => e.Passages);

        Items = groupedPassages.Select(e => new AccountingItem
        {
            Company = e.Key,
            Passages = e.Passages,
            Share = (e.Passages / passagesSum).ToString("P"),
            Money = (e.Passages / passagesSum * MoneySum).ToString("C"),
        }).ToList();
    }

    public class AccountingItem
    {
        public Company Company { get; init; }
        public int Passages { get; init; }
        public string Share { get; init; }
        public string Money { get; init; }
    }
}