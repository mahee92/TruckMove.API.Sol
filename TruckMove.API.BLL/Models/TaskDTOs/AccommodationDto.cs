using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.BLL.Models.JobDTOs;

namespace TruckMove.API.BLL.Models.TaskDTOs
{
    public class AccommodationDto
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public int Status { get; set; }
        public bool? OrganizeNow { get; set; }
        public int? Assignee { get; set; }
        public DateTime? BookingDate { get; set; }
        public int? Driver { get; set; }
        public string? Location { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? ReferenceNumber { get; set; }
        public double? Price { get; set; }
        public bool IsActive { get; set; }

       
    }
}
