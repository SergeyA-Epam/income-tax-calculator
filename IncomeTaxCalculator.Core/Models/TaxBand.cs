using IncomeTaxCalculator.Core.Interfaces;

namespace IncomeTaxCalculator.Core.Models;

public class TaxBand : ITaxBand
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int RangeStart { get; set; }
    public int? RangeEnd { get; set; }
    public int TaxRate { get; set; }

    /// <summary>
    /// Returns salary chunk that overlaps with this tax band.
    /// </summary>
    public decimal GetChunk(decimal salary)
    {
        if (salary <= RangeStart)
        {
            return 0;
        }

        var remainder = salary - RangeStart;

        if (RangeEnd == null || salary <= RangeEnd)
        {
            return remainder;
        }

        return (decimal)RangeEnd - RangeStart;
    }

    public decimal GetTaxChunk(decimal salary) => GetChunk(salary) * TaxRate / 100;

    public bool IsOpenEnded => RangeEnd == null;
}
