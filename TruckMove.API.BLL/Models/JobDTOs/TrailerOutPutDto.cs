using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckMove.API.BLL.Models.JobDTOs
{
    public class TrailerOutPutDto : TrailerDto
    {
        public virtual List<NoteDto> Notes { get; set; }

        public virtual List<ImageDto> Images { get; set; }
    }
}
