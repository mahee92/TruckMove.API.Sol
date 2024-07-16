using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckMove.API.BLL.Models.TaskDTOs
{
    public class PurchaseDto
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public int Status { get; set; }
        public int? Driver { get; set; }
        public bool FromMobile { get; set; }
        public bool? OrganiseNow { get; set; }
        public int? Assignee { get; set; }
        public string? ReciptUrl { get; set; }
        public bool? IsFuel { get; set; }
        public string? Vendor { get; set; }
        public double? Liters { get; set; }
        public double? Cost { get; set; }
        public string? ItemDescription { get; set; }
    }
}
