using IncomeTaxCalculator.Core.Interfaces;
using IncomeTaxCalculator.Core.Models;

namespace IncomeTaxCalculator.Data.Seeding;

public class TaxBandData : ITaxBand
{
    public required string Name { get; set; }
    public int RangeStart { get; set; }
    public int? RangeEnd { get; set; }
    public int TaxRate { get; set; }

    public TaxBand ToTaxBand() =>
        new()
        {
            Name = Name,
            RangeStart = RangeStart,
            RangeEnd = RangeEnd,
            TaxRate = TaxRate,
        };

    public static TaxBandData[] Default =>
        [
            new TaxBandData
            {
                Name = "Tax Band A",
                RangeStart = 0,
                RangeEnd = 5000,
                TaxRate = 0,
            },
            new TaxBandData
            {
                Name = "Tax Band B",
                RangeStart = 5000,
                RangeEnd = 20000,
                TaxRate = 20,
            },
            new TaxBandData
            {
                Name = "Tax Band C",
                RangeStart = 20000,
                TaxRate = 40,
            },
        ];
}
