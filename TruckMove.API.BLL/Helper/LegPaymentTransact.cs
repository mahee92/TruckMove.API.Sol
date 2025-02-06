using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public bool IsDangerousGoods { get; }

        public string GetFixTotalString { get; private set; } = "";
        public string GetPerKmTotalString { get; private set; } = "";
        public string CommercialLoadTotalString { get; private set; } = "";
       
        public string DangerousGoodsTotalString { get; private set; } = "";

        public string totalString { get; private set; } = "";

        public string GetFixCalTotalString { get; private set; } = "";
        public string GetPerKmCalTotalString { get; private set; } = "";
        public string CommercialLoadCalTotalString { get; private set; } = "";

        public string DangerousGoodsCalTotalString { get; private set; } = "";

        public string totalCalString { get; private set; } = "";

        public LegPaymentTransact(int legNumber, string legDay, double totalKm, bool isCommercialLoad, bool isDangerousGoods, PaymentRates rates)
        {
            LegNumber = legNumber;
            LegDay = legDay;
            TotalKm = totalKm;
            _rates = rates ?? throw new ArgumentNullException(nameof(rates));
            IsCommercialLoad = isCommercialLoad;
            IsDangerousGoods = isDangerousGoods;
            LegType = TotalKm > _rates.Max_fixed_job_KMs ? "PerKM" : "Fixed";
        }

        public double Total
        {
            get
            {
                if (IsDangerousGoods)
                {
                    return DangerousGoodsTotal;
                }
                totalCalString = $"{GetPerKmCalTotalString} + {GetFixCalTotalString} + {CommercialLoadCalTotalString}";
                totalString = $"{GetPerKmTotalString} + {GetFixTotalString} + {CommercialLoadTotalString}";
                return PerKmTotal + FixedJobTotal + CommercialLoadTotal;
            }
        }


        public double FixedJobTotal
        {
            get
            {
                if (LegType == "PerKM") return 0;

                GetFixTotalString = LegDay switch
                {
                    "Saturday" => "Saturday_Fixed_rate",
                    "Sunday" => "Sunday_Fixed_rate",
                    "Holiday" => "Public_holiday_Fixed_rate",
                    _ => "Fixed_job_rate"
                };


                GetFixCalTotalString = $"({_rates.Fixed_job_rate})";

                return GetFixTotalString switch
                {
                    "Saturday_Fixed_rate" => _rates.Saturday_Fixed_rate,
                    "Sunday_Fixed_rate" => _rates.Sunday_Fixed_rate,
                    "Public_holiday_Fixed_rate" => _rates.Public_holiday_Fixed_rate,
                    _ => _rates.Fixed_job_rate
                };
            }
        }

        public double PerKmTotal
        {
            get
            {
                if (LegType == "Fixed") return 0;

                GetPerKmTotalString = LegDay switch
                {
                    "Saturday" => "Saturday_KM_rate",
                    "Sunday" => "Sunday_KM_rate",
                    "Holiday" => "Public_holiday_KM_rate",
                    _ => "PerKmRate"
                };
              
                double rate = GetPerKmTotalString switch
                {
                    "Saturday_KM_rate" => _rates.Saturday_KM_rate,
                    "Sunday_KM_rate" => _rates.Sunday_KM_rate,
                    "Public_holiday_KM_rate" => _rates.Public_holiday_KM_rate,
                    _ => _rates.PerKmRate
                };

                GetPerKmTotalString += " * TotalKm";
                GetPerKmCalTotalString = $"{rate} * {TotalKm}";
                return rate * TotalKm;
            }
        }

        public double DangerousGoodsTotal
        {
            get
            {
                if (!IsDangerousGoods) return 0;

                DangerousGoodsTotalString = "Dangerous_Goods_day_Rate * TotalKm";
                DangerousGoodsCalTotalString = $"({_rates.Dangerous_Goods_day_Rate} * {TotalKm})";

                return _rates.Dangerous_Goods_day_Rate * TotalKm;
            }
        }

        public double CommercialLoadTotal
        {
            get
            {
                if (!IsCommercialLoad) return 0;

                CommercialLoadTotalString = "Commercial_load_KM_rate * TotalKm";
                CommercialLoadCalTotalString = $"({_rates.Commercial_load_KM_rate} * {TotalKm})";
                return _rates.Commercial_load_KM_rate * TotalKm;
            }
        }

       



    }
}



