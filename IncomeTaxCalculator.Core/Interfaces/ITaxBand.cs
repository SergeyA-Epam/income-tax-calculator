namespace IncomeTaxCalculator.Core.Interfaces;

public interface ITaxBand
{
    public string Name { get; set; }
    public int RangeStart { get; set; }
    public int? RangeEnd { get; set; }
    public int TaxRate { get; set; }
}
