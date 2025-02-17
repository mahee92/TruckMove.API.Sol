using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using TruckMove.API.DAL.Models;
using static TruckMove.API.DAL.MasterData.MasterData;

namespace TruckMove.API.BLL.Helper
{

    public class LegPaymentTransact
    {
        private readonly PaymentRates _rates;

        public int LegNumber { get; }
        public string LegType { get; }
        public string LegDay { get; }
        public double TotalKm { get; }

        public bool IsCommercialLoad { get; }

        public int HookupSingleCount { get; }
        public int HookupDoubleCount { get; }
        public int Hookup4RACount { get; }

        public double Rate { get; private set; }

        public double TotHours { get; private set; }

        public double RoundedHours { get; private set; }
        public string RoundedHoursSring { get; private set; }

        public string CalculationBreakdown { get; private set; } = string.Empty;

        public int Grade4Hourlyrate = 500;

        public LegPaymentTransact(Leg leg, PaymentRates rates)
        {
            if (leg == null) throw new ArgumentNullException(nameof(leg));
            _rates = rates ?? throw new ArgumentNullException(nameof(rates));

            LegNumber = leg.LegNumber;
            TotalKm = leg.TotalDistance ?? 0;
            IsCommercialLoad = leg.Job.IsCommercialLoad;

            // Count hookups
            HookupSingleCount = leg.Trailers.Count(h => h.HookupType == (int)HookUpTypeEnum.HU_Single);
            HookupDoubleCount = leg.Trailers.Count(h => h.HookupType == (int)HookUpTypeEnum.HU_Double);
            Hookup4RACount = leg.Trailers.Count(h => h.HookupType == (int)HookUpTypeEnum.FOUR_RA);

            // Determine LegType
            LegType = TotalKm > Grade4Hourlyrate ? "PerKM" : "Hourly";
            if (leg.Job.IsDangerousGoods)
            {
                LegType = "PerKMDangerousGoods";
            }
            SetTotalHours(leg.StartTime, leg.EndTime?? leg.StartTime);
            SetRate();
        }


        private void SetTotalHours(DateTime startTime, DateTime endTime)
        {
            TotHours = (endTime - startTime).TotalHours;
            if(TotHours<4)
            {
                RoundedHours = 4;
                RoundedHoursSring = $"4 ( {TotHours} rounded)";
            }
            else
            {
                RoundedHours = TotHours;
                RoundedHoursSring = RoundedHours.ToString();
            }
        }
        private void SetRate()
        {
          

            Rate = LegType switch
            {
                "PerKM" => _rates.PerKmRate,

                "Hourly" => _rates.Grade_4_Hourly_rate,

                "PerKMDangerousGoods" => _rates.Dangerous_Goods_day_Rate,
                _ => throw new NotImplementedException()
            };

           
        }

       
        public double PerKmTotal
        {
            get
            {
                if (LegType == "PerKM")
                {
                    double total = Rate * TotalKm;
                    AppendBreakdown($"Per KM Rate: {Rate} x {TotalKm} km = {total:C}");
                    return total;
                }
                return 0;
            }
        }
        public double HourlyTotal
        {
            get
            {
                if (LegType == "Hourly")
                {                                  
                    double total = Rate * RoundedHours;
                    AppendBreakdown($"Hourly Rate: {Rate:C} x {RoundedHours} hours = {total:C}");
                    return total;
                }
                return 0;
            }
        }

        public double DangerousGoodsTotal
        {
            get
            {
                if (LegType == "PerKMDangerousGoods")
                {
                    double total = Rate * TotalKm;
                    AppendBreakdown($"Dangerous Goods Per KM Rate: {Rate} x {TotalKm} km = {total:C}");
                    return total;
                }
                return 0;
            }
        }

        public double CommercialLoadTotal
        {
            get
            {
                if (IsCommercialLoad)
                {
                    double total = _rates.Commercial_load_KM_rate * TotalKm;
                    AppendBreakdown($"Commercial Load: {_rates.Commercial_load_KM_rate} x {TotalKm} km = {total:C}");
                    return total;
                }
                return 0;
            }
        }

        public double HookupSingleTotal
        {
            get
            {
                if (HookupSingleCount > 0)
                {
                    double total = HookupSingleCount * _rates.hookUp_Single;
                    AppendBreakdown($"Hookup Single: {HookupSingleCount} x {_rates.hookUp_Single:C} = {total:C}");
                    return total;
                }
                return 0;
            }
        }

        public double HookupDoubleTotal
        {
            get
            {
                if (HookupDoubleCount > 0)
                {
                    double total = HookupDoubleCount * _rates.hookUp_Double;
                    AppendBreakdown($"Hookup Double: {HookupDoubleCount} x {_rates.hookUp_Double:C} = {total:C}");
                    return total;
                }
                return 0;
            }
        }

        public double Hookup4RATotal
        {
            get
            {
                if (Hookup4RACount > 0)
                {
                    double total = Hookup4RACount * _rates.hookUp_4RA;
                    AppendBreakdown($"Hookup 4RA: {Hookup4RACount} x {_rates.hookUp_4RA:C} = {total:C}");
                    return total;
                }
                return 0;
            }
        }

        public double HookupTotal => HookupSingleTotal + HookupDoubleTotal + Hookup4RATotal;

        public double Total
        {
            get
            {
                double total = LegType switch
                {
                    "PerKM" => PerKmTotal + CommercialLoadTotal + HookupTotal,
                    "Hourly" => HourlyTotal + CommercialLoadTotal + HookupTotal,
                    "PerKMDangerousGoods" => DangerousGoodsTotal + HookupTotal,
                    _ => 0
                };

                AppendBreakdown($"Total Payment: {total:C}");
                return total;
            }
        }

        private void AppendBreakdown(string text)
        {
            if (!string.IsNullOrEmpty(CalculationBreakdown))
            {
                CalculationBreakdown += "\n";
            }
            CalculationBreakdown += text;
        }

    }


}



