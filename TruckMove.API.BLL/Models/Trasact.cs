using Microsoft.AspNetCore.Mvc;

public class PaymentRates
{
    public int CommercialRatePerKm { get; set; }
    public int CommercialRateKm { get; set; }
    public int PerKmRate { get; set; }
    public int SaturdayRate { get; set; }
    public int SundayRate { get; set; }
    public int HolidayRate { get; set; }
    public int TotalKmThreshold { get; set; }
}

public class LegPaymentTransact
{
    private PaymentRates _rates;

    public string LegName { get; set; }
    public string LegType { get; private set; }
    public string LegDay { get; set; }
    public int TotalKm { get; set; }

    public int CommercialLoadTotal => _rates.CommercialRatePerKm * _rates.CommercialRateKm;
    public int PerKmTotal => GetPerKmTotal();
    public int Total => CommercialLoadTotal + PerKmTotal;
    public string CalculationDetails => GetCalculationDetails();

    public LegPaymentTransact(string legName, string legDay, int totalKm, PaymentRates rates)
    {
        LegName = legName;
        LegDay = legDay;
        TotalKm = totalKm;
        _rates = rates;

        LegType = DetermineLegType();
    }

    private string DetermineLegType()
    {
        return TotalKm > _rates.TotalKmThreshold ? "PerKM" : "Fixed";
    }

    private int GetPerKmTotal()
    {
        int rate = LegDay switch
        {
            "Saturday" => _rates.SaturdayRate,
            "Sunday" => _rates.SundayRate,
            "Holiday" => _rates.HolidayRate,
            _ => _rates.PerKmRate
        };
        return rate * _rates.PerKmRate;
    }

    private string GetCalculationDetails()
    {
        return $"Commercial Load Total = {_rates.CommercialRatePerKm} * {_rates.CommercialRateKm} = {CommercialLoadTotal}, " +
               $"Per Km Total ({LegDay} rate = {PerKmTotal}) Total Payment = {Total}";
    }
}

[ApiController]
[Route("api/[controller]")]
public class LegPaymentController : ControllerBase
{
    [HttpPost("CalculateLegPayments")]
    public IActionResult CalculateLegPayments([FromBody] List<List<LegPaymentTransact>> legGroups)
    {
        if (legGroups == null || !legGroups.Any())
            return BadRequest("No leg data provided.");

        var result = new List<object>();
        int grandTotal = 0;

        foreach (var legList in legGroups)
        {
            var groupResults = new List<object>();
            foreach (var leg in legList)
            {
                var legDetails = new
                {
                    leg.LegName,
                    leg.LegType,
                    leg.LegDay,
                    leg.TotalKm,
                    CommercialLoadTotal = leg.CommercialLoadTotal,
                    PerKmTotal = leg.PerKmTotal,
                    TotalPayment = leg.Total,
                    CalculationDetails = leg.CalculationDetails
                };

                groupResults.Add(legDetails);
                grandTotal += leg.Total;
            }
            result.Add(groupResults);
        }

        return Ok(new { LegGroups = result, GrandTotal = grandTotal });
    }
}
