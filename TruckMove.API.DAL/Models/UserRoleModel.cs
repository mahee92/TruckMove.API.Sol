using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.DAL.Repositories;

namespace TruckMove.API.DAL.Models
{
    public class UserRoleModel : AuditableEntity, IActiveEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public UserModel User { get; set; }

        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public RoleModel Role { get; set; }
        public bool IsActive { get; set; }

        // Other properties and annotations as needed
    }
}
