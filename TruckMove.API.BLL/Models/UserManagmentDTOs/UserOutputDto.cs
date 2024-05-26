using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.DAL.Models;

namespace TruckMove.API.BLL.Models.UserManagmentDTO
{
    public class UserOutputDto : UserDto
    {
        
        public virtual List<RoleDto> Roles { get; set; }

        public string jwtToken { get; set; }



    }
}
