using IncomeTaxCalculator.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace IncomeTaxCalculator.Data;

public class IncomeTaxCalculatorDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<TaxBand> TaxBands { get; set; }
}
