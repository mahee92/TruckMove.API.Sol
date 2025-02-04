using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.BLL.Models.JobDTOs;

namespace TruckMove.API.BLL.Services.PaymentServices
{
    public interface IPaymentService
    {
        IQueryable<LegOutPutDto> GetAllUnpaidLegsByDrivers();
    }
}
