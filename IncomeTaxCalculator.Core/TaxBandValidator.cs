using IncomeTaxCalculator.Core.Interfaces;
using IncomeTaxCalculator.Core.Models;

namespace IncomeTaxCalculator.Core;

public class TaxBandValidator
{
    /// <summary>
    /// Validates tax bands. If there are any issues, returns an error.
    /// </summary>
    public static string? ValidateTaxBands(IList<TaxBand> taxBands)
    {
        var sortedTaxBands = taxBands.Sort();

        for (var i = 0; i < sortedTaxBands.Count; i++)
        {
            var taxBand = sortedTaxBands[i];

            // validate the tax band
            if (taxBand.RangeEnd != null && taxBand.RangeStart >= taxBand.RangeEnd)
            {
                return $"Tax band `{taxBand.Name}` has invalid range. {nameof(taxBand.RangeStart)} ({taxBand.RangeStart}) must be lesser than {nameof(taxBand.RangeEnd)} ({taxBand.RangeEnd}).";
            }

            // if the tax band is not the last one, it cannot be open-ended
            if (i != sortedTaxBands.Count - 1 && taxBand.IsOpenEnded)
            {
                return $"Tax band `{taxBand.Name}` is not the last one and must have {nameof(taxBand.RangeEnd)}.";
            }

            if (i == 0)
            {
                continue;
            }

            var prevTaxBand = sortedTaxBands[i - 1];

            // compare the current tax band and the previous
            if (taxBand.RangeStart != prevTaxBand.RangeEnd)
            {
                return $"Tax band `{taxBand.Name}` {nameof(taxBand.RangeStart)} ({taxBand.RangeStart}) must be equal to previous tax band `{prevTaxBand.Name}` {nameof(prevTaxBand.RangeEnd)} ({prevTaxBand.RangeEnd}).";
            }
        }

        return null;
    }
}
