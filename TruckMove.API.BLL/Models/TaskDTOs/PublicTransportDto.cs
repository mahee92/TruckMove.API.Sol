using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckMove.API.BLL.Models.TaskDTOs
{
    public class PublicTransportDto
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public int? Driver { get; set; }
        public bool OrganizeNow { get; set; }
        public int Status { get; set; }
        public DateTime? Daterequired { get; set; }
        public string? RequiredTosuburb { get; set; }
        public string? RequiredFromsuburb { get; set; }

        public string? Name { get; set; }
        public int? Assignee { get; set; }
        public string? BookingInstructions { get; set; }
        public int? TransportType { get; set; }
        public DateTime? DepartureDateTime { get; set; }
        public DateTime? ArrivalDateTime { get; set; }
        public string? DepartureAddress { get; set; }
        public string? ArrivalAddress { get; set; }
        public string? ReferenceNumber { get; set; }
        public double? TransportCost { get; set; }
        

       
    }
}
