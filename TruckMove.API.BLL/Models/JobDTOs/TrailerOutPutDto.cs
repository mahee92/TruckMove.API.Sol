using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.DAL.Models;
using static TruckMove.API.DAL.MasterData.MasterData;

namespace TruckMove.API.BLL.Models.JobDTOs
{
    public class TrailerOutPutDto : TrailerDto
    {
        
        public virtual HookupType HookupTypeNavigation { get; set; } = null!;
        public virtual List<NoteDto> Notes { get; set; }
        public virtual List<ImageDto> Images { get; set; }

       
    }
}
