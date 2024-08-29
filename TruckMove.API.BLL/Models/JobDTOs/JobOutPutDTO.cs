using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.BLL.Models.Primary;
using TruckMove.API.BLL.Models.PrimaryDTO;
using TruckMove.API.BLL.Models.TaskDTOs;
using TruckMove.API.BLL.Models.UserManagmentDTO;
using TruckMove.API.BLL.Models.VehicleDtos;
using TruckMove.API.BLL.Models.VehicleDTOs;
using TruckMove.API.DAL.Models;

namespace TruckMove.API.BLL.Models.JobDTOs
{
    public class JobOutPutDTO : JobDto
    {
        public virtual List<ContactDto>? Contacts { get; set; }
        public virtual List<JobContactDto>? JobContacts { get; set; }
        public virtual CompanyDto? Company { get; set; }

        public virtual VehicleOutputDto? Vehicle { get; set; }

        public virtual List<WayPointDto>? WayPoints { get; set; }

        public virtual List<TrailerOutPutDto>? Trailers { get; set; }

        public virtual List<PermitsAndPlateOutputDto>? PermitsAndPlates { get; set; }
        public virtual List<AccommodationOutputDto>? Accommodations { get; set; }

        public virtual List<PublicTransportOutputDto>? PublicTransports { get; set; }

        public virtual List<PurchaseOutputDto>? Purchases { get; set; }

        public virtual ICollection<NoteDto>? Notes { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        public virtual UserDto? DriverNavigation { get; set; }

        public virtual UserDto? ControllerNavigation { get; set; }

        public virtual JobStatusDto? StatusNavigation { get; set; }

        public virtual List<ChecklistDto>? Checklists { get; set; }



    }
}
