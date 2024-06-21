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
            Acknowledged = 6,
            InProgress = 7,
            Stopped = 8,
            Delayed=9,
            Arrived = 10,
            ArrivalChecked = 11,
            QADone = 12,
            PaymentDone = 13,
            BillingDone = 14,
            Completed = 15
        }
    }
}
