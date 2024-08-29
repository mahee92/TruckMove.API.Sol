using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.BLL.Models.JobDTOs;
using TruckMove.API.BLL.Models.UserManagmentDTO;

namespace TruckMove.API.BLL.Models.TaskDTOs
{
    public class AccommodationOutputDto : AccommodationDto
    {
        public virtual ICollection<AttachmentDto>? Attachments { get; set; }

        public virtual ICollection<NoteDto>? Notes { get; set; }

        public virtual UserDto? AssigneeNavigation { get; set; }
        public virtual UserDto? DriverNavigation { get; set; }
      
        public virtual TaskStatusDto StatusNavigation { get; set; } = null!;
    }
}
