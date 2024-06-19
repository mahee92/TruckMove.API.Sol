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
    }
}
