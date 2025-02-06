namespace TruckMove.API.BLL.Helper
{
    public class PaymentRates
    {
        public double Commercial_load_KM_rate { get; set; }
        public double PerKmRate { get; set; }
        public double Sunday_KM_rate { get; set; }
        public double Saturday_KM_rate { get; set; }
        public double Public_holiday_KM_rate { get; set; }


        public double Max_fixed_job_KMs { get; set; }

       public double Fixed_job_rate { get; set; }
        public double Sunday_Fixed_rate { get; set; }
        public double Saturday_Fixed_rate { get; set; }
        public double Public_holiday_Fixed_rate { get; set; }

        public double Dangerous_Goods_day_Rate { get; set; }





    }
}