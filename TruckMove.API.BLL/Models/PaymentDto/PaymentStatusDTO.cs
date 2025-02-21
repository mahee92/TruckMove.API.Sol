using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TruckMove.API.DAL.MasterData.MasterData;

namespace TruckMove.API.BLL.Models.PaymentDto
{
    public class PaymentStatusDTO
    {
        public PaymentStatusEnum StatusId { get; set; }
        public int Id { get; set; }
        public bool isLeg { get; set; }
        public bool isDelay { get; set; }
        public bool isPublicTransport { get; set; }

        public int? JobId { get; set; }
        public int? DriverId { get; set; }

    }
}
