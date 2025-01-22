namespace IncomeTaxCalculator.Tests;

using NUnit.Framework;
using System;
using IncomeTaxCalculator.Core.Models;
using IncomeTaxCalculator.Core;

[TestFixture]
public class TaxCalculatorTests
{
    [Test]
    [TestCase(0, 0)] // No tax applied for zero salary
    [TestCase(50000, 7500)] // Tax applied in the only one band
    [TestCase(150000, 27500)] // Tax applied across multiple bands
    public void CalculateIncomeTax_ValidTaxBands_ReturnsCorrectTax(decimal salary, decimal expectedTax)
    {
        IList<TaxBand> taxBands = [
            MakeTaxBand("A", 0, 50000, 15), // 15% tax from 0 to 50,000
            MakeTaxBand("B", 50000, 150000, 20), // 20% tax from 50,000 to 150,000
            MakeTaxBand("C", 150000, null, 25) // 25% above 150,000
        ];

        var result = TaxCalculator.CalculateIncomeTax(taxBands, salary);

        Assert.Multiple(() =>
        {
            Assert.That(result.AnnualTaxPaid, Is.EqualTo(expectedTax));
            Assert.That(result.GrossAnnualSalary, Is.EqualTo(salary));
            Assert.That(result.NetAnnualSalary, Is.EqualTo(salary - expectedTax));
        });
    }

    [Test]
    public void CalculateIncomeTax_InvalidTaxBands_ThrowsArgumentException()
    {
        IList<TaxBand> taxBands = [
            MakeTaxBand("A", 0, 50000, 15),
            MakeTaxBand("B", 60000, 150000, 20) // Non-continuous range generates validation error
        ];

        var ex = Assert.Throws<ArgumentException>(() => TaxCalculator.CalculateIncomeTax(taxBands, 100000));

        Assert.That(ex.Message, Is.Not.Null);
        Assert.That(ex.Message, Does.Contain("must be equal to previous tax band"));
    }

    [Test]
    public void CalculateIncomeTax_SalaryBelowFirstBand_ReturnsZeroTax()
    {
        IList<TaxBand> taxBands = [
            MakeTaxBand("A", 10000, 50000, 15)
        ];

        var result = TaxCalculator.CalculateIncomeTax(taxBands, 5000);

        Assert.That(result.AnnualTaxPaid, Is.EqualTo(0));
    }

    [Test]
    public void CalculateIncomeTax_OpenEndedLastBand_HighSalary()
    {
        IList<TaxBand> taxBands = [
            MakeTaxBand("A", 0, 50000, 10),
            MakeTaxBand("B", 50000, null, 15) // Open-ended
        ];

        decimal salary = 200000; // Large salary to test open-ended band
        var expectedTax = (50000m * 0.1m) + (150000m * 0.15m);
        var result = TaxCalculator.CalculateIncomeTax(taxBands, salary);

        Assert.That(result.AnnualTaxPaid, Is.EqualTo(expectedTax));
    }

    [Test]
    [TestCase(10000, 1000)]
    [TestCase(40000, 11000)]
    public void CalculateIncomeTax_RealTest(int salary, int expectedTax)
    {
        IList<TaxBand> taxBands = [
            MakeTaxBand("Tax Band A", 0, 5000, 0),
            MakeTaxBand("Tax Band B", 5000, 20000, 20),
            MakeTaxBand("Tax Band C", 20000, null, 40)
        ];

        var result = TaxCalculator.CalculateIncomeTax(taxBands, salary);

        Assert.That(result.AnnualTaxPaid, Is.EqualTo(expectedTax));
    }

    private static TaxBand MakeTaxBand(string name, int start, int? end, int taxRate) =>
        new()
        {
            Name = name,
            RangeStart = start,
            RangeEnd = end,
            TaxRate = taxRate
        };
}