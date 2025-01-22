namespace IncomeTaxCalculator.Core.Models;

public class TaxCalculationResult(decimal salary, decimal tax)
{
    public decimal GrossAnnualSalary => salary;
    public decimal GrossMonthlySalary => Monthly(salary);
    public decimal NetAnnualSalary => salary - tax;
    public decimal NetMonthlySalary => Monthly(NetAnnualSalary);
    public decimal AnnualTaxPaid => tax;
    public decimal MonthlyTaxPaid => Monthly(tax);

    private static decimal Monthly(decimal value) => Math.Round(value / 12, 2);
}
