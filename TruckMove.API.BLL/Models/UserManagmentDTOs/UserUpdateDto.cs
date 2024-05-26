using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckMove.API.BLL.Models.UserManagmentDTO
{
    public class UserUpdateDto : UserInputDto
    {
        public string NewEmail { get; set; }
        public int ? UpdatedById { get; set; }

    }
}
