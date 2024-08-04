using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.BLL.Models.PrimaryDTO;

namespace TruckMove.API.BLL.Models.JobDTOs
{
    public class JobContactDto
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public int ContactId { get; set; }

        public virtual ContactDto Contact { get; set; } = null!;
    }
}
