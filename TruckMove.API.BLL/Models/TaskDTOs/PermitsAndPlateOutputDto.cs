using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.BLL.Models.JobDTOs;

namespace TruckMove.API.BLL.Models.TaskDTOs
{
    public class PermitsAndPlateOutputDto :PermitsAndPlateDto
    {
        public virtual ICollection<AttachmentDto> Attachments { get; set; }

        public virtual ICollection<NoteDto> Notes { get; set; }
    }
}
