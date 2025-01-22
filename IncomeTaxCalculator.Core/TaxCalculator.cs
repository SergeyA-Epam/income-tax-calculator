using IncomeTaxCalculator.Core.Models;

namespace IncomeTaxCalculator.Core;

public class TaxCalculator
{
    public static TaxCalculationResult CalculateIncomeTax(IList<TaxBand> taxBands, decimal salary)
    {
        var tax = GetTax(taxBands, salary);

        return new TaxCalculationResult(salary, tax);
    }

    public static decimal GetTax(IList<TaxBand> taxBands, decimal salary)
    {
        var validationError = TaxBandValidator.ValidateTaxBands(taxBands);

        if (validationError != null)
        {
            throw new ArgumentException(validationError);
        }

        return taxBands.Sum(
            taxBand => taxBand.GetTaxChunk(salary)
        );
    }
}
