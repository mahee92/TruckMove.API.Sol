using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.BLL.Models.JobDTOs;

namespace TruckMove.API.DAL.Models
{
    public class PublicTransportOutputDto
    {
        public virtual ICollection<NoteDto> Notes { get; set; }
    }
}
