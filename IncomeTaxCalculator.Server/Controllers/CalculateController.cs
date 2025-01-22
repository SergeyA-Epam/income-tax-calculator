using IncomeTaxCalculator.Core;
using IncomeTaxCalculator.Data.Repositories.Interfaces;
using IncomeTaxCalculator.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace IncomeTaxCalculator.Server.Controllers;

[ApiController]
[Route("api/calculate")]
public class CalculateController(
    ILogger<CalculateController> logger,
    ITaxBandRepository taxBandRepository
) : Controller
{
    [HttpPost]
    public async Task<IActionResult> Calculate(CalculateRequestDto request)
    {
        var salary = request.Salary;

        logger.LogInformation("Got calculate request for salary {Salary}...", salary);

        var taxBands = await taxBandRepository.GetAll();

        var calculationResult = TaxCalculator.CalculateIncomeTax(taxBands, salary);

        return Ok(calculationResult);
    }
}
