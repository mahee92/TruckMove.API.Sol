using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckMove.API.BLL.Models.JobDTOs
{
    public class TrailerDto
    {
        public int Id { get; set; }
        public int HookupType { get; set; }
        public int JobId { get; set; }
        public string? HookupLocation { get; set; }
        public string? DropOffLocation { get; set; }
        public string? Rego { get; set; }
        public string? Type { get; set; }

       
    }
}
