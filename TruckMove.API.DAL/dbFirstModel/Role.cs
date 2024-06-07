using System;
using System.Collections.Generic;

namespace TruckMove.API.DAL.dbFirstModel
{
    public partial class Role
    {
        public Role()
        {
            UserRoles = new HashSet<UserRole>();
        }

        public int Id { get; set; }
        public string RoleName { get; set; } = null!;

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
