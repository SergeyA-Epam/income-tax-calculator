using IncomeTaxCalculator.Core.Interfaces;

namespace IncomeTaxCalculator.Core;

public static class Extensions
{
    public static IList<T> Sort<T>(this IList<T> taxBands) where T : ITaxBand =>
        [.. taxBands
            .OrderBy(tb => tb.RangeStart)
            .ThenBy(tb => tb.RangeEnd ?? int.MaxValue)];
}
