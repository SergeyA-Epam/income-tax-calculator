using IncomeTaxCalculator.Core.Models;

namespace IncomeTaxCalculator.Data.Repositories.Interfaces
{
    public interface ITaxBandRepository
    {
        Task<IList<TaxBand>> GetAll();
    }
}