using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckMove.API.BLL.Models.TaskDTOs
{
    public class AttachmentDto
    {
        public int Id { get; set; }
        public string? Url { get; set; }
        public int? PermitAndPlateId { get; set; }
        public int? AccommodationId { get; set; }

        public int? PublicTransportId { get; set; }

    }
}
