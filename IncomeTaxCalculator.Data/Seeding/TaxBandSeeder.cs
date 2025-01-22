using IncomeTaxCalculator.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace IncomeTaxCalculator.Data.Seeding;

public class TaxBandSeeder
{
    public static void Seed(IncomeTaxCalculatorDbContext context)
    {
        if (context.TaxBands.Any())
        {
            return;
        }

        context.TaxBands.AddRange(DefaultTaxBands);
        context.SaveChanges();
    }

    public static async Task SeedAsync(IncomeTaxCalculatorDbContext context)
    {
        if (await context.TaxBands.AnyAsync())
        {
            return;
        }

        await context.TaxBands.AddRangeAsync(DefaultTaxBands);
        await context.SaveChangesAsync();
    }

    private static IEnumerable<TaxBand> DefaultTaxBands =>
        TaxBandData.Default.Select(tbd => tbd.ToTaxBand());
}
