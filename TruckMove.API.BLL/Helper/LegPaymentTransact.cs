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

        public string CalculationBreakdown { get; private set; } = string.Empty;

        public LegPaymentTransact(Leg leg, PaymentRates rates)
        {
            if (leg == null) throw new ArgumentNullException(nameof(leg));
            _rates = rates ?? throw new ArgumentNullException(nameof(rates));

            LegNumber = leg.LegNumber;
            TotalKm = leg.TotalDistance ?? 0;
            LegDay = GetLegDay(leg.StartTime);
            IsCommercialLoad = leg.Job.IsCommercialLoad;

            // Count hookups
            HookupSingleCount = leg.Trailers.Count(h => h.HookupType == (int)HookUpTypeEnum.HU_Single);
            HookupDoubleCount = leg.Trailers.Count(h => h.HookupType == (int)HookUpTypeEnum.HU_Double);
            Hookup4RACount = leg.Trailers.Count(h => h.HookupType == (int)HookUpTypeEnum.FOUR_RA);

            // Determine LegType
            LegType = TotalKm > _rates.Max_fixed_job_KMs ? "PerKM" : "Fixed";
            if (leg.Job.IsDangerousGoods)
            {
                LegType = "PerKMDangerousGoods";
            }

            SetRate();
        }

        private static string GetLegDay(DateTime legDay) => legDay.DayOfWeek switch
        {
            DayOfWeek.Saturday => "Saturday",
            DayOfWeek.Sunday => "Sunday",
            _ => "WeekDay"
        };

        private void SetRate()
        {
            bool isPerKM = LegType == "PerKM";
            bool isDangerousGoods = LegType == "PerKMDangerousGoods";

            Rate = LegDay switch
            {
                "Saturday" => isPerKM ? _rates.Saturday_KM_rate :
                             isDangerousGoods ? _rates.Dangerous_Goods_day_Rate :
                             _rates.Saturday_Fixed_rate,

                "Sunday" => isPerKM ? _rates.Sunday_KM_rate :
                           isDangerousGoods ? _rates.Dangerous_Goods_day_Rate :
                           _rates.Sunday_Fixed_rate,

                "Holiday" => isPerKM ? _rates.Public_holiday_KM_rate :
                            isDangerousGoods ? _rates.Dangerous_Goods_day_Rate :
                            _rates.Public_holiday_Fixed_rate,

                _ => isPerKM ? _rates.PerKmRate :
                     isDangerousGoods ? _rates.Dangerous_Goods_day_Rate :
                     _rates.Fixed_job_rate
            };
        }

        //public double Total => LegType switch
        //{
        //    "PerKM" => PerKmTotal + CommercialLoadTotal + HookupTotal,
        //    "Fixed" => FixedJobTotal + CommercialLoadTotal + HookupTotal,
        //    "PerKMDangerousGoods" => DangerousGoodsTotal  + HookupTotal,
        //    _ => 0
        //};

        //public double FixedJobTotal => LegType == "Fixed" ? Rate : 0;

        //public double PerKmTotal => LegType == "PerKM" ? Rate * TotalKm : 0;

        //public double DangerousGoodsTotal => LegType == "PerKMDangerousGoods" ? Rate * TotalKm : 0;

        //public double CommercialLoadTotal => IsCommercialLoad ? _rates.Commercial_load_KM_rate * TotalKm : 0;

        //public double HookupSingleTotal => HookupSingleCount * _rates.hookUp_Single;

        //public double HookupDoubleTotal => HookupDoubleCount * _rates.hookUp_Double;

        //public double Hookup4RATotal => Hookup4RACount * _rates.hookUp_4RA;

        //public double HookupTotal => HookupSingleTotal + HookupDoubleTotal + Hookup4RATotal;

        public double FixedJobTotal
        {
            get
            {
                if (LegType == "Fixed")
                {
                    AppendBreakdown($"Fixed Job Rate: {Rate:C}");
                    return Rate;
                }
                return 0;
            }
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
                    "Fixed" => FixedJobTotal + CommercialLoadTotal + HookupTotal,
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



