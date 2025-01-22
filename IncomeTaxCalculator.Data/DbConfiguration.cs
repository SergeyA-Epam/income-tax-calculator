using IncomeTaxCalculator.Data.Repositories;
using IncomeTaxCalculator.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IncomeTaxCalculator.Data;

public class DbConfiguration
{
    public static IServiceCollection ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<IncomeTaxCalculatorDbContext>(
            options => options.UseInMemoryDatabase("IncomeTaxCalculatorDb")
        );

        services.AddScoped<ITaxBandRepository, TaxBandRepository>();

        return services;
    }
}
