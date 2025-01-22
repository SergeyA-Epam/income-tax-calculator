namespace IncomeTaxCalculator.Tests;

using NUnit.Framework;
using IncomeTaxCalculator.Core.Models;

[TestFixture]
public class TaxBandTests
{
    [Test]
    public void GetChunk_WhenSalaryBelowRange_ReturnsZero()
    {
        var taxBand = new TaxBand
        {
            Name = "Low Income",
            RangeStart = 10000,
            RangeEnd = 20000,
            TaxRate = 10
        };

        var salary = 5000; // below the start range
        var chunk = taxBand.GetChunk(salary);

        Assert.That(chunk, Is.EqualTo(0));
    }

    [Test]
    public void GetChunk_WhenSalaryWithinRange_ReturnsCorrectChunk()
    {
        var taxBand = new TaxBand
        {
            Name = "Middle Income",
            RangeStart = 10000,
            RangeEnd = 20000,
            TaxRate = 15
        };

        var salary = 15000;
        var expectedChunk = 5000; // salary - RangeStart
        var chunk = taxBand.GetChunk(salary);

        Assert.That(chunk, Is.EqualTo(expectedChunk));
    }

    [Test]
    public void GetChunk_WhenSalaryAboveRangeButEndDefined_ReturnsMaxChunk()
    {
        var taxBand = new TaxBand
        {
            Name = "Middle Income",
            RangeStart = 10000,
            RangeEnd = 20000,
            TaxRate = 20
        };

        var salary = 25000;
        var expectedChunk = 10000; // RangeEnd - RangeStart
        var chunk = taxBand.GetChunk(salary);

        Assert.That(chunk, Is.EqualTo(expectedChunk));
    }

    [Test]
    public void GetChunk_WhenSalaryAboveRangeAndIsOpenEnded_ReturnsRemainder()
    {
        var taxBand = new TaxBand
        {
            Name = "High Income",
            RangeStart = 20000,
            TaxRate = 25
        };

        var salary = 30000;
        var expectedChunk = 10000; // salary - RangeStart
        var chunk = taxBand.GetChunk(salary);

        Assert.That(chunk, Is.EqualTo(expectedChunk));
    }

    [Test]
    public void GetTaxChunk_CalculatesCorrectTaxChunk()
    {
        var taxBand = new TaxBand
        {
            Name = "Test Band",
            RangeStart = 15000,
            RangeEnd = 25000,
            TaxRate = 10
        };

        var salary = 30000; // 10000 taxable at 10%
        var expectedTaxChunk = 1000; // 10% of 10000
        var taxChunk = taxBand.GetTaxChunk(salary);

        Assert.That(taxChunk, Is.EqualTo(expectedTaxChunk));
    }

    [Test]
    public void IsOpenEnded_WhenRangeEndIsNull_ReturnsTrue()
    {
        var taxBand = new TaxBand
        {
            Name = "Open Ended",
            RangeStart = 50000,
            TaxRate = 30
        };

        var isOpenEnded = taxBand.IsOpenEnded;

        Assert.That(isOpenEnded, Is.True);
    }

    [Test]
    public void IsOpenEnded_WhenRangeEndIsDefined_ReturnsFalse()
    {
        var taxBand = new TaxBand
        {
            Name = "Closed Range",
            RangeStart = 20000,
            RangeEnd = 50000,
            TaxRate = 30
        };

        var isOpenEnded = taxBand.IsOpenEnded;

        Assert.That(isOpenEnded, Is.False);
    }
}