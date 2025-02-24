using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.DAL.Repositories;

namespace TruckMove.API.DAL.Models
{
    public class PaymentAdjustments  :  AuditableEntity
    {
        public int Id { get; set; }

        public int JobId { get; set; }
        public int DriverId { get; set; }

        public string Description { get; set; } = null!;

        public double Amount { get; set; }

        public int PaymentStatus { get; set; }

        public virtual PaymentStatus PaymentStatusNavigation { get; set; } = null!;


    }
}