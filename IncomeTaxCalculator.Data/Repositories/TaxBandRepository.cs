using IncomeTaxCalculator.Core.Models;
using IncomeTaxCalculator.Data.Repositories.Interfaces;
using IncomeTaxCalculator.Data.Seeding;
using Microsoft.EntityFrameworkCore;

namespace IncomeTaxCalculator.Data.Repositories;

public class TaxBandRepository : ITaxBandRepository
{
    private readonly IncomeTaxCalculatorDbContext dbContext;

    public TaxBandRepository(IncomeTaxCalculatorDbContext dbContext)
    {
        this.dbContext = dbContext;

        TaxBandSeeder.Seed(dbContext);
    }

    public async Task<IList<TaxBand>> GetAll()
    {
        return await dbContext.TaxBands.ToListAsync();
    }
}
