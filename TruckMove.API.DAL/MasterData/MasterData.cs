using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckMove.API.DAL.MasterData
{
    public class MasterData
    {
        public enum RoleEnum
        {
            Administrator = 1,
            OpsManager = 2,
            AdminTeam = 3,
            PayrollTeam = 4,
            Driver = 5
        }
        public enum JobStatusEnum
        {
            Planned = 1,
            Booked = 2,
            ReadyForPickup = 3,
            PreDepartureChecked = 4,
            Acknowledged = 5,
            InProgress = 6,
            Stopped = 7,
            Delayed=8,
            Arrived = 9,
            ArrivalChecked = 10,
            QADone = 11,
            PaymentDone = 12,
            BillingDone = 13,
            Completed = 14,
            InStore=15
        }

        public enum LegStatusEnum
        {
            Planned = 1,
            InProgress = 2,
            Completed = 3


        }


        public enum HookUpTypeEnum
        {
            HU_Single = 1,
            HU_Double = 2,
            FOUR_RA = 3


        }
        public enum VariancesEnum
        {
            _default = 0,
            DG = 1,
            Sat_rate = 2,
            Sun_rate = 3,
            G7 = 4,
            Public_Holiday = 5,
            G4 = 6,
            Bookining_Bullbar = 7



        }

        public enum TaskStatusEnum
        {
            Planned = 1,
            InProgress = 2,
            Completed = 3
        }

        public enum PublicTransportTypeEnum
        {
            Train = 1,
            Plane = 2,
            Taxi = 3,
            Uber=4,
            Other=5
        }
        public enum RateEnum
        {
            Per_KM_Rate = 1,
            Max_fixed_job_KMs = 2,
            Fixed_job_rate = 3,
            Commercial_load_KM_rate = 4,
            Delay_hourly_rate = 5,
            Dangerous_Goods_day_Rate = 6,
            Public_transport_hourly_Rate = 7,
            Public_holiday_KM_rate = 8,
            Saturday_KM_rate = 9,
            Sunday_KM_rate = 10,
            Public_holiday_fixed_rate = 11,
            Saturday_fixed_rate = 12,
            Sunday_fixed_rate = 13,
            Hookup_Single = 14,
            Hookup_Double = 15,
            Hookup_4RA = 16,
            Grade_4_Hourly_rate = 17,
            Grade_1_Hourly_rate_riding_on_public_transport = 18,
            Hookup_Road_Train = 19,
            Saturday_Hour_rate = 20,
            Sunday_Hour_rate = 21,
            Holiday_hour_rate = 22,
            Public_Transport_Delay = 23,
            Breakdown_Delay = 24
        }

        public enum TrailerStatusEnum
        {
            NotPicked=0,
            Picked=1,
            Droppped = 2

        }
        public enum PaymentStatusEnum
        {
            QAPending = 1,
            QADone = 2,
            Verified = 3,
            PaymentDone = 4

        }
    }
}
