namespace IncomeTaxCalculator.Tests;

using NUnit.Framework;
using IncomeTaxCalculator.Core;
using System.Collections.Generic;
using IncomeTaxCalculator.Core.Models;

[TestFixture]
public class TaxBandValidatorTests
{
    [Test]
    public void ValidateTaxBands_InvalidRange_ReturnsErrorMessage()
    {
        IList<TaxBand> taxBands = [
            MakeTaxBand("Standard", 0, 50000),
            MakeTaxBand("Higher", 50000, 25000) // invalid
        ];

        var result = TaxBandValidator.ValidateTaxBands(taxBands);

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Contains.Substring("Tax band `Higher` has invalid range"));
    }

    [Test]
    public void ValidateTaxBands_ContinuousRanges_ReturnsNull()
    {
        IList<TaxBand> taxBands = [
            MakeTaxBand("Standard", 0, 50000),
            MakeTaxBand("Higher", 50000, 150000),
            MakeTaxBand("Additional", 150000)
        ];

        var result = TaxBandValidator.ValidateTaxBands(taxBands);

        Assert.That(result, Is.Null);
    }

    [Test]
    public void ValidateTaxBands_NonContinuousRanges_ReturnsErrorMessage()
    {
        IList<TaxBand> taxBands = [
            MakeTaxBand("Standard", 0, 50000),
            MakeTaxBand("Higher", 60000, 150000) // Gap between the bands
        ];

        var result = TaxBandValidator.ValidateTaxBands(taxBands);

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Contains.Substring("must be equal to previous tax band `Standard`"));
    }

    [Test]
    public void ValidateTaxBands_OpenEndedNonLastBand_ReturnsErrorMessage()
    {
        IList<TaxBand> taxBands = [
            MakeTaxBand("Standard", 0), // Open-ended but not last
            MakeTaxBand("Higher", 100000, 200000)
        ];

        var result = TaxBandValidator.ValidateTaxBands(taxBands);

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Contains.Substring("Tax band `Standard` is not the last one and must have RangeEnd"));
    }

    [Test]
    public void ValidateTaxBands_LastBandOpenEnded_ReturnsNull()
    {
        IList<TaxBand> taxBands = [
            MakeTaxBand("Standard", 0, 50000),
            MakeTaxBand("Higher", 50000) // Last band open-ended
        ];

        var result = TaxBandValidator.ValidateTaxBands(taxBands);

        Assert.That(result, Is.Null);
    }

    private static TaxBand MakeTaxBand(string name, int start, int? end = null) =>
        new()
        {
            Name = name,
            RangeStart = start,
            RangeEnd = end
        };
}