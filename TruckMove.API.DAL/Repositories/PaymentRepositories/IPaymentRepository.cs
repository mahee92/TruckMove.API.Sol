using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.DAL.Models;

namespace TruckMove.API.DAL.Repositories.PaymentRepositories
{
    public interface IPaymentRepository
    {
        IQueryable<Leg> GetAllUnpaidLegsByDrivers();
    }
}
