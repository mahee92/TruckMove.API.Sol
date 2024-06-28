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
            Completed = 14
        }

        public enum LegStatusEnum
        {
            Planned = 1,
            InProgress = 2,
            Completed = 3


        }
    }
}
