using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckMove.API.BLL.Models.UserManagmentDTO
{
    public class UserInputDto : UserDto
    {
        public string Password { get; set; }
        
        public int? CreatedById { get; set; }

    }
}
