//public class Job
//{
//    public int Id { get; set; }
//    public List<Leg> Legs { get; set; } = new List<Leg>();
//    public List<Purchase> Purchases { get; set; } = new List<Purchase>();
//    public List<Delay> Delays { get; set; } = new List<Delay>();
//}

//public class Leg
//{
//    public int Id { get; set; }
//    public double Kilometers { get; set; }
//    public bool HasTrailer { get; set; }
//}

//public class Purchase
//{
//    public int Id { get; set; }
//    public double Amount { get; set; }
//}

//public class Delay
//{
//    public int Id { get; set; }
//    public double Hours { get; set; }
//}

//public class DriverPaymentCalculator
//{
//    private const double PerKmRate = 0.5936;
//    private const int MaxFixedJobKMs = 500;
//    private const double FixedJobRate = 200;
//    private const double CommercialLoadKmRate = 0.2;
//    private const double DelayHourlyRate = 32.65;

//    public DriverPaymentDetails CalculatePayment(Job job, int driverId)
//    {
//        var paymentDetails = new DriverPaymentDetails();

//        // Calculate and set leg costs
//        paymentDetails.LegDetails = CalculateLegCosts(job.Legs);
//        paymentDetails.TotalLegCost = CalculateTotalLegCost(paymentDetails.LegDetails);

//        // Calculate and set purchase expenses
//        paymentDetails.TotalPurchaseExpense = CalculateTotalPurchaseCost(job.Purchases);

//        // Calculate and set delay expenses
//        paymentDetails.TotalDelayExpense = CalculateTotalDelayCost(job.Delays);

//        return paymentDetails;
//    }

//    private List<LegDetail> CalculateLegCosts(List<Leg> legs)
//    {
//        var legDetails = new List<LegDetail>();

//        foreach (var leg in legs)
//        {
//            double legCost = CalculateLegCost(leg);
//            legDetails.Add(new LegDetail { LegId = leg.Id, Cost = legCost });
//        }

//        return legDetails;
//    }

//    private double CalculateLegCost(Leg leg)
//    {
//        double legCost = leg.Kilometers * PerKmRate;
//        if (leg.HasTrailer)
//        {
//            legCost += leg.Kilometers * CommercialLoadKmRate;
//        }
//        return legCost;
//    }

//    private double CalculateTotalLegCost(List<LegDetail> legDetails)
//    {
//        return legDetails.Sum(ld => ld.Cost);
//    }

//    private double CalculateTotalPurchaseCost(List<Purchase> purchases)
//    {
//        return purchases.Sum(p => p.Amount);
//    }

//    private double CalculateTotalDelayCost(List<Delay> delays)
//    {
//        return delays.Sum(d => d.Hours * DelayHourlyRate);
//    }
//}

//public class DriverPaymentDetails
//{
//    private List<LegDetail> _legDetails = new List<LegDetail>();
//    private double _totalLegCost;
//    private double _totalPurchaseExpense;
//    private double _totalDelayExpense;

//    public List<LegDetail> LegDetails
//    {
//        get => _legDetails;
//        set => _legDetails = value;
//    }

//    public double TotalLegCost
//    {
//        get => _totalLegCost;
//        set => _totalLegCost = value;
//    }

//    public double TotalPurchaseExpense
//    {
//        get => _totalPurchaseExpense;
//        set => _totalPurchaseExpense = value;
//    }

//    public double TotalDelayExpense
//    {
//        get => _totalDelayExpense;
//        set => _totalDelayExpense = value;
//    }
//}

//public class LegDetail
//{
//    public int LegId { get; set; }
//    public double Cost { get; set; }
//}
