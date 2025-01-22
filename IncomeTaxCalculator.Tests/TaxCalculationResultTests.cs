namespace IncomeTaxCalculator.Tests;

using NUnit.Framework;
using IncomeTaxCalculator.Core.Models;

[TestFixture]
public class TaxCalculationResultTests
{
    [Test]
    [TestCase(10000, 10000)]
    [TestCase(123123, 123123)]
    public void GrossAnnualSalary_Is_Correct(decimal annualSalary, decimal expectedGrossAnnualSalary)
    {
        var taxResult = new TaxCalculationResult(annualSalary, 0);

        Assert.That(
            taxResult.GrossAnnualSalary,
            Is.EqualTo(expectedGrossAnnualSalary),
            "Gross annual salary is incorrect."
        );
    }

    [Test]
    [TestCase(10000, 833.33)]
    [TestCase(123123, 10260.25)]
    public void GrossMonthlySalary_Is_CorrectlyCalculated(decimal annualSalary, decimal expectedGrossMonthlySalary)
    {
        var taxResult = new TaxCalculationResult(annualSalary, 0);

        Assert.That(
            taxResult.GrossMonthlySalary,
            Is.EqualTo(expectedGrossMonthlySalary),
            "Gross monthly salary calculation is incorrect."
        );
    }

    [Test]
    [TestCase(10000, 1000, 9000)]
    [TestCase(123123, 12121, 111002)]
    public void NetAnnualSalary_Is_CorrectlyCalculated(decimal annualSalary, decimal annualTax, decimal expectedNetAnnualSalary)
    {
        var taxResult = new TaxCalculationResult(annualSalary, annualTax);

        Assert.That(
            taxResult.NetAnnualSalary,
            Is.EqualTo(expectedNetAnnualSalary),
            "Net annual salary calculation is incorrect."
        );
    }

    [Test]
    [TestCase(10000, 1000, 750)]
    [TestCase(123123, 12121, 9250.17)]
    public void NetMonthlySalary_Is_CorrectlyCalculated(decimal annualSalary, decimal annualTax, decimal expectedNetMonthlySalary)
    {
        var taxResult = new TaxCalculationResult(annualSalary, annualTax);

        Assert.That(
            taxResult.NetMonthlySalary,
            Is.EqualTo(expectedNetMonthlySalary),
            "Net monthly salary calculation is incorrect."
        );
    }

    [Test]
    [TestCase(1000, 1000)]
    [TestCase(12121, 12121)]
    public void AnnualTaxPaid_Is_Correct(decimal annualTax, decimal expectedAnnualTaxPaid)
    {
        var taxResult = new TaxCalculationResult(0, annualTax);

        Assert.That(
            taxResult.AnnualTaxPaid,
            Is.EqualTo(expectedAnnualTaxPaid),
            "Annual tax paid is incorrect."
        );
    }

    [Test]
    [TestCase(1000, 83.33)]
    [TestCase(12121, 1010.08)]
    public void MonthlyTaxPaid_Is_CorrectlyCalculated(decimal annualTax, decimal expectedMonthlyTaxPaid)
    {
        var taxResult = new TaxCalculationResult(0, annualTax);

        Assert.That(
            taxResult.MonthlyTaxPaid,
            Is.EqualTo(expectedMonthlyTaxPaid),
            "Monthly tax paid calculation is incorrect."
        );
    }

    [Test]
    public void RealTest()
    {
        var taxResult = new TaxCalculationResult(40000, 11000);

        Assert.Multiple(() =>
        {
            Assert.That(taxResult.GrossAnnualSalary, Is.EqualTo(40000));
            Assert.That(taxResult.GrossMonthlySalary, Is.EqualTo(3333.33));
            Assert.That(taxResult.NetAnnualSalary, Is.EqualTo(29000));
            Assert.That(taxResult.NetMonthlySalary, Is.EqualTo(2416.67));
            Assert.That(taxResult.AnnualTaxPaid, Is.EqualTo(11000));
            Assert.That(taxResult.MonthlyTaxPaid, Is.EqualTo(916.67));
        });
    }
}